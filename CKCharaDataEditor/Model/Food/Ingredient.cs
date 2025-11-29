using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Food
{
    public record Ingredient : DiscoveredObjects
    {
        public string objectName { get; set; }
        public string DisplayName { get; set; }
        public IngredientAttribute Attribute { get; set; }

        /// <summary>
        /// Primary食材になった場合に調理される料理
        /// </summary>
        public CookedFood CookedFood { get; set; }

        public Ingredient(int objectID, string objectName, string displayName, CookedFood cookedFood, IngredientAttribute attribute)
            : base(objectID, 0)
        {
            this.objectName = objectName;
            DisplayName = displayName;
            CookedFood = cookedFood;
            Attribute = attribute;
        }

        public Item ToItem(int amount = 1)
        {
            return new Item(objectID, amount, 0, 0, objectName, ItemAuxData.Default);
        }
    }
}
