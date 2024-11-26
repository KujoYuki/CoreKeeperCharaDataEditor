using System.Diagnostics;
using System.Text.RegularExpressions;
using CKFoodMaker.Model;
using CKFoodMaker.Model.ItemAux;
using CKFoodMaker.Resource;

namespace CKFoodMaker
{
    public partial class Form1 : Form
    {

        static readonly string _errorLogFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"ErrorStackTrace.txt");

        private SaveDataManager _saveDataManager = SaveDataManager.Instance;
        private List<InternalItemInfo> _materialCategories = [];
        private List<InternalItemInfo> _cookedCategories = StaticResource.AllCookedBaseCategories.ToList();

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
                variationUpdateCountTextBox.ReadOnly = false;
                auxDataTextBox.ReadOnly = false;
                toMinusOneButton.Visible = true;
                createdNumericNo.Maximum = 869779;
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

                InitilizeFolderPath();
                if (Directory.Exists(SaveDataFolderPath))
                {
                    LoadSlots();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "�����������Ɏ��s���܂����B");
                File.AppendAllText(_errorLogFilePath, DateTime.Now + Environment.NewLine + ex.ToString());
                throw;
            }
        }

        private void SetMaterialCategory()
        {
            var defaultMaterials = StaticResource.AllFoodMaterials
                .Select(c => new InternalItemInfo(c.objectID, c.InternalName, c.DisplayName))
                .ToArray();

            string additionalFoodMaterialFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "AdditionalFoodMaterial.csv")
                ?? throw new FileNotFoundException($"AdditionalFoodMaterial.csv��������܂���B");
            var materialCategories = File.ReadAllLines(additionalFoodMaterialFilePath)
                .Select(line =>
                {
                    string[] words = line.Split(',');
                    return new InternalItemInfo(int.Parse(words[0]), words[1], words[2]);
                })
                .ToArray();
            var allMaterials = defaultMaterials.Concat(materialCategories)
                .OrderBy(c => c.ObjectID)
                .ToList();
            // �J���҃��[�h�̏ꍇ�͔񐄏��H�ނ��\��
            if (Program.IsDeveloper)
            {
                var obsoleteFood = StaticResource.ObsoleteFoodMaterials
                    .Select(c => new InternalItemInfo(c.objectID, c.InternalName, c.DisplayName))
                    .ToArray();
                allMaterials.AddRange(obsoleteFood);
            }
            _materialCategories.AddRange(allMaterials);

            var sortedMaterialNames = _materialCategories
                .Select(c => c.DisplayName)
                .ToArray();
            materialComboBoxA.Items.AddRange(sortedMaterialNames);
            materialComboBoxB.Items.AddRange(sortedMaterialNames);

            materialComboBoxA.SelectedIndex = 0;
            materialComboBoxB.SelectedIndex = 0;
        }

        private void SetCookedCategory()
        {
            var cookedCategoryNames = StaticResource.AllCookedBaseCategories
                .Select(c => c.DisplayName)
                .ToArray();
            cookedCategoryComboBox.Items.AddRange(cookedCategoryNames);
            cookedCategoryComboBox.SelectedIndex = 0;
        }

        private void SetPetTalentCategory()
        {
            var petKinds = Enum.GetNames<PetType>();
            petKindComboBox.Items.AddRange(petKinds);
            var petColors = Enum.GetNames<PetColor>();
            petColorComboBox.Items.AddRange(petColors);

            var TalentIds = StaticResource.PetTalents
                .Select(t => string.Join(":", t.TalentId, t.Name))
                .ToArray();
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
                    MessageBox.Show("�Z�[�u�f�[�^�t�H���_��������܂���ł����B", "�x��", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // �Z�[�u�f�[�^�ꗗ�̎擾
            Regex regex = new(@"^\d{1,2}|debug$", RegexOptions.Compiled);
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
            // �����[�h����index�ێ��ƃN���A
            int selectedInventoryIndex = 0;
            if (inventoryIndexComboBox.Items.Count > 0)
            {
                selectedInventoryIndex = inventoryIndexComboBox.SelectedIndex;
            }
            inventoryIndexComboBox.Items.Clear();

            // �I�����ꂽ�Z�[�u�f�[�^�̃t�@�C���̃A�C�e���ǂݍ���
            string selecetedSaveDataPath = Path.Combine(SaveDataFolderPath, saveSlotNoComboBox.SelectedItem?.ToString() + ".json");
            _saveDataManager.SaveDataPath = selecetedSaveDataPath;

            // �I�𒆂̃Z�[�u�f�[�^�̃A�C�e������inventoryIndexComboBox�ɔ��f����
            int indexNo = 1;
            foreach (var (item, objectName, auxData) in _saveDataManager.Items)
            {
                if (item == ItemBase.Default)
                {
                    inventoryIndexComboBox.Items.Add($"{indexNo,2} : ----");
                }
                else
                {
                    inventoryIndexComboBox.Items.Add($"{indexNo,2} : {objectName}");
                }
                indexNo++;
            }
            inventoryIndexComboBox.SelectedIndex = selectedInventoryIndex;

            LoadPanel();
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
            objectNameTextBox.Text = selectedItem.objectName;
            auxIndexTextBox.Text = selectedItem.auxData.index.ToString();
            auxDataTextBox.Text = selectedItem.auxData.data;

            // �����̏ꍇ�͗��������Z�b�g����
            if (IsCookedItem(selectedObjectID, out var rarity, out var indexBaseOffset))
            {
                ReverseCalcurateVariation(variation, out var materialIDA, out var materialIDB);
                materialComboBoxA.SelectedItem = _materialCategories.SingleOrDefault(c => c.ObjectID == materialIDA)?.DisplayName;
                materialComboBoxB.SelectedItem = _materialCategories.SingleOrDefault(c => c.ObjectID == materialIDB)?.DisplayName;

                cookedCategoryComboBox.SelectedIndex = indexBaseOffset;
                rarityComboBox.SelectedIndex = rarity switch
                {
                    CookRarity.Common => 0,
                    CookRarity.Rare => 1,
                    CookRarity.Epic => 2,
                    _ => throw new NotImplementedException(),
                };
                createdNumericNo.Value = selectedItem.item.amount;
            }

            IEnumerable<int> allPetIds = Enum.GetValues(typeof(PetType)).Cast<int>();
            if (allPetIds.Contains(selectedItem.item.objectID))
            {
                // �y�b�g�̏ꍇ��AuxData���Z�b�g����
                InitLoadedPetTab(selectedItem.auxData, selectedItem.item);
            }
            else
            {
                ResetPetTab();
            }
        }

        private void saveSlotNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void toMaxButton_Click(object sender, EventArgs e)
        {
            createdNumericNo.Value = Program.IsDeveloper ? 869779 : 9999;
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
                // ���[�U�[ID�t�H���_������Ȃ��ꍇ��Steam�t�H���_�ŗ��ߒu��
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
            petTalent1ValidCheckBox.Checked = talents[0].Points == 1;
            petTalent1ComboBox.SelectedIndex = talents[0].Talent;
            petTalent2ValidCheckBox.Checked = talents[1].Points == 1;
            petTalent2ComboBox.SelectedIndex = talents[1].Talent;
            petTalent3ValidCheckBox.Checked = talents[2].Points == 1;
            petTalent3ComboBox.SelectedIndex = talents[2].Talent;
            petTalent4ValidCheckBox.Checked = talents[3].Points == 1;
            petTalent4ComboBox.SelectedIndex = talents[3].Talent;
            petTalent5ValidCheckBox.Checked = talents[4].Points == 1;
            petTalent5ComboBox.SelectedIndex = talents[4].Talent;
            petTalent6ValidCheckBox.Checked = talents[5].Points == 1;
            petTalent6ComboBox.SelectedIndex = talents[5].Talent;
            petTalent7ValidCheckBox.Checked = talents[6].Points == 1;
            petTalent7ComboBox.SelectedIndex = talents[6].Talent;
            petTalent8ValidCheckBox.Checked = talents[7].Points == 1;
            petTalent8ComboBox.SelectedIndex = talents[7].Talent;
            petTalent9ValidCheckBox.Checked = talents[8].Points == 1;
            petTalent9ComboBox.SelectedIndex = talents[8].Talent;
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
            Process.Start(new ProcessStartInfo("https://github.com/KujoYuki/CoreKeeperFoodEditor/blob/main/Document/parameter.md") { UseShellExecute = true });
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (!Program.IsDeveloper)
            {
                if (!_saveDataManager.IsClearData())
                {
                    MessageBox.Show("�N���A�ς݂łȂ��ꍇ�͋@�\�𐧌����܂��B\n�ʏ�N���A��ɂ��y���݂��������B");
                    return;
                }
                if (_saveDataManager.HasOveredHealth(out _))
                {
                    MessageBox.Show("�̗͉ߏ�̂��߁A���p�𐧌����܂��B", "����");
                    return;
                }
            }

            bool result = false;
            ItemBase item;
            string objectName;
            try
            {
                switch (itemEditTabControl.SelectedTab?.Name)
                {
                    case "foodTab":
                        int materialAId = _materialCategories
                        .Single(c => c.DisplayName == materialComboBoxA
                        .SelectedItem?.ToString()).ObjectID;
                        int materialBId = _materialCategories
                            .Single(c => c.DisplayName == materialComboBoxB
                            .SelectedItem?.ToString()).ObjectID;
                        int calculatedVariation = CalculateVariation(materialAId, materialBId);

                        // ���A�x���f
                        int baseObjectId = _cookedCategories.Single(c => c.DisplayName == cookedCategoryComboBox.SelectedItem!.ToString()).ObjectID;
                        DetermineCookedAttributes(baseObjectId, rarityComboBox.SelectedItem?.ToString()!, out int fixedObjectId, out objectName);
                        item = new(objectID: fixedObjectId,
                                   amount: Convert.ToInt32(createdNumericNo.Value),
                                   variation: calculatedVariation);
                        _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, objectName);
                        result = true;
                        break;

                    case "petTab":
                        if (!Enum.GetValues(typeof(PetType)).Cast<int>().Contains(int.Parse(objectIdTextBox.Text)))
                        {
                            MessageBox.Show("�I�𒆂̃A�C�e�����y�b�g�ł͂���܂���B\n�C���x���g���g�Ńy�b�g�A�C�e����I�����ĕҏW���Ă��������B");
                            return;
                        }
                        var allPetTypes = (PetType[])Enum.GetValues(typeof(PetType));
                        var allPetColors = (PetColor[])Enum.GetValues(typeof(PetColor));
                        var auxData = _saveDataManager.GetAuxData(inventoryIndexComboBox.SelectedIndex);
                        auxData.AuxPrefabManager?.UpdatePet(
                            petNameTextBox.Text,
                            allPetColors[petColorComboBox.SelectedIndex],
                            GeneratePetTalentLists());
                        item = new(objectID: (int)allPetTypes[petKindComboBox.SelectedIndex],
                            amount: (int)petExpNumeric.Value,
                            variation: 0);
                        objectName = Enum.GetNames(typeof(PetType))[petKindComboBox.SelectedIndex];

                        // ItemAuxData�����݂ŏ�������
                        result = _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, objectName, auxData);
                        break;

                    case "advancedTab":
                        item = GenerateItemBase();
                        objectName = objectNameTextBox.Text;
                        result = _saveDataManager.WriteItemData(inventoryIndexComboBox.SelectedIndex, item, objectName);
                        break;

                    default:
                        throw new InvalidOperationException();
                }

                if (result)
                {
                    EnableResultMessage($"{objectName}���쐬���܂����B");
                }
                // ����������̍ēǂݍ���
                LoadItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "�������ݏ����Ɏ��s���܂����B");
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
            List<PetTalent> talentList =
            [
                new(petTalent1ComboBox.SelectedIndex, petTalent1ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent2ComboBox.SelectedIndex, petTalent2ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent3ComboBox.SelectedIndex, petTalent3ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent4ComboBox.SelectedIndex, petTalent4ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent5ComboBox.SelectedIndex, petTalent5ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent6ComboBox.SelectedIndex, petTalent6ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent7ComboBox.SelectedIndex, petTalent7ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent8ComboBox.SelectedIndex, petTalent8ValidCheckBox.Checked is true ? 1 : 0),
                new(petTalent9ComboBox.SelectedIndex, petTalent9ValidCheckBox.Checked is true ? 1 : 0),
            ];

            return talentList;
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
                "�R����" => baseObjectId,
                "���A" => baseObjectId + (int)CookRarity.Rare,
                "�G�s�b�N" => baseObjectId + (int)CookRarity.Epic,
                _ => throw new ArgumentException(null, nameof(rarity)),
            };
            string baseInternalName = StaticResource.AllCookedBaseCategories.Single(c => c.ObjectID == baseObjectId).InternalName;
            fixedInternalName = rarity switch
            {
                "�R����" => baseInternalName,
                "���A" => baseInternalName + "Rare",
                "�G�s�b�N" => baseInternalName + "Epic",
                _ => throw new ArgumentException(null, nameof(rarity))
            };
        }

        /// <summary>
        /// ���܂����H��Id���獇����̗�����variation�l���v�Z����B
        /// </summary>
        /// <param name="IdA">1�߂̐H�ނ�Id(dec)</param>
        /// <param name="IdB">2�߂̐H�ނ�Id(dec)</param>
        /// <returns></returns>
        public static int CalculateVariation(int IdA, int IdB)
        {
            // �Q�[��������ɍ��킹�č~���ɓ���ւ�
            if (IdA < IdB) (IdA, IdB) = (IdB, IdA);
            // �eID��16�i���ɂ��A1�̕�����Ƃ��ĂȂ���B
            string combinedHex = IdA.ToString("X4") + IdB.ToString("X4");
            // 10�i���ɖ߂�
            var combinedDecimal = Convert.ToInt32(combinedHex, 16);
            return combinedDecimal;
        }

        private void SetDefaultButton_Click(object sender, EventArgs e)
        {
            objectIdTextBox.Text = ItemBase.Default.objectID.ToString();
            variationTextBox.Text = ItemBase.Default.variation.ToString();
            objectNameTextBox.Text = string.Empty;
            amoutTextBox.Text = ItemBase.Default.amount.ToString();
            variationUpdateCountTextBox.Text = ItemBase.Default.variationUpdateCount.ToString();
            auxIndexTextBox.Text = ItemAuxData.Default.index.ToString();
            auxDataTextBox.Text = ItemAuxData.Default.data.ToString();
        }

        /// <summary>
        /// variation����Id�ւ̋t�Z
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="materialA">�ޗ��̐H��A</param>
        /// <param name="materialB">�ޗ��̐H��B</param>
        public static void ReverseCalcurateVariation(int variation, out int materialA, out int materialB)
        {
            string combinedHex = variation.ToString("X8");
            string strA = combinedHex[..4];
            string strB = combinedHex[4..];
            materialA = Convert.ToInt32(strA, 16);
            materialB = Convert.ToInt32(strB, 16);
        }

        private static bool IsCookedItem(int objectID, out CookRarity rarity, out int indexBaseOffset)
        {
            int[] cookedCategoryAllIds = StaticResource.AllCookedBaseCategories
                .Select(c => c.ObjectID)
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
            Properties.Settings.Default.LastSaveFolderPath = SaveDataFolderPath;
            Properties.Settings.Default.Save();
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
            MessageBox.Show("���쐬�̗����̑g�ݍ��킹���o�͂��܂��B");
            _saveDataManager.ListUncreatedRecipes();
        }

        private void petKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            petColorComboBox.Items.Clear();

            var allPetType = Enum.GetValues<PetType>();
            var petTabDic = Enumerable.Range(0, allPetType.Length - 1)
                .Select(i => (i, allPetType[i]))
                .ToDictionary();
            var colorDic = Enumerable.Range(0, Enum.GetValues<PetColor>().Length - 1)
                .Select(i => (i, Enum.GetValues<PetColor>()[i]))
                .ToDictionary();
            //���F�y�b�g����
            if (new int[] { 0, 1, 2, 4, 9, 10 }.Contains(petKindComboBox.SelectedIndex))
            {
                foreach (var color in Enum.GetValues<PetColor>())
                {
                    petColorComboBox.Items.Add(StaticResource.PetColorDict[(allPetType[petKindComboBox.SelectedIndex], color)]);
                }
            }
            else
            {
                // �X���C���n
                petColorComboBox.Items.Add(StaticResource.PetColorDict[(allPetType[petKindComboBox.SelectedIndex], PetColor.Color_1)]);
            }
            petColorComboBox.SelectedIndex = 0;
        }

        private void openConditionsButton_Click(object sender, EventArgs e)
        {
            if (_saveDataManager.HasOveredHealth(out _) && !Program.IsDeveloper)
            {
                MessageBox.Show("�̗͉ߏ�̂��߁A���p�𐧌����܂��B", "����");
                return;
            }
            var conditionForm = new ConditionForm();
            conditionForm.ShowDialog();
        }

        private void materialComboBoxA_DrawItem(object sender, DrawItemEventArgs e)
        {
            materialComboBox_DrawItem(sender, e);
        }

        private void materialComboBoxB_DrawItem(object sender, DrawItemEventArgs e)
        {
            materialComboBox_DrawItem(sender, e);
        }

        private void materialComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox combo = (ComboBox)sender;
            string selectedText = (string)combo.Items[e.Index]!;

            var displayNames = StaticResource.AllFoodMaterials.Select(c => c.DisplayName);
            var goldernNames = displayNames.Where(name => name.StartsWith("���F��")).SkipLast(1);
            // �w�i�F��ݒ�
            if (goldernNames.Contains(selectedText))
            {
                e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
            }
            else if (displayNames.Contains(selectedText))
            {
                e.DrawBackground();
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }

            e.Graphics.DrawString(selectedText, e.Font!, Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            var baseItem = ItemBase.Default;
            string objectName = string.Empty;
            var auxData = ItemAuxData.Default;
            try
            {
                baseItem = new(int.Parse(objectIdTextBox.Text),
                    int.Parse(amoutTextBox.Text),
                    int.Parse(variationTextBox.Text),
                    int.Parse(variationUpdateCountTextBox.Text));
                objectName = objectNameTextBox.Text;
                auxData = new(int.Parse(auxIndexTextBox.Text), auxDataTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("�R�s�[�ł��Ȃ��l���܂܂�Ă��܂��B");
                return;
            }
            _saveDataManager.CopyItem(baseItem, objectName, auxData);
            EnableResultMessage($"{objectNameTextBox.Text}���R�s�[���܂����B");
        }

        private void PaeteButton_Click(object sender, EventArgs e)
        {
            (ItemBase itemBase, string objectName, ItemAuxData auxData) item = _saveDataManager.PasteItem();
            objectIdTextBox.Text = item.itemBase.objectID.ToString();
            amoutTextBox.Text = item.itemBase.amount.ToString();
            variationTextBox.Text = item.itemBase.variation.ToString();
            variationUpdateCountTextBox.Text = item.itemBase.variationUpdateCount.ToString();
            objectNameTextBox.Text = item.objectName;
            auxIndexTextBox.Text = item.auxData.index.ToString();
            auxDataTextBox.Text = item.auxData.data;
            EnableResultMessage($"{item.objectName}���y�[�X�g���܂����B");
        }
    }
}
