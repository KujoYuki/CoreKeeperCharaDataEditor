namespace CKCharaDataEditor.Model.Food
{
    /// <summary>
    /// 食材の属性を定義したもの
    /// </summary>
    [Flags]
    public enum IngredientAttribute
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
