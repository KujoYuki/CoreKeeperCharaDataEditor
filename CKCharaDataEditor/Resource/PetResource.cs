using CKCharaDataEditor.Model.Pet;

namespace CKCharaDataEditor.Resource
{
    public class PetResource
    {
        /// <summary>
        /// TalentIdに対応するBattleTypeごとバフの有無
        /// 参考：https://core-keeper.fandom.com/wiki/Pet_IDs
        /// </summary>
        //public static IReadOnlyCollection<(int TalentId, bool Melee, bool Range, bool Buff)> TalentIdsByBattleType = new List<(int TalentId, bool Melee, bool Range, bool Buff)>
        //{
        //    {(0, true, false, true)},
        //    {(1, false,true,true)},
        //    {(2, true,true,true)},
        //    {(3, true,true,true)},
        //    {(4, true,false,true)},
        //    {(5, false,true,true)},
        //    {(6, true,true,true)},
        //    {(7, true,true,true)},
        //    {(8, true,true,true)},
        //    {(9, true,true,true)},
        //    {(10, true,true,true)},
        //    {(11, true,true,true)},
        //    {(12, true,true,true)},
        //    {(13, false,false,true)},
        //    {(14, true,true,true)},
        //    {(15, true,true,true)},
        //    {(16, true,true,true)},
        //    {(17, true,true,true)},
        //    {(18, true,true,true)},
        //    {(19, true,true,true)},
        //    {(20, false,true,true)},
        //    {(21, false,false,true)},
        //    {(22, true,false,false)},
        //    {(23, false,false,true)},
        //    {(24, false,false,true)},
        //    {(25, true,false,false)},
        //};

        /// <summary>
        /// TalentIdに対応するBattleTypeごとのTalent名称と効果量説明文
        /// </summary>
        public static IReadOnlyCollection<(int Id, string MeleeRangeDisplayName, string BuffDisplayName, string MeleeRangeDescription, string BuffDescription)> TalentDescriptionDict = 
            new List<(int Id, string MeleeRangeDisplayName, string BuffDisplayName, string MeleeRangeDescription, string BuffDescription)>
            {
                (0, "ジャンプ攻撃", "ダイブボム", "近接攻撃スピード+30.0%", "飼い主へのバフ:近接攻撃スピード+3.0%"),
                (1, "Missing", "群衆", "遠距離攻撃スピード+30.0%", "飼い主へのバフ:遠距離攻撃スピード+3.0%"),
                (2, "ポインタープレイ", "鷹の精神", "クリティカルヒット確率+10%", "飼い主へのバフ:クリティカルヒット確率+3%"),
                (3, "スキンブレイク", "白鶴拳", "クリティカルヒットダメージ+30%", "飼い主へのバフ:クリティカルヒットダメージ+10%"),
                (4, "威嚇の逆毛", "羽の力", "物理近接ダメージ+10%", "飼い主へのバフ:物理近接ダメージ+3.0%"),
                (5, "Missing", "羽の発射物", "物理遠距離ダメージ+10.0%", "飼い主へのバフ:物理遠距離ダメージ+3.0%"),
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
                (22, "エネルギータップ", "Missing", "ヒット時にマナを得られる確率+10%", "飼い主へのバフ:ヒット時にマナを得られる確率"),
                (23, "Missing", "マナボウル", "+2.0マナ/毎秒", "飼い主へのバフ:+2.0マナ/毎秒"),
                (24, "Missing", "神秘術の野獣", "魔法ダメージ+10.0%", "飼い主へのバフ:魔法ダメージ+3.0%"),
                // (25, "Don't use", "Don't use", "Explosion", "Explosion"), // 爆発関連？UIが異常停止するのでスキップする
                (26, "局地的放射性降下物", "放射能汚染の匂い", "周辺の敵全員に20の放射線ダメージを2秒ごとに与える", "飼い主へのバフ:周辺の敵全員に0の放射線ダメージを2秒ごとに与える"),
            };

        public static IReadOnlyDictionary<(PetId petType, PetColor color), string> ColorDict
            = new Dictionary<(PetId petType, PetColor color), string>
        {
            { (PetId.PetDog,PetColor.Color_0) ,"茶(Default)" },
            { (PetId.PetDog,PetColor.Color_1) ,"白(Arctic)" },
            { (PetId.PetDog,PetColor.Color_2) ,"暗灰(Ash)" },
            { (PetId.PetDog,PetColor.Color_3) ,"黄(Blonde)" },
            { (PetId.PetDog,PetColor.Color_4) ,"橙(Caramel)" },
            { (PetId.PetDog,PetColor.Color_5) ,"灰(Gray)" },
            { (PetId.PetDog,PetColor.Color_6) ,"黒(Midnight)" },
            { (PetId.PetDog,PetColor.Color_7) ,"桃(Strawberry)" },
            { (PetId.PetCat,PetColor.Color_0) ,"白(Default)" },
            { (PetId.PetCat,PetColor.Color_1) ,"黄(Blonde)" },
            { (PetId.PetCat,PetColor.Color_2) ,"桃(Camel)" },
            { (PetId.PetCat,PetColor.Color_3) ,"橙(Citrus)" },
            { (PetId.PetCat,PetColor.Color_4) ,"紫(Fuschia)" },
            { (PetId.PetCat,PetColor.Color_5) ,"灰(Gray)" },
            { (PetId.PetCat,PetColor.Color_6) ,"青(Stormy)" },
            { (PetId.PetCat,PetColor.Color_7) ,"茶(Tan)" },
            { (PetId.PetBird,PetColor.Color_0) ,"緑(Default)" },
            { (PetId.PetBird,PetColor.Color_1) ,"橙(Amber)" },
            { (PetId.PetBird,PetColor.Color_2) ,"白(Ice)" },
            { (PetId.PetBird,PetColor.Color_3) ,"紫(Grape)" },
            { (PetId.PetBird,PetColor.Color_4) ,"青(Ocean)" },
            { (PetId.PetBird,PetColor.Color_5) ,"桃(Pink)" },
            { (PetId.PetBird,PetColor.Color_6) ,"灰(Stormy)" },
            { (PetId.PetBird,PetColor.Color_7) ,"黄(Sunny)" },
            { (PetId.PetBunny,PetColor.Color_0) ,"白(Dfault)" },
            { (PetId.PetBunny,PetColor.Color_1) ,"卵(Banana)" },
            { (PetId.PetBunny,PetColor.Color_2) ,"黄(Blonde)" },
            { (PetId.PetBunny,PetColor.Color_3) ,"灰(Johnnyh)" },
            { (PetId.PetBunny,PetColor.Color_4) ,"桃(Petunia)" },
            { (PetId.PetBunny,PetColor.Color_5) ,"黒(Slate)" },
            { (PetId.PetBunny,PetColor.Color_6) ,"茶(Tawny)" },
            { (PetId.PetBunny,PetColor.Color_7) ,"橙(Tiger)" },
            { (PetId.PetMoth,PetColor.Color_0) ,"白(Default)" },
            { (PetId.PetMoth,PetColor.Color_1) ,"灰(BlackSesame)" },
            { (PetId.PetMoth,PetColor.Color_2) ,"茶(Chocochip)" },
            { (PetId.PetMoth,PetColor.Color_3) ,"黄(Mango)" },
            { (PetId.PetMoth,PetColor.Color_4) ,"水色(Mint)" },
            { (PetId.PetMoth,PetColor.Color_5) ,"緑(Pistachio)" },
            { (PetId.PetMoth,PetColor.Color_6) ,"桃(Strawberry)" },
            { (PetId.PetMoth,PetColor.Color_7) ,"卵(Vanilla)" },
            { (PetId.PetTardigrade,PetColor.Color_0) ,"緑(Default)" },
            { (PetId.PetTardigrade,PetColor.Color_1) ,"青(Bobo)" },
            { (PetId.PetTardigrade,PetColor.Color_2) ,"灰(Dreamy)" },
            { (PetId.PetTardigrade,PetColor.Color_3) ,"白(Milkman)" },
            { (PetId.PetTardigrade,PetColor.Color_4) ,"赤(Pomegranate)" },
            { (PetId.PetTardigrade,PetColor.Color_5) ,"黒(Shadow)" },
            { (PetId.PetTardigrade,PetColor.Color_6) ,"橙(Tangerine)" },
            { (PetId.PetTardigrade,PetColor.Color_7) ,"桃(Bubblegum)" },
            { (PetId.PetSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetId.PetSlipperySlimeBlob,PetColor.Color_0) ,"----" },
            { (PetId.PetPoisonSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetId.PetLavaSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetId.PetPrinceSlimeBlob,PetColor.Color_0) ,"----" },
            { (PetId.PetMagic,PetColor.Color_0) ,"----" },
            { (PetId.PetElectric,PetColor.Color_0) ,"----" },
        };

        public static IReadOnlyDictionary<PetId, PetBattleType> BattleType
            = new Dictionary<PetId, PetBattleType>
            {
                { PetId.PetDog, PetBattleType.Melee},
                { PetId.PetCat, PetBattleType.Range},
                { PetId.PetBird, PetBattleType.Buff},
                { PetId.PetSlimeBlob, PetBattleType.Melee},
                { PetId.PetBunny, PetBattleType.Range},
                { PetId.PetSlipperySlimeBlob, PetBattleType.Melee},
                { PetId.PetPoisonSlimeBlob, PetBattleType.Melee},
                { PetId.PetLavaSlimeBlob, PetBattleType.Melee},
                { PetId.PetPrinceSlimeBlob, PetBattleType.Melee},
                { PetId.PetMoth, PetBattleType.Buff},
                { PetId.PetTardigrade, PetBattleType.Melee},
                { PetId.PetMagic, PetBattleType.Buff},
                { PetId.PetElectric, PetBattleType.Melee},
            };
    }
}
