using System.Text;
using System.Diagnostics;
using CKCharaDataEditor.Properties;
using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Resource;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Model.Cattle;

// todo �o���l�e�[�u���i�v���C���[�A�y�b�g�j�̉�͂�Lv�\����Label�Œǉ�
// todo �y�b�g���ƒ{���l���t�@�N�^�����O
// todo �e�[�u�����C�A�E�g�p�l���ɂ�郌�C�A�E�g����
// todo �����֘A�̃��W�b�N��Form1���番�����āA�ʂ̃N���X�ɂ���

namespace CKCharaDataEditor
{
    public partial class Form1 : Form
    {
        private FileManager _fileManager = FileManager.Instance;
        private SaveDataManager _saveDataManager = SaveDataManager.Instance;
        private List<Ingredient> _ingredientCategories = [];
        private List<Ingredient> _cookedCategories = StaticResource.AllCookedBaseCategories.ToList();

        public Form1()
        {
            InitializeComponent();
            CheckUpdate();
            Initialize();
            SetToolTips();
            if (Program.IsDeveloper)
            {
                variationUpdateCountNumericUpDown.ReadOnly = false;
                auxIndexNumericUpDown.ReadOnly = false;
                auxDataTextBox.ReadOnly = false;
                toMinusOneButton.Visible = true;
            }
        }

        public void Initialize()
        {
            InitIngredientCategory();
            InitCookedCategory();
            rarityComboBox.SelectedIndex = 0;

            _fileManager.InstallFolder = Settings.Default.InstallFolderPath;
            if (_fileManager.SaveFilePaths.Count > 0)
            {
                string firstSaveDataPath = _fileManager.SaveFilePaths.First().FullName;
                LoadSlots(firstSaveDataPath);
            }

            InitCattleCategory();
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
                            DialogResult dialogResult = MessageBox.Show($"�V�����o�[�W���� {task.Result.version} �������[�X����Ă��܂��B\n" +
                                $"�_�E�����[�h�y�[�W���J���܂����H", "�V�o�[�W����", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                // �u���E�U�Ń����[�X�y�[�W���J��
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

        private void SetToolTips()
        {
            toolTipDataFormatVersion.SetToolTip(dataFormatLabel, "�Z�[�u�f�[�^�̃o�[�W������\�����܂��B\n�o�[�W�������Â��ꍇ�́A�Q�[�����̃L�����ǂݍ��݂ɂ�莩���I�ɍX�V����܂��B");
            toolTipVariation.SetToolTip(variationNumericUpDown, "����ID�̃I�u�W�F�N�g�ɗp�ӂ��ꂽ�o���G�[�V������ݒ肵�܂��B\n�����̏ꍇ�͐H�ނ̑g�ݍ��킹��\���܂��B\n�A�b�v�O���[�h�ςݑ����ł̓A�C�e��Lv��\���܂��B\n�F�h��ł���A�C�e���ł͐F��\���܂��B\n�������ݒ�ł���A�C�e���ł͕�����\���܂��B");
            toolTipConstAmount.SetToolTip(amountConstCheckBox, "�쐬���ɑΏۂ̃A�C�e����amount��ݒ肵�����ŌŒ肵�܂��B\n�����̃X���b�g�𓯈���ŘA���쐬���������Ɏg���܂��B");
            toolTipObjectName.SetToolTip(objectNameTextBox, "�A�C�e���̓������̂�\�����܂��B\n�󗓂̏ꍇ�̓Q�[���N������objectId����K�؂Ȃ��̂��Z�b�g����܂��B");
            toolTipAmount.SetToolTip(amountNumericUpDown, "�X�^�b�N�\�A�C�e���̏ꍇ�͌��ɂȂ�܂��B\n����/�h��̏ꍇ�͑ϋv�l�ɂȂ�܂��B\n�y�b�g�ł͌o���l�ɂȂ�܂��B\n�ƒ{�ł͖����x�ɂȂ�܂��B");
            toolTipAuxData.SetToolTip(auxDataTextBox, "�A�C�e���̕⏕�f�[�^�ł��B\n�y�b�g��ƒ{�̏���ݒ肷��ꍇ�́A�y�b�g�^�u��ƒ{�^�u�𗘗p���Ă��������B");
            toolTipLockedObject.SetToolTip(objectLockedCheckBox, "�Q�[�����ł̃A�C�e���̃��b�N��Ԃ�ݒ肵�܂��B");
            toolTipCattleStomach.SetToolTip(stomachNumericUpDown, "�ƒ{�̖����x��ݒ肵�܂��B�ʏ�A0�`4�͈̔͂ł��B\n2���ƂɎ����𐶎Y���܂��B");
            toolTipCattleMeal.SetToolTip(mealNumericUpDown, "�ƒ{�̐H���񐔂ł��B30�𒴂��������C���߂��ɂ���Ǝq�����܂�A0�Ƀ��Z�b�g����܂��B\n�q��8�ő�l�ɐ������܂��B");
        }

        private void InitIngredientCategory()
        {
            string additionalIngredientsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "AdditionalIngredients.csv")
                ?? throw new FileNotFoundException($"AdditionalIngredients.csv��������܂���B");
            var ingredientCategories = File.ReadAllLines(additionalIngredientsFilePath)
                .Select(line =>
                {
                    string[] words = line.Split(',');
                    return new Ingredient(int.Parse(words[0]), words[1], words[2], IngredientRoots.Cooked);
                })
                .ToArray();
            var allingredients = StaticResource.AllIngredients.Concat(ingredientCategories)
                .OrderBy(c => c.objectID)
                .ToList();
            // �J���҃��[�h�̏ꍇ�͔񐄏��H�ނ��\������
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

        private void InitCattleCategory()
        {
            cattleComboBox.Items.AddRange(Enum.GetNames<CattleType>());
            Dictionary<string, int> cattles = Enum.GetNames<CattleType>()
                .Zip(Enum.GetValues<CattleType>().Select(type => (int)type).ToArray())
                .Select(values => (obejectName: values.First, id: values.Second))
                .ToDictionary();
            int[] cattleIds = Enum.GetValues<CattleType>().Select(type => (int)type).ToArray();
            for (int i = 0; i < cattleComboBox.Items.Count; i++)
            {
                string objectName = cattleComboBox.Items[i]!.ToString()!;
                string id = cattles[objectName].ToString();
                if (_fileManager.LocalizationData.TryGetValue(id, out string[]? translateResources))
                {
                    string displayName = translateResources[1];
                    cattleComboBox.Items[i] = displayName;
                }
            }
        }

        private void LoadSlots(string filePath)
        {
            foreach (FileInfo savePath in _fileManager.SaveFilePaths)
            {
                // �L�����N�^�[���擾
                _saveDataManager.SaveDataPath = savePath.FullName;
                string characterName = _saveDataManager.GetCharacterName();
                var fileName = Path.GetFileNameWithoutExtension(savePath.FullName);
                if (int.TryParse(fileName, out int saveNoInt))
                {
                    // �Q�[�����ł̃Z�[�u�f�[�^No��1����n�܂邽�� +1
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

            if (!Path.Exists(filePath))
            {
                DisabeleUI();
                itemListBox.Items.Clear();
                return;
            }
            else
            {
                EnabeleUI();
                LoadItems();
            }
        }

        private void LoadItems()
        {
            // �����[�h����index�ێ��ƃN���A
            int selectedInventoryIndex = 0;
            int topIndex = itemListBox.TopIndex;
            if (itemListBox.Items.Count > 0)
            {
                selectedInventoryIndex = itemListBox.SelectedIndex;
            }
            itemListBox.Items.Clear();

            if (_fileManager.SaveFilePaths.Count is 0) return;
            _saveDataManager.SaveDataPath = _fileManager.SaveFilePaths[saveSlotNoComboBox.SelectedIndex].FullName;

            // �I�𒆂̃Z�[�u�f�[�^�̃A�C�e������itemListBox�ɔ��f����
            for (int i = 0; i < _saveDataManager.Items.Count; i++)
            {
                string indexText = StaticResource.ExtendSlotName.TryGetValue(i + 1, out var rim) ?
                    (i + 1) + "," + rim.Segment : (i + 1).ToString();
                if (_saveDataManager.Items[i].objectID == 0)
                {
                    itemListBox.Items.Add($"{indexText} : ----");
                }
                else
                {
                    string objectId = _saveDataManager.Items[i].objectID.ToString();
                    _fileManager.LocalizationData.TryGetValue(objectId, out var displayResource);
                    string displayName = displayResource is null ? _saveDataManager.Items[i].objectName : displayResource[1];
                    itemListBox.Items.Add($"{indexText} : {displayName}");
                }
            }

            itemListBox.SelectedIndex = selectedInventoryIndex < itemListBox.Items.Count ? selectedInventoryIndex : itemListBox.Items.Count - 1;
            itemListBox.TopIndex = topIndex < itemListBox.Items.Count ? topIndex : itemListBox.Items.Count - 1;

            LoadPanel();
        }

        private void LoadPanel()
        {
            Item selectedItem = _saveDataManager.Items[itemListBox.SelectedIndex];
            int variation = selectedItem.variation;
            int amount = selectedItem.amount;

            objectIdTextBox.Text = selectedItem.objectID.ToString();
            amountNumericUpDown.Value = amount;
            objectLockedCheckBox.Checked = selectedItem.Locked;
            variationNumericUpDown.Value = variation;
            variationUpdateCountNumericUpDown.Value = selectedItem.variationUpdateCount;
            objectNameTextBox.Text = selectedItem.objectName;
            auxIndexNumericUpDown.Value = selectedItem.Aux.index;
            auxDataTextBox.Text = selectedItem.Aux.data;

            // �����̏ꍇ�͗��������Z�b�g����
            // hack ���̓���A�C�e�����l�ɗ����A�C�e���N���X���쐬����
            if (IsCookedItem(selectedItem.objectID, out var rarity, out var indexBaseOffset))
            {
                ReverseCalcurateVariation(variation, out var ingredientIdA, out var ingredientIdB);
                ingredientComboBoxA.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.objectID == ingredientIdA)?.DisplayName;
                ingredientComboBoxB.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.objectID == ingredientIdB)?.DisplayName;

                cookedCategoryComboBox.SelectedIndex = indexBaseOffset;
                rarityComboBox.SelectedIndex = rarity switch
                {
                    CookRarity.Common => 0,
                    CookRarity.Rare => 1,
                    CookRarity.Epic => 2,
                    _ => throw new NotImplementedException(),
                };
                createdNumericNo.Value = amount;
            }

            // �y�b�g�̂̏ꍇ�̓y�b�g�����Z�b�g����
            if (Pet.IsPet(selectedItem.objectID))
            {
                petEditControl.PetItem = new(selectedItem);
            }
            else
            {
                petEditControl.ResetPetTab();
            }

            // �ƒ{�̏ꍇ�͉ƒ{�����Z�b�g����
            if (Cattle.IsCattle(selectedItem.objectID))
            {
                var cattle = new Cattle(selectedItem);
                int cattleTypeIndex = Array.IndexOf(Enum.GetValues<CattleType>(), cattle.Type);
                cattleComboBox.SelectedIndex = cattleTypeIndex;
                cattleColorVariationComboBox.SelectedIndex = variation;
                cattleNameTextBox.Text = cattle.Name;
                stomachNumericUpDown.Value = cattle.Stomach;
                mealNumericUpDown.Value = cattle.Meal;
                if (cattle.IsAdult)
                {
                    breedingCheckBox.Enabled = true;
                    breedingCheckBox.Checked = cattle.Breeding;
                }
                else
                {
                    breedingCheckBox.Enabled = false;
                    breedingCheckBox.Checked = false;
                }
            }
            else
            {
                // �ƒ{�^�u�̕\�����N���A
                cattleComboBox.SelectedIndex = -1;
                cattleColorVariationComboBox.SelectedIndex = -1;
                cattleNameTextBox.Text = string.Empty;
                stomachNumericUpDown.Value = 0;
                mealNumericUpDown.Value = 0;
                breedingCheckBox.Enabled = true;
                breedingCheckBox.Checked = false;
            }
        }

        private void saveSlotNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            dataFormatLabel.Text = _saveDataManager.GetCharacterDataVersion().ToString();
            clearedFlagLabel.Text = _saveDataManager.IsClearData() ? "�N���A�ς�" : "���N���A";
        }

        private void toMaxButton_Click(object sender, EventArgs e)
        {
            createdNumericNo.Value = 9999;
        }

        private void toMinusOneButton_Click(object sender, EventArgs e)
        {
            createdNumericNo.Value = -1;
        }

        private void itemListBox_TextChanged(object sender, EventArgs e)
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
                    MessageBox.Show($"�̗͉ߏ�̂��߁A���p�𐧌����܂��B\nCode : {health}", "����");
                    _saveDataManager.TsetseWell();
                    return false;
                }
                if (!_saveDataManager.IsClearData() && !_saveDataManager.IsCreativeData())
                {
                    MessageBox.Show("�N���A�ς݂łȂ��ꍇ�͋@�\�𐧌����܂��B\n�ʏ�N���A��ɂ��y���݂��������B");
                    return false;
                }
                string characterName = _saveDataManager.GetCharacterName();
                foreach (var name in Forbidden.Users)
                {
                    if (characterName.StartsWith(name))
                    {
                        MessageBox.Show($"���Ȃ��̗��p�͋֎~���Ă��܂��B", "����");
                        _saveDataManager.TsetseWell();
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsRunningGame()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName.Equals("CoreKeeper"))
                {
                    MessageBox.Show("�Q�[�����N�����ł��B�ύX�𔽉f������O�ɃQ�[�����I�����Ă��������B", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            Item item;

            switch (itemEditTabControl.SelectedTab?.Name)
            {
                case "foodTab":
                    int ingredientAId = _ingredientCategories
                    .Single(c => c.DisplayName == ingredientComboBoxA
                    .SelectedItem?.ToString()).objectID;
                    int ingredientBId = _ingredientCategories
                        .Single(c => c.DisplayName == ingredientComboBoxB
                        .SelectedItem?.ToString()).objectID;
                    int calculatedVariation = CalculateVariation(ingredientAId, ingredientBId);

                    // ���A�x���f
                    int baseObjectId = _cookedCategories.Single(c => c.DisplayName == cookedCategoryComboBox.SelectedItem!.ToString()).objectID;
                    DetermineCookedAttributes(baseObjectId, rarityComboBox.SelectedItem?.ToString()!, out int fixedObjectId, out string objectName);
                    item = new(objectID: fixedObjectId,
                               amount: Convert.ToInt32(createdNumericNo.Value),
                               variation: calculatedVariation,
                               variationUpdateCount: 0,
                               objectName, ItemAuxData.Default);
                    _saveDataManager.WriteItemData(itemListBox.SelectedIndex, item);
                    result = true;
                    break;

                case "petTab":
                    if (!Pet.IsPet(int.Parse(objectIdTextBox.Text)))
                    {
                        MessageBox.Show("�I�𒆂̃A�C�e�����y�b�g�ł͂���܂���B\n�C���x���g���g�Ńy�b�g�A�C�e����I�����ĕҏW���Ă��������B");
                        return;
                    }

                    item = petEditControl.PetItem!;
                    if (item is null)
                    {
                        MessageBox.Show("���͂��ꂽ�y�b�g��񂪎擾�ł��܂���ł����B");
                        return;
                    }
                    // ItemAuxData�����݂ŏ�������
                    result = _saveDataManager.WriteItemData(itemListBox.SelectedIndex, item);
                    break;

                case "advancedTab":
                    item = GenerateAdvancedItem();
                    result = _saveDataManager.WriteItemData(itemListBox.SelectedIndex, item);
                    break;

                case "cattleTab":
                    int objectID = (int?)Enum.GetValues<CattleType>()[cattleComboBox.SelectedIndex] ?? (int)CattleType.Cow;
                    int Color = cattleColorVariationComboBox.SelectedIndex == -1 ? 0 : cattleColorVariationComboBox.SelectedIndex;
                    item = new Cattle(Cattle.Default) with
                    {
                        objectID = objectID,
                        Color = Color,
                        Stomach = (int)stomachNumericUpDown.Value,
                        objectName = ((CattleType)objectID).ToString(),
                        Name = cattleNameTextBox.Text,
                        Meal = (int)mealNumericUpDown.Value,
                        Breeding = breedingCheckBox.Checked,
                    };
                    result = _saveDataManager.WriteItemData(itemListBox.SelectedIndex, item);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            if (result)
            {
                string itemName = item.objectName;
                if (_fileManager.LocalizationData.TryGetValue(item.objectID.ToString(), out string[]? displayResource))
                {
                    itemName = displayResource[1];
                }
                EnableResultMessage($"{itemName}���쐬���܂����B");
            }
            // ����������̍ēǂݍ���
            LoadItems();
        }

        private Item GenerateAdvancedItem()
        {
            if (amountConstCheckBox.Checked)
            {
                amountNumericUpDown.Value = amountConst.Value;
            }
            var aux = new ItemAuxData(Convert.ToInt32(auxIndexNumericUpDown.Value), auxDataTextBox.Text);
            return new(objectID: int.Parse((objectIdTextBox.Text)),
                amount: Convert.ToInt32(amountNumericUpDown.Value),
                variation: Convert.ToInt32(variationNumericUpDown.Value),
                variationUpdateCount: Convert.ToInt32(variationUpdateCountNumericUpDown.Value),
                objectName:objectNameTextBox.Text,
                aux:aux,
                locked:objectLockedCheckBox.Checked);
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
            string baseInternalName = StaticResource.AllCookedBaseCategories.Single(c => c.objectID == baseObjectId).objectName;
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

            // �eID��16�r�b�g�V�t�g���Č���
            int combined = (IdA << 16) | IdB;
            return combined;
        }

        /// <summary>
        /// variation����Id�ւ̋t�Z
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="ingredientA">�ޗ��̐H��A</param>
        /// <param name="ingredientB">�ޗ��̐H��B</param>
        public static void ReverseCalcurateVariation(int variation, out int ingredientA, out int ingredientB)
        {
            // 16�r�b�g�E�ɃV�t�g���ď��16�r�b�g���擾
            ingredientA = variation >> 16;
            // ����16�r�b�g���擾
            ingredientB = variation & 0xFFFF;
        }

        private void SetDefaultButton_Click(object sender, EventArgs e)
        {
            objectIdTextBox.Text = ItemInfo.Default.objectID.ToString();
            variationNumericUpDown.Value = ItemInfo.Default.variation;
            objectNameTextBox.Text = string.Empty;
            amountNumericUpDown.Value = ItemInfo.Default.amount;
            variationUpdateCountNumericUpDown.Value = ItemInfo.Default.variationUpdateCount;
            auxIndexNumericUpDown.Value = ItemAuxData.Default.index;
            auxDataTextBox.Text = ItemAuxData.Default.data.ToString();
        }

        private static bool IsCookedItem(int objectID, out CookRarity rarity, out int indexBaseOffset)
        {
            int[] cookedCategoryAllIds = StaticResource.AllCookedBaseCategories
                .Select(c => c.objectID)
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
            itemListBox.Enabled = true;
            createButton.Enabled = true;
            listUncreatedRecipesButton.Enabled = true;
            slotReloadbutton.Enabled = true;
            openConditionsButton.Enabled = true;
            openSkillbutton.Enabled = true;
        }
        private void DisabeleUI()
        {
            saveSlotNoComboBox.Enabled = false;
            itemListBox.Enabled = false;
            createButton.Enabled = false;
            listUncreatedRecipesButton.Enabled = false;
            slotReloadbutton.Enabled = false;
            openConditionsButton.Enabled = false;
            openSkillbutton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _fileManager.Dispose();
        }

        private void listUncreatedRecipesButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("���쐬�̗����̑g�ݍ��킹���o�͂��܂��B");
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
            string itemText = (string)combo.Items[e.Index]!;

            var displayNames = StaticResource.AllIngredients.Select(c => c.DisplayName);
            var goldernNames = StaticResource.AllIngredients
                .Where(i => i.MakeRare)
                .Select(i => i.DisplayName);

            // �w�i�F��ݒ肷��
            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // �I�𒆂̃A�C�e���̔w�i�F��ύX
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.HighlightText, e.Bounds);
            }
            else if (goldernNames.Contains(itemText))
            {
                // ���A���H��
                e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else if (displayNames.Contains(itemText))
            {
                // �ʏ�̐H��
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else
            {
                // ��H�ނ������͋��H��
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            var item = Item.Default;
            try
            {
                item.objectID = int.Parse(objectIdTextBox.Text);
                item.amount = Convert.ToInt32(amountNumericUpDown.Value);
                item.variation = Convert.ToInt32(variationNumericUpDown.Value);
                item.variationUpdateCount = Convert.ToInt32(variationUpdateCountNumericUpDown.Value);
                item.objectName = objectNameTextBox.Text;
                item.Aux = new(Convert.ToInt32(auxIndexNumericUpDown.Value), auxDataTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("�R�s�[�ł��Ȃ��l���܂܂�Ă��܂��B");
                return;
            }
            _saveDataManager.CopyItem(item);

            string displayName = item.objectName;
            if (_fileManager.LocalizationData.TryGetValue(item.objectID.ToString(), out string[]? displayResource))
            {
                displayName = displayResource[1];
            }
            EnableResultMessage($"{displayName}���R�s�[���܂����B");
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            Item item = _saveDataManager.PasteItem();
            objectIdTextBox.Text = item.objectID.ToString();
            amountNumericUpDown.Value = item.amount;
            variationNumericUpDown.Value = item.variation;
            variationUpdateCountNumericUpDown.Value = item.variationUpdateCount;
            objectNameTextBox.Text = item.objectName;
            auxIndexNumericUpDown.Value = item.Aux.index;
            auxDataTextBox.Text = item.Aux.data;

            string displayName = item.objectName;
            if (_fileManager.LocalizationData.TryGetValue(item.objectID.ToString(), out string[]? displayResource))
            {
                displayName = displayResource[1];
            }
            EnableResultMessage($"{displayName}���y�[�X�g���܂����B");
        }

        private void inventryCopyButton_Click(object sender, EventArgs e)
        {
            _saveDataManager.CopyInventory();
            EnableResultMessage("�C���x���g����S�ăR�s�[���܂����B");
        }

        private void inventryPasteButton_Click(object sender, EventArgs e)
        {
            if (!Program.IsDeveloper)
            {
                if (_saveDataManager.HasOveredHealth(out _))
                {
                    MessageBox.Show("�̗͉ߏ�̂��߁A���p�𐧌����܂��B", "����");
                    return;
                }
                if (!_saveDataManager.IsClearData() && !_saveDataManager.IsCreativeData())
                {
                    MessageBox.Show("�N���A�ς݂łȂ��ꍇ�͋@�\�𐧌����܂��B\n�ʏ�N���A��ɂ��y���݂��������B");
                    return;
                }
            }
            if (_saveDataManager.HasCopiedInventory())
            {
                string assertion = "�C���x���g���S�̂��y�[�X�g���܂����H\n�㏑�����ꂽ�A�C�e���͖߂�܂���B";
                bool accepet = MessageBox.Show(assertion, "�m�F", MessageBoxButtons.OKCancel) == DialogResult.OK;
                if (accepet)
                {
                    _saveDataManager.PasteInventory();
                    EnableResultMessage("�C���x���g����S�ăy�[�X�g���܂����B");
                    LoadItems();
                }
            }
            else
            {
                MessageBox.Show("�R�s�[���ꂽ�C���x���g��������܂���B");
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
            string assertion = "�����ς݂̃��V�s���폜���܂����H\n�폜�������V�s�͖߂�܂���B";
            bool accepet = MessageBox.Show(assertion, "�m�F", MessageBoxButtons.OKCancel) == DialogResult.OK;
            if (accepet)
            {
                _saveDataManager.DeleteAllRecipes();
                MessageBox.Show("�S�Ẵ��V�s�̍폜���������܂����B", "�m�F", MessageBoxButtons.OKCancel);
            }
        }

        private void slotReloadbutton_Click(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void itemListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string itemText = (string)itemListBox.Items[e.Index]!;
            e.DrawBackground();
            var uniqueSlot = StaticResource.ExtendSlotName.Keys.ToArray();

            // �F�ύX
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                //�I�𒆂̃A�C�e���̔w�i�F��ύX
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.HighlightText, e.Bounds);
            }
            else if (uniqueSlot.Contains(e.Index + 1))
            {
                // �����ȂǓ���̃C���f�b�N�X�̃A�C�e���̔w�i�F��ύX
                Brush color = StaticResource.ExtendSlotName[e.Index + 1].Color;
                e.Graphics.FillRectangle(color, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else
            {
                // �ʏ�̃A�C�e���̔w�i�F��ύX
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        private void itemListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (itemListBox.SelectedIndex < 0) return;

            if (e.Control && e.KeyCode is Keys.C)
            {
                CopyButton_Click(sender, e);
                e.Handled = true;
            }
            if (e.Control && e.KeyCode is Keys.V)
            {
                PasteButton_Click(sender, e);
                createButton_Click(sender, e);
                e.Handled = true;
            }
            if (e.Control && e.Shift && e.KeyCode is Keys.C)
            {
                inventryCopyButton_Click(sender, e);
                e.Handled = true;
            }
            if (e.Control && e.Shift && e.KeyCode is Keys.V)
            {
                inventryPasteButton_Click(sender, e);
                e.Handled = true;
            }
        }

        private void FilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingForm();
            settingsForm.ShowDialog();
            if (settingsForm.DialogResult is DialogResult.OK)
            {
                _fileManager.SaveFolder = settingsForm.SaveFolderPath;
                _fileManager.InstallFolder = settingsForm.InstallFolderPath;
                if (_fileManager.SaveFilePaths.Count > 0)
                {
                    LoadSlots(_fileManager.SaveFilePaths[0].FullName);
                }
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var versionForm = new AboutBox();
            versionForm.ShowDialog();
        }

        private void cattleNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = cattleNameTextBox.Text;
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            // �y�b�g����64�o�C�g�𒴂������ꂽ�����폜����
            if (bytes.Length > 64)
            {
                while (Encoding.UTF8.GetByteCount(text) > 64)
                {
                    text = text[..^1];
                }
                cattleNameTextBox.Text = text;
                cattleNameTextBox.SelectionStart = text.Length; // �L�����b�g�ʒu�𖖔��ɐݒ�
            }
        }

        private void cattleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int colorIndex = cattleColorVariationComboBox.SelectedIndex;
            cattleColorVariationComboBox.Items.Clear();

            if (cattleComboBox.SelectedIndex < 0) return;

            CattleType cattleType = Enum.GetValues<CattleType>().ElementAt(cattleComboBox.SelectedIndex);
            if (cattleType.ToString().EndsWith("Baby"))
            {
                cattleType = CattleResource.CattleSpecies[cattleType];
            }
            string[] colors = Enumerable.Range(0, 5)
                .Select(i => CattleResource.Colors[(cattleType, i)])
                .ToArray();
            cattleColorVariationComboBox.Items.AddRange(colors);
            cattleColorVariationComboBox.SelectedIndex = colorIndex;
        }
    }
}
