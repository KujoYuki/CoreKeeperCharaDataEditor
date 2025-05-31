using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Cattle
{
    public record Cattle : Item
    {
        public int Stomach => amount;
        public bool IsAdult => CattleDic[objectID].IsAdult;
        public bool EnableBreeding
        {
            get => EnableBreeding;
            set => EnableBreeding = IsAdult ? value : false;
        }

        //hack 元データはItemと変わらんのでインデクサをAuxManger使って絞りだした方が健全で済みそう
        public CattleType Type { get; set; }
        public string Name { get; set; }
        public int Meal { get; set; }
        
        public static readonly Dictionary<int, (CattleType CattleType, string ObjectName, bool IsAdult)> CattleDic = new()
        {
            { 1300, (CattleType.Cow, CattleType.Cow.ToString(), true) },
            { 1304, (CattleType.CowBaby, CattleType.CowBaby.ToString(), false) },
            { 1302, (CattleType.Bambuck, CattleType.Bambuck.ToString(), true) },
            { 1305, (CattleType.BambuckBaby, CattleType.BambuckBaby.ToString(), false) },
            { 1303, (CattleType.RolyPoly, CattleType.RolyPoly.ToString(), true) },
            { 1306, (CattleType.RolyPolyBaby, CattleType.RolyPolyBaby.ToString(), false) },
            { 1307, (CattleType.Turtle, CattleType.Turtle.ToString(), true) },
            { 1308, (CattleType.TurtleBaby, CattleType.TurtleBaby.ToString(), false) },
            { 1309, (CattleType.Dodo, CattleType.Dodo.ToString(), true) },
            { 1310, (CattleType.DodoBaby, CattleType.DodoBaby.ToString(), false) },
            { 1311, (CattleType.Camel, CattleType.Camel.ToString(), true) },
            { 1312, (CattleType.CamelBaby, CattleType.CamelBaby.ToString(), true) },
        };

        public static bool IsCattle(int objectID)
        {
            return CattleDic.ContainsKey(objectID);
        }

        // hack CattleにItemAuxData.Defaultは存在しないので、適切に設定する
        //public static new Cattle Default = new(CattleType.Cow, 0, 0, 0, string.Empty, ItemAuxData.Default);

        public Cattle(CattleType cattleType, int stomach, int variation, int variationUpdateCount, string objectName, ItemAuxData aux, bool locked = false) 
            : base((int)cattleType, stomach, variation, 0, cattleType.ToString(), aux, locked)
        {
            // hack Auxnoの中にあるパラメータなので不要になりそう
            aux.GetCattleData(out string name, out int meal, out bool? breeding);
            Name = name;
            Meal = meal;
            if (breeding != null)
            {
                EnableBreeding = breeding ?? false;
            }
        }
    }
}
