using CKCharaDataEditor.Model.ItemAux;

namespace CKCharaDataEditor.Model
{
    public record Item
    {
        public ItemInfo Info { get; set; }

        public string objectName { get; set; }
        public ItemAuxData Aux { get; set; }
        public bool Locked { get; set; }

        /// <summary>
        /// 日本語リソース表示
        /// </summary>
        public string DisplayName { get; private set; }

        public Item(ItemInfo info, string objectName, ItemAuxData aux, bool locked = false)
        {
            Info = info;
            this.objectName = objectName;
            Aux = aux;
            DisplayName = string.Empty;
            Locked = locked;
        }

        public Item(int objectID, string objectName, string displayName, bool locked = false)
        {
            Info = new(objectID);
            this.objectName = objectName;
            Aux = ItemAuxData.Default;
            DisplayName = displayName;
            Locked = locked;
        }

        public static Item Default = new(ItemInfo.Default, string.Empty, ItemAuxData.Default);
    }
}
