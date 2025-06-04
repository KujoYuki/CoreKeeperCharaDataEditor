using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Cattle
{
    public record Cattle : Item
    {
        public int Stomach => amount;
        public int Color => variation;
        public bool IsAdult => Define[objectID].IsAdult;
        public bool EnableBreeding
        {
            get => EnableBreeding;
            set => EnableBreeding = IsAdult ? value : false;
        }

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

        public static bool IsCattle(int objectID)
        {
            return Define.ContainsKey(objectID);
        }

        public Cattle(CattleType cattleType,string name, int meal, bool breeding, int stomach, int variation, int variationUpdateCount, string objectName, ItemAuxData aux, bool locked = false) 
            : base((int)cattleType, stomach, variation, 0, cattleType.ToString(), aux, locked)
        {
            Name = name;
            Meal = meal;
            Breeding = breeding;
        }

        //hack 次はここから
        //public Cattle ConvertFrom(Item item)
        //{
        //    if (IsCattle(item.objectID))
        //    {
        //        throw new InvalidCastException("Cannot convert Item to Cattle.");
        //    }

        //    // ItemからCattleに変換する際、デフォルトを経由してAuxを定義したい。
        //    return new Cattle()
        //    {
        //        objectID = item.objectID,
        //        variation = item.variation,
        //        amount = item.amount,
        //        variationUpdateCount = item.variationUpdateCount,
        //        Aux = item.Aux,
        //    };
        //}

        /// デフォルトの牛を生成します。ItemAuxDataが空だとNameをsetする時に失敗する。定義するなら中身も用意すること
        //public static new Cattle Default = new(CattleType.Cow, string.Empty, 0, true, 0, 0, 0, "Cow", ItemAuxData.Default);
    }
}
