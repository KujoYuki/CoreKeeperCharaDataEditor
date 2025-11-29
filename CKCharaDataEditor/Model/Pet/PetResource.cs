using CKCharaDataEditor.Model.ItemAux;

namespace CKCharaDataEditor.Model.Pet
{
    public class PetResource
    {
        private FileManager _fileManager = FileManager.Instance;

        public static readonly Dictionary<int, (PetType PetId, string objectName, PetBattleType PetBattleType, bool HasMultiColor)> PetDic = new()
        {
            { (int)PetType.PetDog, (PetType.PetDog,PetType.PetDog.ToString(), PetBattleType.Melee,true) },
            { (int)PetType.PetCat, (PetType.PetCat, PetType.PetCat.ToString(), PetBattleType.Range, true) },
            { (int)PetType.PetBird, (PetType.PetBird, PetType.PetBird.ToString(), PetBattleType.Buff, true) },
            { (int)PetType.PetSlimeBlob, (PetType.PetSlimeBlob, PetType.PetSlimeBlob.ToString(), PetBattleType.Melee, false) },
            { (int)PetType.PetBunny, (PetType.PetBunny, PetType.PetBunny.ToString(), PetBattleType.Range, true) },
            { (int)PetType.PetSlipperySlimeBlob, (PetType.PetSlipperySlimeBlob, PetType.PetSlipperySlimeBlob.ToString(), PetBattleType.Melee, false) },
            { (int)PetType.PetPoisonSlimeBlob, (PetType.PetPoisonSlimeBlob, PetType.PetPoisonSlimeBlob.ToString(), PetBattleType.Melee, false) },
            { (int)PetType.PetLavaSlimeBlob, (PetType.PetLavaSlimeBlob, PetType.PetLavaSlimeBlob.ToString(), PetBattleType.Melee, false) },
            { (int)PetType.PetPrinceSlimeBlob, (PetType.PetPrinceSlimeBlob, PetType.PetPrinceSlimeBlob.ToString(), PetBattleType.Melee, false) },
            { (int)PetType.PetMoth, (PetType.PetMoth, PetType.PetMoth.ToString(), PetBattleType.Buff, true) },
            { (int)PetType.PetTardigrade, (PetType.PetTardigrade, PetType.PetTardigrade.ToString(), PetBattleType.Melee, true) },
            { (int)PetType.PetMagic, (PetType.PetMagic, PetType.PetMagic.ToString(), PetBattleType.Buff, false) },
            { (int)PetType.PetElectric, (PetType.PetElectric, PetType.PetElectric.ToString(), PetBattleType.Melee, false) },
            { (int)PetType.PetWarlock, (PetType.PetWarlock, PetType.PetWarlock.ToString(), PetBattleType.Buff, true) },
        };

        /// <summary>
        /// TalentIdに対応するBattleTypeごとのTalent名称と効果量説明文
        /// </summary>
        public static IReadOnlyCollection<(int Id, string MeleeRangeDisplayName, string BuffDisplayName, string MeleeRangeDescription, string BuffDescription)> TalentDescriptionDict =
            new List<(int Id, string MeleeRangeDisplayName, string BuffDisplayName, string MeleeRangeDescription, string BuffDescription)>
            {
                (0, "ジャンプ攻撃", "ダイブボム", "近接攻撃スピード+30.0%", "飼い主へのバフ:近接攻撃スピード+3.0%"),
                (1, "ジャンプ攻撃", "群衆", "遠距離攻撃スピード+30.0%", "飼い主へのバフ:遠距離攻撃スピード+3.0%"),
                (2, "ポインタープレイ", "鷹の精神", "クリティカルヒット確率+10%", "飼い主へのバフ:クリティカルヒット確率+3%"),
                (3, "スキンブレイク", "白鶴拳", "クリティカルヒットダメージ+30%", "飼い主へのバフ:クリティカルヒットダメージ+10%"),
                (4, "威嚇の逆毛", "羽の力", "物理近接ダメージ+10%", "飼い主へのバフ:物理近接ダメージ+3.0%"),
                (5, "威嚇の逆毛", "羽の発射物", "物理遠距離ダメージ+10.0%", "飼い主へのバフ:物理遠距離ダメージ+3.0%"),
                (6, "マウンティング", "支配本能", "ボスへの与ダメージ+15%", "飼い主へのバフ:ボスへの与ダメージ+4%"),
                (7, "リードオフ", "三重苦", "ヒット時にダメージが3倍になる確率7%", "飼い主へのバフ:ヒット時にダメージが3倍になる確率+2%"),
                (8, "燃え盛る怒り", "フェニックス", "ヒット時に燃焼ダメージ+40", "飼い主へのバフ:ヒット時に燃焼ダメージ+20"),
                (9, "咬傷感染", "嫌な落とし物", "ヒットで敵の回復力を75%減少させる毒を与える確率+15%", "飼い主へのバフ:ヒットで敵の回復力を75%減少させる毒を与える確率+5%"),
                (10, "気絶の牙", "ネックグリップ", "近接ヒットで相手を気絶させる確率+10%", "飼い主へのバフ:近接ヒットで相手を気絶させる確率+4%"),
                (11, "ベトベトのおもちゃ", "粘液ミサイル", "相手の移動スピードを4秒間+15.0%低下させる", "飼い主へのバフ:相手の移動スピードを4秒間8.0%低下させる"),
                (12, "スピリットアニマル", "太陽の輝き", "光度+2", "飼い主へのバフ:光度+1"),
                (13, "ソウルアニマル", "月の輝き", "青色の光度+2", "飼い主へのバフ:青色の光度+1"),
                (14, "脅威の存在", "剥製術", "相手の気絶時間が+100%増加", "飼い主へのバフ:相手の気絶時間が+30%増加"),
                (15, "捕食者の標的", "気絶の一撃", "気絶している相手へのダメージ+50%", "飼い主へのバフ:気絶している相手へのダメージ+15%"),
                (16, "ズーミーズ", "バードラン", "移動スピード+30.0%", "飼い主へのバフ:移動スピード+5%"),
                (17, "ベタベロン", "コートフェザー", "ヒット時に敵がすべりやすくなる確率+15%", "飼い主へのバフ:ヒット時に敵がすべりやすくなる確率+8%"),
                (18, "空腹の牙", "インフレイト", "相手の残り体力に応じてダメージが最大+30%増加", "飼い主へのバフ:相手の残り体力に応じてダメージが最大+8%増加"),
                (19, "フレイムキャンディ", "ベローズ", "ヒット時に相手の燃焼効果を消費し即座に合計燃焼ダメージを与える確率+15%", "飼い主へのバフ:ヒット時に相手の燃焼効果を消費し即座に合計燃焼ダメージを与える確率+3%"),
                (20, "ソニッククロウ", "ソニックタロン", "+1体の敵を発射物が貫通", "飼い主へのバフ:+1体の敵を発射物が貫通"),
                (21, "Missing", "ウィングエスケープ", "移動不可および気絶時間減少+0%", "飼い主へのバフ:移動不可および気絶時間減少+10%"),
                (22, "エネルギータップ", "Missing", "ヒット時にマナを得られる確率+10%", "飼い主へのバフ:ヒット時にマナを得られる確率+1%"),
                (23, "Missing", "マナボウル", "+2.0マナ/毎秒", "飼い主へのバフ:+2.0マナ/毎秒"),
                (24, "Missing", "神秘術の野獣", "魔法ダメージ+10.0%", "飼い主へのバフ:魔法ダメージ+3.0%"),
                (25, "Missing", "怒れる群体", "ミニオン攻撃スピード+0.0%", "飼い主へのバフ:ミニオン攻撃スピード+12.0%"),
                (26, "局地的放射性降下物", "放射能汚染の匂い", "周辺の敵全員に20の放射線ダメージを2秒ごとに与える", "飼い主へのバフ:周辺の敵全員に0の放射線ダメージを2秒ごとに与える"),
                (27, "Missing", "レーザーポインター遊び", "ミニオンクリティカルヒット確率+0%", "飼い主へのバフ:ミニオンクリティカルヒット確率+3%"),
                (28, "Missing", "スキンブレイク", "ミニオンのクリティカルヒットダメージ+0%", "飼い主へのバフ:ミニオンのクリティカルヒットダメージ+30%"),
                (29, "Missing", "戦闘狂", "ミニオンダメージ+0.0%", "飼い主へのバフ:ミニオンダメージ+8.0%"),
                (30, "Missing", "穀潰しの威圧", "ボスへのミニオンの与ダメージ+0%", "飼い主へのバフ:ボスへのミニオンの与ダメージ+15%"),
                (31, "Missing", "長命", "ミニオンの寿命+0%", "飼い主へのバフ:ミニオンの寿命+15%"),
                (32, "Missing", "コロニーの採餌", "ミニオンのヒットでライフ+0", "飼い主へのバフ:ミニオンのヒットでライフ+9999"),
                (33, "Missing", "Missing", "ペットのヒットでライフ+9999", "飼い主へのバフ:ペットのヒットでライフ+0"),
            };

        public static IReadOnlyDictionary<(PetType petType, PetColor color), string> ColorDict
            = new Dictionary<(PetType petType, PetColor color), string>
        {
            { (PetType.PetDog,PetColor.Color_0) ,"茶(Default)" },
            { (PetType.PetDog,PetColor.Color_1) ,"白(Arctic)" },
            { (PetType.PetDog,PetColor.Color_2) ,"暗灰(Ash)" },
            { (PetType.PetDog,PetColor.Color_3) ,"黄(Blonde)" },
            { (PetType.PetDog,PetColor.Color_4) ,"橙(Caramel)" },
            { (PetType.PetDog,PetColor.Color_5) ,"灰(Gray)" },
            { (PetType.PetDog,PetColor.Color_6) ,"黒(Midnight)" },
            { (PetType.PetDog,PetColor.Color_7) ,"桃(Strawberry)" },
            { (PetType.PetCat,PetColor.Color_0) ,"白(Default)" },
            { (PetType.PetCat,PetColor.Color_1) ,"黄(Blonde)" },
            { (PetType.PetCat,PetColor.Color_2) ,"桃(Camel)" },
            { (PetType.PetCat,PetColor.Color_3) ,"橙(Citrus)" },
            { (PetType.PetCat,PetColor.Color_4) ,"紫(Fuschia)" },
            { (PetType.PetCat,PetColor.Color_5) ,"灰(Gray)" },
            { (PetType.PetCat,PetColor.Color_6) ,"青(Stormy)" },
            { (PetType.PetCat,PetColor.Color_7) ,"茶(Tan)" },
            { (PetType.PetBird,PetColor.Color_0) ,"緑(Default)" },
            { (PetType.PetBird,PetColor.Color_1) ,"橙(Amber)" },
            { (PetType.PetBird,PetColor.Color_2) ,"白(Ice)" },
            { (PetType.PetBird,PetColor.Color_3) ,"紫(Grape)" },
            { (PetType.PetBird,PetColor.Color_4) ,"青(Ocean)" },
            { (PetType.PetBird,PetColor.Color_5) ,"桃(Pink)" },
            { (PetType.PetBird,PetColor.Color_6) ,"灰(Stormy)" },
            { (PetType.PetBird,PetColor.Color_7) ,"黄(Sunny)" },
            { (PetType.PetBunny,PetColor.Color_0) ,"白(Dfault)" },
            { (PetType.PetBunny,PetColor.Color_1) ,"卵(Banana)" },
            { (PetType.PetBunny,PetColor.Color_2) ,"黄(Blonde)" },
            { (PetType.PetBunny,PetColor.Color_3) ,"灰(Johnnyh)" },
            { (PetType.PetBunny,PetColor.Color_4) ,"桃(Petunia)" },
            { (PetType.PetBunny,PetColor.Color_5) ,"黒(Slate)" },
            { (PetType.PetBunny,PetColor.Color_6) ,"茶(Tawny)" },
            { (PetType.PetBunny,PetColor.Color_7) ,"橙(Tiger)" },
            { (PetType.PetMoth,PetColor.Color_0) ,"白(Default)" },
            { (PetType.PetMoth,PetColor.Color_1) ,"灰(BlackSesame)" },
            { (PetType.PetMoth,PetColor.Color_2) ,"茶(Chocochip)" },
            { (PetType.PetMoth,PetColor.Color_3) ,"黄(Mango)" },
            { (PetType.PetMoth,PetColor.Color_4) ,"水色(Mint)" },
            { (PetType.PetMoth,PetColor.Color_5) ,"緑(Pistachio)" },
            { (PetType.PetMoth,PetColor.Color_6) ,"桃(Strawberry)" },
            { (PetType.PetMoth,PetColor.Color_7) ,"卵(Vanilla)" },
            { (PetType.PetTardigrade,PetColor.Color_0) ,"緑(Default)" },
            { (PetType.PetTardigrade,PetColor.Color_1) ,"青(Bobo)" },
            { (PetType.PetTardigrade,PetColor.Color_2) ,"灰(Dreamy)" },
            { (PetType.PetTardigrade,PetColor.Color_3) ,"白(Milkman)" },
            { (PetType.PetTardigrade,PetColor.Color_4) ,"赤(Pomegranate)" },
            { (PetType.PetTardigrade,PetColor.Color_5) ,"黒(Shadow)" },
            { (PetType.PetTardigrade,PetColor.Color_6) ,"橙(Tangerine)" },
            { (PetType.PetTardigrade,PetColor.Color_7) ,"桃(Bubblegum)" },
            { (PetType.PetWarlock,PetColor.Color_0) ,"灰黒(Default)" },
            { (PetType.PetWarlock,PetColor.Color_1) ,"黒(Black)" },
            { (PetType.PetWarlock,PetColor.Color_2) ,"青(Blue)" },
            { (PetType.PetWarlock,PetColor.Color_3) ,"茶(Brown)" },
            { (PetType.PetWarlock,PetColor.Color_4) ,"緑(Green)" },
            { (PetType.PetWarlock,PetColor.Color_5) ,"紫(Purple)" },
            { (PetType.PetWarlock,PetColor.Color_6) ,"赤(Red)" },
            { (PetType.PetWarlock,PetColor.Color_7) ,"黄(Yellow)" },
            { (PetType.PetSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetType.PetSlipperySlimeBlob,PetColor.Color_0) ,"----" },
            { (PetType.PetPoisonSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetType.PetLavaSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetType.PetPrinceSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetType.PetMagic,PetColor.Color_0) ,"----" },
            { (PetType.PetElectric,PetColor.Color_0) ,"----" },
        };

        public static IReadOnlyDictionary<PetType, PetBattleType> BattleType
            = new Dictionary<PetType, PetBattleType>
            {
                { PetType.PetDog, PetBattleType.Melee},
                { PetType.PetCat, PetBattleType.Range},
                { PetType.PetBird, PetBattleType.Buff},
                { PetType.PetSlimeBlob, PetBattleType.Melee},
                { PetType.PetBunny, PetBattleType.Range},
                { PetType.PetSlipperySlimeBlob, PetBattleType.Melee},
                { PetType.PetPoisonSlimeBlob, PetBattleType.Melee},
                { PetType.PetLavaSlimeBlob, PetBattleType.Melee},
                { PetType.PetPrinceSlimeBlob, PetBattleType.Melee},
                { PetType.PetMoth, PetBattleType.Buff},
                { PetType.PetTardigrade, PetBattleType.Melee},
                { PetType.PetMagic, PetBattleType.Buff},
                { PetType.PetElectric, PetBattleType.Melee},
                { PetType.PetWarlock, PetBattleType.Buff},
            };

        public static readonly PetType[] ColorSelectablePets =
        [
            PetType.PetDog,
            PetType.PetCat,
            PetType.PetBird,
            PetType.PetBunny,
            PetType.PetMoth,
            PetType.PetTardigrade,
            PetType.PetWarlock
        ];

        public static readonly List<PetTalent> DefaultTalents = Enumerable.Repeat(new PetTalent(2, 0), 9).ToList();

        public static readonly List<AuxPrefab> DefaultPetPrefabs =
            [
            new AuxPrefab(AuxHash.ItemNameGroupHash, [new AuxStableType(AuxHash.ItemNameHash, [""])]),
            new AuxPrefab(AuxHash.PetGroupHash, [
                new AuxStableType(AuxHash.PetColorHash, ["0"]),
                new AuxStableType(AuxHash.PetTalentsHash, DefaultTalents.Select(t => t.ToJsonString()))])
            ];
        public static readonly ItemAuxData DefaultPetAuxData = new(0, new AuxPrefabManager(DefaultPetPrefabs));
    }
}
