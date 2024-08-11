using System.Diagnostics;

namespace CKFoodMaker
{
    public partial class Form1 : Form
    {
        private SaveDataManager _saveDataManager = new();
        private List<InternalItemInfo> _materialCategories = new();
        private List<InternalItemInfo> _cookedCategories = new();
        public string SaveDataFolderPath { get; set; } = string.Empty;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            SaveDataFolderPath = InitilizeFolderPath();
            LoadSlots();
            rarityComboBox.SelectedIndex = 0;
            itemSlotToolTip.SetToolTip(itemSlotLabel, "----で空枠、番号は入っているobjectIdです。表示はインベントリ内30個まで。");

            SetMaterialCategory();
            SetCookedCategory();
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

        private string InitilizeFolderPath(string dialogPath = "")
        {
            if (!string.IsNullOrEmpty(dialogPath))
            {
            }

            string appDataPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))!.FullName;
            string coreKeeperPath = dialogPath == "" ? Path.Combine(appDataPath, @"LocalLow\Pugstorm\Core Keeper") : dialogPath;
            if (Directory.Exists(coreKeeperPath))
            {
                savePathTextBox.Text = SaveDataFolderPath = coreKeeperPath;
            }
            else
            {
                savePathTextBox.Text = SaveDataFolderPath = appDataPath;
            }
            return savePathTextBox.Text;
        }

        private void LoadSlots()
        {
            inventoryIndexComboBox.Items.Clear();
            // セーブデータ一覧の取得
            string steamDirecotory = Path.Combine(savePathTextBox.Text, "Steam");
            SaveDataFolderPath = Path.Combine(Directory.GetDirectories(steamDirecotory).SingleOrDefault()!, "saves");
            string[] savePaths = new DirectoryInfo(SaveDataFolderPath).GetFiles(@"*.json").Select(fileInfo => fileInfo.FullName).ToArray();

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
            // リロード時のindex保持
            int selectedInventoryIndex = 0;
            if (inventoryIndexComboBox.Items.Count>0)
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
            saveFolderBrowserDialog.SelectedPath = Path.Combine(appDataPath, "LocalLow");
            saveFolderBrowserDialog.ShowDialog();
        }

        private void LoadPanel()
        {
            objectIdTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.objectID.ToString();
            amoutTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.amount.ToString();
            variationTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.variation.ToString();
            variationUpdateCountTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].item.variationUpdateCount.ToString();
            internalNameTextBox.Text = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex].objectName;
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
                int materialAId = int.Parse(_materialCategories
                .Single(c => c.DisplayName == materialComboBoxA.SelectedItem?
                .ToString())
                .ObjectId);
                int materialBId = int.Parse(_materialCategories
                    .Single(c => c.DisplayName == materialComboBoxB.SelectedItem?
                    .ToString())
                    .ObjectId);
                string calculatedVariation = CalcrateVariation(materialAId, materialBId);

                // レア度反映
                string baseObjectId = _cookedCategories.Single(c => c.DisplayName == cookedCategoryComboBox.SelectedItem!.ToString()).ObjectId;
                DetermineObjectAttributes(baseObjectId, rarityComboBox.SelectedItem?.ToString()!, out string fixedObjectId, out internalName);
                item = new(objectId: fixedObjectId,
                           amount: createdNumericNo.Value.ToString(),
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

        private void DetermineObjectAttributes(string baseObjectId, string rarity, out string fixedObjectId, out string fixedInternalName)
        {
            fixedObjectId = rarity switch
            {
                "コモン" => baseObjectId,
                "レア" => (int.Parse(baseObjectId) + 50).ToString(),
                "エピック" => (int.Parse(baseObjectId) + 75).ToString(),
                _ => throw new ArgumentException(nameof(rarity)),
            };
            string baseInternalName = _cookedCategories.Single(c => c.ObjectId == baseObjectId).InternalName;
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
        public static string CalcrateVariation(int IdA, int IdB)
        {
            // 各IDを16進数にし、1つの文字列としてつなげる。
            string combinedHex = IdA.ToString("X4") + IdB.ToString("X4");
            // 10進数に戻す
            var combinedDecimal = Convert.ToInt32(combinedHex, 16);
            return combinedDecimal.ToString();
        }

        private void SetDefaultButton_Click(object sender, EventArgs e)
        {
            objectIdTextBox.Text = Item.Default.objectID.ToString();
            amoutTextBox.Text = Item.Default.amount.ToString();
            variationTextBox.Text = Item.Default.variation.ToString();
            variationUpdateCountTextBox.Text = Item.Default.variationUpdateCount.ToString();
        }
    }
}
