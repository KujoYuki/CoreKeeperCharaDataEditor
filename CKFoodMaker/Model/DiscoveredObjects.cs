namespace CKFoodMaker.Model
{
    public record DiscoveredObjects
    {
        public int objectID { get; set; }
        public int variation { get; set; }

        public DiscoveredObjects(int objectID, int variation)
        {
            this.objectID = objectID;
            this.variation = variation;
        }
    }
}
