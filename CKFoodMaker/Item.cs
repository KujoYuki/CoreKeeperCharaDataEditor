namespace CKFoodMaker
{
    public record Item
    {
        public int objectID { get; set; }
        public int amount { get; set; }
        public int variation { get; set; }
        public int variationUpdateCount { get; set; }

        public static readonly Item Default = new(0, 0, 0, 0);

        public Item(string objectId, string amount, string variation, string variationUpdateCount = "0")
        {
            this.objectID = int.Parse(objectId);
            this.amount = int.Parse(amount);
            this.variation = int.Parse(variation);
            this.variationUpdateCount = int.Parse(variationUpdateCount);
        }

        public Item(int objectId, int amount, int variation, int variationUpdateCount = 0)
        {
            this.objectID = objectId;
            this.amount = amount;
            this.variation = variation;
            this.variationUpdateCount = variationUpdateCount;
        }
    }
}
