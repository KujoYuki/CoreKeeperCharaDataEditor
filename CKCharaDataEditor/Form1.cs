using System.Text;
using System.Diagnostics;
using CKCharaDataEditor.Properties;
using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Resource;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Model.Cattle;

// todo 経験値テーブル（プレイヤー、ペット）の解析とLv表示をLabelで追加
// todo ペットも家畜同様リファクタリング
// todo テーブルレイアウトパネルによるレイアウト調整
// todo 料理関連のロジックをForm1から分離して、別のクラスにする

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
            toolTipVariation.SetToolTip(variationNumericUpDown, "同一IDのオブジェクトに用意されたバリエーションを設定します。\n料理の場合は食材の組み合わせを表します。\nアップグレード済み装備ではアイテムLvを表します。\n色塗りできるアイテムでは色を表します。\n方向が設定できるアイテムでは方向を表します。");
            toolTipConstAmount.SetToolTip(amountConstCheckBox, "作成時に対象のアイテムのamountを設定した数で固定します。\n複数のスロットを同一個数で連続作成したい時に使います。");
            toolTipObjectName.SetToolTip(objectNameTextBox, "アイテムの内部名称を表示します。\n空欄の場合はゲーム起動時にobjectIdから適切なものがセットされます。");
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
                // キャラクター名取得
                _saveDataManager.SaveDataPath = savePath.FullName;
                string characterName = _saveDataManager.GetCharacterName();
                var fileName = Path.GetFileNameWithoutExtension(savePath.FullName);
                if (int.TryParse(fileName, out int saveNoInt))
                {
                    // ゲーム内でのセーブデータNoは1から始まるため +1
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
            // リロード時のindex保持とクリア
            int selectedInventoryIndex = 0;
            int topIndex = itemListBox.TopIndex;
            if (itemListBox.Items.Count > 0)
            {
                selectedInventoryIndex = itemListBox.SelectedIndex;
            }
            itemListBox.Items.Clear();

            if (_fileManager.SaveFilePaths.Count is 0) return;
            _saveDataManager.SaveDataPath = _fileManager.SaveFilePaths[saveSlotNoComboBox.SelectedIndex].FullName;

            // 選択中のセーブデータのアイテム情報をitemListBoxに反映する
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

            // 料理の場合は料理情報をセットする
            // hack 他の特殊アイテム同様に料理アイテムクラスを作成する
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

            // ペットのの場合はペット情報をセットする
            if (Pet.IsPet(selectedItem.objectID))
            {
                petEditControl.PetItem = new(selectedItem);
            }
            else
            {
                petEditControl.ResetPetTab();
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

                    // レア度反映
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
                        MessageBox.Show("選択中のアイテムがペットではありません。\nインベントリ枠でペットアイテムを選択して編集してください。");
                        return;
                    }

                    item = petEditControl.PetItem!;
                    if (item is null)
                    {
                        MessageBox.Show("入力されたペット情報が取得できませんでした。");
                        return;
                    }
                    // ItemAuxDataを込みで書きこむ
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
                "コモン" => baseObjectId,
                "レア" => baseObjectId + (int)CookRarity.Rare,
                "エピック" => baseObjectId + (int)CookRarity.Epic,
                _ => throw new ArgumentException(null, nameof(rarity)),
            };
            string baseInternalName = StaticResource.AllCookedBaseCategories.Single(c => c.objectID == baseObjectId).objectName;
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
            string itemText = (string)combo.Items[e.Index]!;

            var displayNames = StaticResource.AllIngredients.Select(c => c.DisplayName);
            var goldernNames = StaticResource.AllIngredients
                .Where(i => i.MakeRare)
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
                MessageBox.Show("コピーできない値が含まれています。");
                return;
            }
            _saveDataManager.CopyItem(item);

            string displayName = item.objectName;
            if (_fileManager.LocalizationData.TryGetValue(item.objectID.ToString(), out string[]? displayResource))
            {
                displayName = displayResource[1];
            }
            EnableResultMessage($"{displayName}をコピーしました。");
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
            EnableResultMessage($"{displayName}をペーストしました。");
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
            // ペット名が64バイトを超えたら溢れた分を削除する
            if (bytes.Length > 64)
            {
                while (Encoding.UTF8.GetByteCount(text) > 64)
                {
                    text = text[..^1];
                }
                cattleNameTextBox.Text = text;
                cattleNameTextBox.SelectionStart = text.Length; // キャレット位置を末尾に設定
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
