namespace CKCharaDataEditor.Model.Food
{
    [Flags]
    public enum IngredientRoots
    {
        None,
        Harvest,
        Fish,
        Cooked,
        Season,
        /// <summary>
        /// 削除済みアイテム
        /// </summary>
        Deprecated,
    }
}
