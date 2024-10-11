using CKFoodMaker.Model.ItemAux;

namespace CKFoodMaker.Model
{
    /// <summary>
    /// ソフト側で持つアイテムの基本情報
    /// </summary>
    public record InternalItemInfo
    {
        public int ObjectID { get; private set; }
        public string InternalName { get; private set; }
        public string DisplayName { get; private set; }
        public ItemAuxData AuxData { get; private set; } = ItemAuxData.Default;

        public InternalItemInfo(int objectID, string internalName, string displayName)
        {
            ObjectID = objectID;
            InternalName = internalName;
            DisplayName = displayName;
        }

        // 補助データが存在する場合のコンストラクタ
        public InternalItemInfo(string objectId, string internalName, string displayName, ItemAuxData auxData) 
            : this(int.Parse(objectId), internalName, displayName)
        {
            AuxData = auxData;
        }
    }
}
