namespace CKFoodMaker.Model
{
    public record ItemInfo
    {
        public int objectID { get; set; }
        public int amount { get; set; }
        public int variation { get; set; }
        public int variationUpdateCount { get; set; }

        public static readonly ItemInfo Default = new(0, 0, 0, 0);

        public ItemInfo(string objectID, string amount, string variation, string variationUpdateCount = "0")
        {
            this.objectID = int.Parse(objectID);
            this.amount = int.Parse(amount);
            this.variation = int.Parse(variation);
            this.variationUpdateCount = int.Parse(variationUpdateCount);
        }

        public ItemInfo(int objectID, int amount = 0, int variation = 0, int variationUpdateCount = 0)
        {
            this.objectID = objectID;
            this.amount = amount;
            this.variation = variation;
            this.variationUpdateCount = variationUpdateCount;
        }
    }
}
