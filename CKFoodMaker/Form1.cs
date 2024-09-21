using System.Diagnostics;
using System.Text.RegularExpressions;
using CKFoodMaker.Model;
using CKFoodMaker.Model.ItemAux;
using CKFoodMaker.Resource;

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
            if (Program.IsDeveloper) 
            {
                Text += "(Dev)";
            }
        }

        public void Initialize()
        {
            try
            {
                SetMaterialCategory();
                SetCookedCategory();
                SetPetTalentCategory();
                rarityComboBox.SelectedIndex = 0;
                itemSlotToolTip.SetToolTip(itemSlotLabel, "----で空枠、番号は入っているobjectIdです。表示はインベントリ内50個まで。");
                
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
            // hack StaticResource.AllFoodMaterialsからの読み込みを使い、csvからは本来調理しない追加料理だけを保持するようにする
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

                _materialCategories.Add(new(objectID: int.Parse(words[0]),
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
            foreach (var category in StaticResource.AllCookedBaseCategories)
            {
                _cookedCategories.Add(new(category.objectID, category.InternalName, category.JapaneseResource));
                cookedCategoryComboBox.Items.Add(category.JapaneseResource);
            }
            cookedCategoryComboBox.SelectedIndex = 0;
        }

        private void SetPetTalentCategory()
        {
            var petKinds = Enum.GetNames<PetType>();
            petKindComboBox.Items.AddRange(petKinds);
            var petColors = Enum.GetNames<PetColor>();
            petColorComboBox.Items.AddRange(petColors);

            string TalentFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "PetTalent.csv");
            //var allPetTalents = File.ReadAllLines(TalentFilePath);
            var TalentIds = File.ReadAllLines(TalentFilePath)
                .Select(line => line.Split(",").Take(2))
                .Select(info=>string.Join(":",info))
                .ToArray();    // Idと効果確認だけの暫定処理
            //hack 同一TalentIdでもペットにより異なるスキルについて、支援タイプのペットか判定によって一部スキルをswitchしつつセットするよう変える。
            petTalent1ComboBox.Items.AddRange(TalentIds);
            petTalent2ComboBox.Items.AddRange(TalentIds);
            petTalent3ComboBox.Items.AddRange(TalentIds);
            petTalent4ComboBox.Items.AddRange(TalentIds);
            petTalent5ComboBox.Items.AddRange(TalentIds);
            petTalent6ComboBox.Items.AddRange(TalentIds);
            petTalent7ComboBox.Items.AddRange(TalentIds);
            petTalent8ComboBox.Items.AddRange(TalentIds);
            petTalent9ComboBox.Items.AddRange(TalentIds);
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
            int indexNo = 1;
            foreach (var itemInfo in _saveDataManager.Items)
            {
                if (itemInfo.item == ItemBase.Default)
                {
                    inventoryIndexComboBox.Items.Add($"{indexNo,2} : ----");
                }
                else
                {
                    inventoryIndexComboBox.Items.Add($"{indexNo,2} : {itemInfo.objectName}");
                }
                indexNo++;
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
            createdNumericNo.Value = 9999;
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
            var selectedItem = _saveDataManager.Items[inventoryIndexComboBox.SelectedIndex];
            int selectedObjectID = selectedItem.item.objectID;
            int variation = selectedItem.item.variation;

            objectIdTextBox.Text = selectedObjectID.ToString();
            amoutTextBox.Text = selectedItem.item.amount.ToString();
            variationTextBox.Text = variation.ToString();
            variationUpdateCountTextBox.Text = selectedItem.item.variationUpdateCount.ToString();
            internalNameTextBox.Text = selectedItem.objectName;

            // 料理の場合は料理情報をセットする
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
                createdNumericNo.Value = selectedItem.item.amount;
            }

            // ペットの場合はAuxDataをセットする
            IEnumerable<int> allPetIds = Enum.GetValues(typeof(PetType)).Cast<int>();
            if (allPetIds.Contains(selectedItem.item.objectID))
            {
                InitLoadedPetTab(selectedItem.auxData, selectedItem.item);
            }
            else
            {
                ResetPetTab();
            }
        }

        private void InitLoadedPetTab(ItemAuxData auxData, ItemBase item)
        {
            auxData.GetPetData(out var name, out var color, out var talents);
            petKindComboBox.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(PetType)).Cast<int>().ToArray(), item.objectID);
            petColorComboBox.SelectedIndex = color;
            petExpNumeric.Value = item.amount;
            petNameTextBox.Text = name;

            InitLoadedPetTalents(talents);
        }

        private void ResetPetTab()
        {
            petKindComboBox.SelectedIndex = -1;
            petColorComboBox.SelectedIndex = -1;
            petExpNumeric.Value = 0;
            petNameTextBox.Text = string.Empty;
            petTalent1ComboBox.SelectedIndex = -1;
            petTalent2ComboBox.SelectedIndex = -1;
            petTalent3ComboBox.SelectedIndex = -1;
            petTalent4ComboBox.SelectedIndex = -1;
            petTalent5ComboBox.SelectedIndex = -1;
            petTalent6ComboBox.SelectedIndex = -1;
            petTalent7ComboBox.SelectedIndex = -1;
            petTalent8ComboBox.SelectedIndex = -1;
            petTalent9ComboBox.SelectedIndex = -1;
            petTalent1ValidCheckBox.Checked = false;
            petTalent2ValidCheckBox.Checked = false;
            petTalent3ValidCheckBox.Checked = false;
            petTalent4ValidCheckBox.Checked = false;
            petTalent5ValidCheckBox.Checked = false;
            petTalent6ValidCheckBox.Checked = false;
            petTalent7ValidCheckBox.Checked = false;
            petTalent8ValidCheckBox.Checked = false;
            petTalent9ValidCheckBox.Checked = false;
        }

        private void InitLoadedPetTalents(List<PetTalent> talents)
        {
            petTalent1ValidCheckBox.Checked = talents[0].Points == 1 ? true : false;
            petTalent1ComboBox.SelectedIndex = talents[0].Talent;
            petTalent2ValidCheckBox.Checked = talents[1].Points == 1 ? true : false;
            petTalent2ComboBox.SelectedIndex = talents[1].Talent;
            petTalent3ValidCheckBox.Checked = talents[2].Points == 1 ? true : false;
            petTalent3ComboBox.SelectedIndex = talents[2].Talent;
            petTalent4ValidCheckBox.Checked = talents[3].Points == 1 ? true : false;
            petTalent4ComboBox.SelectedIndex = talents[3].Talent;
            petTalent5ValidCheckBox.Checked = talents[4].Points == 1 ? true : false;
            petTalent5ComboBox.SelectedIndex = talents[4].Talent;
            petTalent6ValidCheckBox.Checked = talents[5].Points == 1 ? true : false;
            petTalent6ComboBox.SelectedIndex = talents[5].Talent;
            petTalent7ValidCheckBox.Checked = talents[6].Points == 1 ? true : false;
            petTalent7ComboBox.SelectedIndex = talents[6].Talent;
            petTalent8ValidCheckBox.Checked = talents[7].Points == 1 ? true : false;
            petTalent8ComboBox.SelectedIndex = talents[7].Talent;
            petTalent9ValidCheckBox.Checked = talents[8].Points == 1 ? true : false;
            petTalent9ComboBox.SelectedIndex = talents[8].Talent;
        }

        private void ResetPetUI()
        {
            petKindComboBox.SelectedIndex = 0;
            petColorComboBox.SelectedIndex = 0;
            petExpNumeric.Value = 0;
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
            if (!Program.IsDeveloper)
            {
                if (!_saveDataManager.IsClearData())
                {
                    MessageBox.Show("クリア済みでない場合は機能を制限します。\n通常クリア後にお楽しみください。");
                    return;
                }
                if (_saveDataManager.HasOveredHealth(out int overedHealth))
                {
                    MessageBox.Show($"Code : {overedHealth}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            bool result = false;
            ItemBase item;
            string internalName;
            try
            {
                if (itemEditTabControl.SelectedIndex is 0)
                {
                    int materialAId = _materialCategories
                        .Single(c => c.DisplayName == materialComboBoxA
                        .SelectedItem?.ToString()).ObjectID;
                    int materialBId = _materialCategories
                        .Single(c => c.DisplayName == materialComboBoxB
                        .SelectedItem?.ToString()).ObjectID;
                    int calculatedVariation = CalculateVariation(materialAId, materialBId);

                    // レア度反映
                    int baseObjectId = _cookedCategories.Single(c => c.DisplayName == cookedCategoryComboBox.SelectedItem!.ToString()).ObjectID;
                    DetermineObjectAttributes(baseObjectId, rarityComboBox.SelectedItem?.ToString()!, out int fixedObjectId, out internalName);
                    item = new(objectID: fixedObjectId,
                               amount: Convert.ToInt32(createdNumericNo.Value),
                               variation: calculatedVariation);
                    _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, internalName);
                    result = true;
                }
                else if (itemEditTabControl.SelectedIndex is 1)
                {
                    item = GenerateItemBase();
                    internalName = internalNameTextBox.Text;
                    _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, internalName);
                    result = true;
                }
                else
                {
                    // ペットTabの処理
                    var allPetTypes = (PetType[])Enum.GetValues(typeof(PetType));
                    var petType = allPetTypes[petKindComboBox.SelectedIndex];
                    var petTalents = GeneratePetTalentLists();
                    var allPetColors = (PetColor[])Enum.GetValues(typeof(PetColor));
                    var petColor = allPetColors[petColorComboBox.SelectedIndex];
                    string petName = petNameTextBox.Text;
                    item = new(objectID: (int)allPetTypes[petKindComboBox.SelectedIndex],
                        amount: (int)petExpNumeric.Value,
                        variation: 0);

                    internalName = Enum.GetNames(typeof(PetType))[petKindComboBox.SelectedIndex];

                    // ItemAuxDataを込みで書きこむ
                    result = _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, internalName, petName, petColor, petTalents);
                }

                if (result)
                {
                    EnableResultMessage($"{internalName}を作成しました。");
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

        private ItemBase GenerateItemBase()
        {
            if (amountConstCheckBox.Checked)
            {
                amoutTextBox.Text = amountConst.Value.ToString();
            }
            return new(objectID: objectIdTextBox.Text, amount: amoutTextBox.Text, variation: variationTextBox.Text);
        }

        private List<PetTalent> GeneratePetTalentLists()
        {
            List<PetTalent> talentList = new(9);

            talentList.Add(new(petTalent1ComboBox.SelectedIndex, petTalent1ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent2ComboBox.SelectedIndex, petTalent2ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent3ComboBox.SelectedIndex, petTalent3ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent4ComboBox.SelectedIndex, petTalent4ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent5ComboBox.SelectedIndex, petTalent5ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent6ComboBox.SelectedIndex, petTalent6ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent7ComboBox.SelectedIndex, petTalent7ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent8ComboBox.SelectedIndex, petTalent8ValidCheckBox.Checked is true ? 1 : 0));
            talentList.Add(new(petTalent9ComboBox.SelectedIndex, petTalent9ValidCheckBox.Checked is true ? 1 : 0));

            return talentList;
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
        public static int CalculateVariation(int IdA, int IdB)
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
            objectIdTextBox.Text = ItemBase.Default.objectID.ToString();
            amoutTextBox.Text = ItemBase.Default.amount.ToString();
            variationTextBox.Text = ItemBase.Default.variation.ToString();
            variationUpdateCountTextBox.Text = ItemBase.Default.variationUpdateCount.ToString();
            internalNameTextBox.Text = string.Empty;
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
            unlockAllRecipeButton.Enabled = true;
        }
        private void DisabeleUI()
        {
            saveSlotNoComboBox.Enabled = false;
            inventoryIndexComboBox.Enabled = false;
            createButton.Enabled = false;
            previousItemButton.Enabled = false;
            nextItemButton.Enabled = false;
            unlockAllRecipeButton.Enabled = false;
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

        private void unlockAllRecipeButton_Click(object sender, EventArgs e)
        {
            _saveDataManager.UnlockAllResipe();

            // todo レシピ全追加が実現したらこちらを有効化する。
            //var result = MessageBox.Show("この操作は元に戻せません。実行しますか？", "確認", MessageBoxButtons.YesNo);
            //if (result is DialogResult.Yes)
            //{
            //    _saveDataManager.UnlockAllResipe();
            //}
        }
    }
}
