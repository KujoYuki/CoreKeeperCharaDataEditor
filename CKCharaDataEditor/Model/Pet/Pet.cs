using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Pet
{
    public record Pet : Item
    {
        public static new Pet Default = new(new Item((int)PetType.PetDog, 0, 0, 0, PetType.PetDog.ToString(), PetResource.DefaultPetAuxData!));

        #region properties

        public PetType Type
        {
            get
            {
                return (PetType)objectID;
            }
            set
            {
                objectID = (int)value;
            }
        }

        public int Exp
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
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

        public PetColor Color
        {
            get
            {
                string colorNum = Aux.AuxPrefabManager!.GetData(AuxHash.PetGroupHash, AuxHash.PetColorHash)!.FirstOrDefault()!;
                return  (PetColor)Convert.ToInt32(colorNum);
            }
            set
            {
                Aux.AuxPrefabManager!.UpdateData(AuxHash.PetGroupHash, AuxHash.PetColorHash, [((int)value).ToString()]);
            }
        }

        public PetTalent[] Talents
        {
            get
            {
                return Aux.AuxPrefabManager!.GetData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash)!
                    .Select(str => new PetTalent(str)).ToArray();
            }
            set
            {
                Aux.AuxPrefabManager!.UpdateData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash,
                    value.Select(t => t.ToJsonString()));
            }
        }
        #endregion
        public static bool IsPet(int objectId)
        {
            return Enum.GetValues<PetType>().Cast<int>().Contains(objectId);
        }

        public Pet(Item item) : base(item) { }
    }
}
