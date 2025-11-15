using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Food
{
    public record Recipe : DiscoveredObjects
    {
        private int _baseRecipeObjectID = default;
        private string _baseObjectName = string.Empty;

        public override int objectID
        {
            get => _baseRecipeObjectID + (int)Rarity;
            set
            {
                // ObjectIDにRarityの補正が入っているか判定させる
                var rarity = (value, AllCookedBaseCategories) switch
                {
                    var (v, cats) when cats.Select(c => c.Value._baseRecipeObjectID + (int)CookRarity.Rare).Contains(v)
                        => CookRarity.Rare,
                    var (v, cats) when cats.Select(c => c.Value._baseRecipeObjectID + (int)CookRarity.Epic).Contains(v)
                        => CookRarity.Epic,
                    _ => CookRarity.Common
                };

                Rarity = rarity;
                _baseRecipeObjectID = value - (int)rarity;
            }
        }

        public string objectName
        {
            get
            {
                string prefix = Rarity switch
                {
                    CookRarity.Rare => "Rare",
                    CookRarity.Epic => "Epic",
                    _ => string.Empty
                };
                string baseRecipeName = AllCookedBaseCategories
                    .SingleOrDefault(r => r.Value._baseRecipeObjectID == _baseRecipeObjectID)!.Value._baseObjectName;
                return $"{prefix}{baseRecipeName}";
            }
        }

        public int PrimaryIngredient
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

        public int SecondaryIngredient
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

        public int BaseRecipeID
        {
            get
            {
                return _baseRecipeObjectID;
            }
            set
            {
                if (AllCookedBaseCategories.Select(c => c.Value.objectID).Contains(value))
                {
                    _baseRecipeObjectID = value;
                }
            }
        }

        public CookRarity Rarity { get; set; }

        /// <summary>
        /// 翻訳辞書に依存させず、最初から持たせておく表示名。hack 主に料理タブ内で使用する
        /// </summary>
        public string DefaultDisplayName { get; set; } = string.Empty;

        private static string CreateDisplayName(int objectID, CookRarity rarity)
        {
            string rarityPrefix = rarity switch
            {
                CookRarity.Rare => "レア",
                CookRarity.Epic => "エピック",
                _ => string.Empty,
            };

            int baseRecipeObjectID = objectID - (int)rarity;
            string baseDisplayName = AllCookedBaseCategories
                .SingleOrDefault(r => r.Value._baseRecipeObjectID == baseRecipeObjectID).Value?.DefaultDisplayName ?? string.Empty;
            return rarityPrefix + baseDisplayName;
        }

        public Recipe(Item original) : base(original)
        {
            objectID = original.objectID;
            _baseObjectName = original.objectName;
            DefaultDisplayName = CreateDisplayName(objectID, Rarity);
        }

        public Recipe(int objectID, string objectName, string defaultDisplayName)
            : base(Default with { objectID = objectID })
        {
            DefaultDisplayName = defaultDisplayName;
            _baseRecipeObjectID = objectID;
            _baseObjectName = objectName;
            Rarity = CookRarity.Common;
        }

        public Recipe(int recipeObjectID, int ingredientA, int ingredientB, CookRarity rarity)
            : base(Default)
        {
            DefaultDisplayName = AllCookedBaseCategories
                .SingleOrDefault(r => r.Value._baseRecipeObjectID == recipeObjectID).Value?.DefaultDisplayName ?? string.Empty;
            _baseRecipeObjectID = recipeObjectID;
            PrimaryIngredient = GetPrimaryIngredient(ingredientA, ingredientB);
            SecondaryIngredient = GetSecondaryIngredient(ingredientA, ingredientB);
            Rarity = rarity;
            // objectNameはsetterで設定されるのでここでは設定しない
        }

        public Item ToItem(int amount)
        {
            // RecipeはItemを継承しないので、Itemに変換する
            return new Item(objectID, amount, variation, 0, objectName, ItemAuxData.Default);
        }

        public static bool IsCookedItem(int objectID)
        {
            int startID = AllCookedBaseCategories.First().Value.objectID;
            int endID = AllCookedBaseCategories.Last().Value.objectID + (int)CookRarity.Epic; // Epicまで含む
            return (startID <= objectID && objectID <= endID);
        }

        /// <summary>
        /// 調理済み料理のベースカテゴリの一覧です。レア度についての情報は含まれていません。
        /// </summary>
        //public static IReadOnlyList<Recipe> AllCookedBaseCategories { get; } =
        public static IReadOnlyDictionary<CookedFood, Recipe> AllCookedBaseCategories = new Dictionary<CookedFood, Recipe>
        {
            { CookedFood.Soup, new(9500, "CookedSoup", "スープ") },
            { CookedFood.Pudding, new(9501, "CookedPudding", "プリン") },
            { CookedFood.Salad, new(9502, "CookedSalad", "サラダ")  },
            { CookedFood.Wrap, new(9503, "CookedWrap", "ピーマンラップ") },
            { CookedFood.Steak, new(9504, "CookedSteak", "ステーキ") },
            { CookedFood.Cheese, new(9505, "CookedCheese", "チーズ") },
            { CookedFood.DipSnack, new(9506, "CookedDipSnack", "ディップスナック") },
            { CookedFood.Sushi, new(9507, "CookedSushi", "スシ") },
            { CookedFood.FishBalls, new(9508, "CookedFishBalls", "つみれ") },
            { CookedFood.Fillet, new(9509, "CookedFillet", "フィレ") },
            { CookedFood.Cereal, new(9510, "CookedCereal", "シリアル") },
            { CookedFood.Smoothie, new(9511, "CookedSmoothie", "スムージー") },
            { CookedFood.Sandwich, new(9512, "CookedSandwich", "サンドイッチ") },
            { CookedFood.PanCurry, new(9513, "CookedPanCurry", "カレー") },
            { CookedFood.Cake, new(9514, "CookedCake", "ケーキ") },
        };

        /// <summary>
        /// 食材の定義一覧です。組み合わせた際にレア度が上昇する場合はCanMakeRareがtrueになります。
        /// </summary>
        public static IReadOnlyCollection<Ingredient> AllIngredients { get; } =
        [
            new(1645,"LarvaMeat","幼虫肉", IngredientRoots.None, CookedFood.Steak),
            new(1646,"GoldenLarvaMeat","金色の幼虫肉", IngredientRoots.None, CookedFood.Steak),
            new(5500,"Mushroom","きのこ", IngredientRoots.Harvest, CookedFood.Soup),
            new(5502,"GiantMushroom2","ジャンボマッシュルーム", IngredientRoots.Harvest, CookedFood.Salad),
            new(5503,"AmberLarva2","幼虫の琥珀", IngredientRoots.None, CookedFood.Pudding),
            new(7901,"Meat","霜降り肉", IngredientRoots.None, CookedFood.Steak),
            new(7902,"Egg","ドードーの卵", IngredientRoots.None, CookedFood.Cake),
            new(8003,"HeartBerry","ハートベリー", IngredientRoots.Harvest, CookedFood.Pudding),
            new(8006,"GlowingTulipFlower","発光チューリップ", IngredientRoots.Harvest, CookedFood.Salad),
            new(8009,"BombPepper","爆弾ピーマン", IngredientRoots.Harvest, CookedFood.Wrap),
            new(8012,"Carrock","キャロック", IngredientRoots.Harvest, CookedFood.DipSnack),
            new(8015,"Puffungi","カビキノコ", IngredientRoots.Harvest, CookedFood.Cheese),
            new(8024,"BloatOat","ボウチョウ麦", IngredientRoots.Harvest, CookedFood.Cereal),
            new(8027,"Pewpaya","ピュパイヤ", IngredientRoots.Harvest, CookedFood.Smoothie),
            new(8030,"Pinegrapple","パイグラップル", IngredientRoots.Harvest, CookedFood.Smoothie),
            new(8033,"Grumpkin","ふきげんカボチャ", IngredientRoots.Harvest, CookedFood.Soup),
            new(8036,"Sunrice","サンライス", IngredientRoots.Harvest, CookedFood.PanCurry),
            new(8039,"Lunacorn","ルナコーン", IngredientRoots.Harvest, CookedFood.Sandwich),
            new(8100,"HeartBerryRare","金色のハートベリー", IngredientRoots.Harvest, CookedFood.Pudding),
            new(8101,"GlowingTulipFlowerRare","金色の発光チューリップ", IngredientRoots.Harvest, CookedFood.Sandwich),
            new(8102,"BombPepperRare","金色の爆弾ピーマン", IngredientRoots.Harvest, CookedFood.Wrap),
            new(8103,"CarrockRare","金色のキャロック", IngredientRoots.Harvest, CookedFood.DipSnack),
            new(8104,"PuffungiRare","金色のカビキノコ", IngredientRoots.Harvest, CookedFood.Cheese),
            new(8105,"BloatOatRare","金色のボウチョウ麦", IngredientRoots.Harvest, CookedFood.Cereal),
            new(8106,"PewpayaRare","金色のピュパイヤ", IngredientRoots.Harvest, CookedFood.Smoothie),
            new(8107,"PinegrappleRare","金色のパイグラップル", IngredientRoots.Harvest, CookedFood.Smoothie),
            new(8108,"GrumpkinRare","金色のふきげんガボチャ", IngredientRoots.Harvest, CookedFood.Soup),
            new(8109, "SunriceRare", "金色のサンライス", IngredientRoots.Harvest, CookedFood.PanCurry),
            new(8110, "LunacornRare", "金色のルナコーン", IngredientRoots.Harvest, CookedFood.Sandwich),
            new(9618,"AtlantianWormHeart","アトラスワームの心臓", IngredientRoots.Fish, CookedFood.Steak),
            new(9700,"OrangeCaveGuppy","オレンジ色の洞窟グッピー", IngredientRoots.Fish, CookedFood.Fillet),
            new(9701,"BlueCaveGuppy","青色の洞窟グッピー", IngredientRoots.Fish, CookedFood.Fillet),
            new(9702,"RockJaw","ロックジョー", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9703,"GemCrab","宝石ガニ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9704,"DaggerFin","カミソリウオ", IngredientRoots.Fish, CookedFood.Fillet),
            new(9705,"PinkPalaceFish","モモイロキュウデンギョ", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9706,"TealPalaceFish","アオミドリキュウデンギョ", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9707,"CrownSquid","大王イカ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9708,"YellowBlisterHead","イエローブリスターヘッド", IngredientRoots.Fish, CookedFood.Fillet),
            new(9709,"GreenBlisterHead","緑色のブリスターヘッド", IngredientRoots.Fish, CookedFood.Fillet),
            new(9710,"DevilWorm","デビルワーム", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9711,"VampireEel","吸血ウナギ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9712,"MoldShark","カビザメ", IngredientRoots.Fish, CookedFood.Fillet),
            new(9713,"RotFish","ロットフィッシュ", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9714,"BlackSteelUrchin","クロガネウニ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9715,"AzureFeatherFish","空色のハネウオ", IngredientRoots.Fish, CookedFood.Fillet),
            new(9716,"EmeraldFeatherFish","翡翠色のハネウオ", IngredientRoots.Fish, CookedFood.Fillet),
            new(9717,"SpiritVeil","スピリットベール", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9718,"AstralJelly","星クラゲ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9719,"BottomTracer","ボトムトレーサー", IngredientRoots.Fish, CookedFood.Fillet),
            new(9720,"SilverTorrentDart","銀色のダート", IngredientRoots.Fish, CookedFood.Sushi),
            new(9721,"GoldenTorrentDart","金色のダート", IngredientRoots.Fish, CookedFood.Sushi),
            new(9722,"PinkCoralotl","ピンクサンゴウオ", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9723,"WhiteCoralotl","シロサンゴウオ", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9724,"SolidSpikeback","ハードスパイクバック", IngredientRoots.Fish, CookedFood.Fillet),
            new(9725,"SandySpikeback","サンドスパイクバック", IngredientRoots.Fish, CookedFood.Fillet),
            new(9726,"GreyDuneTail","グレイデューンテール", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9727,"BrownDuneTail","ブラウンデューンテール", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9728,"TornisKingfish","トーニスキングフィッシュ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9729,"DarkLavaEater","ダークラヴァイーター", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9730,"BrightLavaEater","ブライトラヴァイーター", IngredientRoots.Fish, CookedFood.FishBalls),
            new(9731,"VerdantDragonfish","グリーンドラゴンフィッシュ", IngredientRoots.Fish, CookedFood.Fillet),
            new(9732,"ElderDragonfish","エルダードラゴンフィッシュ", IngredientRoots.Fish, CookedFood.Fillet),
            new(9733,"StarlightNautilus","スターライトノーチラス", IngredientRoots.Fish, CookedFood.Sushi),
            new(9734,"AngleFish","緑柱石のアングルフィッシュ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9735,"Deepstalker","玲瓏なディープストーカー", IngredientRoots.Fish, CookedFood.Fillet),
            new(9736,"CosmicForm","宇宙の形状", IngredientRoots.Fish, CookedFood.Sushi),
            new(9737,"GoldenAngleFish","碧玉のアングルフィッシュ", IngredientRoots.Fish, CookedFood.Sushi),
            new(9738,"GoldenDeepstalker","華麗なディープストーカー", IngredientRoots.Fish, CookedFood.Fillet),
            new(9739,"TerraTrilobite","陸三葉虫", IngredientRoots.Fish, CookedFood.Sushi),
            new(9740,"LithoTrilobite","岩三葉虫", IngredientRoots.Fish, CookedFood.Fillet),
            new(9741,"PinkhornPico","ピンク角のピコ", IngredientRoots.Fish, CookedFood.Pudding),
            new(9742,"GreenhornPico","緑角のピコ", IngredientRoots.Fish, CookedFood.Pudding),
            new(9743,"RiftianLampfish","亀裂のランプフィッシュ", IngredientRoots.Fish, CookedFood.FishBalls),
        ];

        public static IReadOnlyCollection<Ingredient> ObsoleteIngredients =>
        [
            new(5501,"GiantMushroom2","ジャンボマッシュルーム(古)", IngredientRoots.Harvest & IngredientRoots.Deprecated, CookedFood.Salad),
            new(5607,"AmberLarva2","幼虫の琥珀(古)", IngredientRoots.None & IngredientRoots.Deprecated, CookedFood.Pudding),
        ];

        /// <summary>
        /// variationからIdへの逆算
        /// </summary>
        /// <param name="ingredientA">材料の食材A</param>
        /// <param name="ingredientB">材料の食材B</param>
        public static void UnpackVariation(int variation, out int ingredientA, out int ingredientB)
        {
            // 16ビット右にシフトして上位16ビットを取得
            ingredientA = variation >> 16;
            // 下位16ビットを取得
            ingredientB = variation & 0xFFFF;
        }

        /// <summary>
        /// 決まった食材Idから合成後の料理のvariation値を計算する。
        /// </summary>
        /// <param name="ingredientA">1つめの食材のId(dec)</param>
        /// <param name="ingredientB">2つめの食材のId(dec)</param>
        /// <returns></returns>
        public static int CalculateVariation(int ingredientA, int ingredientB)
        {
            // hack ゲーム内ロジックに合わせてプライマリ食材を先頭に持ってくる
            int primary = GetPrimaryIngredient(ingredientA, ingredientB);
            if (primary == ingredientB)
            {
                (ingredientA, ingredientB) = (ingredientB, ingredientA);
            }
            
            // 各IDを16ビットシフトして結合
            int combined = (ingredientA << 16) | ingredientB;
            return combined;
        }

        #region プライマリ食材判定ロジック
        static readonly List<int> GoldenPlant = Enumerable.Range(8100, 11).ToList();
        const int StarlightNautilus = 9733;

        public static bool IngredientShouldBePrimary(int ingredient)
        {
            return GoldenPlant.Contains(ingredient) || ingredient == StarlightNautilus;
        }

        public static int GetPrimaryIngredient(int ingredient1, int ingredient2)
        {
            // どちらが「プライマリ候補」かを明示的に判定して可読性を上げる
            bool firstPreferred = IngredientShouldBePrimary(ingredient1);
            bool secondPreferred = IngredientShouldBePrimary(ingredient2);

            // 片方のみ が優先で他方が優先でないなら 優先の食べ物をプライマリで返す
            if (firstPreferred && !secondPreferred)
            {
                return ingredient1;
            }
            else if (!firstPreferred && secondPreferred)
            {
                return ingredient2;
            }
            else
            {
                // 両方とも優先、または両方とも非優先の場合は決定論的な比較で決める
                return FirstIngredientIsPrimary(ingredient1, ingredient2) ? ingredient1 : ingredient2;
            }
        }

        public static int GetSecondaryIngredient(int ingredient1, int ingredient2)
        {
            int primary = GetPrimaryIngredient(ingredient1, ingredient2);
            return (primary == ingredient1) ? ingredient2 : ingredient1;
        }

        private static bool FirstIngredientIsPrimary(int ingredient1, int ingredient2)
        {
            Unity.Mathematics.Random fromIndex1 = Unity.Mathematics.Random.CreateFromIndex((uint)(ingredient1 * 2 + ingredient2 + 87931));
            Unity.Mathematics.Random fromIndex2 = Unity.Mathematics.Random.CreateFromIndex((uint)(ingredient2 * 2 + ingredient1 + 87931));
            return (double)fromIndex1.NextFloat() > (double)fromIndex2.NextFloat();
        }
        #endregion
    }
}
