namespace CKFoodMaker.Model
{
    public record FoodMaterial
    {
        public int objectID { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }

        public FoodMaterial(int objectID, string internalName, string displayName)
        {
            this.objectID = objectID;
            InternalName = internalName;
            DisplayName = displayName;
        }
    }
}
