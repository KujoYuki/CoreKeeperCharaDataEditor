using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.Cattle;
using CKCharaDataEditor.Model.Food;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Properties;
using CKCharaDataEditor.Resource;
using System.Diagnostics;

// todo 経験値テーブル（プレイヤー、ペット）の解析とLv表示をLabelで追加
// todo ペット、家畜の色コントロールを当該の色にする

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
                keyNameTextBox.ReadOnly = false;
                auxIndexNumericUpDown.ReadOnly = false;
                auxDataTextBox.ReadOnly = false;
                toMinusOneButton.Visible = true;
                dupeEquipmentEachLv.Visible = true;
                lastConnectedWorldLabel.Visible = true;
                exportTrancelateButton.Visible = true;
            }
        }

        public void Initialize()
        {
            _fileManager.InstallFolder = Settings.Default.InstallFolderPath;
            InitIngredientCategory();
            InitCookedCategory();
            rarityComboBox.SelectedIndex = 0;
            InitCattleCategory();

            if (_fileManager.CharacterFilePaths.Count > 0)
            {
                string firstSaveDataPath = _fileManager.CharacterFilePaths.First().FullName;
                LoadSlots(firstSaveDataPath);
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

        private void SetToolTips()
        {
            toolTipDataFormatVersion.SetToolTip(dataFormatLabel, "セーブデータのバージョンを表示します。\nバージョンが古い場合は、ゲーム中のキャラ読み込みにより自動的に更新されます。");
            toolTipVariation.SetToolTip(variationNumericUpDown, "同一IDのオブジェクトに用意されたバリエーションを設定します。\n料理の場合は食材の組み合わせを表します。\n装備ではアイテムLvを表します。\n色塗りできるアイテムでは色を表します。\n方向が設定できるアイテムでは方向を表します。");
            toolTipConstAmount.SetToolTip(amountConstCheckBox, "作成時に対象のアイテムのamountを設定した数で固定します。\n複数のスロットを同一個数で連続作成したい時に使います。");
            toolTipKeyName.SetToolTip(keyNameTextBox, "アイテムの内部キー名を表示します。\n空欄の場合は起動時にゲーム側から適切なKeyがセットされます。");
            toolTipAmount.SetToolTip(amountNumericUpDown, "スタック可能アイテムの場合は個数になります。\n武器/防具の場合は耐久値になります。\nペットでは経験値になります。\n家畜では満腹度になります。");
            toolTipAuxData.SetToolTip(auxDataTextBox, "アイテムの補助データです。\nペットや家畜の情報を設定する場合は、ペットタブや家畜タブを利用してください。");
            toolTipLockedObject.SetToolTip(objectLockedCheckBox, "ゲーム内でのアイテムのロック状態を設定します。");
            toolTipCattleStomach.SetToolTip(stomachNumericUpDown, "家畜の満腹度を設定します。通常、0～4の範囲です。\n2ごとに資源を生産します。");
            toolTipCattleMeal.SetToolTip(mealNumericUpDown, "家畜の食事回数です。30を超えた同種二匹が近くにいると子が生まれ、0にリセットされます。\n子は8で大人に成長します。");
        }

        private void InitIngredientCategory()
        {
            string additionalIngredientsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "AdditionalIngredients.csv")
                ?? throw new FileNotFoundException($"AdditionalIngredients.csvが見つかりません。");
            var additionalIngredientCategories = File.ReadAllLines(additionalIngredientsFilePath)
                .Select(line =>
                {
                    string[] words = line.Split(',');
                    // 調理後食材は存在しないのでスープで固定する
                    return new Ingredient(int.Parse(words[0]), words[1], words[2], CookedFood.Soup, IngredientAttribute.Cooked);
                })
                .ToArray();
            var allingredients = RecipeHelper.AllIngredients.Concat(additionalIngredientCategories)
                .OrderBy(c => c.objectID)
                .ToList();
            // 開発者モードの場合は非推奨食材も表示する
            if (Program.IsDeveloper)
            {
                allingredients.AddRange(RecipeHelper.ObsoleteIngredients);
            }
            _ingredientCategories.AddRange(allingredients);

            var sortedIngredientNames = _ingredientCategories
                .Select(c => c.DisplayName)
                .ToArray();
            primaryIngredientComboBox.Items.AddRange(sortedIngredientNames);
            secondaryIngredientComboBox.Items.AddRange(sortedIngredientNames);

            primaryIngredientComboBox.SelectedIndex = 0;
            secondaryIngredientComboBox.SelectedIndex = 0;
        }

        private void InitCookedCategory()
        {
            var cookedCategoryNames = RecipeHelper.AllCookedBaseCategories.Values
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
            // 翻訳データがある場合に表示名を設定する
            for (int i = 0; i < cattleComboBox.Items.Count; i++)
            {
                string keyName = cattleComboBox.Items[i]!.ToString()!;
                int objectID = cattles[keyName];
                if (_fileManager.LocalizationData.TryGetValue(objectID, out var translateResources))
                {
                    string displayName = translateResources.DisplayName;
                    cattleComboBox.Items[i] = displayName;
                }
            }
        }

        private void LoadSlots(string filePath)
        {
            saveSlotNoComboBox.Items.Clear();
            foreach (FileInfo savePath in _fileManager.CharacterFilePaths)
            {
                // キャラクター名取得
                _saveDataManager.SaveDataPath = savePath.FullName;
                string characterName = _saveDataManager.GetCharacterName();
                var fileName = Path.GetFileNameWithoutExtension(savePath.FullName);
                if (int.TryParse(fileName, out int saveNoInt))
                {
                    // ゲーム内でのセーブデータNoは1から始まるため +1
                    saveSlotNoComboBox.Items.Add($"{(saveNoInt + 1)}, {characterName}");
                }
                else
                {
                    saveSlotNoComboBox.Items.Add($"{fileName}, {characterName}");
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
            if (_fileManager.CharacterFilePaths.Count is 0) return;
            _saveDataManager.SaveDataPath = _fileManager.CharacterFilePaths[saveSlotNoComboBox.SelectedIndex].FullName;

            // 開発者向けの最終接続ワールドIDチェック
            if (Program.IsDeveloper && _saveDataManager.GetCharacterDataVersion() >= 12)
            {
                lastConnectedWorldLabel.Text = _saveDataManager.GetLastConnnectedServerId().ToString();
            }

            // リロード時のindex保持
            int selectedInventoryIndex = 0;
            int topIndex = itemListBox.TopIndex;
            if (itemListBox.Items.Count > 0)
            {
                selectedInventoryIndex = itemListBox.SelectedIndex;
            }

            // 選択中のセーブデータのアイテム情報をitemListBoxに反映する
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
                    string displayName = TranslateDisplayName(item);
                    itemListBox.Items.Add($"{indexText} : {displayName}");
                }
            }
            itemListBox.SelectedIndex = selectedInventoryIndex < itemListBox.Items.Count ? selectedInventoryIndex : itemListBox.Items.Count - 1;
            itemListBox.TopIndex = topIndex < itemListBox.Items.Count ? topIndex : itemListBox.Items.Count - 1;
            itemListBox.EndUpdate();

            LoadPanel();
        }

        private void LoadPanel()
        {
            Item selectedItem = _saveDataManager.Items[itemListBox.SelectedIndex];
            int variation = selectedItem.variation;
            int amount = selectedItem.amount;

            objectIdNumericUpDown.Value = Convert.ToDecimal(selectedItem.objectID);
            if (string.IsNullOrEmpty(keyNameTextBox.Text))
            {
                // 辞書から取得出来なければセーブのkeyNameをセットする
                keyNameTextBox.Text = selectedItem.keyName;
            }
            amountNumericUpDown.Value = amount;
            objectLockedCheckBox.Checked = selectedItem.Locked;
            variationNumericUpDown.Value = variation;
            variationUpdateCountNumericUpDown.Value = selectedItem.variationUpdateCount;
            auxIndexNumericUpDown.Value = selectedItem.Aux.index;
            auxDataTextBox.Text = selectedItem.Aux.data;
            DisplayNameTextBox.Text = selectedItem.DisplayName;
            petEditControl.LoadPetKind();

            // 料理の場合は料理情報をセットする
            if (Recipe.IsCookedItem(selectedItem.objectID))
            {
                Recipe food = new(selectedItem);
                primaryIngredientComboBox.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.objectID == food.PrimaryIngredient)?.DisplayName;
                secondaryIngredientComboBox.SelectedItem = _ingredientCategories.SingleOrDefault(c => c.objectID == food.SecondaryIngredient)?.DisplayName;
                cookedCategoryComboBox.SelectedIndex =
                    Array.IndexOf(RecipeHelper.AllCookedBaseCategories.Values.Select(c => c.BaseRecipeID).ToArray(), food.BaseRecipeID);
                rarityComboBox.SelectedIndex = food.Rarity switch
                {
                    CookRarity.Common => 0,
                    CookRarity.Rare => 1,
                    CookRarity.Epic => 2,
                    _ => throw new ArgumentException(),
                };
                createdNumericNo.Value = amount;
            }

            // ペットの場合はペット情報をセットする
            if (Pet.IsPet(selectedItem.objectID))
            {
                petEditControl.PetItem = new(selectedItem);
            }
            else
            {
                petEditControl.PetItem = Pet.Default;
            }

            // 家畜の場合は家畜情報をセットする
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
                // 家畜タブの表示をクリア
                cattleComboBox.SelectedIndex = 0;
                cattleColorVariationComboBox.SelectedIndex = 0;
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
            clearedFlagLabel.Text = _saveDataManager.IsClearData() ? "クリア済み" : "未クリア";
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
#if !debug
                foreach (var name in Forbidden.Users)
                {
                    if (characterName.StartsWith(name))
                    {
                        MessageBox.Show($"あなたの利用は禁止しています。", "注意");
                        _saveDataManager.TsetseWell();
                        return false;
                    }
                }
#endif
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
            int index = itemListBox.SelectedIndex;
            Item item = _saveDataManager.Items[index];

            switch (itemEditTabControl.SelectedTab?.Name)
            {
                case "foodTab":
                    int recipeId = RecipeHelper.AllCookedBaseCategories.Values.ElementAt(cookedCategoryComboBox.SelectedIndex).objectID;
                    int PrimaryIngredientId = _ingredientCategories[primaryIngredientComboBox.SelectedIndex].objectID;
                    int SecondaryIngredientId = _ingredientCategories[secondaryIngredientComboBox.SelectedIndex].objectID;
                    CookRarity rarity = rarityComboBox.SelectedIndex switch
                    {
                        0 => CookRarity.Common,
                        1 => CookRarity.Rare,
                        2 => CookRarity.Epic,
                        _ => throw new IndexOutOfRangeException()
                    };
                    item = new Recipe(recipeId, PrimaryIngredientId, SecondaryIngredientId, rarity)
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
                        keyName = ((CattleType)objectID).ToString(),
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
                string itemName = TranslateDisplayName(item);
                EnableResultMessage($"{itemName}を作成しました。");
            }
            // 書き換え後の再読み込み
            LoadItems();
        }

        private Item GenerateAdvancedItem()
        {
            if (amountConstCheckBox.Checked)
            {
                amountNumericUpDown.Value = amountConst.Value;
            }
            var aux = new ItemAuxData(Convert.ToInt32(auxIndexNumericUpDown.Value), auxDataTextBox.Text);
            int objectId = Convert.ToInt32(objectIdNumericUpDown.Value);
            return new(objectID: objectId,
                amount: Convert.ToInt32(amountNumericUpDown.Value),
                variation: Convert.ToInt32(variationNumericUpDown.Value),
                variationUpdateCount: Convert.ToInt32(variationUpdateCountNumericUpDown.Value),
                keyName: keyNameTextBox.Text,
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
            objectIdNumericUpDown.Value = Convert.ToDecimal(ItemInfo.Default.objectID);
            variationNumericUpDown.Value = ItemInfo.Default.variation;
            keyNameTextBox.Text = string.Empty;
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
            MessageBox.Show("未作成の料理の組み合わせを出力します。");
            _saveDataManager.ListUncreatedRecipes();
        }

        private void openConditionsButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;

            using var conditionForm = new ConditionForm();
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

            var displayNames = RecipeHelper.AllIngredients.Select(c => c.DisplayName);
            var goldernNames = RecipeHelper.AllIngredients
                .Where(i => RecipeHelper.IngredientShouldBePrimary(i.objectID))
                .Select(i => i.DisplayName);

            // 背景色を設定する
            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // 選択中のアイテムの背景色を変更
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.HighlightText, e.Bounds);
            }
            else if (goldernNames.Contains(itemText))
            {
                // レア化食材
                e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else if (displayNames.Contains(itemText))
            {
                // 通常の食材
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else
            {
                // 非食材もしくは旧食材
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private string TranslateDisplayName(Item item)
        {
            string displayName = item.keyName;
            if (_fileManager.LocalizationData.TryGetValue(item.objectID, out var displayResource))
            {
                displayName = displayResource.DisplayName;
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
            string displayName = TranslateDisplayName(copiedItem);
            EnableResultMessage($"{displayName}をコピーしました。");
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;
            Item pasteItem = _saveDataManager.PasteItem();
            objectIdNumericUpDown.Value = Convert.ToDecimal(pasteItem.objectID);
            amountNumericUpDown.Value = pasteItem.amount;
            variationNumericUpDown.Value = pasteItem.variation;
            variationUpdateCountNumericUpDown.Value = pasteItem.variationUpdateCount;
            keyNameTextBox.Text = pasteItem.keyName;
            auxIndexNumericUpDown.Value = pasteItem.Aux.index;
            auxDataTextBox.Text = pasteItem.Aux.data;

            string displayName = TranslateDisplayName(pasteItem);
            EnableResultMessage($"{displayName}の情報をペーストしました。");
        }

        private void inventryCopyButton_Click(object sender, EventArgs e)
        {
            _saveDataManager.CopyInventory();
            EnableResultMessage("インベントリを全てコピーしました。");
        }

        private void inventryPasteButton_Click(object sender, EventArgs e)
        {
            if (!IsLegalSaveData()) return;
            if (IsRunningGame()) return;
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

            using var skillPointForm = new SkillPointForm();
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

        private void addAllRecipeButton_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("全てのレシピを追加しますか？\n既に発見済みのレシピは上書きされます。", "確認", MessageBoxButtons.OKCancel);
            if (dialogResult is DialogResult.OK)
            {
                _saveDataManager.AddAllRecipes();
                MessageBox.Show("全てのレシピの追加が完了しました。", "確認", MessageBoxButtons.OKCancel);
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

            // 色変更
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                //選択中のアイテムの背景色を変更
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.HighlightText, e.Bounds);
            }
            else if (uniqueSlot.Contains(e.Index + 1))
            {
                // 装備など特定のインデックスのアイテムの背景色を変更
                Brush color = StaticResource.ExtendSlotName[e.Index + 1].Color;
                e.Graphics.FillRectangle(color, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else
            {
                // 通常のアイテムの背景色を変更
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
                string displayName = TranslateDisplayName(pasteItem);
                EnableResultMessage($"{displayName}をペーストしました。");
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
                // デフォルトアイテムで上書きして削除扱い
                _saveDataManager.WriteItemData(itemListBox.SelectedIndex, Item.Default);
                LoadItems();
                EnableResultMessage("アイテムを削除しました。");
                e.Handled = true;
            }
        }

        private void FilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var settingsForm = new SettingForm();
            settingsForm.ShowDialog();
            if (settingsForm.DialogResult is DialogResult.OK)
            {
                _fileManager.SaveFolder = settingsForm.SaveFolderPath;
                _fileManager.InstallFolder = settingsForm.InstallFolderPath;
                if (_fileManager.CharacterFilePaths.Count > 0)
                {
                    LoadSlots(_fileManager.CharacterFilePaths[0].FullName);
                }
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var versionForm = new AboutBox();
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
            string assertion = "インベントリの最初の4つの装備を各アイテムLvごとに複製します。\n続行しますか？\n\n" +
                "※対象のスロットに既に存在するアイテムは上書きされます。";
            DialogResult result = MessageBox.Show(assertion, "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result is DialogResult.OK)
            {
                _saveDataManager.DupeEquipmentEachLv();
                LoadItems();
            }
        }

        private void lastConnectedWorldLabel_Click(object sender, EventArgs e)
        {
            // 最終接続ワールドIDをコピー
            Clipboard.SetText(lastConnectedWorldLabel.Text);
            EnableResultMessage("最終接続ワールドIDをコピーしました。");
        }

        private void worldEditButton_Click(object sender, EventArgs e)
        {
            if (IsRunningGame()) return;

            using var worldSettingForm = new WorldSetteingForm();
            worldSettingForm.ShowDialog();
        }

        private void ListupUnobtainedEquipButton_Click(object sender, EventArgs e)
        {
            _saveDataManager.ListupUnobtainedItem();
        }

        private void mapButton_Click(object sender, EventArgs e)
        {
            // パス設定が間違えてる場合は警告を出す
            if (_fileManager.CanOpenCharaFiles())
            {
                using var mapForm = new MapForm();
                mapForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("設定からセーブデータパスを指定してください。\nマップ表示はゲーム内の定義データから行います。", "注意");
            }
        }

        private void dropButton_Click(object sender, EventArgs e)
        {
            // パス設定が間違えてる場合は警告を出す
            if (_fileManager.CanOpenLootFiles())
            {
                using var dropForm = new DropForm();
                _ = dropForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("設定からインストールパスを指定してください。\nドロップ率計算はゲーム内の定義データから行います。", "注意");
            }
        }

        private void ingredientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 初期化中は無視
            if (primaryIngredientComboBox.SelectedIndex < 0 ||
                secondaryIngredientComboBox.SelectedIndex < 0 ||
                cookedCategoryComboBox.SelectedIndex < 0)
            {
                return;
            }
            // 調理後の料理カテゴリを自動選択。 primaryに決定した食材の調理後料理で決定する
            int primaryId = _ingredientCategories[primaryIngredientComboBox.SelectedIndex].objectID;
            int secondaryId = _ingredientCategories[secondaryIngredientComboBox.SelectedIndex].objectID;
            int primaryObjectID = RecipeHelper.GetPrimaryIngredient(primaryId, secondaryId);
            CookedFood? cookedFood = RecipeHelper.AllIngredients
                .Concat(RecipeHelper.ObsoleteIngredients)
                .FirstOrDefault(c => c.objectID == primaryObjectID)?.CookedFood;
            //AllCookedBaseCategories 該当する料理カテゴリを探す
            if (cookedFood is not null)
            {
                var cookedFoodIndex = RecipeHelper.AllCookedBaseCategories.Keys.ToList().IndexOf((CookedFood)cookedFood);
                cookedCategoryComboBox.SelectedIndex = cookedFoodIndex;
            }
            else
            {
                // 元から調理後料理が存在しない場合は何もしない。１つ前に決定された料理のまま。
            }
            // レア度のコンボボックスにも反映させる
            int containsRarityIngredient = RecipeHelper.ContainsRareIngredient(primaryId, secondaryId);
            rarityComboBox.SelectedIndex = containsRarityIngredient switch
            {
                1 or 2 => 1, // レア料理が1以上の場合はRare
                _ => 0, // Common
            };
        }

        private void rarityComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string itemText = (string)rarityComboBox.Items[e.Index]!;
            e.DrawBackground();

            // 色変更
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                //選択中アイテムの背景色
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.HighlightText, e.Bounds);
            }
            else if (e.Index == 1)
            {
                // レア料理の背景色
                e.Graphics.FillRectangle(Brushes.RoyalBlue, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else if (e.Index == 2)
            {
                // エピック料理の背景色
                e.Graphics.FillRectangle(Brushes.Violet, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            else
            {
                // コモン料理の背景色
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void exportTrancelateButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("アイテム名の対応ファイルと翻訳差分を出力します。");
            LanguageLoader.OutputVisibleDictionary();
        }

        private void objectIdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // undone objectIDに対応するKeyの自動取得
            if (_fileManager.ObjectIdWithKey.TryGetValue((int)objectIdNumericUpDown.Value, out string? objectKey))
            {
                keyNameTextBox.Text = objectKey;
            }
            else
            {
                keyNameTextBox.Text = string.Empty;
            }
        }
    }
}