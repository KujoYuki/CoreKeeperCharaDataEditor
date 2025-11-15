using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Food
{
    public record Ingredient : DiscoveredObjects
    {
        public string objectName { get; set; }
        public string DisplayName { get; set; }
        public IngredientRoots Roots { get; set; }

        public CookedFood CookedFood { get; set; }

        public Ingredient(int objectID, string objectName, string displayName, IngredientRoots roots, CookedFood cookedFood)
            : base(objectID, 0)
        {
            this.objectName = objectName;
            DisplayName = displayName;
            Roots = roots;
            CookedFood = cookedFood;
        }

        public Item ToItem(int amount = 1)
        {
            return new Item(objectID, amount, 0, 0, objectName, ItemAuxData.Default);
        }
    }
}
