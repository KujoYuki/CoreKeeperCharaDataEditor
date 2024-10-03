using System.Text;
using System.Text.Encodings.Web;
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
    public class SaveDataManager
    {
        public static int LoadItemLimit;

        public string SaveDataPath { get; private set; } = String.Empty;
        public List<(ItemBase item, string objectName, ItemAuxData auxData)> Items { get; private set; } = [];
        private JsonObject SaveData { get; set; }
        public SaveDataManager()
        {
        }

        public SaveDataManager(string saveDataPath)
        {
            SaveDataPath = saveDataPath;
            LoadItemLimit = Program.IsDeveloper ? 86 : 50;
            SaveData = LoadInventory(out var items);
            Items = items;
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

            string saveDataContents = File.ReadAllText(SaveDataPath);
            // conditionsList中のInfinity文字列により例外が出るのを回避する
            saveDataContents = SanitizeJsonString(saveDataContents);

            SaveData = JsonNode.Parse(saveDataContents)!.AsObject();

            var inventoryBase = SaveData["inventory"]!.AsArray();
            var inventoryName = SaveData["inventoryObjectNames"]!.AsArray();
            var inventoryAuxData = SaveData["inventoryAuxData"]!.AsArray();

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

            return SaveData;
        }

        public ItemAuxData GetAuxData(int insertIndex)
        {
            var auxData = SaveData["inventoryAuxData"]![insertIndex]!;
            return new ItemAuxData(auxData["index"]!.GetValue<int>(), auxData["data"]!.GetValue<string>());
        }

        // 補助データ込みの書き込みメソッド
        public bool WriteItemData(int insertIndex, ItemBase itemBase, string objectName, ItemAuxData? auxData = null)
        {
            auxData ??= ItemAuxData.Default;
            auxData.GetPetData(out _, out _, out var tempTalent);
            var success = false;
            SaveData["inventory"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(itemBase, StaticResource.SerializerOption));
            SaveData["inventoryObjectNames"]![insertIndex] = objectName;
            SaveData["inventoryAuxData"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(auxData, StaticResource.SerializerOption));
            
            string changedJson = JsonSerializer.Serialize(SaveData, StaticResource.SerializerOption);

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
            MessageBox.Show($"{verifyBuilder.ToString()}", "書き込み内容確認");
            SaveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif

            // 書き込む前に元jsonの構文に戻す
            changedJson = RestoreJsonString(changedJson);

            File.WriteAllText(SaveDataPath, changedJson);
            success = true;
            return success;
        }

        public bool IsClearData()
        {
            if (SaveData["hasUnlockedSouls"]?.GetValue<bool>() is true &&
                SaveData["collectedSouls"]?.AsArray().Count is 6 &&
                SaveData["hasPlayedOutro"]?.GetValue<bool>() is true)
            {
                return true;
            }
            return false;
        }

        // IncreasedMaxHealthPermanentの増分検知
        public bool HasOveredHealth(out int increasedHealth)
        {
            int? increasedHealthNullable = SaveData["conditionsList"]?.AsArray()
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

        public void UnlockAllRecipe()
        {
            var result = MessageBox.Show("未作成の料理の組み合わせを出力します。");

            var allCookedCategoryId = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.objectID, c.objectID + (int)CookRarity.Rare })
                .OrderBy(id => id)
                .ToList();
            List<int> allFoodID = StaticResource.AllFoodMaterials.Select(c => c.objectID).ToList();
            List<int> allVariations = allFoodID
                .SelectMany((ID, index) => allFoodID.Skip(index), (IdA, IdB) => Form1.CalculateVariation(IdA, IdB))
                .ToList();

            var allDiscoverdVariation = SaveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(obj => allCookedCategoryId.Contains(obj.objectID))
                .Select(c => c!.variation)
                .Distinct()
                .ToList();
            var intersectRecipeVariation = allVariations.Intersect(allDiscoverdVariation).ToList();
            double cookRate = (double)intersectRecipeVariation.Count / allVariations.Count;
            var outputResult = MessageBox.Show($"現在の正規レシピ網羅率は {cookRate:P2} %です。" +
                $"（{intersectRecipeVariation.Count} / {allVariations.Count}）\n" +
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
                    foodBuilder.AppendLine(FoodA + " + " + FoodB);
                }
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"UncreatedRecipe.txt");
                File.WriteAllText(path, foodBuilder.ToString());
                MessageBox.Show($"{exceptRecipe.Length} 通りのレシピを出力しました。\n{path}");

            }
            
            //todo 全レシピ追加
            //全組み合わせの勝敗表からカテゴリを自動で決定させる:要食材勝敗テーブルの解明
        }

        public void DeleteAllRecipe()
        {
            var allCookedCategoryId = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.objectID, c.objectID + (int)CookRarity.Rare + (int)CookRarity.Epic })
                .OrderBy(id => id)
                .ToList();
            var discoveredObjectWithoutRecipe = SaveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(obj => !allCookedCategoryId.Contains(obj.objectID))
                .ToList();
            SaveData["discoveredObjects2"] = JsonNode.Parse(JsonSerializer.Serialize(discoveredObjectWithoutRecipe, StaticResource.SerializerOption));
            // データ書き込み
            string changedJson = JsonSerializer.Serialize(SaveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(SaveDataPath, changedJson);
        }
    }
}
