using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CKFoodMaker
{
    public partial class Form1 : Form
    {
        const int _rareCorrectionValue = 50;
        const int _epicCorrectionValue = 75;
        static readonly string _errorLogFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"ErrorStackTrace.txt");

        private SaveDataManager _saveDataManager = new();
        private List<InternalItemInfo> _materialCategories = new();
        private List<InternalItemInfo> _cookedCategories = new();

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
            Initialize();
        }

        public void Initialize()
        {
            try
            {
                SetMaterialCategory();
                SetCookedCategory();
                rarityComboBox.SelectedIndex = 0;
                itemSlotToolTip.SetToolTip(itemSlotLabel, "----で空枠、番号は入っているobjectIdです。表示はインベントリ内30個まで。");

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

        private void SetMaterialCategory()
        {
            string materialCatergoryDefineFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "MaterialCategory.csv")
                ?? throw new FileNotFoundException($"MaterialCategory.csvが見つかりません。");
            var materialCategories = File.ReadAllLines(materialCatergoryDefineFilePath).ToArray();
            foreach (var line in materialCategories)
            {
                string[] words = line.Split(',');
                if (!Enum.TryParse(words[3], out MaterialSubCategory subCategory))
                {
                    throw new ArgumentException($"無効なMaterialSubCategory:{words[3]}");
                }

                _materialCategories.Add(new(objectId: words[0],
                                          internalName: words[1],
                                          displayName: words[2],
                                          subCategory: subCategory));
            }

            //hack 視認性向上のサブカテゴリネームを追加するか検討
            var sortedMaterialCategories = _materialCategories.OrderBy(c => c.SubCategory).Select(c => c.DisplayName).ToArray();
            materialComboBoxA.Items.AddRange(sortedMaterialCategories);
            materialComboBoxB.Items.AddRange(sortedMaterialCategories);

            materialComboBoxA.SelectedIndex = 0;
            materialComboBoxB.SelectedIndex = 0;
        }

        private void SetCookedCategory()
        {
            string cookedCatergoryDefineFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "CookedCategory.csv")
                ?? throw new FileNotFoundException($"CookedCategory.csvが見つかりません。");
            var cookedCategories = File.ReadAllLines(cookedCatergoryDefineFilePath).ToArray();
            foreach (var line in cookedCategories)
            {
                string[] words = line.Split(',');
                _cookedCategories.Add(new(objectId: words[0], internalName: words[1], displayName: words[2]));
                cookedCategoryComboBox.Items.Add(words[2]);
            }
            cookedCategoryComboBox.SelectedIndex = 0;
        }

        private void InitilizeFolderPath()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.LastSaveFolderPath))
            {
                SaveDataFolderPath = Properties.Settings.Default.LastSaveFolderPath;
            }
            else
            {
                string appDataPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))!.FullName;
                string generalPath = Path.Combine(appDataPath, @"LocalLow\Pugstorm\Core Keeper\Steam");
                if (Directory.GetDirectories(generalPath).Count() is 0)
                {
                    SaveDataFolderPath = generalPath;
                    return;
                }
                SaveDataFolderPath = Path.Combine(Directory.GetDirectories(generalPath).SingleOrDefault()!, "saves");
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
            Regex regex = new Regex(@"^\d{1,2}|debug$", RegexOptions.Compiled);
            string[] savePaths = new DirectoryInfo(SaveDataFolderPath).GetFiles(@"*.json")
                .Where(file => regex.IsMatch(Path.GetFileNameWithoutExtension(file.Name)))
                .Select(fileInfo => fileInfo.FullName).ToArray();
            if (savePaths.Length is 0)
            {
                DisabeleUI();
                return;
            }
            else
            {
                EnabeleUI();
            }

            foreach (string savePath in savePaths)
            {
                var saveNo = Path.GetFileNameWithoutExtension(savePath);
                saveSlotNoComboBox.Items.Add(saveNo);
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
            string selecetedSaveDataPath = Path.Combine(SaveDataFolderPath, saveSlotNoComboBox.SelectedItem?.ToString() + ".json");
            _saveDataManager = new(selecetedSaveDataPath);

            // 選択中のセーブデータのアイテム情報をinventoryIndexComboBoxに反映する
            foreach (var itemInfo in _saveDataManager.Items)
            {
                if (itemInfo.item == Item.Default)
                {
                    inventoryIndexComboBox.Items.Add($"----");
                }
                else
                {
                    inventoryIndexComboBox.Items.Add($"{itemInfo.objectName}");
                }
            }
            inventoryIndexComboBox.SelectedIndex = selectedInventoryIndex;

            LoadPanel();
        }

        private void saveSlotNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void toMaxButton_Click(object sender, EventArgs e)
        {
            createdNumericNo.Value = 999;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createdNumericNo.Value = -1;
        }

        private void openSevePathDialogButton_Click(object sender, EventArgs e)
        {
            string appDataPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))!.FullName;
            string generalPath = Path.Combine(appDataPath, @"LocalLow\Pugstorm\Core Keeper\Steam");
            if (Directory.GetDirectories(generalPath).Count() is 0)
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

        private void LoadPanel()
        {
            int selectedObjectID = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.objectID;
            objectIdTextBox.Text = selectedObjectID.ToString();
            amoutTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.amount.ToString();
            int variation = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.variation;
            variationTextBox.Text = variation.ToString();
            variationUpdateCountTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.variationUpdateCount.ToString();
            internalNameTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].objectName;

            // 料理の場合は解析情報をセットする
            if (IsCookedItem(selectedObjectID))
            {
                ReverseCalcurateVariation(variation, out var materialIDA, out var materialIDB);
                materialComboBoxA.SelectedItem = _materialCategories.SingleOrDefault(c => c.ObjectID == materialIDA)?.DisplayName;
                materialComboBoxB.SelectedItem = _materialCategories.SingleOrDefault(c => c.ObjectID == materialIDB)?.DisplayName;

                var baseCookedArray = _cookedCategories.Select(c => c.ObjectID).ToArray();
                if (baseCookedArray.Contains(selectedObjectID))
                {
                    rarityComboBox.SelectedIndex = 0;
                    cookedCategoryComboBox.SelectedIndex = Array.IndexOf(baseCookedArray, selectedObjectID);

                }
                else if (baseCookedArray.Select(c => c + _rareCorrectionValue).Contains(selectedObjectID))
                {
                    rarityComboBox.SelectedIndex = 1;
                    cookedCategoryComboBox.SelectedIndex = Array.IndexOf(baseCookedArray.Select(c => c + _rareCorrectionValue).ToArray(), selectedObjectID);
                }
                else
                {
                    rarityComboBox.SelectedIndex = 2;
                    cookedCategoryComboBox.SelectedIndex = Array.IndexOf(baseCookedArray.Select(c => c + _epicCorrectionValue).ToArray(), selectedObjectID);
                }
                createdNumericNo.Value = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.amount;
            }
        }

        private void inventoryIndexComboBox_TextChanged(object sender, EventArgs e)
        {
            LoadPanel();
        }

        private void objectIdsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "https://core-keeper.fandom.com/wiki/Object_IDs");
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            Item item;
            string internalName;
            if (itemEditTabControl.SelectedIndex == 0)
            {
                // 料理作成タブ操作
                int materialAId = _materialCategories
                    .Single(c => c.DisplayName == materialComboBoxA
                    .SelectedItem?.ToString()).ObjectID;
                int materialBId = _materialCategories
                    .Single(c => c.DisplayName == materialComboBoxB
                    .SelectedItem?.ToString()).ObjectID;
                int calculatedVariation = CalcrateVariation(materialAId, materialBId);

                // レア度反映
                int baseObjectId = _cookedCategories.Single(c => c.DisplayName == cookedCategoryComboBox.SelectedItem!.ToString()).ObjectID;
                DetermineObjectAttributes(baseObjectId, rarityComboBox.SelectedItem?.ToString()!, out int fixedObjectId, out internalName);
                item = new(objectId: fixedObjectId,
                           amount: Convert.ToInt32(createdNumericNo.Value),
                           variation: calculatedVariation);
            }
            else
            {
                // 上級者向けタブ操作
                if (objectIdTextBox.Text == string.Empty ||
                    amoutTextBox.Text == string.Empty ||
                    variationTextBox.Text == string.Empty ||
                    variationUpdateCountTextBox.Text == string.Empty ||
                    internalNameTextBox.Text == string.Empty)
                {
                    MessageBox.Show("空欄があります。", "入力値不備", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                item = new(objectId: objectIdTextBox.Text, amount: amoutTextBox.Text, variation: variationTextBox.Text);
                internalName = internalNameTextBox.Text;
            }
            _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, internalName);
            EnableResultMessage($"{internalName}の作成に成功しました。");

            // 書き換え後の再読み込み
            LoadItems();
        }

        private async void EnableResultMessage(string message)
        {
            resultLabel.Text = message;
            resultLabel.Visible = true;
            await Task.Delay(3000);
            resultLabel.Visible = false;
        }

        private void DetermineObjectAttributes(int baseObjectId, string rarity, out int fixedObjectId, out string fixedInternalName)
        {
            fixedObjectId = rarity switch
            {
                "コモン" => baseObjectId,
                "レア" => baseObjectId + 50,
                "エピック" => baseObjectId + 75,
                _ => throw new ArgumentException(nameof(rarity)),
            };
            string baseInternalName = _cookedCategories.Single(c => c.ObjectID == baseObjectId).InternalName;
            fixedInternalName = rarity switch
            {
                "コモン" => baseInternalName,
                "レア" => baseInternalName + "Rare",
                "エピック" => baseInternalName + "Epic",
                _ => throw new ArgumentException(nameof(rarity))
            };
        }

        /// <summary>
        /// 決まった食材Idから合成後の料理のvariation値を計算する。
        /// </summary>
        /// <param name="IdA">1つめの食材のId(dec)</param>
        /// <param name="IdB">2つめの食材のId(dec)</param>
        /// <returns></returns>
        public static int CalcrateVariation(int IdA, int IdB)
        {
            // ゲーム内動作に合わせで降順に入れ替え
            if (IdA < IdB)
            {
                var _ = IdA;
                IdA = IdB;
                IdB = _;
            }
            // 各IDを16進数にし、1つの文字列としてつなげる。
            string combinedHex = IdA.ToString("X4") + IdB.ToString("X4");
            // 10進数に戻す
            var combinedDecimal = Convert.ToInt32(combinedHex, 16);
            return combinedDecimal;
        }

        private void SetDefaultButton_Click(object sender, EventArgs e)
        {
            objectIdTextBox.Text = Item.Default.objectID.ToString();
            amoutTextBox.Text = Item.Default.amount.ToString();
            variationTextBox.Text = Item.Default.variation.ToString();
            variationUpdateCountTextBox.Text = Item.Default.variationUpdateCount.ToString();
        }

        /// <summary>
        /// variationからIdへの逆算
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="materialA">材料の食材A</param>
        /// <param name="materialB">材料の食材B</param>
        public static void ReverseCalcurateVariation(int variation, out int materialA, out int materialB)
        {
            string combinedHex = variation.ToString("X8");
            string strA = combinedHex.Substring(0, 4);
            string strB = combinedHex.Substring(4);
            materialA = Convert.ToInt32(strA, 16);
            materialB = Convert.ToInt32(strB, 16);
        }

        private bool IsCookedItem(int objectID)
        {
            List<int> cookedCategoryIds = _cookedCategories
                .SelectMany(id => new[] { id.ObjectID, id.ObjectID + _rareCorrectionValue, id.ObjectID + _epicCorrectionValue })
                .ToList();
            return cookedCategoryIds.Contains(objectID);
        }

        private void EnabeleUI()
        {
            saveSlotNoComboBox.Enabled = true;
            inventoryIndexComboBox.Enabled = true;
            createButton.Enabled = true;
            previousItemButton.Enabled = true;
            nextItemButton.Enabled = true;
        }
        private void DisabeleUI()
        {
            saveSlotNoComboBox.Enabled = false;
            inventoryIndexComboBox.Enabled = false;
            createButton.Enabled = false;
            previousItemButton.Enabled = false;
            nextItemButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastSaveFolderPath = SaveDataFolderPath;
            Properties.Settings.Default.Save();
        }

        private void savePathTextBox_TextChanged(object sender, EventArgs e)
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
    }
}
