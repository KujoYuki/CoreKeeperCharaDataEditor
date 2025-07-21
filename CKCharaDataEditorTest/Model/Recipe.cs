using System.Text;

namespace CKCharaDataEditorTest.Model
{
    internal class Recipe
    {
        public Recipe(int ingredientA, int ingredientB, int recipeId)
        {
            IngredientA = ingredientA;
            IngredientB = ingredientB;
            RecipeId = recipeId;
        }

        public const string Header = "食材A_Id, 食材B_Id, 料理_Id, 食材A_名前, 食材B_名前, 料理名";
        public const string HeaderWithBinary = "食材A_Id, 食材B_Id, 料理_Id, バイナリA, バイナリB, 論理積, 論理和, 排他的論理和, 食材A_名前, 食材B_名前, 料理名";

        public override string ToString()
        {
            return $"{IngredientA},{IngredientB},{RecipeId},{AnalyzeResource.FoodDic[IngredientA]}, {AnalyzeResource.FoodDic[IngredientB]}, {AnalyzeResource.FoodDic[RecipeId]}";
        }

        public string ToStringWithBinary()
        {
            var binaryA = Convert.ToString(IngredientA, 2).PadLeft(16, '0');
            var binaryB = Convert.ToString(IngredientB, 2).PadLeft(16, '0');
            var andResult = Convert.ToString(IngredientA & IngredientB, 2).PadLeft(16, '0');
            var orResult = Convert.ToString(IngredientA | IngredientB, 2).PadLeft(16, '0');
            var xorResult = Convert.ToString(IngredientA ^ IngredientB, 2).PadLeft(16, '0');

            return $"{IngredientA},{IngredientB},{RecipeId},{SplitUnderbar(binaryA)}, {SplitUnderbar(binaryB)}, " +
                $"{SplitUnderbar(andResult)}, {SplitUnderbar(orResult)}, {SplitUnderbar(xorResult)}, " +    // 論理積, 論理和, 排他的論理和
                //$"{CountOneBits(SplitUnderbar(andResult))}, {CountOneBits(SplitUnderbar(orResult))}, {CountOneBits(SplitUnderbar(xorResult))}, " +  // 論理積, 論理和, 排他的論理和のセットビット数
                $"{AnalyzeResource.FoodDic[IngredientA]}, {AnalyzeResource.FoodDic[IngredientB]}, {AnalyzeResource.FoodDic[RecipeId]}, ";
        }

        private static string SplitUnderbar(string binaryString)
        {
            StringBuilder sb = new();
            for (int i = 0; i < binaryString.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    sb.Append('_');
                }
                sb.Append(binaryString[i]);
            }
            return sb.ToString();
        }

        public int IngredientA { get; set; }
        public int IngredientB { get; set; }
        public int RecipeId { get; set; }
    }
}
