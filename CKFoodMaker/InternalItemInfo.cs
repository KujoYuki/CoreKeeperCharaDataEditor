namespace CKFoodMaker
{
    /// <summary>
    /// ソフト側で持つアイテムの基本情報
    /// </summary>
    public record InternalItemInfo
    {
        public int ObjectID { get; private set; }
        public string InternalName { get; private set; }
        public string DisplayName { get; private set; }
        public MaterialSubCategory SubCategory { get; private set; } = MaterialSubCategory.None;

        public InternalItemInfo(string objectId, string internalName, string displayName, MaterialSubCategory subCategory = MaterialSubCategory.None)
        {
            ObjectID = int.Parse(objectId);
            InternalName = internalName;
            DisplayName = displayName;
            SubCategory = subCategory;
        }
    }
}
