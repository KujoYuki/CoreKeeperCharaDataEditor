namespace CKCharaDataEditor.Model.Pet
{
    /// <summary>
    /// ペットの戦闘タイプ
    /// </summary>
    public enum PetBattleType
    {
        Undefined,
        /// <summary>
        /// 近接
        /// </summary>
        Melee,
        /// <summary>
        /// 遠距離
        /// </summary>
        Range,
        /// <summary>
        /// バフ
        /// </summary>
        Buff,
    }
}
