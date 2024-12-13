using CKFoodMaker.Model;
using CKFoodMaker.Resource;
using CKFoodMaker;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace CKFoodMakerTest
{
    [TestClass]
    public class AnalyzeTest
    {
        private static readonly string winUserName = Environment.UserName;
        private const int id = 449703638;
        private const int targetSlotNo = 0;
        private string _saveDataPath = $@"C:\Users\{winUserName}\AppData\LocalLow\Pugstorm\Core Keeper\Steam\{id}\saves\{targetSlotNo}.json";
        private const string _recipeFile = "analyzedRecipe.csv";

        // 全ての食材と料理の表示名を取得
        private static readonly List<(int objectID, string DisplayName)> foodMaterials = StaticResource.AllFoodMaterials
            .Concat(StaticResource.ObsoleteFoodMaterials)   // レシピには載らないが料理は作成できるため含める
            .Select(item => (objectID: item.Info.objectID, DisplayName: item.DisplayName))
            .ToList();

        // Id=>DisplayNameの辞書
        private static readonly Dictionary<int, string> FoodDic = StaticResource.AllCookedBaseCategories
            .Select(item =>
            {
                return new[] { item.Info.objectID, item.Info.objectID + (int)CookRarity.Rare, item.Info.objectID + (int)CookRarity.Epic }
                .Select(id => (objectID: id, DisplayName: item.DisplayName));
            })
            .SelectMany(food => food).ToList()
            .Concat(foodMaterials)
            .ToDictionary();

        // 金色食材のIDリスト
        private static readonly List<int> rareFoodIds = Enumerable.Range(8100, 11).Append(9733).ToList();

        [TestMethod]
        public void AnalyzeReipeTest()
        {
            string saveDataContents = File.ReadAllText(_saveDataPath);
            // conditionsList中のInfinity文字列により例外が出るのを回避する
            saveDataContents = SaveDataManager.SanitizeJsonString(saveDataContents);

            JsonObject _saveData = JsonNode.Parse(saveDataContents)!.AsObject();

            // 料理のコモンとレアのカテゴリIDリスト
            List<int> allCookedCategoryId = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.Info.objectID, c.Info.objectID + (int)CookRarity.Rare })
                .OrderBy(id => id)
                .ToList();

            // 全ての食材と料理の表示名を取得
            var foodMaterials = StaticResource.AllFoodMaterials
                .Concat(StaticResource.ObsoleteFoodMaterials)   // レシピには載らないが料理は作成できるため含める
                .Select(item => (objectID: item.Info.objectID, DisplayName: item.DisplayName))
                .ToList();


            // 全ての発見済みアイテムからレシピのみを抽出し、表示名を含めてcsv形式のレコード変換
            List<Recipe> discoveredAllRecipe = _saveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(o => allCookedCategoryId.Contains(o.objectID))   // 非料理アイテムは除外
                .Where(o => o.variation > 0 && (uint)o.variation <= uint.MaxValue)   // variationが0か32bitで表現できない場合は除外
                .OrderBy(o => o.variation)
                .ThenBy(o => o.objectID)

                // 料理の鉄人でレア化されたレシピが含まれている場合は除外
                // 各グループの最初の要素のみを選択（脱法料理とレアしか知らない料理を除外できないことに注意）
                .GroupBy(o => o.variation)
                .Select(g => g.First())

                // 食材に対しての検証
                .Where(r =>
                {
                    Form1.ReverseCalcurateVariation(r.variation, out int materialA, out int materialB);
                    int[] materials = [materialA, materialB];
                    foreach (var material in materials)
                    {
                        // 食材じゃないものが食材の場合を除外（シーズンアイテム系など）
                        if (!FoodDic.ContainsKey(material))
                        {
                            return false;
                        }
                    }
                    return true;
                })
                .Select(r =>
                {
                    // csv用レコードに変換
                    Form1.ReverseCalcurateVariation(r.variation, out int materialA, out int materialB);
                    return new Recipe(materialA, materialB, r.objectID, FoodDic[materialA], FoodDic[materialB], FoodDic[r.objectID]);
                })
                .OrderBy(r => r.MaterialA)
                .ThenBy(r => r.MaterialB)
                .ToList();

            var sb = new StringBuilder();
            string header = "食材A_Id,食材B_Id,料理_Id,食材A_名前,食材B_名前,料理名";
            sb.AppendLine(header);
            foreach (var recipe in discoveredAllRecipe)
            {
                sb.AppendLine(recipe.ToString());
            }
            string currentPath = Path.Combine(Directory.GetCurrentDirectory(), _recipeFile)!;
            File.WriteAllText(currentPath, sb.ToString());
        }

        [TestMethod]
        public void AnlyzeEachRecipeTest()
        {
            // 前段の料理一覧から得た全料理組み合わせに対し、個別の料理事の勝敗を検証する
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _recipeFile)!;
            List<Recipe> receipeRecords =
                File.ReadAllLines(filePath)
                .Skip(1)
                .Select(line =>
                {
                    var items = line.Split(',');
                    return new Recipe(int.Parse(items[0]), int.Parse(items[1]), int.Parse(items[2]), items[3], items[4], items[5]);
                })
                .ToList();
            string header = File.ReadAllLines(filePath).First();




            string eachMaterialDirectoryPath = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "eachMateral")).FullName;

            // 食材ごとに料理になったレシピを修飾子になったレシピで再分類する
            foreach (var material in foodMaterials)
            {
                var locatedA = new List<Recipe>();
                var locatedB = new List<Recipe>();
                var sb = new StringBuilder();
                foreach (var recipe in receipeRecords)
                {
                    // レア化させる食材を別に扱う場合有効化
                    //if (rareFoodIds.Contains(recipe.MaterialA) || rareFoodIds.Contains(recipe.MaterialB))
                    //    continue;
                    // レア化させない食材
                    if (recipe.MaterialA == recipe.MaterialB)
                        continue;
                    {
                    }
                    if (material.objectID == recipe.MaterialA)
                    {
                        locatedA.Add(recipe);
                    }
                    if (material.objectID == recipe.MaterialB)
                    {
                        locatedB.Add(recipe);
                    }
                }
                // 再分類結果の出力
                string result = $"料理になった数:{locatedA.Count}\n修飾子になった数:{locatedB.Count}";
                sb.AppendLine(result);
                sb.AppendLine(header);
                foreach (var recipe in locatedA)
                {
                    sb.AppendLine(recipe.ToString());
                }
                sb.AppendLine();   // 空行挿入
                foreach (var recipe in locatedB)
                {
                    sb.AppendLine(recipe.ToString());
                }
                sb.AppendLine();   // 空行挿入

                // ファイル出力
                string fileName = Path.Combine(eachMaterialDirectoryPath, $"{material.objectID}_{material.DisplayName}.csv");
                File.WriteAllText(fileName, sb.ToString());
            }
        }
    }

    public record Recipe
    {
        public Recipe(int materialA, int materialB, int recipeId, string materialNameA, string materialNameB, string recipeCategoryName)
        {
            MaterialA = materialA;
            MaterialB = materialB;
            RecipeId = recipeId;
            MaterialNameA = materialNameA;
            MaterialNameB = materialNameB;
            RecipeCategoryName = recipeCategoryName;
        }

        public override string ToString()
        {
            return $"{MaterialA},{MaterialB},{RecipeId},{MaterialNameA},{MaterialNameB},{RecipeCategoryName}";
        }

        public int MaterialA { get; set; }
        public int MaterialB { get; set; }
        public int RecipeId { get; set; }
        public string MaterialNameA { get; set; } = string.Empty;
        public string MaterialNameB { get; set; } = string.Empty;
        public string RecipeCategoryName { get; set; } = string.Empty;
    }
}
