using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;

namespace CKCharaDataEditor.Model.Food
{
    public record Recipe : DiscoveredObjects
    {
        private int _baseRecipeObjectID = default;
        private string _baseKeyName = string.Empty;

        public override int objectID
        {
            get => _baseRecipeObjectID + (int)Rarity;
            set
            {
                // ObjectIDにRarityの補正が入っているか判定させる
                var rarity = (value, RecipeHelper.AllCookedBaseCategories) switch
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

        public string keyName
        {
            get
            {
                string prefix = Rarity switch
                {
                    CookRarity.Rare => "Rare",
                    CookRarity.Epic => "Epic",
                    _ => string.Empty
                };
                string baseRecipeName = RecipeHelper.AllCookedBaseCategories
                    .SingleOrDefault(r => r.Value._baseRecipeObjectID == _baseRecipeObjectID)!.Value._baseKeyName;
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
                if (RecipeHelper.AllCookedBaseCategories.Select(c => c.Value.objectID).Contains(value))
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
            //hack 将来的には修飾子を含めた表示名を取得する。優先度:低
            string rarityPrefix = rarity switch
            {
                CookRarity.Rare => "レア",
                CookRarity.Epic => "エピック",
                _ => string.Empty,
            };

            int baseRecipeObjectID = objectID - (int)rarity;
            string baseDisplayName = RecipeHelper.AllCookedBaseCategories
                .SingleOrDefault(r => r.Value._baseRecipeObjectID == baseRecipeObjectID).Value?.DefaultDisplayName ?? string.Empty;
            return rarityPrefix + baseDisplayName;
        }

        public Recipe(Item original) : base(original)
        {
            objectID = original.objectID;
            _baseKeyName = original.keyName;
            DefaultDisplayName = CreateDisplayName(objectID, Rarity);
        }

        public Recipe(int objectID, string keyName, string defaultDisplayName)
            : base(Default with { objectID = objectID })
        {
            _baseRecipeObjectID = objectID;
            DefaultDisplayName = defaultDisplayName;
            _baseKeyName = keyName;
            Rarity = CookRarity.Common; // デフォルトはCommonとする
        }

        public Recipe(int recipeObjectID, int ingredientA, int ingredientB, CookRarity rarity)
            : base(Default)
        {
            DefaultDisplayName = RecipeHelper.AllCookedBaseCategories
                .SingleOrDefault(r => r.Value._baseRecipeObjectID == recipeObjectID).Value?.DefaultDisplayName ?? string.Empty;
            _baseRecipeObjectID = recipeObjectID;
            PrimaryIngredient = RecipeHelper.GetPrimaryIngredient(ingredientA, ingredientB);
            SecondaryIngredient = RecipeHelper.GetSecondaryIngredient(ingredientA, ingredientB);
            Rarity = rarity;
            // keyNameはsetterで設定されるのでここでは設定しない
        }

        public Recipe(DiscoveredObjects discoveredObjects) : base(discoveredObjects)
        {
            objectID = discoveredObjects.objectID;  // setteを通して_baseRecipeObjectIDとRarityを設定する
            _baseKeyName = string.Empty;
            DefaultDisplayName = CreateDisplayName(objectID, Rarity);
        }

        public Item ToItem(int amount)
        {
            // RecipeはItemを継承しないので、Itemに変換する
            return new Item(objectID, amount, variation, 0, keyName, ItemAuxData.Default);
        }

        public DiscoveredObjects ToDiscoveredObjects()
        {
            return new DiscoveredObjects(objectID, variation);
        }

        public static bool IsCookedItem(int objectID)
        {
            int startID = RecipeHelper.AllCookedBaseCategories.First().Value.objectID;
            int endID = RecipeHelper.AllCookedBaseCategories.Last().Value.objectID + (int)CookRarity.Epic; // Epicまで含む
            return (startID <= objectID && objectID <= endID);
        }
    }

    // Recipeはrecord型なのでコンパイラ生成のEquals()を生成してしまうため、意図する比較用にIEqualityComparerを実装したクラスを用意する
    class RecipeComparer : IEqualityComparer<Recipe>
    {
        public bool Equals(Recipe? x, Recipe? y)
        {
            if (x is null || y is null) return false;
            return x.objectID == y.objectID
                && x.keyName == y.keyName
                && x.variation == y.variation
                && x.Rarity == y.Rarity;
        }
        public int GetHashCode(Recipe obj)
        {
            HashCode hash = new();
            hash.Add(obj.objectID);
            hash.Add(obj.keyName);
            hash.Add(obj.variation);
            hash.Add(obj.Rarity);

            return hash.ToHashCode();
        }
    }
}
