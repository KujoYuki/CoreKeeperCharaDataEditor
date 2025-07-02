using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Food
{
    public record Food : Item
    {
        public int IngredientA
        {
            get
            {
                // 16ビット右にシフトして上位16ビットを取得
                return variation >> 16;
            }
            set
            {
                variation &= 0x0000FFFF;    // 上位16ビットをクリア
                variation |= (value << 16); // 新しい値を上位16ビットに設定
            }
        }

        public int IngredientB
        {
            get
            {
                // 下位16ビットを取得
                return variation & 0x0000FFFF;
            }
            set
            {
                // unchecked構文を使用して定数値の変換エラーを回避
                variation &= unchecked((int)0xFFFF0000);    // 下位16ビットをクリア
                variation |= value;         // 新しい値を下位16ビットに設定
            }
        }

        public IngredientRoots Roots
        {
            get;
            set;
        }

        public bool CanMakeRare 
        {
            get;
            set;
        }

        /// <summary>
        /// variationからIdへの逆算
        /// </summary>
        /// <param name="ingredientA">材料の食材A</param>
        /// <param name="ingredientB">材料の食材B</param>
        private void UnpackVariation(out int ingredientA, out int ingredientB)
        {
            // 16ビット右にシフトして上位16ビットを取得
            ingredientA = variation >> 16;
            // 下位16ビットを取得
            ingredientB = variation & 0xFFFF;
        }

        protected Food(Item original) : base(original)
        {
        }

        public Food(int objectID, string objectName, string displayName, IngredientRoots roots = IngredientRoots.None, bool canMakeRare = false)
            : base(Default with { objectID = objectID, objectName = objectName})
        {
            //hack 翻訳ファイルを解す場合はObjectName不要
            Roots = roots;
            CanMakeRare = canMakeRare;
        }

        // todo 調理カテゴリや食材の定義をStaticResourceから移行するか検討する。

        // todo 翻訳ファイルが無くても、日本語表示リソースは持っておきたい =>displayNameを持つべきか検討する
    }
}
