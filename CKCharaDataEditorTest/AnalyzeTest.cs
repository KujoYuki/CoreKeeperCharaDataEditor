﻿using CKCharaDataEditor.Model;
using CKCharaDataEditor.Resource;
using CKCharaDataEditor;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using CKCharaDataEditorTest.Model;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditorTest
{
    [TestClass]
    public class AnalyzeTest
    {
        [TestMethod]
        public void AnalyzeReipeTest()
        {
            string saveDataContents = File.ReadAllText(AnalyzeResource.SaveDataPath);
            // conditionsList中のInfinity文字列により例外が出るのを回避する
            saveDataContents = SaveDataManager.SanitizeJsonString(saveDataContents);

            JsonObject _saveData = JsonNode.Parse(saveDataContents)!.AsObject();

            // 料理のコモンとレアのカテゴリIDリスト
            List<int> allCookedCategoryId = StaticResource.AllCookedBaseCategories
                .SelectMany(c => new[] { c.objectID, c.objectID + (int)CookRarity.Rare })
                .OrderBy(id => id)
                .ToList();

            // 全ての食材と料理の表示名を取得
            var ingredients = StaticResource.AllIngredients
                .Concat(StaticResource.ObsoleteIngredients)   // レシピには載らないが料理は作成できるため含める
                .Select(item => (objectID: item.objectID, DisplayName: item.DisplayName))
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
                    Form1.ReverseCalcurateVariation(r.variation, out int ingredientA, out int ingredientB);
                    int[] ingredients = [ingredientA, ingredientB];
                    foreach (var ingredient in ingredients)
                    {
                        // 食材じゃないものが食材の場合を除外（シーズンアイテム系など）
                        if (!AnalyzeResource.FoodDic.ContainsKey(ingredient))
                        {
                            return false;
                        }
                    }
                    return true;
                })
                .Select(r =>
                {
                    // csv用レコードに変換
                    Form1.ReverseCalcurateVariation(r.variation, out int ingredientA, out int ingredientB);
                    return new Recipe(ingredientA, ingredientB, r.objectID);
                })
                .OrderBy(r => r.IngredientA)
                .ThenBy(r => r.IngredientB)
                .ToList();

            var sb = new StringBuilder(Recipe.Header);
            foreach (var recipe in discoveredAllRecipe)
            {
                sb.AppendLine(recipe.ToString());
            }
            string currentPath = Path.Combine(Directory.GetCurrentDirectory(), AnalyzeResource.RecipeFile)!;
            File.WriteAllText(currentPath, sb.ToString());
        }

        /// <summary>
        /// 前段のレシピに対して個別の組み合わせを検証する
        /// </summary>
        [TestMethod]
        public void AnlyzeEachRecipeTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), AnalyzeResource.RecipeFile)!;
            List<Recipe> receipeRecords =
                File.ReadAllLines(filePath)
                .Skip(1)    // ヘッダー行をスキップ
                .Select(line =>
                {
                    var items = line.Split(',');
                    return new Recipe(int.Parse(items[0]), int.Parse(items[1]), int.Parse(items[2]));
                })
                .ToList();

            string eachIngredientDirectoryPath = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "eachMateral")).FullName;

            // レシピを任意ルールで再分類する
            foreach (var ingredient in AnalyzeResource.Ingredients)
            {
                var locatedA = new List<Recipe>();
                var locatedB = new List<Recipe>();
                var sb = new StringBuilder();
                foreach (var recipe in receipeRecords)
                {
                    // レア化させる食材を別に扱う場合、true
                    if (false)
                    {
                        if (AnalyzeResource.RareFoodIds.Contains(recipe.IngredientA) || AnalyzeResource.RareFoodIds.Contains(recipe.IngredientB))
                            continue;
                    }
                    // 同一食材を使った料理は除外する場合、false
                    if (true)
                    {
                        if (recipe.IngredientA == recipe.IngredientB)
                            continue;
                    }
                    // 魚食材を使った料理のみを抽出する場合、true
                    if (true)
                    {
                        if (!(AnalyzeResource.FishFoodIds.Contains(recipe.IngredientA) && AnalyzeResource.FishFoodIds.Contains(recipe.IngredientB)))
                            continue;
                    }

                    if (ingredient.objectID == recipe.IngredientA)
                    {
                        locatedA.Add(recipe);
                    }
                    if (ingredient.objectID == recipe.IngredientB)
                    {
                        locatedB.Add(recipe);
                    }
                }
                // 再分類結果の出力
                string result = $"料理になった数:{locatedA.Count}\n修飾子になった数:{locatedB.Count}";
                sb.AppendLine(result);
                //sb.AppendLine(Recipe.Header);　// 通常出力用ヘッダー
                sb.AppendLine(Recipe.HeaderWithBinary);   // バイナリ出力用ヘッダー
                foreach (var recipe in locatedA)
                {
                    sb.AppendLine(recipe.ToStringWithBinary());
                }
                sb.AppendLine();   // 空行挿入
                foreach (var recipe in locatedB)
                {
                    // レシピ内の表示順序を逆にする時、true
                    if (false)
                    {
                        sb.AppendLine(new Recipe(recipe.IngredientB, recipe.IngredientA, recipe.RecipeId).ToStringWithBinary());
                    }
                    else
                    {
                        sb.AppendLine(recipe.ToStringWithBinary());
                    }
                }
                sb.AppendLine();   // 空行挿入

                // ファイル出力
                string fileName = Path.Combine(eachIngredientDirectoryPath, $"{ingredient.objectID}_{ingredient.DisplayName}.csv");
                File.WriteAllText(fileName, sb.ToString());
            }
        }
    }
}
