using CKCharaDataEditor.Model.ItemAux;

namespace CKCharaDataEditor.Model.Items
{
    public record Item : ItemInfo
    {
        public string objectName { get; set; }
        public ItemAuxData Aux { get; set; }
        public bool Locked { get; set; }

        /// <summary>
        /// 日本語リソース表示
        /// </summary>
        public string DisplayName { get; private set; }

        public Item(int objectID, int amount, int variation, int variationUpdateCount, string objectName, ItemAuxData aux, bool locked = false)
            :base(objectID, amount, variation, variationUpdateCount)
        {
            this.objectName = objectName;
            Aux = aux;
            DisplayName = string.Empty;
            Locked = locked;
        }

        public static new Item Default = new(0, 0, 0, 0, string.Empty, ItemAuxData.Default);
    }
}
