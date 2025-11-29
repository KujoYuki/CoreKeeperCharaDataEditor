using CKCharaDataEditor.Resource;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace CKCharaDataEditor.Model.Items
{
    public static class LootTableHelper
    {
        /// <summary>
        /// JSONファイルから戦利品テーブルを読み込む
        /// </summary>
        /// <param name="filePath">JSONファイルのパス</param>
        /// <returns>デシリアライズされた戦利品テーブル</returns>
        public static async Task<LootTable> LoadFromFileAsync(string filePath)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(filePath);
                LootTable tabel = JsonSerializer.Deserialize<LootTable>(jsonContent, StaticResource.SerializerOption)!;
                tabel.ComputeGuaranteedRoll();
                tabel.ComputeRoll();
                return tabel;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"戦利品テーブルファイルの読み込みに失敗しました: {filePath}", ex);
            }
        }

        /// <summary>
        /// 戦利品テーブルをJSON文字列に変換する
        /// </summary>
        /// <param name="lootTable">変換する戦利品テーブル</param>
        /// <returns>JSON文字列</returns>
        public static string ToJson(LootTable lootTable)
        {
            try
            {
                return JsonSerializer.Serialize(lootTable, StaticResource.SerializerOption);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("戦利品テーブルのシリアライズに失敗しました", ex);
            }
        }

        public static readonly ReadOnlyDictionary<Biome, Brush> BiomeColor = new(new Dictionary<Biome, Brush>
        {
            { Biome.Dirt, ConvertColor("7f5f30") },
            { Biome.Clay, ConvertColor("e88b69") },
            { Biome.Larva, ConvertColor("c77463") },
            { Biome.Stone, ConvertColor("678397") },
            { Biome.Nature, ConvertColor("3d9b41") },
            { Biome.Mold, ConvertColor("6cbce0") },
            { Biome.Sea, ConvertColor("34d0ff") },
            { Biome.City, ConvertColor("578084") },
            { Biome.Desert, ConvertColor("d29a7c") },
            { Biome.Temple, ConvertColor("536460") },
            { Biome.Oasis, ConvertColor("c77138") },
            { Biome.Lava, ConvertColor("554e6a") },
            { Biome.Crystal, ConvertColor("3988db") },
            { Biome.Passage, ConvertColor("c0bacf") },
        });

        public static Brush ConvertColor(string colorCode)
        {
            if (colorCode.Length != 6)
                throw new ArgumentException("Color code must be 6 characters long.");
            int r = Convert.ToInt32(colorCode[..2], 16);
            int g = Convert.ToInt32(colorCode[2..4], 16);
            int b = Convert.ToInt32(colorCode[4..], 16);
            return new SolidBrush(Color.FromArgb(r, g, b));
        }

        /// <summary>
        /// ルートテーブルファイルと描画や計算に関わる情報を定義してセットにする
        /// アプデにより、新しいテーブルが追加されたら狩値を追加するために、readonlyにはしない
        /// </summary>
        public static Dictionary<string, (Biome Biome, TableAction Action)> TableDetails = new Dictionary<string, (Biome Biome, TableAction Action)>
        {
            { "SlimeBlobs" ,(Biome.Dirt, TableAction.Enemy) },
            { "SlimeBoss" ,(Biome.Dirt, TableAction.Boss) },
            { "PoisonSlimeBlobs" ,(Biome.Nature, TableAction.Enemy) },
            { "SlipperySlimeBlobs" ,(Biome.Sea, TableAction.Enemy) },
            { "PoisonSlimeBoss" ,(Biome.Nature, TableAction.Boss) },
            { "SlipperySlimeBoss" ,(Biome.Sea, TableAction.Boss) },
            { "MushroomEnemy" ,(Biome.Dirt, TableAction.Enemy) },
            { "Larva" ,(Biome.Larva, TableAction.Enemy) },
            { "Biglarva" ,(Biome.Larva, TableAction.Enemy) },
            { "Bosslarva" ,(Biome.Larva, TableAction.Boss) },
            { "LarvaHiveDestructible" ,(Biome.Larva, TableAction.Destructible) },
            { "HiveBoss" ,(Biome.Larva, TableAction.Boss) },
            { "Cocoon" ,(Biome.Larva, TableAction.Enemy) },
            { "LargeSlimeDestructible" ,(Biome.Dirt, TableAction.Destructible) },
            { "FallRockDestructible" ,(Biome.Dirt, TableAction.Destructible) },
            { "AncientDestructible" ,(Biome.Stone, TableAction.Destructible) },
            { "LargeAncientDestructible" ,(Biome.Stone, TableAction.Destructible) },
            { "WoodenDestructible" ,(Biome.Dirt, TableAction.Destructible) },
            { "LargeWoodenDestructible" ,(Biome.Dirt, TableAction.Destructible) },
            { "ClayPot" ,(Biome.Clay, TableAction.Enemy) },
            { "DiggingSpot" ,(Biome.Dirt, TableAction.Enemy) },
            { "DiggingSpotNature" ,(Biome.Nature, TableAction.Enemy) },
            { "DiggingSpotSea" ,(Biome.Sea, TableAction.Enemy) },
            { "DiggingSpotDesert" ,(Biome.Desert, TableAction.Enemy) },
            { "DiggingSpotLava" ,(Biome.Lava, TableAction.Enemy) },
            { "Caveling" ,(Biome.Stone, TableAction.Enemy) },
            { "CavelingBrute" ,(Biome.Stone, TableAction.Enemy) },
            { "CavelingGardener" ,(Biome.Nature, TableAction.Enemy) },
            { "CavelingHunter" ,(Biome.Nature, TableAction.Enemy) },
            { "CavelingShaman" ,(Biome.Stone, TableAction.Enemy) },
            { "ShamanBoss" ,(Biome.Stone, TableAction.Boss) },
            { "CavelingSpearman" ,(Biome.Clay, TableAction.Enemy) },
            { "CavelingSkirmisher" ,(Biome.Clay, TableAction.Enemy) },
            { "BirdBoss" ,(Biome.Nature, TableAction.Boss) },
            { "DirtWall" ,(Biome.Dirt, TableAction.Wall) },
            { "StoneWall" ,(Biome.Stone, TableAction.Wall) },
            { "ClayWall" ,(Biome.Clay, TableAction.Wall) },
            { "WoodRoot" ,(Biome.Dirt, TableAction.Enemy) },
            { "ThornWoodRoot" ,(Biome.Nature, TableAction.Enemy) },
            { "CoralWoodRoot" ,(Biome.Sea, TableAction.Enemy) },
            { "GleamWoodRoot" ,(Biome.Crystal, TableAction.Enemy) },
            { "HiveDungeon" ,(Biome.Larva, TableAction.Enemy) },
            { "CopperChest" ,(Biome.Dirt, TableAction.Chest) },
            { "IronChest" ,(Biome.Stone, TableAction.Chest) },
            { "ScarletChest" ,(Biome.Nature, TableAction.Chest) },
            { "OctarineChest" ,(Biome.Sea, TableAction.Chest) },
            { "GalaxiteChest" ,(Biome.Desert, TableAction.Chest) },
            { "SolariteChest" ,(Biome.Crystal, TableAction.Chest) },
            { "AlienChest" ,(Biome.Crystal, TableAction.Chest) },
            { "PassageChest" ,(Biome.Passage, TableAction.Chest) },
            { "CicadaDungeonChest" ,(Biome.Oasis, TableAction.Chest) },
            { "OasisChest" ,(Biome.Oasis, TableAction.Chest) },
            { "WorldChest" ,(Biome.Dirt, TableAction.Chest) },  // バイオーム不明
            { "WorldChestNature" ,(Biome.Nature, TableAction.Enemy) },
            { "TreasurePedestal" ,(Biome.Dirt, TableAction.Enemy) },
            { "WoodRoomPedestal" ,(Biome.Dirt, TableAction.Enemy) },
            { "ArcheologistWallLoot" ,(Biome.None, TableAction.Enemy) },
            { "NatureDestructible" ,(Biome.Nature, TableAction.Destructible) },
            { "LargeNatureDestructable" ,(Biome.Nature, TableAction.Enemy) },
            { "WoodenNatureDestructable" ,(Biome.Nature, TableAction.Enemy) },
            { "LargeWoodenNatureDestructable" ,(Biome.Nature, TableAction.Enemy) },
            { "NatureWall" ,(Biome.Nature, TableAction.Wall) },
            { "Grass" ,(Biome.Dirt, TableAction.Destructible) }, // バイオーム不明
            { "CavelingDestructbile" ,(Biome.Clay, TableAction.Destructible) },
            { "MoldDungeonChest" ,(Biome.Mold, TableAction.Chest) },
            { "MoldDestructable" ,(Biome.Mold, TableAction.Enemy) },
            { "LargeMoldDestructable" ,(Biome.Mold, TableAction.Enemy) },
            { "InfectedCaveling" ,(Biome.Mold, TableAction.Enemy) },
            { "DirtFishes" ,(Biome.Dirt, TableAction.FishingFish) },
            { "DirtFishingLoot" ,(Biome.Dirt, TableAction.FishingLoot) },
            { "LarvaFishes" ,(Biome.Larva, TableAction.FishingFish) },
            { "LarvaFishingLoot" ,(Biome.Larva, TableAction.FishingLoot) },
            { "StoneFishes" ,(Biome.Stone, TableAction.FishingFish) },
            { "StoneFishingLoot" ,(Biome.Stone, TableAction.FishingLoot) },
            { "NatureFishes" ,(Biome.Nature, TableAction.FishingFish) },
            { "NatureFishingLoot" ,(Biome.Nature, TableAction.FishingLoot) },
            { "MoldFishes" ,(Biome.Mold, TableAction.FishingFish) },
            { "MoldFishingLoot" ,(Biome.Mold, TableAction.FishingLoot) },
            { "JellyfishDestructible" ,(Biome.Sea, TableAction.Destructible) },
            { "LargeJellyfishDestructible" ,(Biome.Sea, TableAction.Destructible) },
            { "SeaBiomeChest" ,(Biome.Sea, TableAction.Chest) },
            { "AcidLarva" ,(Biome.Larva, TableAction.Enemy) },
            { "Tentacle" ,(Biome.Sea, TableAction.Enemy) },
            { "MoldTentacle" ,(Biome.Mold, TableAction.Enemy) },
            { "RedSlimeBlob" ,(Biome.Dirt, TableAction.Enemy) },
            { "SnarePlant" ,(Biome.Nature, TableAction.Enemy) },
            { "CrabEnemy" ,(Biome.Sea, TableAction.Enemy) },
            { "CityDungeonChest" ,(Biome.City, TableAction.Chest) },
            { "CityDestructible" ,(Biome.City, TableAction.Destructible) },
            { "LargeCityDestructible" ,(Biome.City, TableAction.Destructible) },
            { "SeaFishes" ,(Biome.Sea, TableAction.FishingFish) },
            { "SeaFishingLoot" ,(Biome.Sea, TableAction.FishingLoot) },
            { "CavelingScholar" ,(Biome.City, TableAction.Enemy) },
            { "AncientGolem" ,(Biome.City, TableAction.Enemy) },
            { "OctopusBoss" ,(Biome.Sea, TableAction.Boss) },
            { "HalloweenGoodieBag" ,(Biome.None, TableAction.Enemy) },
            { "ChristmasPresent" ,(Biome.None, TableAction.Enemy) },
            { "ChristmasLuxuryPresent" ,(Biome.None, TableAction.Enemy) },
            { "ValentinePresent" ,(Biome.None, TableAction.Enemy) },
            { "RedEnvelope" ,(Biome.None, TableAction.Enemy) },
            { "BombScarab" ,(Biome.Desert, TableAction.Enemy) },
            { "CavelingAssassin" ,(Biome.Desert, TableAction.Enemy) },
            { "LavaButterfly" ,(Biome.Lava, TableAction.Enemy) },
            { "DesertDestructible" ,(Biome.Desert, TableAction.Destructible) },
            { "LargeDesertDestructible" ,(Biome.Desert, TableAction.Destructible) },
            { "GreenDesertDestructible" ,(Biome.Desert, TableAction.Destructible) },
            { "GreenLargeDesertDestructible" ,(Biome.Desert, TableAction.Destructible) },
            { "ScarabBoss" ,(Biome.Desert, TableAction.Boss) },
            { "LavaSlimeBlob" ,(Biome.Lava, TableAction.Enemy) },
            { "LavaSlimeBoss" ,(Biome.Lava, TableAction.Boss) },
            { "LavaWoodenDestructible" ,(Biome.Lava, TableAction.Destructible) },
            { "LargeLavaWoodenDestructible" ,(Biome.Lava, TableAction.Destructible) },
            { "DesertTempleDestructible" ,(Biome.Desert, TableAction.Destructible) },
            { "LargeDesertTempleDestructible" ,(Biome.Desert, TableAction.Destructible) },
            { "DesertDungeonChest" ,(Biome.Desert, TableAction.Chest) },
            { "LavaDungeonChest" ,(Biome.Lava, TableAction.Chest) },
            { "DesertFishes" ,(Biome.Desert, TableAction.FishingFish) },
            { "DesertFishingLoot" ,(Biome.Desert, TableAction.FishingLoot) },
            { "LavaFishes" ,(Biome.Lava, TableAction.FishingFish) },
            { "LavaFishingLoot" ,(Biome.Lava, TableAction.FishingLoot) },
            { "CrystalBigSnail" ,(Biome.Crystal, TableAction.Enemy) },
            { "OasisDestructible" ,(Biome.Oasis, TableAction.Destructible) },
            { "CrystalWall" ,(Biome.Crystal, TableAction.Wall) },
            { "LargeAlienTechDestructible" ,(Biome.Crystal, TableAction.Destructible) },
            { "AlienTechDestructible" ,(Biome.Crystal, TableAction.Destructible) },
            { "CrystalDestructible" ,(Biome.Crystal, TableAction.Destructible) },
            { "LargeCrystalDestructible" ,(Biome.Crystal, TableAction.Destructible) },
            { "AtlanteanWormBoss" ,(Biome.Sea, TableAction.Boss) },
            { "CrystalFishes" ,(Biome.Crystal, TableAction.FishingFish) },
            { "CrystalFishingLoot" ,(Biome.Crystal, TableAction.FishingLoot) },
            { "Mimite" ,(Biome.Crystal, TableAction.Enemy) },
            { "OrbitalTurret" ,(Biome.Crystal, TableAction.Enemy) },
            { "AlienEventTerminal" ,(Biome.Crystal, TableAction.Enemy) },
            { "Nilipede" ,(Biome.Crystal, TableAction.Enemy) },
            { "StarGrub" ,(Biome.Clay, TableAction.Enemy) },
            { "HydraBossNature" ,(Biome.Nature, TableAction.Boss) },
            { "HydraBossSea" ,(Biome.Sea, TableAction.Boss) },
            { "HydraBossDesert" ,(Biome.Desert, TableAction.Boss) },
            { "CoreCommander" ,(Biome.Desert, TableAction.Boss) },
            { "GoldenBombScarab" ,(Biome.Oasis, TableAction.Enemy) },
            { "PassageWall" ,(Biome.Passage, TableAction.Wall) },
            { "PandoriumCrystal" ,(Biome.Passage, TableAction.Enemy) },
            { "PandoriumCrystalSmall" ,(Biome.Passage, TableAction.Enemy) },
            { "AmoebaWorm" ,(Biome.Passage, TableAction.Enemy) },
            { "AmoebaGiantWorm" ,(Biome.Passage, TableAction.Enemy) },
            { "WallBoss" ,(Biome.Passage, TableAction.Boss) },
            { "PassageDestructible" ,(Biome.Passage, TableAction.Destructible) },
            { "LargePassageDestructible" ,(Biome.Passage, TableAction.Destructible) },
            { "CavelingMummy" ,(Biome.Desert, TableAction.Enemy) },
            { "ShroomanBrute" ,(Biome.Dirt, TableAction.Enemy) },
            { "PassageFishes" ,(Biome.Passage, TableAction.FishingFish) },
            { "PassageFishingLoot" ,(Biome.Passage, TableAction.FishingLoot) },
            { "NatureCicada" ,(Biome.Nature, TableAction.Enemy) },
            { "GiantCicadaBoss" ,(Biome.Oasis, TableAction.Boss) },
            { "CicadaNymph" ,(Biome.Oasis, TableAction.Enemy) },
            { "AbioticFactorDestructible" ,(Biome.Stone, TableAction.Destructible) },
            { "AbioticFactorLargeDestructible" ,(Biome.Stone, TableAction.Destructible) },
            { "AbioticFactorElectroPest" ,(Biome.Stone, TableAction.Enemy) },
        };

        /// <summary>
        /// ドロップテーブル表示名の追加辞書
        /// </summary>
        public static readonly ReadOnlyDictionary<string, string> AdditionalTableNameDic = new(new Dictionary<string, string>()
        {
            {"SlimeBlobs", "オレンジスライム" },
            {"SlimeBoss", "奇怪生命体グラーチ" },
            {"PoisonSlimeBlobs", "パープルスライム" },
            {"SlipperySlimeBlobs", "ブルースライム" },
            {"PoisonSlimeBoss", "猛毒生命体アイヴィー" },
            {"SlipperySlimeBoss", "水棲生命体モルファ" },
            {"Biglarva", "巨大幼虫" },
            {"Bosslarva", "大喰らいのゴーム" },
            {"LarvaHiveDestructible", "巣のできもの" },
            {"HiveBoss", "ハイブマザー" },
            {"LargeSlimeDestructible", "大きなスライムの器" },
            {"FallRockDestructible", "落石" },
            {"LargeWoodenDestructible", "大きな木の箱" }, // 正式には「木の箱」
            {"DiggingSpot", "発掘ポイント(土、粘土)" },
            {"DiggingSpotNature", "発掘ポイント(森)" },
            {"DiggingSpotSea", "発掘ポイント(海)" },
            {"DiggingSpotDesert", "発掘ポイント(砂漠)" },
            {"DiggingSpotLava", "発掘ポイント(溶岩)" },
            {"ShamanBoss", "頽廃のマルガズ" },
            {"BirdBoss", "天空の巨鳥アゼオス" },
            {"DirtWall", "土の壁" },
            {"StoneWall", "石の壁" },
            {"ClayWall", "粘土の壁" },
            {"WoodRoot", "木の枝" },
            {"ThornWoodRoot", "トゲの木の枝" },
            {"CoralWoodRoot", "コーラルウッドの枝" },
            {"GleamWoodRoot", "光り木の枝" },
            {"HiveDungeon", "幼虫の巣の宝箱" },
            {"CicadaDungeonChest", "ニムルザのダンジョンのチェスト" },
            {"OasisChest", "ラクラクーダ牧場のチェスト" },
            {"WorldChest", "音楽の扉(土)" },
            {"WorldChestNature", "音楽の扉(森)" },
            {"TreasurePedestal", "土のシーンの台座" },
            {"WoodRoomPedestal", "木の家の台座" },
            {"ArcheologistWallLoot", "考古学者(スキル)" },
            {"LargeNatureDestructable", "大きな花の器" },
            {"WoodenNatureDestructable", "生い茂る箱" },
            {"LargeWoodenNatureDestructable", "大きな生い茂る箱" },
            {"NatureWall", "草の壁" },
            //{"Grass", "" },
            {"CavelingDestructbile", "ケイヴリングの壺" },
            {"MoldDungeonChest", "カビだらけの宝箱" },
            {"MoldDestructable", "カビの器" },
            {"LargeMoldDestructable", "大きなカビの器" },
            {"DirtFishes", "釣り【土の洞窟・魚】" },
            {"DirtFishingLoot", "釣り【土の洞窟・アイテム】" },
            {"LarvaFishes", "釣り【酸の水・魚】" },
            {"LarvaFishingLoot", "釣り【酸の水・アイテム】" },
            {"StoneFishes", "釣り【忘却の遺跡・魚】" },
            {"StoneFishingLoot", "釣り【忘却の遺跡・アイテム】" },
            {"NatureFishes", "釣り【森・魚】" },
            {"NatureFishingLoot", "釣り【森・アイテム】" },
            {"MoldFishes", "釣り【カビ・魚】" },
            {"MoldFishingLoot", "釣り【カビ・魚】" },
            {"JellyfishDestructible", "ビーチクラゲ" },
            {"LargeJellyfishDestructible", "大きなビーチクラゲ" },
            {"SeaBiomeChest", "貝がらの宝箱" },
            {"Tentacle", "触手" },
            {"RedSlimeBlob", "レッドスライム" },
            {"CityDungeonChest", "大都市の宝箱" },
            {"SeaFishes", "釣り【海・魚】" },
            {"SeaFishingLoot", "釣り【海・アイテム】" },
            {"OctopusBoss", "海の巨獣オモロス" },
            {"ScarabBoss", "砂の巨獣ラ・アカール" },
            {"LavaSlimeBoss", "溶岩の塊イグニアス" },
            {"LargeLavaWoodenDestructible", "大きな黒こげの箱" },
            {"DesertDungeonChest", "砂漠の宝箱" },
            {"LavaDungeonChest", "くすぶりの宝箱" },
            {"DesertFishes", "釣り【砂漠・魚】" },
            {"DesertFishingLoot", "釣り【海・アイテム】" },
            {"LavaFishes", "釣り【溶岩・魚】" },
            {"LavaFishingLoot", "釣り【溶岩・アイテム】" },
            //{"OasisDestructible", "" },
            {"CrystalWall", "結晶の壁" },
            {"AtlanteanWormBoss", "アトラスワーム" },
            {"CrystalFishes", "釣り【輝く水・魚】" },
            {"CrystalFishingLoot", "釣り【輝く水・アイテム】" },
            {"AlienEventTerminal", "バトルアリーナ" },
            {"Nilipede", "ニリピード" },
            {"StarGrub", "クレイバロワー" },
            {"HydraBossNature", "森の巨竜ドルイドラ" },
            {"HydraBossSea", "氷の巨竜クライドラ" },
            {"HydraBossDesert", "炎の巨竜ファイドラ" },
            {"CoreCommander", "コアコマンダー" },
            {"PassageWall", "通路の壁" },
            {"PandoriumCrystalSmall", "小さなパンドリウムの結晶" },
            {"AmoebaWorm", "イオウムシ" },
            {"AmoebaGiantWorm", "巨大アメーバ" },
            {"WallBoss", "ウルシュライム" },
            {"ShroomanBrute", "きのこ人間ブルート" },
            {"PassageFishes", "釣り【汚れた水・魚】" },
            {"PassageFishingLoot", "釣り【汚れた水・アイテム】" },
            {"NatureCicada", "フローラルゼミ" },
            {"GiantCicadaBoss", "砂潜りの女王ニムルザ" },
            {"AbioticFactorDestructible", "オフィスの箱" },
            {"AbioticFactorLargeDestructible", "大きなオフィスの箱" },
            {"AbioticFactorElectroPest", "電子ペスト" },
        });
    }

    public enum Biome
    {
        None,
        Dirt,
        Clay,
        Larva,
        Stone,
        Nature,
        Mold,
        Sea,
        City,
        Desert,
        Temple,
        Oasis,
        Lava,
        Crystal,
        Passage,
    }
    public enum TableAction
    {
        None,
        Enemy,
        Boss,
        Chest,
        FishingFish,
        FishingLoot,
        Destructible,
        Wall,
    }
}
