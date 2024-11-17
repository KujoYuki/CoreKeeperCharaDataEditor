using System.Resources.Tools;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using CKFoodMaker.Model;
using CKFoodMaker.Model.ItemAux;
using CKFoodMaker.Resource;

namespace CKFoodMaker
{
    /// <summary>
    /// 単一のセーブデータに対する読み書きモジュール
    /// </summary>
    public sealed class SaveDataManager
    {
        public static int LoadItemLimit;

        private static SaveDataManager? _instance;
        public static SaveDataManager Instance => _instance ??= new();

        private string _saveDataPath = string.Empty;

        private (ItemBase itemBase, string objectName, ItemAuxData auxData) _copiedItem;
        public string SaveDataPath
        {
            get => _saveDataPath;
            set
            {
                _saveDataPath = value;
                LoadItemLimit = Program.IsDeveloper ? 86 : 50;
                _saveData = LoadInventory(out var items);
                Items = items;
            }
        }

        public List<(ItemBase item, string objectName, ItemAuxData auxData)> Items { get; private set; } = [];
        private JsonObject _saveData = [];

        private SaveDataManager()
        {
        }

        private static string SanitizeJsonString(string origin)
        {
            return origin.Replace("Infinity", "\"Infinity\"");
        }

        private static string RestoreJsonString(string processed)
        {
            return processed.Replace("\"Infinity\"", "Infinity");
        }

        private JsonObject LoadInventory(out List<(ItemBase item, string objectName, ItemAuxData auxData)> items)
        {
            try
            {
                string saveDataContents = File.ReadAllText(SaveDataPath);
                // conditionsList中のInfinity文字列により例外が出るのを回避する
                saveDataContents = SanitizeJsonString(saveDataContents);

                _saveData = JsonNode.Parse(saveDataContents)!.AsObject();

                var inventoryBase = _saveData["inventory"]!.AsArray();
                var inventoryName = _saveData["inventoryObjectNames"]!.AsArray();
                var inventoryAuxData = _saveData["inventoryAuxData"]!.AsArray();

                var limitedItems = inventoryBase
                    .Zip(inventoryName, inventoryAuxData)
                    .Take(LoadItemLimit)
                    .Select(x => (item: x.First!, objectName: x.Second!, auxData: x.Third!));
                items = [];
                foreach (var (item, objectName, auxData) in limitedItems)
                {
                    var itemBase = new ItemBase(
                        objectID: item["objectID"]!.GetValue<int>(),
                        amount: item["amount"]!.GetValue<int>(),
                        variation: item["variation"]!.GetValue<int>(),
                        variationUpdateCount: item["variationUpdateCount"]!.GetValue<int>());
                    string objectInternalName = objectName!.GetValue<string>()!;
                    var itemAux = new ItemAuxData(auxData["index"]!.GetValue<int>(), auxData["data"]!.GetValue<string>());
                    items.Add((itemBase, objectInternalName, itemAux));
                }

                return _saveData;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("セーブデータの読み込みに失敗しました。", ex);
            }
        }

        public ItemAuxData GetAuxData(int insertIndex)
        {
            var auxData = _saveData["inventoryAuxData"]![insertIndex]!;
            return new ItemAuxData(auxData["index"]!.GetValue<int>(), auxData["data"]!.GetValue<string>());
        }

        // 補助データ込みの書き込みメソッド
        public bool WriteItemData(int insertIndex, ItemBase itemBase, string objectName, ItemAuxData? auxData = null)
        {
            auxData ??= ItemAuxData.Default;
            var success = false;
            _saveData["inventory"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(itemBase, StaticResource.SerializerOption));
            _saveData["inventoryObjectNames"]![insertIndex] = objectName;
            _saveData["inventoryAuxData"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(auxData, StaticResource.SerializerOption));

            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);

#if DEBUG
            // 確認用に別名ファイルで作成
            var verifyBuilder = new StringBuilder();
            verifyBuilder.AppendLine($"insertIndex = {insertIndex}");
            verifyBuilder.AppendLine($"objectName = {objectName}");
            verifyBuilder.AppendLine($"itemBase = {JsonSerializer.Serialize(itemBase, StaticResource.SerializerOption)}");
            if (auxData != ItemAuxData.Default)
            {
                verifyBuilder.AppendLine($"auxData = {JsonSerializer.Serialize(auxData, StaticResource.SerializerOption)}");
            }
            MessageBox.Show($"{verifyBuilder}", "書き込み内容確認");
            _saveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif

            // 書き込む前に元jsonの構文に戻す
            changedJson = RestoreJsonString(changedJson);

            File.WriteAllText(SaveDataPath, changedJson);
            success = true;
            return success;
        }

        public bool IsClearData()
        {
            return _saveData["hasUnlockedSouls"]?.GetValue<bool>() is true
                   && _saveData["collectedSouls"]?.AsArray().Count is 6
                   && _saveData["hasPlayedOutro"]?.GetValue<bool>() is true;
        }

        /// <summary>
        /// IncreasedMaxHealthPermanentの増分検知
        /// </summary>
        /// <param name="increasedHealth"></param>
        /// <returns></returns>
        public bool HasOveredHealth(out int increasedHealth)
        {
            int? increasedHealthNullable = _saveData["conditionsList"]?.AsArray()
                .Select(x => JsonSerializer.Deserialize<Condition>(x, StaticResource.SerializerOption))
                .SingleOrDefault(c => c?.Id == 16)?.Value;
            if (increasedHealthNullable is null)
            {
                increasedHealth = 0;
                return false;
            }
            increasedHealth = increasedHealthNullable.Value;
            if (increasedHealth < 2000) return false;

            return true;
        }

        public void ListUncreatedRecipes()
        {
            var allCookedCategoryId = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.ObjectID, c.ObjectID + (int)CookRarity.Rare })
                .OrderBy(id => id)
                .ToArray();
            int[] allFoodID = StaticResource.AllFoodMaterials.Select(c => c.objectID).ToArray();
            int[] allVariations = allFoodID
                .SelectMany((ID, index) => allFoodID.Skip(index), Form1.CalculateVariation)
                .ToArray();

            int[] allDiscoverdVariation = _saveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(obj => allCookedCategoryId.Contains(obj.objectID))
                .Select(c => c!.variation)
                .Distinct()
                .ToArray();
            var intersectRecipeVariation = allVariations.Intersect(allDiscoverdVariation).ToList();
            double cookRate = (double)intersectRecipeVariation.Count / allVariations.Length;

            DialogResult outputResult = MessageBox.Show($"現在のレシピ網羅率は {cookRate:P2} %です。" +
                $"（{intersectRecipeVariation.Count} / {allVariations.Length}）\n" +
                $"※ゲーム内レシピブックとカウントが異なる場合があります。\n\n" +
                $"一度も調理していない組み合わせを出力しますか？", "", MessageBoxButtons.YesNo);
            if (outputResult is DialogResult.Yes)
            {
                var exceptRecipe = allVariations.Except(allDiscoverdVariation).ToArray();
                var foodBuilder = new StringBuilder();
                foreach (var variation in exceptRecipe)
                {
                    Form1.ReverseCalcurateVariation(variation, out int materialIdA, out int materialIdB);
                    string FoodA = StaticResource.AllFoodMaterials.Single(f => f.objectID == materialIdA).DisplayName;
                    string FoodB = StaticResource.AllFoodMaterials.Single(f => f.objectID == materialIdB).DisplayName;
                    foodBuilder.AppendLine($"{FoodA} + {FoodB}");
                }

                using SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = "UncreatedRecipe.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    File.WriteAllText(path, foodBuilder.ToString());
                    MessageBox.Show($"{exceptRecipe.Length} 通りのレシピを出力しました。");
                }
            }
            //todo 全レシピ追加 全組み合わせの勝敗表からカテゴリを自動で決定させる:要食材勝敗テーブルのアルゴリズム解明
        }

        public void DeleteAllRecipes()
        {
            var allCookedCategoryId = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.ObjectID, c.ObjectID + (int)CookRarity.Rare + (int)CookRarity.Epic })
                .OrderBy(id => id)
                .ToList();
            var discoveredObjectWithoutRecipe = _saveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(obj => !allCookedCategoryId.Contains(obj.objectID))
                .ToList();
            _saveData["discoveredObjects2"] = JsonNode.Parse(JsonSerializer.Serialize(discoveredObjectWithoutRecipe, StaticResource.SerializerOption));
            // データ書き込み
            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(SaveDataPath, changedJson);
        }

        public List<Condition> GetConditions()
        {
            var conditions = _saveData["conditionsList"]?.AsArray()
                .Select(c => JsonSerializer.Deserialize<Condition>(c, StaticResource.SerializerOption)!)
                .ToList()!;
            return conditions;
        }

        public static void BackUpConditions(IEnumerable<Condition> conditions, string filePath)
        {
            conditions = conditions.OrderBy(c => c.Id).ToList();
            // 必要あらば例外処理

            var conditionsNode = JsonNode.Parse(JsonSerializer.Serialize(conditions, StaticResource.SerializerOption));
            string changedJson = JsonSerializer.Serialize(conditionsNode, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(filePath, changedJson);
        }

        /// <summary>
        /// Conditionsのみのバックアップファイルから読み込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<Condition> LoadConditions(string filePath)
        {
            string jsonString = SanitizeJsonString(File.ReadAllText(filePath));
            var conditions = JsonSerializer.Deserialize<List<Condition>>(jsonString, StaticResource.SerializerOption)?
                .OrderBy(c => c.Id)
                .ToList();
            return conditions ??= [];
        }

        public void OverrideConditions(IEnumerable<Condition> conditions)
        {
            _saveData["conditionsList"] = JsonNode.Parse(JsonSerializer.Serialize(conditions, StaticResource.SerializerOption));
            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
#if DEBUG
            _saveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif
            File.WriteAllText(SaveDataPath, changedJson);
        }

        // 既にレシピがおかしいデータへの解析処理
        private void AnalyzeRecepe()
        {
            var resultFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"AnalyzeRecipe.txt");
            var allCookedCategoryId = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.ObjectID, c.ObjectID + (int)CookRarity.Rare + (int)CookRarity.Epic })
                .OrderBy(id => id)
                .ToList();
            var CookedCategoryCommon = StaticResource.AllCookedBaseCategories
                .Select(c => c.ObjectID)
                .ToArray();
            var CookedCategoryRare = CookedCategoryCommon.Select(id => id + (int)CookRarity.Rare).ToArray();
            var CookedCategoryEpic = CookedCategoryCommon.Select(id => id + (int)CookRarity.Epic).ToArray();

            List<DiscoveredObjects> discoveredAllRecipe = _saveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(obj => allCookedCategoryId.Contains(obj.objectID))
                .ToList();

            var dicoveredCommonRecipe = discoveredAllRecipe
                .Where(recipe => CookedCategoryCommon.Contains(recipe.objectID))
                .Select(recipe =>
                {
                    var catergoryDisplayName = StaticResource.AllCookedBaseCategories.Single(c => c.ObjectID == recipe.objectID).DisplayName;
                    Form1.ReverseCalcurateVariation(recipe.variation, out int materialIdA, out int materialIdB);
                    string foodA = StaticResource.AllFoodMaterials.Concat(StaticResource.ObsoleteFoodMaterials)
                    .Single(f => f.objectID == materialIdA)?.DisplayName ?? string.Empty;
                    string foodB = StaticResource.AllFoodMaterials.Concat(StaticResource.ObsoleteFoodMaterials)
                    .Single(f => f.objectID == materialIdB)?.DisplayName ?? string.Empty;
                    return $"C:{catergoryDisplayName:10} = {foodA:10} + {foodB:10}";
                }).ToArray();
            var dicoveredRareRecipe = discoveredAllRecipe
                .Where(recipe => CookedCategoryRare.Contains(recipe.objectID))
                .Select(recipe =>
                {
                    var catergoryDisplayName = StaticResource.AllCookedBaseCategories.Single(c => c.ObjectID == recipe.objectID).DisplayName;
                    Form1.ReverseCalcurateVariation(recipe.variation, out int materialIdA, out int materialIdB);
                    string foodA = StaticResource.AllFoodMaterials.Concat(StaticResource.ObsoleteFoodMaterials)
                    .Single(f => f.objectID == materialIdA)?.DisplayName ?? string.Empty;
                    string foodB = StaticResource.AllFoodMaterials.Concat(StaticResource.ObsoleteFoodMaterials)
                    .Single(f => f.objectID == materialIdB)?.DisplayName ?? string.Empty;
                    return $"R:{catergoryDisplayName:10} = {foodA:10} + {foodB:10}";
                }).ToArray();
            var dicoveredEpicRecipe = discoveredAllRecipe
                .Where(recipe => CookedCategoryEpic.Contains(recipe.objectID))
                .Select(recipe =>
                {
                    var catergoryDisplayName = StaticResource.AllCookedBaseCategories.Single(c => c.ObjectID == recipe.objectID).DisplayName;
                    Form1.ReverseCalcurateVariation(recipe.variation, out int materialIdA, out int materialIdB);
                    string foodA = StaticResource.AllFoodMaterials.Concat(StaticResource.ObsoleteFoodMaterials)
                    .Single(f => f.objectID == materialIdA)?.DisplayName ?? string.Empty;
                    string foodB = StaticResource.AllFoodMaterials.Concat(StaticResource.ObsoleteFoodMaterials)
                    .Single(f => f.objectID == materialIdB)?.DisplayName ?? string.Empty;
                    return $"E{catergoryDisplayName:10} = {foodA:10} + {foodB:10}";
                }).ToArray();
            var sb = new StringBuilder();
            foreach (var item in dicoveredCommonRecipe.Concat(dicoveredRareRecipe).Concat(dicoveredEpicRecipe))
            {
                sb.AppendLine(item.ToString());
            }
            File.WriteAllText(resultFilePath, sb.ToString());
        }

        internal void CopyItem(ItemBase itemBase, string objectName, ItemAuxData auxData)
        {
            _copiedItem = (itemBase, objectName, auxData);
        }

        internal (ItemBase itemBase, string objectName, ItemAuxData auxData) PasteItem()
        {
            return (_copiedItem.itemBase, _copiedItem.objectName, _copiedItem.auxData);
        }
    }
}
