using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Cattle
{
    public record Cattle : Item
    {
        #region static

        public static readonly Dictionary<int, (CattleType CattleType, string ObjectName, bool IsAdult)> Define = new()
        {
            { (int)CattleType.Cow, (CattleType.Cow, CattleType.Cow.ToString(), true) },
            { (int)CattleType.CowBaby, (CattleType.CowBaby, CattleType.CowBaby.ToString(), false) },
            { (int)CattleType.Bambuck, (CattleType.Bambuck, CattleType.Bambuck.ToString(), true) },
            { (int)CattleType.BambuckBaby, (CattleType.BambuckBaby, CattleType.BambuckBaby.ToString(), false) },
            { (int)CattleType.RolyPoly, (CattleType.RolyPoly, CattleType.RolyPoly.ToString(), true) },
            { (int)CattleType.RolyPolyBaby, (CattleType.RolyPolyBaby, CattleType.RolyPolyBaby.ToString(), false) },
            { (int)CattleType.Turtle, (CattleType.Turtle, CattleType.Turtle.ToString(), true) },
            { (int)CattleType.TurtleBaby, (CattleType.TurtleBaby, CattleType.TurtleBaby.ToString(), false) },
            { (int)CattleType.Dodo, (CattleType.Dodo, CattleType.Dodo.ToString(), true) },
            { (int)CattleType.DodoBaby, (CattleType.DodoBaby, CattleType.DodoBaby.ToString(), false) },
            { (int)CattleType.Camel, (CattleType.Camel, CattleType.Camel.ToString(), true) },
            { (int)CattleType.CamelBaby, (CattleType.CamelBaby, CattleType.CamelBaby.ToString(), true) },
        };

        public static readonly List<AuxPrefab> DefaultCattlePrefabs =
        [
            new AuxPrefab(AuxHash.ItemNameGroupHash, [new AuxStableType(AuxHash.ItemNameHash, [""])]),
            new AuxPrefab(AuxHash.CattleMealGroupHash, [new AuxStableType(AuxHash.CattleMealHash, ["0"])]),
            new AuxPrefab(AuxHash.CattleBreedingGroupHash, [new AuxStableType(AuxHash.CattleBreedingHash, ["False"])]),
        ];

        public static readonly ItemAuxData DefaultAdultCattle = new(0, new AuxPrefabManager(DefaultCattlePrefabs!));

        public static new Cattle Default = new Cattle(new Item((int)CattleType.Cow, 0, 0, 0, CattleType.Cow.ToString(), DefaultAdultCattle));

        #endregion

        #region properties
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
            return Define.ContainsKey(objectID);
        }

        public bool IsAdult => Define[objectID].IsAdult;

        /// <summary>
        /// alias objectID for CattleType
        /// </summary>
        public CattleType Type
        {
            get
            {
                return Define[objectID].CattleType;
            }
            set
            {
                objectID = (int)value;
            }
        }
        public Cattle(Item item) : base(item) { }
    }
}
