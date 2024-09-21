namespace CKFoodMaker.Model
{
    public record ItemBase
    {
        public int objectID { get; set; }
        public int amount { get; set; }
        public int variation { get; set; }
        public int variationUpdateCount { get; set; }

        public static readonly ItemBase Default = new(0, 0, 0, 0);

        public ItemBase(string objectID, string amount, string variation, string variationUpdateCount = "0")
        {
            this.objectID = int.Parse(objectID);
            this.amount = int.Parse(amount);
            this.variation = int.Parse(variation);
            this.variationUpdateCount = int.Parse(variationUpdateCount);
        }

        public ItemBase(int objectID, int amount, int variation, int variationUpdateCount = 0)
        {
            this.objectID = objectID;
            this.amount = amount;
            this.variation = variation;
            this.variationUpdateCount = variationUpdateCount;
        }
    }
}
