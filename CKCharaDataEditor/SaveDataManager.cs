using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.Food;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Resource;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CKCharaDataEditor
{
    /// <summary>
    /// 単一のセーブデータに対する読み書きモジュール
    /// </summary>
    public sealed class SaveDataManager
    {
        private static SaveDataManager? _instance;
        public static SaveDataManager Instance => _instance ??= new();
        private int CharaDataFormatVersion => GetCharacterDataVersion();
        private Item? _copiedItem;
        private Item[]? _copiedInventory;
        private string _saveDataPath = string.Empty;
        public string SaveDataPath
        {
            get => _saveDataPath;
            set
            {
                if (Path.Exists(value))
                {
                    _saveDataPath = value;
                    _saveData = LoadInventory(out var items);
                    Items = items;
                }
            }
        }

        private string _installFolderPath = string.Empty;
        public string InstallFolderPath
        {
            get => _installFolderPath;
            set
            {
                _installFolderPath = value;
            }
        }

        public List<Item> Items { get; private set; } = [];
        private JsonObject _saveData = [];

        private SaveDataManager()
        {
            // Singleton pattern
        }

        public static string SanitizeJsonString(string origin)
        {
            return origin.Replace("Infinity", "\"Infinity\"");
        }

        public static string RestoreJsonString(string processed)
        {
            return processed.Replace("\"Infinity\"", "Infinity");
        }

        private JsonObject LoadInventory(out List<Item> items)
        {
            try
            {
                string saveDataContents = File.ReadAllText(SaveDataPath);
                // conditionsList中のInfinity文字列により例外が出るのを回避する
                saveDataContents = SanitizeJsonString(saveDataContents);

                _saveData = JsonNode.Parse(saveDataContents)!.AsObject();

                var inventoryBase = _saveData["inventory"]!.AsArray();
                var inventoryName = _saveData["inventoryObjectNames"]!.AsArray();
                var inventoryAuxData = _saveData["inventoryAuxData"]!.AsArray();

                if (CharaDataFormatVersion < 11)
                {
                    var limitedItems = inventoryBase
                    .Zip(inventoryName, inventoryAuxData)
                    .Select(x => (item: x.First!, objectName: x.Second!, auxData: x.Third!));

                    items = [];
                    foreach (var (item, objectName, auxData) in limitedItems)
                    {
                        int objectID = item["objectID"]!.GetValue<int>();
                        int amount = item["amount"]!.GetValue<int>();
                        int variation = item["variation"]!.GetValue<int>();
                        int variationUpdateCount = item["variationUpdateCount"]!.GetValue<int>();
                        string objectInternalName = objectName!.GetValue<string>()!;
                        var itemAux = new ItemAuxData(auxData["index"]!.GetValue<int>(), auxData["data"]!.GetValue<string>());
                        items.Add(new(objectID, amount, variation, variationUpdateCount, objectInternalName, itemAux));
                    }
                    return _saveData;
                }
                else
                {
                    var inventoryLocked = _saveData["lockedObjects"]!.AsArray();

                    var limitedItems = inventoryBase
                    .Zip(inventoryName, inventoryAuxData)
                    .Zip(inventoryLocked)
                    .Select(x => (item: x.First.First!, objectName: x.First.Second!, auxData: x.First.Third!, locked: x.Second!));

                    items = [];
                    foreach (var (item, objectName, auxData, locked) in limitedItems)
                    {
                        int objectID = item["objectID"]!.GetValue<int>();
                        int amount = item["amount"]!.GetValue<int>();
                        int variation = item["variation"]!.GetValue<int>();
                        int variationUpdateCount = item["variationUpdateCount"]!.GetValue<int>();
                        string objectInternalName = objectName!.GetValue<string>()!;
                        var itemAux = new ItemAuxData(auxData["index"]!.GetValue<int>(), auxData["data"]!.GetValue<string>());
                        bool objectLocked = locked!.GetValue<bool>()!;
                        items.Add(new(objectID, amount, variation, variationUpdateCount, objectInternalName, itemAux, objectLocked));
                    }
                    return _saveData;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("セーブデータの読み込みに失敗しました。", ex);
            }
        }

        public ItemAuxData GetAuxData(int insertIndex)
        {
            var auxData = _saveData["inventoryAuxData"]![insertIndex]!;
            return new ItemAuxData(auxData["index"]!.GetValue<int>(), auxData["data"]!.GetValue<string>());
        }

        /// <summary>
        /// アイテムのデータを書き込みます。
        /// </summary>
        /// <param name="insertIndex">インベントリ内の上書き位置</param>
        /// <param name="item">新しいアイテム</param>
        /// <returns></returns>
        public bool WriteItemData(int insertIndex, Item item)
        {
            ItemInfo itemBase = new(item.objectID, item.amount, item.variation, item.variationUpdateCount);
            try
            {
                _saveData["inventory"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(itemBase, StaticResource.SerializerOption));
                _saveData["inventoryObjectNames"]![insertIndex] = item.objectName;
                _saveData["inventoryAuxData"]![insertIndex] = JsonNode.Parse(JsonSerializer.Serialize(item.Aux, StaticResource.SerializerOption));
                if (CharaDataFormatVersion >= 11)
                {
                    _saveData["lockedObjects"]![insertIndex] = item.Locked;
                }

                BreakLastConnectedServerId();
                string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);

                // 書き込む前に元jsonの構文に戻す
                changedJson = RestoreJsonString(changedJson);

                File.WriteAllText(SaveDataPath, changedJson);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RewriteAllItemData()
        {
            _saveData["inventory"] = JsonNode.Parse(JsonSerializer.Serialize(Items.Select(i => i), StaticResource.SerializerOption));
            _saveData["inventoryObjectNames"] = JsonNode.Parse(JsonSerializer.Serialize(Items.Select(i => i.objectName), StaticResource.SerializerOption));
            _saveData["inventoryAuxData"] = JsonNode.Parse(JsonSerializer.Serialize(Items.Select(i => i.Aux), StaticResource.SerializerOption));
            BreakLastConnectedServerId();
            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(SaveDataPath, changedJson);
        }

        public bool IsClearData()
        {
            return _saveData["hasUnlockedSouls"]?.GetValue<bool>() is true
                   && _saveData["collectedSouls"]?.AsArray().Count is 6
                   && _saveData["hasPlayedOutro"]?.GetValue<bool>() is true;
        }

        public bool IsCreativeData()
        {
            string charaSlotString = Path.GetFileNameWithoutExtension(SaveDataPath);
            if (int.TryParse(charaSlotString, out int charaSlotNo))
            {
                if (charaSlotNo >= 30)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// IncreasedMaxHealthPermanentの増分検知
        /// </summary>
        /// <param name="increasedHealth"></param>
        /// <returns></returns>
        public bool HasOveredHealth(out int increasedHealth)
        {
            int? increasedHealthNullable = _saveData["conditionsList"]?.AsArray()
                .Select(x => JsonSerializer.Deserialize<Condition>(x, StaticResource.SerializerOption))
                .SingleOrDefault(c => c?.Id == 16)?.Value;
            if (increasedHealthNullable is null)
            {
                increasedHealth = 0;
                return false;
            }
            increasedHealth = increasedHealthNullable.Value;
            if (increasedHealth < 2000) return false;

            return true;
        }

        public void CalculateRecipeCounts(out int userRecipeCount, out int allRecipeTempVariationCount, out List<Tuple<int, int>> exceptRecipe)
        {
            int[] allFoodID = Recipe.AllIngredients.Select(c => c.objectID).ToArray();
            List<Tuple<int, int>> allIngredientPairs = allFoodID
                .SelectMany((ID, index) => allFoodID.Skip(index), (ID1, ID2) => Tuple.Create(Math.Min(ID1, ID2), Math.Max(ID1, ID2)))
                .ToList();
            allRecipeTempVariationCount = allIngredientPairs.Count;

            // 料理のコモンとレアのカテゴリIDリスト
            List<int> allCookedCategoryId = Recipe.AllCookedBaseCategories
                .SelectMany(c => new[] { c.objectID, c.objectID + (int)CookRarity.Rare })   // Epicはレシピに載らないため除外
                .OrderBy(id => id)
                .ToList();
            List<Tuple<int, int>> allUserRecipeTempVariation = _saveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(o => allCookedCategoryId.Contains(o.objectID))   // 非料理アイテムは除外
                .Where(o => o.variation > 0 && (uint)o.variation <= uint.MaxValue)   // variationが0か32bitで表現できない場合は除外
                .Select(c => c!.variation)
                .Distinct()
                .Select(variation =>
                {
                    Recipe.UnpackVariation(variation, out int materialA, out int materialB);
                    return Tuple.Create(Math.Min(materialA, materialB), Math.Max(materialA, materialB));
                })
                .Where(pair => allFoodID.Contains(pair.Item1) && allFoodID.Contains(pair.Item2))    // 食材以外を食材とするレシピの除外
                .ToList();

            userRecipeCount = allUserRecipeTempVariation.Count;
            exceptRecipe = allIngredientPairs.Except(allUserRecipeTempVariation).ToList();    //未作成の組み合わせ
        }

        public void ListUncreatedRecipes()
        {
            CalculateRecipeCounts(out int userRecipeCount, out int allRecipeCount, out var exceptRecipes);
            double cookRate = (double)userRecipeCount / allRecipeCount;

            DialogResult outputResult = MessageBox.Show($"現在のレシピ網羅率は {cookRate:P2} %です。" +
                $"（{userRecipeCount} / {allRecipeCount}）\n" +
                $"※ゲーム内レシピブックとカウントが異なる場合があります。\n\n" +
                $"一度も調理していない組み合わせを出力しますか？", "", MessageBoxButtons.YesNo);
            if (outputResult is DialogResult.Yes)
            {
                var foodBuilder = new StringBuilder();
                foreach ((var foodA, var foodB) in exceptRecipes)
                {
                    string FoodA = Recipe.AllIngredients.Single(f => f.objectID == foodA).DisplayName;
                    string FoodB = Recipe.AllIngredients.Single(f => f.objectID == foodB).DisplayName;
                    foodBuilder.AppendLine($"{FoodA} + {FoodB}");
                }

                using SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = "UncreatedRecipe.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    File.WriteAllText(path, foodBuilder.ToString());
                    MessageBox.Show($"{exceptRecipes.Count} 通りのレシピを出力しました。");
                }
            }
            //todo 全レシピ追加 全組み合わせの勝敗表からカテゴリを自動で決定させる:要食材勝敗テーブルのアルゴリズム解明
        }

        public void DeleteAllRecipes()
        {
            var allCookedCategoryId = Recipe.AllCookedBaseCategories
                .SelectMany(c => new[] { c.objectID, c.objectID + (int)CookRarity.Rare, c.objectID + (int)CookRarity.Epic })
                .OrderBy(id => id)
                .ToList();
            var discoveredObjectWithoutRecipe = _saveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Where(obj => !allCookedCategoryId.Contains(obj.objectID))
                .ToList();
            _saveData["discoveredObjects2"] = JsonNode.Parse(JsonSerializer.Serialize(discoveredObjectWithoutRecipe, StaticResource.SerializerOption));
            // データ書き込み
            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(SaveDataPath, changedJson);
        }

        public List<Condition> GetConditions()
        {
            var conditions = _saveData["conditionsList"]?.AsArray()
                .Select(c => JsonSerializer.Deserialize<Condition>(c, StaticResource.SerializerOption)!)
                .ToList()!;
            return conditions;
        }

        public static void BackUpConditions(IEnumerable<Condition> conditions, string filePath)
        {
            conditions = conditions.OrderBy(c => c.Id);
            // 必要あらば例外処理

            var conditionsNode = JsonNode.Parse(JsonSerializer.Serialize(conditions, StaticResource.SerializerOption));
            string changedJson = JsonSerializer.Serialize(conditionsNode, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(filePath, changedJson);
        }

        /// <summary>
        /// Conditionsのみを記述したバックアップファイルを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<Condition> LoadConditions(string filePath)
        {
            string jsonString = SanitizeJsonString(File.ReadAllText(filePath));
            var conditions = JsonSerializer.Deserialize<List<Condition>>(jsonString, StaticResource.SerializerOption)?
                .OrderBy(c => c.Id)
                .ToList();
            return conditions ??= [];
        }

        public void OverrideConditions(IEnumerable<Condition> conditions)
        {
            _saveData["conditionsList"] = JsonNode.Parse(JsonSerializer.Serialize(conditions, StaticResource.SerializerOption));
            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
#if DEBUG
            _saveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif
            File.WriteAllText(SaveDataPath, changedJson);
        }

        public void TsetseWell()
        {
            var conditions = GetConditions();
            if (!conditions.Any(c => c.Id == 210))
            {
                conditions.Add(new(210, 2, double.PositiveInfinity, -1));
            }
            _saveData["conditionsList"] = JsonNode.Parse(JsonSerializer.Serialize(conditions, StaticResource.SerializerOption));
            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(SaveDataPath, changedJson);
        }

        internal Item CopyItem(int index)
        {
            _copiedItem = Items[index];
            return _copiedItem;
        }

        internal Item PasteItem()
        {
            return _copiedItem ?? Item.Default;
        }

        internal void CopyInventory()
        {
            _copiedInventory = Items.ToArray();
        }

        internal void PasteInventory()
        {
            if (_copiedInventory is not null)
            {
                Items = _copiedInventory.ToList();
                RewriteAllItemData();
            }
        }

        internal bool HasCopiedInventory()
        {
            return _copiedInventory is not null;
        }

        internal List<Skill> LoadSkillPoint()
        {
            return _saveData["skills"]!.AsArray()
                .Select(s => JsonSerializer.Deserialize<Skill>(s, StaticResource.SerializerOption)!)
                .OrderBy(s => s.skillID)
                .ToList();
        }

        internal void WriteSkillPoint(IEnumerable<Skill> skills)
        {
            _saveData["skills"] = JsonNode.Parse(JsonSerializer.Serialize(skills, StaticResource.SerializerOption));

            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            changedJson = RestoreJsonString(changedJson);
            File.WriteAllText(SaveDataPath, changedJson);
        }

        internal string GetCharacterName()
        {
            var byteName = _saveData["characterCustomization"]!["name"]!["bytes"]!["offset0000"]!.AsObject();
            var byteList = new List<byte>();
            foreach (var byteEntry in byteName)
            {
                // 値を取得し、バイトデータに変換
                if (byteEntry.Value is JsonValue byteValue && byteValue.TryGetValue<byte>(out byte byteData))
                {
                    // ASCII値が0の場合は終端とみなして終了
                    if (byteData == 0) break;

                    byteList.Add(byteData);
                }
            }
            string characterName = Encoding.UTF8.GetString(byteList.ToArray());
            return characterName;
        }

        internal int GetCharacterDataVersion()
        {
            var version = _saveData["version"]!.GetValue<int>();
            return version;
        }

        internal Guid GetLastConnnectedServerId()
        {
            var changedOrderWorldId = _saveData["lastConnectedServerGuid"]!["Value"]!.AsObject()
                .Select(fourbyte =>
                {
                    byte[] bytes = new byte[4];
                    uint values =(fourbyte.Value as JsonValue)!.GetValue<uint>();
                    return BitConverter.GetBytes(values).ToArray();
                })
                .SelectMany(bytes => bytes)
                .Select(bytes =>
                {
                    byte upper = (byte)((bytes & 0xF0) >> 4);
                    byte lower = (byte)((bytes & 0x0F) << 4);
                    return (byte)(upper | lower);
                })
                .ToArray();

            byte[] originalId = RestoreByteOrder(changedOrderWorldId); 
            return new Guid(originalId);
        }

        private static byte[] RestoreByteOrder(byte[] changedbytes)
        {
            byte[] originalId = new byte[16];
            int[] map = 
            [
                3, 2, 1, 0,    // reverse first 4 bytes
                5, 4,          // reverse next 2 bytes
                7, 6,          // reverse next 2 bytes
                8, 9, 10, 11, 12, 13, 14, 15 // keep as is
            ];
            for (int i = 0; i < map.Length; i++)
            {
                originalId[i] = changedbytes[map[i]];
            }
            return originalId;
        }

        private void BreakLastConnectedServerId()
        {
            if (CharaDataFormatVersion < 12)
            {
                // 12未満のバージョンではlastConnectedServerGuidが存在しないため、何もしない
                return;
            }
            JsonObject valueObj = _saveData["lastConnectedServerGuid"]!["Value"]!.AsObject();
            uint x = valueObj["x"]!.GetValue<uint>();
            valueObj["x"] = x + 1;
            _saveData["lastConnectedServerGuid"]!["Value"] = valueObj;

        }

        /// <summary>
        /// インベントリ先頭の装備アイテム4つを、アイテムLvごとに20個ずつ複製します。
        /// </summary>
        internal void DupeEquipmentEachLv()
        {
            Item[] items = Items.Take(4).ToArray();

            // 通常のアイテム枠とポーチのアイテム枠
            Queue<int> indexes = new(
                Enumerable.Range(0, 50)
                    .Concat(Enumerable.Range(87, 10))
                    .Concat(Enumerable.Range(98, 10))
                    .Concat(Enumerable.Range(109, 10))
                    .Concat(Enumerable.Range(120, 10))
                    );
            foreach (var item in items)
            {
                for (int i = 0; i < 20; i++)
                {
                    Item newItem = item with
                    {
                        variation = i
                    };
                    WriteItemData(indexes.Dequeue(), newItem);
                }
            }
        }

        public void ListupUnobtainedItem()
        {
            if (FileManager.Instance.LocalizationData.Count is 0)
            {
                MessageBox.Show("言語リソースのパスが設定されていません。\n処理を中断します", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StringBuilder outputText = new("objectID\tアイテム名\n");

            List<int> discoveredEquipIds = _saveData["discoveredObjects2"]!.AsArray()
                .Select(obj => JsonSerializer.Deserialize<DiscoveredObjects>(obj)!)
                .Select(obj => obj.objectID)
                .Distinct()
                .Order()
                .ToList();
            List<int> allEquip = FileManager.Instance.LocalizationData.ToList()
                .Select(kv => kv.Key)
                .Order()
                .ToList();
            var unobtainedEquipIds = allEquip.Except(discoveredEquipIds)
                .Where(objectID => objectID > 3000 && objectID >= 3400) // 敵モブアイテムを除外
                .Select(objectID =>
                {
                    string displayName = FileManager.Instance.LocalizationData.TryGetValue(objectID, out string[]? translateResources) ?
                        translateResources[1] : $"アイテム名が取得できませんでした。";
                    outputText.AppendLine($"{objectID}\t{displayName}");
                    return (objectID: objectID, displayName: displayName);
                })
                .ToList();

            using SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = "UnobtainedItem.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllText(path, outputText.ToString());
                MessageBox.Show($"{unobtainedEquipIds.Count} の未発見アイテムを出力しました。");
            }
        }
    }
}
