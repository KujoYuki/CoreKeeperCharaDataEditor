namespace CKCharaDataEditor.Model.Items
{
    public record DiscoveredObjects : IItem
    {
        public int objectID { get; set; }
        public int variation { get; set; }

        public DiscoveredObjects(int objectID, int variation)
        {
            this.objectID = objectID;
            this.variation = variation;
        }
        public static readonly DiscoveredObjects Default = new(0, 0);
    }
}
