namespace CKCharaDataEditor.Model.Items
{
    public record ItemInfo : DiscoveredObjects
    {
        public int amount { get; set; }
        public int variationUpdateCount { get; set; }

        public static new readonly ItemInfo Default = new(0, 0, 0, 0);

        public ItemInfo(string objectID, string amount, string variation, string variationUpdateCount = "0") : base(int.Parse(objectID), int.Parse(variation))
        {
            this.amount = int.Parse(amount);
            this.variationUpdateCount = int.Parse(variationUpdateCount);
        }

        public ItemInfo(int objectID, int amount = 0, int variation = 0, int variationUpdateCount = 0) : base(objectID, variation)
        {
            this.amount = amount;
            this.variationUpdateCount = variationUpdateCount;
        }
    }
}
