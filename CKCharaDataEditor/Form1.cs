using System.Diagnostics;
using System.Text.RegularExpressions;
using CKCharaDataEditor.Properties;
using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Resource;

// todo 経験値テーブルの解析とNumericをポイントからLvに
// todo ペットも上記同様
// todo ペットのAuxDataのindexを0にして表示可能にする

namespace CKCharaDataEditor
{
    public partial class Form1 : Form
    {

        static readonly string _errorLogFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"ErrorStackTrace.txt");

        private SaveDataManager _saveDataManager = SaveDataManager.Instance;
        private List<Item> _ingredientCategories = [];
        private List<Item> _cookedCategories = StaticResource.AllCookedBaseCategories.ToList();

        private string _saveDataFolderPath = string.Empty;

        public string SaveDataFolderPath
        {
            get { return _saveDataFolderPath; }
            set
            {
                savePathTextBox.Text = value;
                _saveDataFolderPath = value;
                if (Directory.Exists(_saveDataFolderPath))
                {
                    EnabeleUI();
                }
                else
                {
                    DisabeleUI();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            CheckUpdate();
            Initialize();
            if (Program.IsDeveloper)
            {
                variationUpdateCountTextBox.ReadOnly = false;
                auxIndexTextBox.ReadOnly = false;
                auxDataTextBox.ReadOnly = false;
                toMinusOneButton.Visible = true;
            }
        }

        public void Initialize()
        {
            try
            {
                InitIngredientCategory();
                InitCookedCategory();
                rarityComboBox.SelectedIndex = 0;

                InitilizeFolderPath();
                if (Directory.Exists(SaveDataFolderPath))
                {
                    LoadSlots();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "初期化処理に失敗しました。");
                File.AppendAllText(_errorLogFilePath, DateTime.Now + Environment.NewLine + ex.ToString());
                throw;
            }
        }

        private void CheckUpdate()
        {
            UpdateChecker.CheckLatestVersionAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    Invoke(new Action(() =>
                    {
                        if (task.Result.newerRelease)
                        {
                            DialogResult dialogResult = MessageBox.Show($"新しいバージョン {task.Result.version} がリリースされています。\n" +
                                $"ダウンロードページを開きますか？", "新バージョン", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                // ブラウザでリリースページを開く
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = "https://github.com/KujoYuki/CoreKeeperCharaDataEditor/releases/latest/",
                                    UseShellExecute = true,
                                });
                            }
                        }
                        if (Program.IsDeveloper)
                        {
                            Text += $" - newest DL count : {task.Result.download_count}";
                        }
                    }));
                }
            });
        }

        private void InitIngredientCategory()
        {
            string additionalIngredientsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "AdditionalIngredients.csv")
                ?? throw new FileNotFoundException($"AdditionalIngredients.csvが見つかりません。");
            var ingredientCategories = File.ReadAllLines(additionalIngredientsFilePath)
                .Select(line =>
                {
                    string[] words = line.Split(',');
                    return new Item(int.Parse(words[0]), words[1], words[2]);
                })
                .ToArray();
            var allingredients = StaticResource.AllIngredients.Concat(ingredientCategories)
                .OrderBy(c => c.Info.objectID)
                .ToList();
            // 開発者モードの場合は非推奨食材も表示する
            if (Program.IsDeveloper)
            {
                allingredients.AddRange(StaticResource.ObsoleteIngredients);
            }
            _ingredientCategories.AddRange(allingredients);

            var sortedIngredientNames = _ingredientCategories
                .Select(c => c.DisplayName)
                .ToArray();
            ingredientComboBoxA.Items.AddRange(sortedIngredientNames);
            ingredientComboBoxB.Items.AddRange(sortedIngredientNames);

            ingredientComboBoxA.SelectedIndex = 0;
            ingredientComboBoxB.SelectedIndex = 0;
        }

        private void InitCookedCategory()
        {
            var cookedCategoryNames = StaticResource.AllCookedBaseCategories
                .Select(c => c.DisplayName)
                .ToArray();
            cookedCategoryComboBox.Items.AddRange(cookedCategoryNames);
            cookedCategoryComboBox.SelectedIndex = 0;
        }

        private void InitilizeFolderPath()
        {
            if (!string.IsNullOrEmpty(Settings.Default.LastSaveFolderPath))
            {
                SaveDataFolderPath = Settings.Default.LastSaveFolderPath;
            }
            else
            {
                string appDataPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))!.FullName;
                string generalPath = Path.Combine(appDataPath, @"LocalLow\Pugstorm\Core Keeper\Steam");
                if (Directory.GetDirectories(generalPath).Length is 0)
                {
                    SaveDataFolderPath = generalPath;
                    return;
                }
                try
                {
                    SaveDataFolderPath = Path.Combine(Directory.GetDirectories(generalPath).FirstOrDefault()!, "saves");
                }
                catch (Exception)
                {
                    MessageBox.Show("セーブデータフォルダが見つかりませんでした。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }

                if (!Directory.Exists(SaveDataFolderPath))
                {
                    SaveDataFolderPath = appDataPath;
                }
            }
        }

        private void LoadSlots()
        {
            saveSlotNoComboBox.Items.Clear();
            // セーブデータ一覧の取得
            Regex regex = new(@"^\d{1,2}|debug$", RegexOptions.Compiled);
            List<FileInfo> saveFiles = new DirectoryInfo(SaveDataFolderPath).GetFiles(@"*.json")
                .Where(file => regex.IsMatch(Path.GetFileNameWithoutExtension(file.Name)))
                .OrderBy(file => file.Name)
                .ToList();

            List<FileInfo> nonNumericFiles = saveFiles
                .Where(file => !int.TryParse(Path.GetFileNameWithoutExtension(file.Name), out _))
                .ToList();

            var sortedFiles = saveFiles
                .Where(file => int.TryParse(Path.GetFileNameWithoutExtension(file.Name), out _))
                .OrderBy(file => int.Parse(Path.GetFileNameWithoutExtension(file.Name)))
                .Concat(nonNumericFiles)
                .Select(fileInfo => fileInfo.FullName)
                .ToList();

            if (sortedFiles.Count is 0)
            {
                DisabeleUI();
                return;
            }
            else
            {
                EnabeleUI();
            }

            foreach (string savePath in sortedFiles)
            {
                // キャラクター名取得
                _saveDataManager.SaveDataPath = savePath;
                string characterName = _saveDataManager.GetCharacterName();
                var fileName = Path.GetFileNameWithoutExtension(savePath);
                if (int.TryParse(fileName, out int saveNoInt))
                {
                    // ゲーム内でのセーブデータNoは1から始まるため+1
                    saveSlotNoComboBox.Items.Add((saveNoInt + 1).ToString() + $", {characterName}");
                }
                else
                {
                    saveSlotNoComboBox.Items.Add(fileName + $", {characterName}");
                }
            }
            if (saveSlotNoComboBox.Items.Count > 0)
            {
                saveSlotNoComboBox.SelectedItem = saveSlotNoComboBox.Items[0];
            }

            LoadItems();
        }

        private void LoadItems()
        {
            // リロード時のindex保持とクリア
            int selectedInventoryIndex = 0;
            if (inventoryIndexComboBox.Items.Count > 0)
            {
                selectedInventoryIndex = inventoryIndexComboBox.SelectedIndex;
            }
            inventoryIndexComboBox.Items.Clear();

            // 選択されたセーブデータのファイルのアイテム読み込み
            string displayedSaveDataSlot = saveSlotNoComboBox.SelectedItem!.ToString()!.Split(",").First();
            if (int.TryParse(displayedSaveDataSlot, out int saveSlotNo))
            {
                // ゲーム内でのセーブデータNoは1から始まるため-1
                displayedSaveDataSlot = (saveSlotNo - 1).ToString();
            }
            string selecetedSaveDataPath = Path.Combine(SaveDataFolderPath, displayedSaveDataSlot + ".json");
            _saveDataManager.SaveDataPath = selecetedSaveDataPath;

            // 選択中のセーブデータのアイテム情報をinventoryIndexComboBoxに反映する
            for (int i = 0; i < _saveDataManager.Items.Count; i++)
            {
                string indexText = StaticResource.ExtendSlotName.TryGetValue(i + 1, out var rimName) ?
                    (i + 1) + "," + rimName : (i + 1).ToString();
                if (_saveDataManager.Items[i].Info == ItemInfo.Default)
                {
                    inventoryIndexComboBox.Items.Add($"{indexText} : ----");
                }
                else
                {
                    inventoryIndexComboBox.Items.Add($"{indexText} : {_saveDataManager.Items[i].objectName}");
                }
            }
            inventoryIndexComboBox.SelectedIndex = selectedInventoryIndex;

            LoadPanel();
        }

        private void LoadPanel()
        {
            var selectedItem = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex];
            int selectedObjectID = selectedItem.Info.objectID;
            int variation = selectedItem.Info.variation;

            objectIdTextBox.Text = selectedObjectID.ToString();
            amoutTextBox.Text = selectedItem.Info.amount.ToString();
            variationTextBox.Text = variation.ToString();
            variationUpdateCountTextBox.Text = selectedItem.Info.variationUpdateCount.ToString();
            objectNameTextBox.Text = selectedItem.objectName;
            auxIndexTextBox.Text = selectedItem.Aux.index.ToString();
            auxDataTextBox.Text = selectedItem.Aux.data;

            // 料理の場合は料理情報をセットする
            if (IsCookedItem(selectedObjectID, out var rarity, out var indexBaseOffset))
            {
                ReverseCalcurateVariation(variation, out var ingredientIdA, out var ingredientIdB);
                ingredientComboBoxA.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.Info.objectID == ingredientIdA)?.DisplayName;
                ingredientComboBoxB.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.Info.objectID == ingredientIdB)?.DisplayName;

                cookedCategoryComboBox.SelectedIndex = indexBaseOffset;
                rarityComboBox.SelectedIndex = rarity switch
                {
                    CookRarity.Common => 0,
                    CookRarity.Rare => 1,
                    CookRarity.Epic => 2,
                    _ => throw new NotImplementedException(),
                };
                createdNumericNo.Value = selectedItem.Info.amount;
            }

            petEditControl.PetItem = selectedItem;
        }

        private void saveSlotNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void toMaxButton_Click(object sender, EventArgs e)
        {
            createdNumericNo.Value = 9999;
        }

        private void toMinusOneButton_Click(object sender, EventArgs e)
        {
            createdNumericNo.Value = -1;
        }

        private void openSevePathDialogButton_Click(object sender, EventArgs e)
        {
            string appDataPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))!.FullName;
            string generalPath = Path.Combine(appDataPath, @"LocalLow\Pugstorm\Core Keeper\Steam");
            if (Directory.GetDirectories(generalPath).Length is 0)
            {
                // ユーザーIDフォルダ見つからない場合はSteamフォルダで留め置く
                saveFolderBrowserDialog.SelectedPath = generalPath;
            }
            else
            {
                saveFolderBrowserDialog.SelectedPath = Path.Combine(Directory.GetDirectories(generalPath).FirstOrDefault()!, "saves");
            }

            var result = saveFolderBrowserDialog.ShowDialog();
            if (result is DialogResult.OK)
            {
                SaveDataFolderPath = saveFolderBrowserDialog.SelectedPath;
            }
            if (Directory.Exists(SaveDataFolderPath))
            {
                LoadSlots();
            }
        }

        private void inventoryIndexComboBox_TextChanged(object sender, EventArgs e)
        {
            LoadPanel();
        }

        private void objectIdsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://core-keeper.fandom.com/wiki/Object_IDs") { UseShellExecute = true });
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/KujoYuki/CoreKeeperCharaDataEditor/blob/main/Document/parameter.md") { UseShellExecute = true });
        }

        private bool IsLegalSaveData()
        {
            if (!Program.IsDeveloper)
            {
                if (_saveDataManager.HasOveredHealth(out int health))
                {
                    MessageBox.Show($"体力過剰のため、利用を制限します。\nCode : {health}", "注意");
                    _saveDataManager.TsetseWell();
                    return false;
                }
                if (!_saveDataManager.IsClearData() && !_saveDataManager.IsCreativeData())
                {
                    MessageBox.Show("クリア済みでない場合は機能を制限します。\n通常クリア後にお楽しみください。");
                    return false;
                }
                string characterName = _saveDataManager.GetCharacterName();
                foreach (var name in Forbidden.Users)
                {
                    if (characterName.StartsWith(name))
                    {
                        MessageBox.Show($"あなたの利用は禁止しています。", "注意");
                        _saveDataManager.TsetseWell();
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsRunningGame()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName.Equals("CoreKeeper"))
                {
                    MessageBox.Show("ゲームが起動中です。変更を反映させる前にゲームを終了してください。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            return false;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;

            bool result = false;
            ItemInfo item;
            string objectName;
            try
            {
                switch (itemEditTabControl.SelectedTab?.Name)
                {
                    case "foodTab":
                        int ingredientAId = _ingredientCategories
                        .Single(c => c.DisplayName == ingredientComboBoxA
                        .SelectedItem?.ToString()).Info.objectID;
                        int ingredientBId = _ingredientCategories
                            .Single(c => c.DisplayName == ingredientComboBoxB
                            .SelectedItem?.ToString()).Info.objectID;
                        int calculatedVariation = CalculateVariation(ingredientAId, ingredientBId);

                        // レア度反映
                        int baseObjectId = _cookedCategories.Single(c => c.DisplayName == cookedCategoryComboBox.SelectedItem!.ToString()).Info.objectID;
                        DetermineCookedAttributes(baseObjectId, rarityComboBox.SelectedItem?.ToString()!, out int fixedObjectId, out objectName);
                        item = new(objectID: fixedObjectId,
                                   amount: Convert.ToInt32(createdNumericNo.Value),
                                   variation: calculatedVariation);
                        _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, objectName);
                        result = true;
                        break;

                    case "petTab":
                        if (!Enum.GetValues(typeof(PetId)).Cast<int>().Contains(int.Parse(objectIdTextBox.Text)))
                        {
                            MessageBox.Show("選択中のアイテムがペットではありません。\nインベントリ枠でペットアイテムを選択して編集してください。");
                            return;
                        }

                        var petItem = petEditControl.PetItem;
                        if (petItem is null)
                        {
                            MessageBox.Show("入力されたペット情報が取得できませんでした。");
                            return;
                        }
                        objectName = petItem.objectName;
                        // ItemAuxDataを込みで書きこむ
                        result = _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, petItem.Info, objectName, petItem.Aux);
                        break;

                    case "advancedTab":
                        item = GenerateItemBase();
                        objectName = objectNameTextBox.Text;
                        var newAuxData = new ItemAuxData(int.Parse(auxIndexTextBox.Text), auxDataTextBox.Text);
                        result = _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, objectName, newAuxData);
                        break;

                    default:
                        throw new InvalidOperationException();
                }

                if (result)
                {
                    EnableResultMessage($"{objectName}を作成しました。");
                }
                // 書き換え後の再読み込み
                LoadItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "書き込み処理に失敗しました。");
                File.AppendAllText(_errorLogFilePath, DateTime.Now + Environment.NewLine + ex.ToString());
                throw;
            }
        }

        private ItemInfo GenerateItemBase()
        {
            if (amountConstCheckBox.Checked)
            {
                amoutTextBox.Text = amountConst.Value.ToString();
            }
            return new(objectID: objectIdTextBox.Text, amount: amoutTextBox.Text, variation: variationTextBox.Text);
        }



        private async void EnableResultMessage(string message)
        {
            resultLabel.Text = message;
            resultLabel.Visible = true;
            await Task.Delay(3000);
            resultLabel.Visible = false;
        }

        private static void DetermineCookedAttributes(int baseObjectId, string rarity, out int fixedObjectId, out string fixedInternalName)
        {
            fixedObjectId = rarity switch
            {
                "コモン" => baseObjectId,
                "レア" => baseObjectId + (int)CookRarity.Rare,
                "エピック" => baseObjectId + (int)CookRarity.Epic,
                _ => throw new ArgumentException(null, nameof(rarity)),
            };
            string baseInternalName = StaticResource.AllCookedBaseCategories.Single(c => c.Info.objectID == baseObjectId).objectName;
            fixedInternalName = rarity switch
            {
                "コモン" => baseInternalName,
                "レア" => baseInternalName + "Rare",
                "エピック" => baseInternalName + "Epic",
                _ => throw new ArgumentException(null, nameof(rarity))
            };
        }

        /// <summary>
        /// 決まった食材Idから合成後の料理のvariation値を計算する。
        /// </summary>
        /// <param name="IdA">1つめの食材のId(dec)</param>
        /// <param name="IdB">2つめの食材のId(dec)</param>
        /// <returns></returns>
        public static int CalculateVariation(int IdA, int IdB)
        {
            // ゲーム内動作に合わせて降順に入れ替え
            if (IdA < IdB) (IdA, IdB) = (IdB, IdA);

            // 各IDを16ビットシフトして結合
            int combined = (IdA << 16) | IdB;
            return combined;
        }

        /// <summary>
        /// variationからIdへの逆算
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="ingredientA">材料の食材A</param>
        /// <param name="ingredientB">材料の食材B</param>
        public static void ReverseCalcurateVariation(int variation, out int ingredientA, out int ingredientB)
        {
            // 16ビット右にシフトして上位16ビットを取得
            ingredientA = variation >> 16;
            // 下位16ビットを取得
            ingredientB = variation & 0xFFFF;
        }

        private void SetDefaultButton_Click(object sender, EventArgs e)
        {
            objectIdTextBox.Text = ItemInfo.Default.objectID.ToString();
            variationTextBox.Text = ItemInfo.Default.variation.ToString();
            objectNameTextBox.Text = string.Empty;
            amoutTextBox.Text = ItemInfo.Default.amount.ToString();
            variationUpdateCountTextBox.Text = ItemInfo.Default.variationUpdateCount.ToString();
            auxIndexTextBox.Text = ItemAuxData.Default.index.ToString();
            auxDataTextBox.Text = ItemAuxData.Default.data.ToString();
        }

        private static bool IsCookedItem(int objectID, out CookRarity rarity, out int indexBaseOffset)
        {
            int[] cookedCategoryAllIds = StaticResource.AllCookedBaseCategories
                .Select(c => c.Info.objectID)
                .SelectMany(id => new[] { id, id + (int)CookRarity.Rare, id + (int)CookRarity.Epic })
                .OrderBy(id => id)
                .ToArray();
            int categorySize = StaticResource.AllCookedBaseCategories.Count;
            var commonIDs = cookedCategoryAllIds.Take(categorySize).ToArray();
            var rareIDs = cookedCategoryAllIds.Skip(categorySize).Take(categorySize).ToArray();
            if (commonIDs.Contains(objectID))
            {
                rarity = CookRarity.Common;
                indexBaseOffset = Array.IndexOf(commonIDs, objectID);
            }
            else if (rareIDs.Contains(objectID))
            {
                rarity = CookRarity.Rare;
                indexBaseOffset = Array.IndexOf(rareIDs, objectID);
            }
            else
            {
                rarity = CookRarity.Epic;
                var epicIDs = cookedCategoryAllIds.Skip(categorySize * 2).ToArray();
                indexBaseOffset = Array.IndexOf(epicIDs, objectID);
            }
            return cookedCategoryAllIds.Contains(objectID);
        }

        private void EnabeleUI()
        {
            saveSlotNoComboBox.Enabled = true;
            inventoryIndexComboBox.Enabled = true;
            createButton.Enabled = true;
            previousItemButton.Enabled = true;
            nextItemButton.Enabled = true;
            listUncreatedRecipesButton.Enabled = true;
        }
        private void DisabeleUI()
        {
            saveSlotNoComboBox.Enabled = false;
            inventoryIndexComboBox.Enabled = false;
            createButton.Enabled = false;
            previousItemButton.Enabled = false;
            nextItemButton.Enabled = false;
            listUncreatedRecipesButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.LastSaveFolderPath = SaveDataFolderPath;
            Settings.Default.Save();
        }

        private void savePathTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataFolderPath = savePathTextBox.Text;
        }
        private void previousItemButton_Click(object sender, EventArgs e)
        {
            if (inventoryIndexComboBox.SelectedIndex > 0)
            {
                inventoryIndexComboBox.SelectedIndex--;
            }
        }

        private void nextItemButton_Click(object sender, EventArgs e)
        {
            if (inventoryIndexComboBox.SelectedIndex < SaveDataManager.LoadItemLimit - 1)
            {
                inventoryIndexComboBox.SelectedIndex++;
            }
        }

        private void listUncreatedRecipesButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未作成の料理の組み合わせを出力します。");
            _saveDataManager.ListUncreatedRecipes();
        }

        private void openConditionsButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;

            var conditionForm = new ConditionForm();
            conditionForm.ShowDialog();
        }

        private void ingredientComboBoxA_DrawItem(object sender, DrawItemEventArgs e)
        {
            ingredientComboBox_DrawItem(sender, e);
        }

        private void ingredientComboBoxB_DrawItem(object sender, DrawItemEventArgs e)
        {
            ingredientComboBox_DrawItem(sender, e);
        }

        private void ingredientComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox combo = (ComboBox)sender;
            string selectedText = (string)combo.Items[e.Index]!;

            var displayNames = StaticResource.AllIngredients.Select(c => c.DisplayName);
            var goldernNames = displayNames
                .Where(name => name.StartsWith("金色の"))
                .Where(name => name != "金色のダート")
                .Where(name => name != "金色の幼虫肉")
                .Append("スターライトノーチラス");

            // 背景色を設定する
            if (goldernNames.Contains(selectedText))
            {
                // レア化食材
                e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
            }
            else if (displayNames.Contains(selectedText))
            {
                e.DrawBackground();
            }
            else
            {
                // 非食材もしくは旧食材
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }

            e.Graphics.DrawString(selectedText, e.Font!, Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            var item = Item.Default;
            try
            {
                item.Info = new(int.Parse(objectIdTextBox.Text),
                    int.Parse(amoutTextBox.Text),
                    int.Parse(variationTextBox.Text),
                    int.Parse(variationUpdateCountTextBox.Text));
                item.objectName = objectNameTextBox.Text;
                item.Aux = new(int.Parse(auxIndexTextBox.Text), auxDataTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("コピーできない値が含まれています。");
                return;
            }
            _saveDataManager.CopyItem(item);
            EnableResultMessage($"{objectNameTextBox.Text}をコピーしました。");
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            Item item = _saveDataManager.PasteItem();
            objectIdTextBox.Text = item.Info.objectID.ToString();
            amoutTextBox.Text = item.Info.amount.ToString();
            variationTextBox.Text = item.Info.variation.ToString();
            variationUpdateCountTextBox.Text = item.Info.variationUpdateCount.ToString();
            objectNameTextBox.Text = item.objectName;
            auxIndexTextBox.Text = item.Aux.index.ToString();
            auxDataTextBox.Text = item.Aux.data;
            EnableResultMessage($"{item.objectName}をペーストしました。");
        }

        private void inventryCopyButton_Click(object sender, EventArgs e)
        {
            _saveDataManager.CopyInventory();
            EnableResultMessage("インベントリを全てコピーしました。");
        }

        private void inventryPasteButton_Click(object sender, EventArgs e)
        {
            if (!Program.IsDeveloper)
            {
                if (_saveDataManager.HasOveredHealth(out _))
                {
                    MessageBox.Show("体力過剰のため、利用を制限します。", "注意");
                    return;
                }
                if (!_saveDataManager.IsClearData() && !_saveDataManager.IsCreativeData())
                {
                    MessageBox.Show("クリア済みでない場合は機能を制限します。\n通常クリア後にお楽しみください。");
                    return;
                }
            }
            if (_saveDataManager.HasCopiedInventory())
            {
                string assertion = "インベントリ全体をペーストしますか？\n上書きされたアイテムは戻りません。";
                bool accepet = MessageBox.Show(assertion, "確認", MessageBoxButtons.OKCancel) == DialogResult.OK;
                if (accepet)
                {
                    _saveDataManager.PasteInventory();
                    EnableResultMessage("インベントリを全てペーストしました。");
                    LoadItems();
                }
            }
            else
            {
                MessageBox.Show("コピーされたインベントリがありません。");
            }
        }

        private void openSkillButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;

            var skillPointForm = new SkillPointForm();
            skillPointForm.ShowDialog();
        }

        private void deleteDiscoveredReciepesButton_Click(object sender, EventArgs e)
        {
            string assertion = "発見済みのレシピを削除しますか？\n削除したレシピは戻りません。";
            bool accepet = MessageBox.Show(assertion, "確認", MessageBoxButtons.OKCancel) == DialogResult.OK;
            if (accepet)
            {
                _saveDataManager.DeleteAllRecipes();
                MessageBox.Show("全てのレシピの削除が完了しました。", "確認", MessageBoxButtons.OKCancel);
            }
        }

        private void slotReloadbutton_Click(object sender, EventArgs e)
        {
            LoadItems();
        }
    }
}
