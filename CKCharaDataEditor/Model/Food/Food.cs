using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Food
{
    //hack 料理アイテムとしてのクラスにしItemを継承させない。ToItem()で変換する
    //Ingradientクラスは統合せずに残す。食材定義もIngradientで残す。
    public record Food : Item
    {
        public override int objectID
        {
            get => _baseRecipeID + (int)Rarity;
            set
            {
                // ObjectIDにRarityの補正が入っているか判定させる
                var rarity = (value, AllCookedBaseCategories) switch
                {
                    var (v, cats) when cats.Select(c => c._baseRecipeID + (int)CookRarity.Rare).Contains(v)
                        => CookRarity.Rare,
                    var (v, cats) when cats.Select(c => c._baseRecipeID + (int)CookRarity.Epic).Contains(v)
                        => CookRarity.Epic,
                    _ => CookRarity.Common
                };

                Rarity = rarity;
                _baseRecipeID = value + (int)rarity;

                //todo RootsとCanMakeRareも計算してセットする
                Rarity = AllIngredients.SingleOrDefault(c=>c.objectID == value)?.Rarity ?? CookRarity.Common;
                Roots = AllIngredients.SingleOrDefault(c => c.objectID == value)?.Roots ?? IngredientRoots.None;
            }
        }

        public override string objectName
        {
            get
            {
                string prefix = Rarity switch
                {
                    CookRarity.Rare => "Rare",
                    CookRarity.Epic => "Epic",
                    _ => string.Empty
                };
                // todo ここでobjectNameの循環参照が発生している。
                string baseRecipeName = AllCookedBaseCategories
                    .SingleOrDefault(c => c.objectID == _baseRecipeID) is Item item ? item.objectName : string.Empty;
                return $"{prefix}{baseRecipeName}";
            }
        }

        public int IngredientA
        {
            get
            {
                // 16ビット右にシフトして上位16ビットを取得
                return variation >> 16;
            }
            set
            {
                variation &= 0x0000FFFF;    // 上位16ビットをクリア
                variation |= (value << 16); // 新しい値を上位16ビットに設定
            }
        }

        public int IngredientB
        {
            get
            {
                // 下位16ビットを取得
                return variation & 0x0000FFFF;
            }
            set
            {
                // unchecked構文を使用して定数値の変換エラーを回避
                variation &= unchecked((int)0xFFFF0000);    // 下位16ビットをクリア
                variation |= value;         // 新しい値を下位16ビットに設定
            }
        }

        public IngredientRoots Roots
        {
            get;
            set;
        }

        public bool CanMakeRare 
        {
            get;
            set;
        }

        private int _baseRecipeID;
        public int BaseRecipeID
        {
            get
            {
                return _baseRecipeID;
            }
            set
            {
                if (AllCookedBaseCategories.Select(c => c.objectID).Contains(value))
                {
                    _baseRecipeID = value;
                }
            }
        }

        public CookRarity Rarity { get; set; }

        /// <summary>
        /// 翻訳辞書に依存させず、最初から持たせておく表示名。主に料理タブ内で使用する
        /// </summary>
        public string DefaultDisplayName { get; set; }

        /// <summary>
        /// variationからIdへの逆算
        /// </summary>
        /// <param name="ingredientA">材料の食材A</param>
        /// <param name="ingredientB">材料の食材B</param>
        private void UnpackVariation(out int ingredientA, out int ingredientB)
        {
            // 16ビット右にシフトして上位16ビットを取得
            ingredientA = variation >> 16;
            // 下位16ビットを取得
            ingredientB = variation & 0xFFFF;
        }

        public Food(Item original) : base(original)
        {
            // 増やしたプロパティ分の参照を計算、設定する
            objectID = original.objectID;
        }

        public Food(int objectID, string objectName, string defaultDisplayName, IngredientRoots roots = IngredientRoots.None, bool canMakeRare = false)
            : base(Default with { objectID = objectID, objectName = objectName})
        {
            DefaultDisplayName = defaultDisplayName;
            _baseRecipeID = objectID;
            Rarity = CookRarity.Common;
            Roots = roots;
            CanMakeRare = canMakeRare;   
        }

        public Food(int recipeObjectID, int ingredientA, int ingredientB, CookRarity rarity)
            : base(Default)
        {
            _baseRecipeID = recipeObjectID;
            IngredientA = ingredientA;
            IngredientB = ingredientB;
            Rarity = rarity;
            Roots = IngredientRoots.Cooked;
            // objectNameはsetterで設定されるのでここでは設定しない
        }

        public static bool IsCookedItem(int objectID)
        {
            int startID = AllCookedBaseCategories.First().objectID;
            int endID = AllCookedBaseCategories.Last().objectID + (int)CookRarity.Epic; // Epicまで含む
            return (startID <= objectID && objectID <= endID);
        }

        /// <summary>
        /// 調理済み料理ののベースカテゴリの一覧です。レア度についての情報は含まれていません。
        /// </summary>
        public static IReadOnlyList<Food> AllCookedBaseCategories { get; } =
        [
            new(9500, "CookedSoup", "スープ", IngredientRoots.Cooked),
            new(9501, "CookedPudding", "プリン", IngredientRoots.Cooked),
            new(9502, "CookedSalad", "サラダ", IngredientRoots.Cooked),
            new(9503, "CookedWrap", "ピーマンラップ", IngredientRoots.Cooked),
            new(9504, "CookedSteak", "ステーキ", IngredientRoots.Cooked),
            new(9505, "CookedCheese", "チーズ", IngredientRoots.Cooked),
            new(9506, "CookedDipSnack", "ディップスナック", IngredientRoots.Cooked),
            new(9507, "CookedSushi", "スシ", IngredientRoots.Cooked),
            new(9508, "CookedFishBalls", "つみれ", IngredientRoots.Cooked),
            new(9509, "CookedFillet", "フィレ", IngredientRoots.Cooked),
            new(9510, "CookedCereal", "シリアル", IngredientRoots.Cooked),
            new(9511, "CookedSmoothie", "スムージー", IngredientRoots.Cooked),
            new(9512, "CookedSandwich", "サンドイッチ", IngredientRoots.Cooked),
            new(9513, "CookedPanCurry", "カレー", IngredientRoots.Cooked),
            new(9514, "CookedCake", "ケーキ", IngredientRoots.Cooked),
        ];

        public static IReadOnlyList<Food> AllIngredients { get; } =
        [
            new(1645,"LarvaMeat","幼虫肉", IngredientRoots.None),
            new(1646,"GoldenLarvaMeat","金色の幼虫肉", IngredientRoots.None),
            new(5500,"Mushroom","きのこ", IngredientRoots.Harvest),
            new(5502,"GiantMushroom2","ジャンボマッシュルーム", IngredientRoots.Harvest),
            new(5503,"AmberLarva2","幼虫の琥珀", IngredientRoots.None),
            new(7901,"Meat","霜降り肉", IngredientRoots.None),
            new(7902,"Egg","ドードーの卵", IngredientRoots.None),
            new(8003,"HeartBerry","ハートベリー", IngredientRoots.Harvest),
            new(8006,"GlowingTulipFlower","発光チューリップ", IngredientRoots.Harvest),
            new(8009,"BombPepper","爆弾ピーマン", IngredientRoots.Harvest),
            new(8012,"Carrock","キャロック", IngredientRoots.Harvest),
            new(8015,"Puffungi","カビキノコ", IngredientRoots.Harvest),
            new(8024,"BloatOat","ボウチョウ麦", IngredientRoots.Harvest),
            new(8027,"Pewpaya","ピュパイヤ", IngredientRoots.Harvest),
            new(8030,"Pinegrapple","パイグラップル", IngredientRoots.Harvest),
            new(8033,"Grumpkin","ふきげんカボチャ", IngredientRoots.Harvest),
            new(8036,"Sunrice","サンライス", IngredientRoots.Harvest),
            new(8039,"Lunacorn","ルナコーン", IngredientRoots.Harvest),
            new(8100,"HeartBerryRare","金色のハートベリー", IngredientRoots.Harvest, true),
            new(8101,"GlowingTulipFlowerRare","金色の発光チューリップ", IngredientRoots.Harvest, true),
            new(8102,"BombPepperRare","金色の爆弾ピーマン", IngredientRoots.Harvest, true),
            new(8103,"CarrockRare","金色のキャロック", IngredientRoots.Harvest, true),
            new(8104,"PuffungiRare","金色のカビキノコ", IngredientRoots.Harvest, true),
            new(8105,"BloatOatRare","金色のボウチョウ麦", IngredientRoots.Harvest, true),
            new(8106,"PewpayaRare","金色のピュパイヤ", IngredientRoots.Harvest, true),
            new(8107,"PinegrappleRare","金色のパイグラップル", IngredientRoots.Harvest, true),
            new(8108,"GrumpkinRare","金色のふきげんガボチャ", IngredientRoots.Harvest, true),
            new(8109, "SunriceRare", "金色のサンライス", IngredientRoots.Harvest, true),
            new(8110, "LunacornRare", "金色のルナコーン", IngredientRoots.Harvest, true),
            new(9618,"AtlantianWormHeart","アトラスワームの心臓", IngredientRoots.Fish),
            new(9700,"OrangeCaveGuppy","オレンジ色の洞窟グッピー", IngredientRoots.Fish),
            new(9701,"BlueCaveGuppy","青色の洞窟グッピー", IngredientRoots.Fish),
            new(9702,"RockJaw","ロックジョー", IngredientRoots.Fish),
            new(9703,"GemCrab","宝石ガニ", IngredientRoots.Fish),
            new(9704,"DaggerFin","カミソリウオ", IngredientRoots.Fish),
            new(9705,"PinkPalaceFish","モモイロキュウデンギョ", IngredientRoots.Fish),
            new(9706,"TealPalaceFish","アオミドリキュウデンギョ", IngredientRoots.Fish),
            new(9707,"CrownSquid","大王イカ", IngredientRoots.Fish),
            new(9708,"YellowBlisterHead","イエローブリスターヘッド", IngredientRoots.Fish),
            new(9709,"GreenBlisterHead","緑色のブリスターヘッド", IngredientRoots.Fish),
            new(9710,"DevilWorm","デビルワーム", IngredientRoots.Fish),
            new(9711,"VampireEel","吸血ウナギ", IngredientRoots.Fish),
            new(9712,"MoldShark","カビザメ", IngredientRoots.Fish),
            new(9713,"RotFish","ロットフィッシュ", IngredientRoots.Fish),
            new(9714,"BlackSteelUrchin","クロガネウニ", IngredientRoots.Fish),
            new(9715,"AzureFeatherFish","空色のハネウオ", IngredientRoots.Fish),
            new(9716,"EmeraldFeatherFish","翡翠色のハネウオ", IngredientRoots.Fish),
            new(9717,"SpiritVeil","スピリットベール", IngredientRoots.Fish),
            new(9718,"AstralJelly","星クラゲ", IngredientRoots.Fish),
            new(9719,"BottomTracer","ボトムトレーサー", IngredientRoots.Fish),
            new(9720,"SilverTorrentDart","銀色のダート", IngredientRoots.Fish),
            new(9721,"GoldenTorrentDart","金色のダート", IngredientRoots.Fish),
            new(9722,"PinkCoralotl","ピンクサンゴウオ", IngredientRoots.Fish),
            new(9723,"WhiteCoralotl","シロサンゴウオ", IngredientRoots.Fish),
            new(9724,"SolidSpikeback","ハードスパイクバック", IngredientRoots.Fish),
            new(9725,"SandySpikeback","サンドスパイクバック", IngredientRoots.Fish),
            new(9726,"GreyDuneTail","グレイデューンテール", IngredientRoots.Fish),
            new(9727,"BrownDuneTail","ブラウンデューンテール", IngredientRoots.Fish),
            new(9728,"TornisKingfish","トーニスキングフィッシュ", IngredientRoots.Fish),
            new(9729,"DarkLavaEater","ダークラヴァイーター", IngredientRoots.Fish),
            new(9730,"BrightLavaEater","ブライトラヴァイーター", IngredientRoots.Fish),
            new(9731,"VerdantDragonfish","グリーンドラゴンフィッシュ", IngredientRoots.Fish),
            new(9732,"ElderDragonfish","エルダードラゴンフィッシュ", IngredientRoots.Fish),
            new(9733,"StarlightNautilus","スターライトノーチラス", IngredientRoots.Fish, true),
            new(9734,"AngleFish","緑柱石のアングルフィッシュ", IngredientRoots.Fish),
            new(9735,"Deepstalker","玲瓏なディープストーカー", IngredientRoots.Fish),
            new(9736,"CosmicForm","宇宙の形状", IngredientRoots.Fish),
            new(9737,"GoldenAngleFish","碧玉のアングルフィッシュ", IngredientRoots.Fish),
            new(9738,"GoldenDeepstalker","華麗なディープストーカー", IngredientRoots.Fish),
            new(9739,"TerraTrilobite","陸三葉虫", IngredientRoots.Fish),
            new(9740,"LithoTrilobite","岩三葉虫", IngredientRoots.Fish),
            new(9741,"PinkhornPico","ピンク角のピコ", IngredientRoots.Fish),
            new(9742,"GreenhornPico","緑角のピコ", IngredientRoots.Fish),
            new(9743,"RiftianLampfish","亀裂のランプフィッシュ", IngredientRoots.Fish),
        ];

        public static IReadOnlyCollection<Food> ObsoleteIngredients =>
        [
            new(5501,"GiantMushroom2","ジャンボマッシュルーム(古)", IngredientRoots.Harvest & IngredientRoots.Deprecated),
            new(5607,"AmberLarva2","幼虫の琥珀(古)", IngredientRoots.None & IngredientRoots.Deprecated),
        ];

        // todo 翻訳ファイルが無くても、日本語表示リソースは持っておきたい =>displayNameを持つべきか検討する
    }
}
