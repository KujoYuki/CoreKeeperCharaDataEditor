using System.Text.Json;

namespace CKFoodMaker
{
    /// <summary>
    /// 単一のセーブデータに対する読み書きモジュール
    /// </summary>
    public class SaveDataManager
    {
        //インベントリ数（バッグ装備）が最大の場合は50
        private int _loadItemLimit = 50;

        public string SaveDataPath { get; private set; } = String.Empty;
        public List<(Item item, string objectName)> Items { get; private set; } = new();
        private JsonDocument JsonDocument { get; set; }
        public SaveDataManager()
        {
        }

        public SaveDataManager(string saveDataPath)
        {
            SaveDataPath = saveDataPath;

            JsonDocument = LoadInventory(out var items);
            Items = items;
        }

        private string SanitizeJsonString(string origin)
        {
            return origin.Replace("Infinity", "\"Infinity\"");
        }

        private string RestoreJsonString(string processed)
        {
            return processed.Replace("\"Infinity\"", "Infinity");
        }

        private JsonDocument LoadInventory(out List<(Item item, string objectName)> items)
        {
            string saveDataContents = File.ReadAllText(SaveDataPath);
            items = new();

            // conditionsList中のInfinity文字列により例外が出るのを回避する
            saveDataContents = SanitizeJsonString(saveDataContents);

            JsonDocument jsonDocument = JsonDocument.Parse(saveDataContents);

            if (jsonDocument.RootElement.TryGetProperty("inventory", out JsonElement inventoryElement) &&
                inventoryElement.ValueKind is JsonValueKind.Array &&
                jsonDocument.RootElement.TryGetProperty("inventoryObjectNames", out JsonElement inventoryNameElement) &&
                inventoryNameElement.ValueKind is JsonValueKind.Array)
            {
                var limitedItems = inventoryElement.EnumerateArray()
                    .Zip(inventoryNameElement.EnumerateArray())
                    .Take(_loadItemLimit);
                foreach (var (inventoryItem, objectName) in limitedItems)
                {
                    if (!inventoryItem.TryGetProperty("objectID", out var objectIdElement) ||
                        !inventoryItem.TryGetProperty("amount", out var amountElement) ||
                        !inventoryItem.TryGetProperty("variation", out var variationElement) ||
                        !inventoryItem.TryGetProperty("variationUpdateCount", out var variationUpdateCountElement))
                    {
                        MessageBox.Show($"アイテム読み込みに失敗しました。\n{inventoryItem}", "エラーが発生しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Item item = new Item(
                        objectId: inventoryItem.GetProperty("objectID").GetInt32(),
                        amount: inventoryItem.GetProperty("amount").GetInt32(),
                        variation: inventoryItem.GetProperty("variation").GetInt32(),
                        variationUpdateCount: inventoryItem.GetProperty("variationUpdateCount").GetInt32());
                    string objectInternalName = objectName.GetString()!;
                    items.Add((item, objectInternalName));
                }
            }
            return jsonDocument;
        }

        /// <summary>
        /// 変更したアイテムを書き込む
        /// </summary>
        /// <param name="insertIndex"></param>
        /// <param name="newItem">変更後アイテム</param>
        /// <param name="internalName">変更後アイテムのInternalName</param>
        public void WriteItemData(int insertIndex, Item newItem, string internalName)
        {
            if (JsonDocument.RootElement.TryGetProperty("inventory", out var inventoryElement) &&
                inventoryElement.ValueKind is JsonValueKind.Array &&
                JsonDocument.RootElement.TryGetProperty("inventoryObjectNames", out JsonElement inventoryNameElement) &&
                inventoryNameElement.ValueKind is JsonValueKind.Array)
            {
                // inventoryとinventoryObjectNamesを部分的に取得して置換する
                var inventoryList = inventoryElement.EnumerateArray().ToList();
                var inventoryObjectNamesList = inventoryNameElement.EnumerateArray().ToList();
                if (insertIndex >= 0 && insertIndex < _loadItemLimit)
                {
                    // 単一アイテム分のみをシリアライズ
                    string newItemJson = JsonSerializer.Serialize(newItem);
                    string newItemObjectName = JsonSerializer.Serialize(internalName);
                    JsonElement newItemElement = JsonDocument.Parse(newItemJson).RootElement;
                    JsonElement newItemObjectNameElement = JsonDocument.Parse(newItemObjectName).RootElement;

                    // 指定indexのリプレース
                    inventoryList[insertIndex] = newItemElement;
                    inventoryObjectNamesList[insertIndex] = newItemObjectNameElement;

                    // 配列ごとシリアライズ
                    string newInventoryJson = JsonSerializer.Serialize(inventoryList);
                    string newInventoryObjetNameJson = JsonSerializer.Serialize(inventoryObjectNamesList);

                    // 置換はRawデータで配列ごと実行
                    string changedJson = File.ReadAllText(SaveDataPath)
                        .Replace(inventoryElement.GetRawText(), newInventoryJson)
                        .Replace(inventoryNameElement.GetRawText(), newInventoryObjetNameJson);

                    // 書き込む前にjsonStringを逆サニタイズ
                    changedJson = RestoreJsonString(changedJson);

#if DEBUG
                    MessageBox.Show($"insertIndex = {insertIndex}\n{newItemJson}\n{internalName}", "書き込み内容確認");
                    SaveDataPath = Path.Combine(Path.GetDirectoryName(SaveDataPath)!, "debug.json");
#endif
                    // 確認用に別名ファイルで作成

                    File.WriteAllText(SaveDataPath, changedJson);
                }
            }
        }
    }
}
