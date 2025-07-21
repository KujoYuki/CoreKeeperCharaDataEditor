using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.Cattle;
using CKCharaDataEditor.Model.Food;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Properties;
using CKCharaDataEditor.Resource;
using System.Diagnostics;
using System.Text;

// todo �o���l�e�[�u���i�v���C���[�A�y�b�g�j�̉�͂�Lv�\����Label�Œǉ�
// todo �y�b�g���ƒ{���l���t�@�N�^�����O
// todo �y�b�g�A�ƒ{�̐F�R���g���[���𓖊Y�̐F�ɂ���
// todo �e�[�u�����C�A�E�g�p�l���ɂ�郌�C�A�E�g����

namespace CKCharaDataEditor
{
    public partial class Form1 : Form
    {
        private FileManager _fileManager = FileManager.Instance;
        private SaveDataManager _saveDataManager = SaveDataManager.Instance;
        private List<Ingredient> _ingredientCategories = [];
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
                dupeEquipmentEachLv.Visible = true;
                lastConnectedWorldLabel.Visible = true;
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
            var allingredients = Recipe.AllIngredients.Concat(ingredientCategories)
                .OrderBy(c => c.objectID)
                .ToList();
            // �J���҃��[�h�̏ꍇ�͔񐄏��H�ނ��\������
            if (Program.IsDeveloper)
            {
                allingredients.AddRange(Recipe.ObsoleteIngredients);
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
            var cookedCategoryNames = Recipe.AllCookedBaseCategories
                .Select(c => c.DefaultDisplayName)
                .ToArray();
            cookedCategoryComboBox.Items.AddRange(cookedCategoryNames);
            cookedCategoryComboBox.SelectedIndex = 0;
        }

        private void InitCattleCategory()
        {
            cattleComboBox.Items.AddRange(Enum.GetNames<CattleType>());
            Dictionary<string, int> cattles = Enum.GetValues<CattleType>()
                .ToDictionary(type => type.ToString(), type => (int)type);
            // �|��f�[�^������ꍇ�ɕ\������ݒ肷��
            for (int i = 0; i < cattleComboBox.Items.Count; i++)
            {
                string objectName = cattleComboBox.Items[i]!.ToString()!;
                int objectID = cattles[objectName];
                if (_fileManager.LocalizationData.TryGetValue(objectID, out string[]? translateResources))
                {
                    string displayName = translateResources[1];
                    cattleComboBox.Items[i] = displayName;
                }
            }
        }

        private void LoadSlots(string filePath)
        {
            saveSlotNoComboBox.Items.Clear();
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

            if (Path.Exists(filePath))
            {
                EnabeleUI();
                LoadItems();
            }
            else
            {
                DisabeleUI();
                itemListBox.Items.Clear();
                return;
            }
        }

        private void LoadItems()
        {
            if (_fileManager.SaveFilePaths.Count is 0) return;
            _saveDataManager.SaveDataPath = _fileManager.SaveFilePaths[saveSlotNoComboBox.SelectedIndex].FullName;

            // �J���Ҍ����̍ŏI�ڑ����[���hID�`�F�b�N
            if (Program.IsDeveloper)
            {
                lastConnectedWorldLabel.Text = _saveDataManager.GetLastConnnectedServerId().ToString();
            }

            // �����[�h����index�ێ�
            int selectedInventoryIndex = 0;
            int topIndex = itemListBox.TopIndex;
            if (itemListBox.Items.Count > 0)
            {
                selectedInventoryIndex = itemListBox.SelectedIndex;
            }

            // �I�𒆂̃Z�[�u�f�[�^�̃A�C�e������itemListBox�ɔ��f����
            itemListBox.BeginUpdate();
            itemListBox.Items.Clear();
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
                    Item item = _saveDataManager.Items[i];
                    string displayName = TranslateObjectName(item);
                    itemListBox.Items.Add($"{indexText} : {displayName}");
                }
            }
            itemListBox.EndUpdate();

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
            DisplayNameTextBox.Text = selectedItem.DisplayName;
            petEditControl.LoadPetKind();

            // �����̏ꍇ�͗��������Z�b�g����
            if (Recipe.IsCookedItem(selectedItem.objectID))
            {
                Recipe food = new(selectedItem);
                ingredientComboBoxA.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.objectID == food.IngredientA)?.DisplayName;
                ingredientComboBoxB.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.objectID == food.IngredientB)?.DisplayName;
                cookedCategoryComboBox.SelectedIndex =
                    Array.IndexOf(Recipe.AllCookedBaseCategories.Select(c => c.BaseRecipeID).ToArray(), food.BaseRecipeID);
                rarityComboBox.SelectedIndex = food.Rarity switch
                {
                    CookRarity.Common => 0,
                    CookRarity.Rare => 1,
                    CookRarity.Epic => 2,
                    _ => throw new ArgumentException(),
                };
                createdNumericNo.Value = amount;
            }

            if (Pet.IsPet(selectedItem.objectID))
            {
                petEditControl.PetItem = new(selectedItem);
            }
            else
            {
                petEditControl.PetItem = Pet.Default;
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
            int index = itemListBox.SelectedIndex;
            Item item = _saveDataManager.Items[index];

            switch (itemEditTabControl.SelectedTab?.Name)
            {
                case "foodTab":
                    // undone ���A�x�ƐH�ނ̑g�ݍ��킹����ObjectId���Ɍ��肷��
                    int recipeId = Recipe.AllCookedBaseCategories.ElementAt(cookedCategoryComboBox.SelectedIndex).objectID;
                    int ingredientAId = _ingredientCategories[ingredientComboBoxA.SelectedIndex].objectID;
                    int ingredientBId = _ingredientCategories[ingredientComboBoxB.SelectedIndex].objectID;
                    CookRarity rarity = rarityComboBox.SelectedIndex switch
                    {
                        0 => CookRarity.Common,
                        1 => CookRarity.Rare,
                        2 => CookRarity.Epic,
                        _ => throw new ArgumentException()
                    };
                    item = new Recipe(recipeId, ingredientAId, ingredientBId, rarity)
                        .ToItem(amount: Convert.ToInt32(createdNumericNo.Value));
                    _saveDataManager.WriteItemData(index, item);
                    result = true;
                    break;

                case "petTab":
                    item = petEditControl.PetItem;
                    result = _saveDataManager.WriteItemData(index, item);
                    break;

                case "advancedTab":
                    item = GenerateAdvancedItem();
                    result = _saveDataManager.WriteItemData(index, item);
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
                    result = _saveDataManager.WriteItemData(index, item);
                    break;

                case "otherTab":
                    string overrideName = DisplayNameTextBox.Text;
                    item.DisplayName = overrideName;
                    result = _saveDataManager.WriteItemData(index, item);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            if (result)
            {
                string itemName = TranslateObjectName(item);
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
                objectName: objectNameTextBox.Text,
                aux: aux,
                locked: objectLockedCheckBox.Checked);
        }



        private async void EnableResultMessage(string message)
        {
            resultLabel.Text = message;
            resultLabel.Visible = true;
            await Task.Delay(3000);
            resultLabel.Visible = false;
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

            var displayNames = Recipe.AllIngredients.Select(c => c.DisplayName);
            var goldernNames = Recipe.AllIngredients
                .Where(i => i.CanMakeRare)
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

        private string TranslateObjectName(Item item)
        {
            string displayName = item.objectName;
            if (_fileManager.LocalizationData.TryGetValue(item.objectID, out string[]? displayResource))
            {
                displayName = displayResource[1];
            }
            if (Recipe.IsCookedItem(item.objectID))
            {
                displayName = new Recipe(item).DefaultDisplayName;
            }
            if (item.DisplayName != string.Empty)
            {
                displayName = item.DisplayName;
            }
            return displayName;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            int selectedItemIndex = itemListBox.SelectedIndex;
            Item copiedItem = _saveDataManager.CopyItem(selectedItemIndex);
            string displayName = TranslateObjectName(copiedItem);
            EnableResultMessage($"{displayName}���R�s�[���܂����B");
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;
            Item pasteItem = _saveDataManager.PasteItem();
            objectIdTextBox.Text = pasteItem.objectID.ToString();
            amountNumericUpDown.Value = pasteItem.amount;
            variationNumericUpDown.Value = pasteItem.variation;
            variationUpdateCountNumericUpDown.Value = pasteItem.variationUpdateCount;
            objectNameTextBox.Text = pasteItem.objectName;
            auxIndexNumericUpDown.Value = pasteItem.Aux.index;
            auxDataTextBox.Text = pasteItem.Aux.data;

            string displayName = TranslateObjectName(pasteItem);
            EnableResultMessage($"{displayName}�̏����y�[�X�g���܂����B");
        }

        private void inventryCopyButton_Click(object sender, EventArgs e)
        {
            _saveDataManager.CopyInventory();
            EnableResultMessage("�C���x���g����S�ăR�s�[���܂����B");
        }

        private void inventryPasteButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;
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
                if (!IsLegalSaveData()) return;
                if (IsRunningGame()) return;
                Item pasteItem = _saveDataManager.PasteItem();
                _saveDataManager.WriteItemData(itemListBox.SelectedIndex, pasteItem);
                string displayName = TranslateObjectName(pasteItem);
                EnableResultMessage($"{displayName}���y�[�X�g���܂����B");
                LoadItems();
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
            if (e.KeyCode == Keys.Delete)
            {
                // �f�t�H���g�A�C�e���ŏ㏑�����č폜����
                _saveDataManager.WriteItemData(itemListBox.SelectedIndex, Item.Default);
                LoadItems();
                EnableResultMessage("�A�C�e�����폜���܂����B");
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
            if (sender is TextBox textBox)
            {
                StaticResource.SanitizeTextBoxText(textBox);
            }
        }

        private void DisplayNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                StaticResource.SanitizeTextBoxText(textBox);
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

        private void dupeEquipmentEachLv_Click(object sender, EventArgs e)
        {
            string assertion = "�C���x���g���̍ŏ���4�̑������e�A�C�e��Lv���Ƃɕ������܂��B\n���s���܂����H\n\n" +
                "���Ώۂ̃X���b�g�Ɋ��ɑ��݂���A�C�e���͏㏑������܂��B";
            DialogResult result = MessageBox.Show(assertion, "�m�F", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result is DialogResult.OK)
            {
                _saveDataManager.DupeEquipmentEachLv();
                LoadItems();
            }
        }

        private void lastConnectedWorldLabel_Click(object sender, EventArgs e)
        {
            // �ŏI�ڑ����[���hID���R�s�[
            Clipboard.SetText(lastConnectedWorldLabel.Text);
            EnableResultMessage("�ŏI�ڑ����[���hID���R�s�[���܂����B");
        }
    }
}
