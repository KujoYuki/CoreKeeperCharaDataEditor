namespace CKCharaDataEditor.Model.Food
{
    public class RecipeHelper
    {
        /// <summary>
        /// 調理済み料理のベースカテゴリの一覧
        /// </summary>
        //public static IReadOnlyList<Recipe> AllCookedBaseCategories { get; } =
        public static IReadOnlyDictionary<CookedFood, Recipe> AllCookedBaseCategories = new Dictionary<CookedFood, Recipe>
        {
            { CookedFood.Soup, new(9500, "CookedSoup", "スープ") },
            { CookedFood.Pudding, new(9501, "CookedPudding", "プリン") },
            { CookedFood.Salad, new(9502, "CookedSalad", "サラダ")  },
            { CookedFood.Wrap, new(9503, "CookedWrap", "ラップ") },
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

        public static readonly List<int> CookedFoodAllIds = AllCookedBaseCategories
                .SelectMany(c => new[] { c.Value.objectID, c.Value.objectID + (int)CookRarity.Rare, c.Value.objectID + (int)CookRarity.Epic })   // Epicはレシピに載らないため除外
                .OrderBy(id => id)
                .ToList();

        /// <summary>
        /// 食材の定義一覧
        /// </summary>
        public static IReadOnlyCollection<Ingredient> AllIngredients { get; } =
        [
            new(1645,"LarvaMeat","幼虫肉", CookedFood.Steak, IngredientAttribute.None),
            new(1646,"GoldenLarvaMeat","金色の幼虫肉", CookedFood.Steak, IngredientAttribute.None),
            new(5500,"Mushroom","きのこ", CookedFood.Soup, IngredientAttribute.Harvest),
            new(5502,"GiantMushroom2","ジャンボマッシュルーム", CookedFood.Salad, IngredientAttribute.Harvest),
            new(5503,"AmberLarva2","幼虫の琥珀", CookedFood.Pudding, IngredientAttribute.None),
            new(5773,"GlowingMushroom","ヒカリキノコ", CookedFood.Soup, IngredientAttribute.None),
            new(7901,"Meat","霜降り肉", CookedFood.Steak, IngredientAttribute.None),
            new(7902,"Egg","ドードーの卵", CookedFood.Cake, IngredientAttribute.None),
            new(8003,"HeartBerry","ハートベリー", CookedFood.Pudding, IngredientAttribute.Harvest),
            new(8006,"GlowingTulipFlower","発光チューリップ", CookedFood.Salad, IngredientAttribute.Harvest),
            new(8009,"BombPepper","爆弾ピーマン", CookedFood.Wrap, IngredientAttribute.Harvest),
            new(8012,"Carrock","キャロック", CookedFood.DipSnack, IngredientAttribute.Harvest),
            new(8015,"Puffungi","カビキノコ", CookedFood.Cheese, IngredientAttribute.Harvest),
            new(8024,"BloatOat","ボウチョウ麦", CookedFood.Cereal, IngredientAttribute.Harvest),
            new(8027,"Pewpaya","ピュパイヤ", CookedFood.Smoothie, IngredientAttribute.Harvest),
            new(8030,"Pinegrapple","パイグラップル", CookedFood.Smoothie, IngredientAttribute.Harvest),
            new(8033,"Grumpkin","ふきげんカボチャ", CookedFood.Soup, IngredientAttribute.Harvest),
            new(8036,"Sunrice","サンライス", CookedFood.PanCurry, IngredientAttribute.Harvest),
            new(8039,"Lunacorn","ルナコーン", CookedFood.Sandwich, IngredientAttribute.Harvest),
            new(8100,"HeartBerryRare","金色のハートベリー", CookedFood.Pudding, IngredientAttribute.Harvest),
            new(8101,"GlowingTulipFlowerRare","金色の発光チューリップ", CookedFood.Salad, IngredientAttribute.Harvest),
            new(8102,"BombPepperRare","金色の爆弾ピーマン", CookedFood.Wrap, IngredientAttribute.Harvest),
            new(8103,"CarrockRare","金色のキャロック", CookedFood.DipSnack, IngredientAttribute.Harvest),
            new(8104,"PuffungiRare","金色のカビキノコ", CookedFood.Cheese, IngredientAttribute.Harvest),
            new(8105,"BloatOatRare","金色のボウチョウ麦", CookedFood.Cereal, IngredientAttribute.Harvest),
            new(8106,"PewpayaRare","金色のピュパイヤ", CookedFood.Smoothie, IngredientAttribute.Harvest),
            new(8107,"PinegrappleRare","金色のパイグラップル", CookedFood.Smoothie, IngredientAttribute.Harvest),
            new(8108,"GrumpkinRare","金色のふきげんガボチャ", CookedFood.Soup, IngredientAttribute.Harvest),
            new(8109, "SunriceRare", "金色のサンライス", CookedFood.PanCurry, IngredientAttribute.Harvest),
            new(8110, "LunacornRare", "金色のルナコーン", CookedFood.Sandwich, IngredientAttribute.Harvest),
            new(9618,"AtlantianWormHeart","アトラスワームの心臓", CookedFood.Steak, IngredientAttribute.Fish),
            new(9622,"VoidHydraHeart","オブリドラの心臓", CookedFood.Steak, IngredientAttribute.None),
            new(9700,"OrangeCaveGuppy","オレンジ色の洞窟グッピー", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9701,"BlueCaveGuppy","青色の洞窟グッピー", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9702,"RockJaw","ロックジョー", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9703,"GemCrab","宝石ガニ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9704,"DaggerFin","カミソリウオ", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9705,"PinkPalaceFish","モモイロキュウデンギョ", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9706,"TealPalaceFish","アオミドリキュウデンギョ", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9707,"CrownSquid","大王イカ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9708,"YellowBlisterHead","イエローブリスターヘッド", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9709,"GreenBlisterHead","緑色のブリスターヘッド", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9710,"DevilWorm","デビルワーム", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9711,"VampireEel","吸血ウナギ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9712,"MoldShark","カビザメ", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9713,"RotFish","ロットフィッシュ", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9714,"BlackSteelUrchin","クロガネウニ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9715,"AzureFeatherFish","空色のハネウオ", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9716,"EmeraldFeatherFish","翡翠色のハネウオ", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9717,"SpiritVeil","スピリットベール", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9718,"AstralJelly","星クラゲ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9719,"BottomTracer","ボトムトレーサー", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9720,"SilverTorrentDart","銀色のダート", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9721,"GoldenTorrentDart","金色のダート", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9722,"PinkCoralotl","ピンクサンゴウオ", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9723,"WhiteCoralotl","シロサンゴウオ", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9724,"SolidSpikeback","ハードスパイクバック", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9725,"SandySpikeback","サンドスパイクバック", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9726,"GreyDuneTail","グレイデューンテール", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9727,"BrownDuneTail","ブラウンデューンテール", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9728,"TornisKingfish","トーニスキングフィッシュ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9729,"DarkLavaEater","ダークラヴァイーター", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9730,"BrightLavaEater","ブライトラヴァイーター", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9731,"VerdantDragonfish","グリーンドラゴンフィッシュ", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9732,"ElderDragonfish","エルダードラゴンフィッシュ", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9733,"StarlightNautilus","スターライトノーチラス", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9734,"AngleFish","緑柱石のアングルフィッシュ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9735,"Deepstalker","玲瓏なディープストーカー", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9736,"CosmicForm","宇宙の形状", CookedFood.FishBalls, IngredientAttribute.Fish),
            new(9737,"GoldenAngleFish","碧玉のアングルフィッシュ", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9738,"GoldenDeepstalker","華麗なディープストーカー", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9739,"TerraTrilobite","陸三葉虫", CookedFood.Sushi, IngredientAttribute.Fish),
            new(9740,"LithoTrilobite","岩三葉虫", CookedFood.Fillet, IngredientAttribute.Fish),
            new(9741,"PinkhornPico","ピンク角のピコ", CookedFood.Pudding, IngredientAttribute.Fish),
            new(9742,"GreenhornPico","緑角のピコ", CookedFood.Pudding, IngredientAttribute.Fish),
            new(9743,"RiftianLampfish","亀裂のランプフィッシュ", CookedFood.FishBalls, IngredientAttribute.Fish),
        ];

        /// <summary>
        /// 廃止済みの食材定義一覧
        /// </summary>
        public static IReadOnlyCollection<Ingredient> ObsoleteIngredients =>
        [
            new(5501,"GiantMushroom2","ジャンボマッシュルーム(古)", CookedFood.Salad, IngredientAttribute.Harvest & IngredientAttribute.Deprecated),
            new(5607,"AmberLarva2","幼虫の琥珀(古)", CookedFood.Pudding, IngredientAttribute.None & IngredientAttribute.Deprecated),
        ];

        public static IReadOnlyCollection<Ingredient> AllIngredientsWithObsolute =>
            AllIngredients.Concat(ObsoleteIngredients).ToList();

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
        /// 決まった食材Idから合成後の料理のvariation値を計算する。Primary/Secondaryの順番は内部で計算する。
        /// </summary>
        /// <param name="ingredientA">1つめの食材のId(dec)</param>
        /// <param name="ingredientB">2つめの食材のId(dec)</param>
        /// <returns></returns>
        public static int CalculateVariation(int ingredientA, int ingredientB)
        {
            // ゲーム内ロジックに合わせてプライマリ食材を先頭に持ってくる
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

        public static int ContainsRareIngredient(int ingredient1, int ingredient2)
        {
            int rareIngredient = 0;
            if (IngredientShouldBePrimary(ingredient1)) rareIngredient++;
            if (IngredientShouldBePrimary(ingredient2)) rareIngredient++;
            return rareIngredient;
        }

        /// <summary>
        /// 全ての正常なレシピ組み合わせ一覧を列挙する。
        /// </summary>
        public static readonly IReadOnlyList<Recipe> AllRecipes = CalulateRecipeCombination(AllIngredients);

        /// <summary>
        /// 廃止済み食材を含む全てのレシピ組み合わせ一覧を列挙する。
        /// </summary>
        public static readonly IReadOnlyList<Recipe> AllRecipesWithObsoluteIngredient = CalulateRecipeCombination(AllIngredientsWithObsolute);

        /// <summary>
        /// レシピの組み合わせを計算して列挙する
        /// </summary>
        /// <param name="allIngredients"></param>
        /// <returns></returns>
        private static IReadOnlyList<Recipe> CalulateRecipeCombination(IEnumerable<Ingredient> ingredients)
        {
            List<Recipe> recipes = [];
            List<int> ids = ingredients.Select(i => i.objectID).ToList();
            // 全てのレシピ組み合わせを列挙する
            for (int indexA = 0; indexA < ids.Count(); indexA++)
            {
                for (int indexB = indexA; indexB < ids.Count(); indexB++)
                {
                    int objectIdA = ingredients.ElementAt(indexA).objectID;
                    int objectIdB = ingredients.ElementAt(indexB).objectID;
                    int primaryIngredientID = GetPrimaryIngredient(objectIdA, objectIdB);
                    int secondaryIngredientID = (primaryIngredientID == objectIdA) ? objectIdB : objectIdA;
                    CookedFood cookedFood = ingredients.First(i => i.objectID == primaryIngredientID).CookedFood;

                    int containsRareIngredient = ContainsRareIngredient(primaryIngredientID, secondaryIngredientID);
                    CookRarity rarity = containsRareIngredient > 0 ? CookRarity.Rare : CookRarity.Common;

                    Recipe cookedFoodBase = AllCookedBaseCategories[cookedFood] with
                    {
                        PrimaryIngredient = primaryIngredientID,
                        SecondaryIngredient = secondaryIngredientID,
                        Rarity = rarity,
                    };
                    recipes.Add(cookedFoodBase);
                }
            }
            return recipes;
        }
    }
}
