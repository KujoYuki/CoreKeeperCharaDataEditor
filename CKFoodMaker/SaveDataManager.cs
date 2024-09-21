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
        public static readonly JsonSerializerOptions SerializerOptionWrite = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        static readonly JsonSerializerOptions _durationOption = new JsonSerializerOptions
        {
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
        };

        public static int LoadItemLimit = 0;

        public string SaveDataPath { get; private set; } = String.Empty;
        public List<(ItemBase item, string objectName, ItemAuxData auxData)> Items { get; private set; } = new();
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
            items = new();
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

        /// <summary>
        /// 変更したアイテムを書き込む
        /// </summary>
        /// <param name="insertIndex"></param>
        /// <param name="newItem">変更後アイテム</param>
        /// <param name="internalName">変更後アイテムのInternalName</param>
        public void WriteItemData(int insertIndex, ItemBase newItem, string internalName)
        {
            SaveData["inventory"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(newItem));
            SaveData["inventoryObjectNames"]![insertIndex] = internalName;
            //hack 家畜のようなペットではないが補助データを含むレコードの取り扱いを考慮する（元の値を保持させる）
            SaveData["inventoryAuxData"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(ItemAuxData.Default));

            string changedJson = SaveData.ToJsonString(SerializerOptionWrite);
#if DEBUG
            // 確認用に別名ファイルで作成
            MessageBox.Show($"insertIndex = {insertIndex}\n{JsonSerializer.Serialize(newItem)}\n{internalName}", "書き込み内容確認");
            SaveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif
            // 書き込む前に元jsonの構文に戻す
            changedJson = RestoreJsonString(changedJson);

            File.WriteAllText(SaveDataPath, changedJson);
        }

        // ペットを含んだ書き込みをする
        public bool WriteItemData(int insertIndex, ItemBase newItem, string internalName, string petName, PetColor petColor, IEnumerable<PetTalent> petTalents)
        {
            SaveData["inventory"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(newItem));
            SaveData["inventoryObjectNames"]![insertIndex] = internalName;

            // リソース指定の固定ハッシュ値があるためManger経由で変更する
            var auxLawData = SaveData["inventoryAuxData"]![insertIndex]!["data"]!.GetValue<string>();
            if (string.IsNullOrEmpty(auxLawData))
            {
                MessageBox.Show("選択中のアイテムがペットではありません。\n編集を中断します。");
                return false;
            }
            var auxManager = new AuxPrefabManager(JsonNode.Parse(auxLawData)!.AsObject());
            auxManager.UpdateData(AuxHash.PetGroupHash, AuxHash.PetColorHash,
                new[] { ((int)petColor).ToString() });
            auxManager.UpdateData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash,
                petTalents.Select(t => t.ToJsonString()));
            auxManager.UpdateData(AuxHash.PetNameGroupHash, AuxHash.PetNameHash,
                new[] { petName });
            SaveData["inventoryAuxData"]![insertIndex]!["data"] = auxManager.ToInnerJsonString(SerializerOptionWrite);

            string changedJson = JsonSerializer.Serialize(SaveData, SerializerOptionWrite);
#if DEBUG
            // 確認用に別名ファイルで作成
            MessageBox.Show($"insertIndex = {insertIndex}\n{JsonSerializer.Serialize(newItem)}\n{internalName}", "書き込み内容確認");
            if (Enum.GetValues<PetType>().Cast<int>().Contains(newItem.objectID))
            {
                var insertedAuxData = JsonNode.Parse(changedJson)?.AsObject()
                    ["inventoryAuxData"]![insertIndex]!["data"]!.GetValue<string>();
                MessageBox.Show(insertedAuxData);
                Clipboard.SetText(insertedAuxData!);
            }
            SaveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif
            // 書き込む前に元jsonの構文に戻す
            changedJson = RestoreJsonString(changedJson);

            File.WriteAllText(SaveDataPath, changedJson);
            return true;
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
                .Select(x => JsonSerializer.Deserialize<Condition>(x, _durationOption))
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

        internal void UnlockAllResipe()
        {
            var result = MessageBox.Show("全レシピ解放機能は未実装です。\n代わりに未作成の料理の組み合わせを出力します。");

            var allCookedCategory = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.objectID, c.objectID + 50})
                .OrderBy(id => id)
                .ToList();
            List<int> allFoodID = StaticResource.AllFoodMaterials.Select(c => c.objectID).ToList();
            List<int> allVariations = allFoodID
                .SelectMany((ID, index) => allFoodID.Skip(index), (IdA, IdB) => Form1.CalculateVariation(IdA, IdB))
                .ToList();

            var allDiscoverdVariation = SaveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj))
                .Where(obj => allCookedCategory.Contains(obj!.objectID))
                .Select(c => c!.variation)
                .Distinct()
                .ToList();
            var intersectRecipeVariation = allVariations.Intersect(allDiscoverdVariation).ToList();
            double cookRate = (double)intersectRecipeVariation.Count / allVariations.Count;
            var outputResult = MessageBox.Show($"現在の正規レシピ網羅率は {cookRate.ToString("P2")} %です。" +
                $"（{intersectRecipeVariation.Count} / {allVariations.Count}）\n" +
                $"まだ調理していない組み合わせを出力しますか？", "", MessageBoxButtons.YesNo);
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
                MessageBox.Show($"{exceptRecipe.Count()} 通りのレシピを{path}に出力しました。");

            }
            //// 全レシピ削除
            //var countBefore = SaveData["discoveredObjects2"]!.AsArray().Count();
            //SaveData["discoveredObjects2"] = JsonNode.Parse(JsonSerializer.Serialize(allDiscoverdObjectWithoutRecipe, SerializerOptionWrite));
            //var countAfter = SaveData["discoveredObjects2"]!.AsArray().Count();
            //var deletedRecipe = countAfter-countBefore; //確認

            //todo 全レシピ追加
            //全組み合わせの勝敗表からカテゴリを自動で決定させる

            //todo データ書き込み
            string changedJson = JsonSerializer.Serialize(SaveData, SerializerOptionWrite);
            changedJson = RestoreJsonString(changedJson);
#if DEBUG
            SaveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif
            File.WriteAllText(SaveDataPath, changedJson);
        }
    }
}
