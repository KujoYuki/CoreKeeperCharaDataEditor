using CKCharaDataEditor.Models.ItemAux;

namespace CKCharaDataEditor.Models.Cattle
{
    public class CattleResource
    {
        public static readonly Dictionary<int, (CattleType CattleType, string KeyName, bool IsAdult)> Define = new()
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
            { (int)CattleType.CamelBaby, (CattleType.CamelBaby, CattleType.CamelBaby.ToString(), false) },
            { (int)CattleType.Raptor, (CattleType.Raptor, CattleType.Raptor.ToString(), true) },
            { (int)CattleType.RaptorBaby, (CattleType.RaptorBaby, CattleType.RaptorBaby.ToString(), false) },
        };

        public static readonly List<AuxPrefab> DefaultCattlePrefabs =
            [
            new AuxPrefab(AuxHash.ItemNameGroupHash, [new AuxStableType(AuxHash.ItemNameHash, [""])]),
            new AuxPrefab(AuxHash.CattleMealGroupHash, [new AuxStableType(AuxHash.CattleMealHash, ["0"])]),
            new AuxPrefab(AuxHash.CattleBreedingGroupHash, [new AuxStableType(AuxHash.CattleBreedingHash, ["False"])])
            ];

        public static readonly Dictionary<(CattleType, int), string> Colors = new Dictionary<(CattleType, int), string>()
        {
            { (CattleType.Cow, 0), "紫(Default)"},
            { (CattleType.Cow, 1), "黄"},
            { (CattleType.Cow, 2), "灰"},
            { (CattleType.Cow, 3), "アイボリー"},
            { (CattleType.Cow, 4), "緑"},
            { (CattleType.Bambuck, 0), "ピンク(Default)"},
            { (CattleType.Bambuck, 1), "白"},
            { (CattleType.Bambuck, 2), "黒"},
            { (CattleType.Bambuck, 3), "黄"},
            { (CattleType.Bambuck, 4), "桃"},
            { (CattleType.RolyPoly, 0), "黄(Default)"},
            { (CattleType.RolyPoly, 1), "橙"},
            { (CattleType.RolyPoly, 2), "紫"},
            { (CattleType.RolyPoly, 3), "緑"},
            { (CattleType.RolyPoly, 4), "青"},
            { (CattleType.Turtle, 0), "水(Default)"},
            { (CattleType.Turtle, 1), "灰"},
            { (CattleType.Turtle, 2), "白"},
            { (CattleType.Turtle, 3), "青"},
            { (CattleType.Turtle, 4), "紫"},
            { (CattleType.Dodo, 0), "白(Default)"},
            { (CattleType.Dodo, 1), "水"},
            { (CattleType.Dodo, 2), "薄茶"},
            { (CattleType.Dodo, 3), "灰"},
            { (CattleType.Dodo, 4), "黄"},
            { (CattleType.Camel, 0), "黄(Default)"},
            { (CattleType.Camel, 1), "黄？"},
            { (CattleType.Camel, 2), "黒"},
            { (CattleType.Camel, 3), "茶"},
            { (CattleType.Camel, 4), "白"},
            { (CattleType.Raptor, 0), "緑(Default)"},
            { (CattleType.Raptor, 1), "茶"},
            { (CattleType.Raptor, 2), "青"},
            { (CattleType.Raptor, 3), "紫"},
            { (CattleType.Raptor, 4), "黒"},
        };

        public static readonly Dictionary<CattleType, CattleType> CattleSpecies = new Dictionary<CattleType, CattleType>()
        {
            { CattleType.CowBaby, CattleType.Cow },
            { CattleType.BambuckBaby, CattleType.Bambuck },
            { CattleType.RolyPolyBaby, CattleType.RolyPoly },
            { CattleType.TurtleBaby, CattleType.Turtle },
            { CattleType.DodoBaby, CattleType.Dodo },
            { CattleType.CamelBaby, CattleType.Camel },
            { CattleType.RaptorBaby, CattleType.Raptor },
        };

        public static readonly ItemAuxData DefaultAdultCattleAuxData = new(0, new AuxPrefabManager(DefaultCattlePrefabs));

    }
}
