namespace CKCharaDataEditor.Model
{
    public record CookedCategory
    {
        public int objectID { get; set; }
        public string InternalName { get; set; }
        public string JapaneseResource { get; set; }

        public CookedCategory(int objectID, string internalName, string japaneseResource)
        {
            this.objectID = objectID;
            InternalName = internalName;
            JapaneseResource = japaneseResource;
        }
    }
}
