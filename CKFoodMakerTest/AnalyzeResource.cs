using CKFoodMaker.Model;
using CKFoodMaker.Resource;
using System.Text;

namespace CKFoodMakerTest
{
    public static class AnalyzeResource
    {
        private static readonly string winUserName = Environment.UserName;
        internal const int id = 449703638;
        internal const string targetSlotName = "完全レシピ";
        internal static readonly string SaveDataPath = $@"C:\Users\{winUserName}\AppData\LocalLow\Pugstorm\Core Keeper\Steam\{id}\saves\{targetSlotName}.json";
        internal const string RecipeFile = "analyzedRecipe.csv";

        // 全ての食材と料理の表示名を取得
        internal static readonly List<(int objectID, string DisplayName)> FoodMaterials = StaticResource.AllFoodMaterials
            .Concat(StaticResource.ObsoleteFoodMaterials)   // レシピには載らないが料理は作成できるため含める
            .Select(item => (objectID: item.Info.objectID, DisplayName: item.DisplayName))
            .ToList();

        // Id=>DisplayNameの辞書
        internal static readonly Dictionary<int, string> FoodDic = StaticResource.AllCookedBaseCategories
            .Select(item =>
            {
                return new[] { item.Info.objectID, item.Info.objectID + (int)CookRarity.Rare, item.Info.objectID + (int)CookRarity.Epic }
                .Select(id => (objectID: id, DisplayName: item.DisplayName));
            })
            .SelectMany(food => food).ToList()
            .Concat(FoodMaterials)
            .ToDictionary();

        // 金色食材のIDリスト
        internal static readonly List<int> RareFoodIds = Enumerable.Range(8100, 11).Append(9733).ToList();

        // 魚食材のIDリスト
        internal static readonly List<int> FishFoodIds = Enumerable.Range(9700, 44).Where(id => id != 9733).ToList();
    }
}
