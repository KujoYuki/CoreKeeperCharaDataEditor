using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace CKFoodMakerTest.Model
{
    internal class Recipe
    {
        public Recipe(int materialA, int materialB, int recipeId)
        {
            MaterialA = materialA;
            MaterialB = materialB;
            RecipeId = recipeId;
        }

        public const string Header = "食材A_Id, 食材B_Id, 料理_Id, 食材A_名前, 食材B_名前, 料理名";
        public const string HeaderWithBinary = "食材A_Id, 食材B_Id, 料理_Id, バイナリA, バイナリB, 論理積, 論理和, 排他的論理和, 食材A_名前, 食材B_名前, 料理名";

        public override string ToString()
        {
            return $"{MaterialA},{MaterialB},{RecipeId},{AnalyzeResource.FoodDic[MaterialA]}, {AnalyzeResource.FoodDic[MaterialB]}, {AnalyzeResource.FoodDic[RecipeId]}";
        }

        public string ToStringWithBinary()
        {
            var binaryA = Convert.ToString(MaterialA, 2).PadLeft(16, '0');
            var binaryB = Convert.ToString(MaterialB, 2).PadLeft(16, '0');
            var andResult = Convert.ToString(MaterialA & MaterialB, 2).PadLeft(16, '0');
            var orResult = Convert.ToString(MaterialA | MaterialB, 2).PadLeft(16, '0');
            var xorResult = Convert.ToString(MaterialA ^ MaterialB, 2).PadLeft(16, '0');

            return $"{MaterialA},{MaterialB},{RecipeId},{SplitUnderbar(binaryA)}, {SplitUnderbar(binaryB)}, " +
                $"{SplitUnderbar(andResult)}, {SplitUnderbar(orResult)}, {SplitUnderbar(xorResult)}, " +    // 論理積, 論理和, 排他的論理和
                //$"{CountOneBits(SplitUnderbar(andResult))}, {CountOneBits(SplitUnderbar(orResult))}, {CountOneBits(SplitUnderbar(xorResult))}, " +  // 論理積, 論理和, 排他的論理和のセットビット数
                $"{AnalyzeResource.FoodDic[MaterialA]}, {AnalyzeResource.FoodDic[MaterialB]}, {AnalyzeResource.FoodDic[RecipeId]}, ";
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

        private static int CountOneBits(string binaryString)
        {
            int count = 0;
            foreach (char c in binaryString)
            {
                if (c == '1')
                {
                    count++;
                }
            }
            return count;
        }

        public int MaterialA { get; set; }
        public int MaterialB { get; set; }
        public int RecipeId { get; set; }
    }
}
