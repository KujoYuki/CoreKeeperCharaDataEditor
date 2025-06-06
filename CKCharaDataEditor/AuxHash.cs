﻿namespace CKCharaDataEditor
{
    public static class AuxHash
    {
        #region 複数種類のデータで共通するリソース識別用ハッシュ
        public const ulong ItemNameGroupHash = 2811915185;
        public const ulong ItemNameHash = 9923282613123898873;
        #endregion

        #region ペットデータで使うリソース識別用ハッシュ
        public const ulong PetGroupHash = 1145011307;
        public const ulong PetColorHash = 13695103918181693450;
        public const ulong PetTalentsHash = 16038764625220822319;
        #endregion

        #region 家畜データで使うリソース識別用ハッシュ
        public const ulong CattleMealGroupHash = 97913750;
        public const ulong CattleMealHash = 18362501804100791937;
        public const ulong CattleBreedingGroupHash = 1023625683;
        public const ulong CattleBreedingHash = 3386982922340789878;
        #endregion
    }
}
