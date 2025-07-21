using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.Food;
using System.Text;

namespace CKCharaDataEditorTest
{
    public static class AnalyzeResource
    {
        private static readonly string winUserName = Environment.UserName;
        internal const int steamId = 449703638;
        internal const string targetSlotName = "完全レシピ";
        internal static readonly string SaveDataPath = $@"C:\Users\{winUserName}\AppData\LocalLow\Pugstorm\Core Keeper\Steam\{steamId}\saves\{targetSlotName}.json";
        internal const string RecipeFile = "analyzedRecipe.csv";

        // 全ての食材と料理の表示名を取得
        internal static readonly List<(int objectID, string DisplayName)> Ingredients = Recipe.AllIngredients
            .Concat(Recipe.ObsoleteIngredients)   // レシピには載らないが料理は作成できるため含める
            .Select(item => (item.objectID, item.DisplayName))
            .ToList();

        // Id=>DisplayNameの辞書
        internal static readonly Dictionary<int, string> FoodDic = Recipe.AllCookedBaseCategories
            .Select(item =>
            {
                return new[] { item.objectID, item.objectID + (int)CookRarity.Rare, item.objectID + (int)CookRarity.Epic }
                .Select(id => (objectID: id, item.objectName));
            })
            .SelectMany(food => food).ToList()
            .Concat(Ingredients)
            .ToDictionary();

        // 金色食材のIDリスト
        internal static readonly List<int> RareFoodIds = Enumerable.Range(8100, 11).Append(9733).ToList();

        // 魚食材のIDリスト
        internal static readonly List<int> FishFoodIds = Enumerable.Range(9700, 44).Where(id => id != 9733).ToList();
    }
}
