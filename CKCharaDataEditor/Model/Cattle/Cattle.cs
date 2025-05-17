using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Cattle
{
    public record Cattle : Item
    {
        public static new Cattle Default = new(new Item((int)CattleType.Cow, 0, 0, 0, CattleType.Cow.ToString(), CattleResource.DefaultAdultCattleAuxData));

        #region properties

        /// <summary>
        /// alias objectID for CattleType
        /// </summary>
        public CattleType Type
        {
            get
            {
                return (CattleType)objectID;
            }
            set
            {
                objectID = (int)value;
            }
        }
        public int Color
        {
            get => variation;
            set => variation = value;
        }
        public int Stomach
        {
            get => amount;
            set => amount = value;
        }
        public string Name
        {
            get
            {
                return Aux.AuxPrefabManager!.GetData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash)!.FirstOrDefault()!;
            }
            set
            {
                Aux.AuxPrefabManager!.UpdateData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash, [value]);
            }
        }
        public int Meal
        {
            get
            {
                return Convert.ToInt32(Aux.AuxPrefabManager!.GetData(AuxHash.CattleMealGroupHash, AuxHash.CattleMealHash)!.FirstOrDefault()!);
            }
            set
            {
                Aux.AuxPrefabManager!.UpdateData(AuxHash.CattleMealGroupHash, AuxHash.CattleMealHash, [value.ToString()]);
            }
        }
        public bool Breeding
        {
            get
            {
                return Aux.AuxPrefabManager!.GetData(AuxHash.CattleBreedingGroupHash, AuxHash.CattleBreedingHash)!.FirstOrDefault() == "False";
            }
            set
            {
                if (IsAdult)
                {
                    // データ側がbool値が逆になってるので注意
                    Aux.AuxPrefabManager!.UpdateData(AuxHash.CattleBreedingGroupHash, AuxHash.CattleBreedingHash, [value ? "False" : "True"]);
                }
            }
        }
        #endregion

        public static bool IsCattle(int objectID)
        {
            return Enum.GetValues<CattleType>().Cast<int>().Contains(objectID);
        }

        public bool IsAdult => CattleResource.Define[objectID].IsAdult;

        public Cattle(Item item) : base(item) { }
    }
}
