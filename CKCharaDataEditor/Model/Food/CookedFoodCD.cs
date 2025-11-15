using System;
using System.Collections.Generic;
using System.Text;

namespace CKCharaDataEditor.Model.Food
{
    //hack 全組み合わせを列挙して作成するメソッドを追加
    /// <summary>
    /// 調理済み食べ物のプライマリ食材決定ロジック
    /// </summary>
    public static class CookedFoodCD
    {
        //static readonly List<int> GoldenPlant = Enumerable.Range(8100, 11).ToList();
        //const int StarlightNautilus = 9733;

        //public static bool IngredientShouldBePrimary(int ingredient)
        //{
        //    return GoldenPlant.Contains(ingredient) || ingredient == StarlightNautilus;
        //}
        //public static int GetPrimaryIngredient(int ingredient1, int ingredient2)
        //{
        //    // どちらが「プライマリ候補」かを明示的に判定して可読性を上げる
        //    bool firstPreferred = CookedFoodCD.IngredientShouldBePrimary(ingredient1);
        //    bool secondPreferred = CookedFoodCD.IngredientShouldBePrimary(ingredient2);

        //    // 片方のみ が優先で他方が優先でないなら 優先の食べ物をプライマリで返す
        //    if (firstPreferred && !secondPreferred)
        //    {
        //        return ingredient1;
        //    }
        //    else if (!firstPreferred && secondPreferred)
        //    {
        //        return ingredient2;
        //    }
        //    else
        //    {
        //        // 両方とも優先、または両方とも非優先の場合は決定論的な比較で決める
        //        return CookedFoodCD.FirstIngredientIsPrimary(ingredient1, ingredient2) ? ingredient1 : ingredient2;
        //    }
        //}

        //private static bool FirstIngredientIsPrimary(int ingredient1, int ingredient2)
        //{
        //    Unity.Mathematics.Random fromIndex1 = Unity.Mathematics.Random.CreateFromIndex((uint)(ingredient1 * 2 + ingredient2 + 87931));
        //    Unity.Mathematics.Random fromIndex2 = Unity.Mathematics.Random.CreateFromIndex((uint)(ingredient2 * 2 + ingredient1 + 87931));
        //    return (double)fromIndex1.NextFloat() > (double)fromIndex2.NextFloat();
        //}
    }
}
