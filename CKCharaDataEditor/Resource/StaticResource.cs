using CKCharaDataEditor.Model;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CKCharaDataEditor.Resource
{
    public static class StaticResource
    {
        public static readonly JsonSerializerOptions SerializerOption = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        };

        public static IReadOnlyCollection<Item> AllCookedBaseCategories { get; } =
        [
            new(9500, "CookedSoup", "スープ"),
            new(9501, "CookedPudding", "プリン"),
            new(9502, "CookedSalad", "サラダ"),
            new(9503, "CookedWrap", "ピーマンラップ"),
            new(9504, "CookedSteak", "ステーキ"),
            new(9505, "CookedCheese", "チーズ"),
            new(9506, "CookedDipSnack", "ディップスナック"),
            new(9507, "CookedSushi", "スシ"),
            new(9508, "CookedFishBalls", "つみれ"),
            new(9509, "CookedFillet", "フィレ"),
            new(9510, "CookedCereal", "シリアル"),
            new(9511, "CookedSmoothie", "スムージー"),
            new(9512, "CookedSandwich", "サンドイッチ"),
            new(9513, "CookedPanCurry", "カレー"),
            new(9514, "CookedCake", "ケーキ"),
        ];

        public static IReadOnlyCollection<Item> AllIngredients { get; } =
        [
            // hack レア化の有無で食材を分ける（ = レシピ用食材クラスの追加,ToItemメソッドの実装）を検討する
            new(1645,"LarvaMeat","幼虫肉"),
            new(1646,"GoldenLarvaMeat","金色の幼虫肉"),
            new(5500,"Mushroom","きのこ"),
            new(5502,"GiantMushroom2","ジャンボマッシュルーム"),
            new(5503,"AmberLarva2","幼虫の琥珀"),
            new(7901,"Meat","霜降り肉"),
            new(7902,"Egg","ドードーの卵"),
            new(8003,"HeartBerry","ハートベリー"),
            new(8006,"GlowingTulipFlower","発光チューリップ"),
            new(8009,"BombPepper","爆弾ピーマン"),
            new(8012,"Carrock","キャロック"),
            new(8015,"Puffungi","カビキノコ"),
            new(8024,"BloatOat","ボウチョウ麦"),
            new(8027,"Pewpaya","ピュパイヤ"),
            new(8030,"Pinegrapple","パイグラップル"),
            new(8033,"Grumpkin","ふきげんカボチャ"),
            new(8036,"Sunrice","サンライス"),
            new(8039,"Lunacorn","ルナコーン"),
            new(8100,"HeartBerryRare","金色のハートベリー"),
            new(8101,"GlowingTulipFlowerRare","金色の発光チューリップ"),
            new(8102,"BombPepperRare","金色の爆弾ピーマン"),
            new(8103,"CarrockRare","金色のキャロック"),
            new(8104,"PuffungiRare","金色のカビキノコ"),
            new(8105,"BloatOatRare","金色のボウチョウ麦"),
            new(8106,"PewpayaRare","金色のピュパイヤ"),
            new(8107,"PinegrappleRare","金色のパイグラップル"),
            new(8108,"GrumpkinRare","金色のふきげんガボチャ"),
            new(8109,"SunriceRare","金色のサンライス"),
            new(8110,"LunacornRare","金色のルナコーン"),
            new(9618,"AtlantianWormHeart","アトラスワームの心臓"),
            new(9700,"OrangeCaveGuppy","オレンジ色の洞窟グッピー"),
            new(9701,"BlueCaveGuppy","青色の洞窟グッピー"),
            new(9702,"RockJaw","ロックジョー"),
            new(9703,"GemCrab","宝石ガニ"),
            new(9704,"DaggerFin","カミソリウオ"),
            new(9705,"PinkPalaceFish","モモイロキュウデンギョ"),
            new(9706,"TealPalaceFish","アオミドリキュウデンギョ"),
            new(9707,"CrownSquid","大王イカ"),
            new(9708,"YellowBlisterHead","イエローブリスターヘッド"),
            new(9709,"GreenBlisterHead","緑色のブリスターヘッド"),
            new(9710,"DevilWorm","デビルワーム"),
            new(9711,"VampireEel","吸血ウナギ"),
            new(9712,"MoldShark","カビザメ"),
            new(9713,"RotFish","ロットフィッシュ"),
            new(9714,"BlackSteelUrchin","クロガネウニ"),
            new(9715,"AzureFeatherFish","空色のハネウオ"),
            new(9716,"EmeraldFeatherFish","翡翠色のハネウオ"),
            new(9717,"SpiritVeil","スピリットベール"),
            new(9718,"AstralJelly","星クラゲ"),
            new(9719,"BottomTracer","ボトムトレーサー"),
            new(9720,"SilverTorrentDart","銀色のダート"),
            new(9721,"GoldenTorrentDart","金色のダート"),
            new(9722,"PinkCoralotl","ピンクサンゴウオ"),
            new(9723,"WhiteCoralotl","シロサンゴウオ"),
            new(9724,"SolidSpikeback","ハードスパイクバック"),
            new(9725,"SandySpikeback","サンドスパイクバック"),
            new(9726,"GreyDuneTail","グレイデューンテール"),
            new(9727,"BrownDuneTail","ブラウンデューンテール"),
            new(9728,"TornisKingfish","トーニスキングフィッシュ"),
            new(9729,"DarkLavaEater","ダークラヴァイーター"),
            new(9730,"BrightLavaEater","ブライトラヴァイーター"),
            new(9731,"VerdantDragonfish","グリーンドラゴンフィッシュ"),
            new(9732,"ElderDragonfish","エルダードラゴンフィッシュ"),
            new(9733,"StarlightNautilus","スターライトノーチラス"),
            new(9734,"AngleFish","緑柱石のアングルフィッシュ"),
            new(9735,"Deepstalker","玲瓏なディープストーカー"),
            new(9736,"CosmicForm","宇宙の形状"),
            new(9737,"GoldenAngleFish","碧玉のアングルフィッシュ"),
            new(9738,"GoldenDeepstalker","華麗なディープストーカー"),
            new(9739,"TerraTrilobite","陸三葉虫"),
            new(9740,"LithoTrilobite","岩三葉虫"),
            new(9741,"PinkhornPico","ピンク角のピコ"),
            new(9742,"GreenhornPico","緑角のピコ"),
            new(9743,"RiftianLampfish","亀裂のランプフィッシュ"),
        ];

        public static IReadOnlyCollection<Item> ObsoleteIngredients { get; } =
        [
            new(5501,"GiantMushroom2","ジャンボマッシュルーム(古)"),
            new(5607,"AmberLarva2","幼虫の琥珀(古)")
        ];

        public static IReadOnlyDictionary<int, (string Segment, Brush Color)> ExtendSlotName
            = new Dictionary<int, (string Segment, Brush Color)>
            {
                {51, ("Cursor", Brushes.Gray)},
                {52, ("Set1,Helm", Brushes.LawnGreen)},
                {53, ("Set1,Necklace", Brushes.LawnGreen)},
                {54, ("Set1,Breast", Brushes.LawnGreen)},
                {55, ("Set1,Pants", Brushes.LawnGreen)},
                {56, ("Set1,RingA", Brushes.LawnGreen)},
                {57, ("Set1,RingB", Brushes.LawnGreen)},
                {58, ("Set1,OffHand", Brushes.LawnGreen)},
                {59, ("Set1,Bag", Brushes.LawnGreen)},
                {60, ("Sell,Slot1", Brushes.Gray)},
                {61, ("Sell,Slot2", Brushes.Gray)},
                {62, ("Sell,Slot3", Brushes.Gray)},
                {63, ("Sell,Slot4", Brushes.Gray)},
                {64, ("Sell,Slot5", Brushes.Gray)},
                {65, ("Sell,Slot6", Brushes.Gray)},
                {66, ("TrashCan", Brushes.Gray)},
                {67, ("Dresser,Helm", Brushes.Peru)},
                {68, ("Dresser,Breast", Brushes.Peru)},
                {69, ("Dresser,Pants", Brushes.Peru)},
                {70, ("Set2,Helm", Brushes.SkyBlue)},
                {71, ("Set2,Necklace", Brushes.SkyBlue)},
                {72, ("Set2,Breast", Brushes.SkyBlue)},
                {73, ("Set2,Pants", Brushes.SkyBlue)},
                {74, ("Set2,RingA", Brushes.SkyBlue)},
                {75, ("Set2,RingB", Brushes.SkyBlue)},
                {76, ("Set2,OffHand", Brushes.SkyBlue)},
                {77, ("Set3,Helm", Brushes.LightCoral)},
                {78, ("Set3,Necklace", Brushes.LightCoral)},
                {79, ("Set3,Breast", Brushes.LightCoral)},
                {80, ("Set3,Pants", Brushes.LightCoral)},
                {81, ("Set3,RingA", Brushes.LightCoral)},
                {82, ("Set3,RingB", Brushes.LightCoral)},
                {83, ("Set3,OffHand", Brushes.LightCoral)},
                {84, ("Pet", Brushes.BurlyWood)},
                {85, ("Lantan", Brushes.BurlyWood)},
                {86, ("Upgrade", Brushes.Gray)},
                {87, ("Pouch_A", Brushes.BurlyWood)},
                {98, ("Pouch_B", Brushes.BurlyWood)},
                {109, ("Pouch_C", Brushes.BurlyWood)},
                {120, ("Pouch_D", Brushes.BurlyWood)},
            };
    }
}
