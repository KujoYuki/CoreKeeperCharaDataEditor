namespace CKCharaDataEditor.Model.Items
{
    public record Ingredient : DiscoveredObjects
    {
        public string objectName { get; set; }
        public string DisplayName { get; set; }
        public bool MakeRare { get; set; }
        public IngredientRoots Roots { get; set; }

        public Ingredient(int objectID, string objectName, string displayName, IngredientRoots roots, bool makeRare = false)
            : base(objectID, 0)
        {
            this.objectName = objectName;
            DisplayName = displayName;
            Roots = roots;
            MakeRare = makeRare;
            Roots = roots;
        }
    }
}
