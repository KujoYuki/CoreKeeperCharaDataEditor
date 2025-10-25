using CKCharaDataEditor.Model.Items;
using System.Data;
using System.Diagnostics;

namespace CKCharaDataEditor
{
    public partial class DropForm : Form
    {
        private FileManager _fileManager = FileManager.Instance;
        private List<LootTable> _lootTables = [];
        private Func<int, double>? CalculateChancePerRoll;    // ドロップ期待値計算式(n回やって少なくともm個取得できる)
        private const double DropUpperLimit = 0.9; // 複数回シミュレートにおける計算上限確率

        public DropForm()
        {
            InitializeComponent();
            InitControls();
            LoadLootFiles();
        }

        private void InitControls()
        {
            foreach (DataGridViewColumn col in lootDataGridView.Columns)
            {
                col.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }
            string[] selectablePlayerCounts = Enumerable.Range(1, 8).Select(i => i.ToString()).ToArray();
            playerCountComboBox.Items.AddRange(selectablePlayerCounts);
            playerCountComboBox.SelectedIndex = 0;
            worldModeComboBox.SelectedIndex = 0;

            skillNotFishToolTip.SetToolTip(notFishLabel, "魚以外のアイテムが釣れる確率を上昇させる確率");
        }

        private async void LoadLootFiles()
        {
            List<FileInfo> lootDirectory = _fileManager.LootFilePaths;
            var data = await Task.WhenAll(
                lootDirectory.Select(async fileInfo =>
                {
                    string path = fileInfo.FullName;
                    string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    LootTable lootTable = await LootTableHelper.LoadFromFileAsync(path);
                    return (fileName, lootTable);
                })
            );
            var orderedData = data.OrderBy(t => t.lootTable.LootTableID).ToArray();

            // ドロップテーブルリストの翻訳
            lootTableListBox.Items.AddRange(orderedData.Select(d => d.fileName).ToArray());
            _lootTables = orderedData.Select(d => d.lootTable).ToList();
            if (lootTableListBox.Items.Count > 0)
            {
                // 最初のアイテムを選択状態にする
                lootTableListBox.SelectedIndex = 0;
            }

        }

        private void lootTableListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            lootDataGridView.Rows.Clear();
            LootTable table = _lootTables[lootTableListBox.SelectedIndex];
            lootIdLabel.Text = table.LootTableID.ToString();

            // ドロップテーブルの表示
            var orderedLootItems = table.Loots.OrderByDescending(loot => loot.IsOneOfGuaranteedToDrop)
                    .ThenBy(loot => loot.ObjectID);
            foreach (LootItem item in orderedLootItems)
            {
                string displayName = item.ObjectID.ToString();
                if (_fileManager.LocalizationData.TryGetValue(item.ObjectID, out var translateResources))
                {
                    displayName = translateResources.DisplayName;
                }
                lootDataGridView.Rows.Add(item.ObjectID, displayName, item.Amount, item.Weight, item.RollPerDrop * 100f,
                                          item.GuaranteedRollPerDrop * 100f);
            }

            // 抽選枠数の表示
            guaranteedRollCountLabel.Text = table.Loots.Exists(item => item.IsOneOfGuaranteedToDrop) ? "1" : "0";

            SimurateTrialCounts();
        }

        private void lootDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var colName = lootDataGridView.Columns[e.ColumnIndex];
            switch (colName)
            {
                case { Name: "objectID" }:
                    lootDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if ((int)e.Value! == 0)
                    {
                        // ObjectIDが0の場合は空欄にする
                        e.Value = string.Empty;
                    }
                    break;
                case { Name: "ItemName" }:
                    if (e.Value!.ToString() == "0")
                    {
                        // ドロップ無しの表記
                        e.Value = "なし";
                    }
                    break;
                case { Name: "Amount" }:
                    lootDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case { Name: "Weight" }:
                    lootDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    e.Value = ((double)e.Value!).ToString("F3");
                    break;
                case { Name: "RollPerDrop" }:
                    lootDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case { Name: "RollPerDop" }:
                case { Name: "GuaranteedRoll" }:
                    if ((double)e.Value! == 0)
                    {
                        // 0%の場合は空欄にする
                        e.Value = "----";
                        break;
                    }
                    e.Value = ((double)e.Value!).ToString($"F{decimalPlacesNumericUpDown.Value.ToString()}");
                    break;
                default:
                    break;
            }
        }

        private void lootDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SimurateTrialCounts();
        }

        private void worldModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SimurateTrialCounts();
        }

        private void playerCountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SimurateTrialCounts();
        }

        private void increasedChanceToGetFishLoot_ValueChanged(object sender, EventArgs e)
        {
            SimurateTrialCounts();
        }

        private void increasedChanceToGetFish_ValueChanged(object sender, EventArgs e)
        {
            SimurateTrialCounts();
        }

        /// <summary>
        /// 複数回シミュレートの期待値計算
        /// </summary>
        private void SimurateTrialCounts()
        {
            if (lootDataGridView.CurrentCell is null || _lootTables.Count is 0) return;

            LootTable selectedTable = _lootTables[lootTableListBox.SelectedIndex];
            int selectedRowIndex = lootDataGridView.CurrentCell.RowIndex;
            int selectedObjectId = Convert.ToInt32(lootDataGridView.Rows[selectedRowIndex].Cells["objectID"].Value);
            LootItem item = selectedTable.Loots.FirstOrDefault(li => li.ObjectID == selectedObjectId)!;
            double baseRollCountRatio = 1;  //基本抽選枠の枠数倍率
            string fileName = lootTableListBox.SelectedItem!.ToString()!;
            TableAction tableAction = LootTableHelper.TableDetails[fileName].Action;
            DropRange dropRange = selectedTable.UniqueDrops.Copy();
            string correctRollRange;

            // Bossテーブルの場合の補正
            if (tableAction is TableAction.Boss)
            {
                // ワールドモードによる補正
                if (worldModeComboBox.SelectedIndex == 1) // HardMode
                    baseRollCountRatio *= 1.5f;

                // プレイヤー人数による補正
                int playerCount = Convert.ToInt32(playerCountComboBox.SelectedItem);
                // hack 2-3人プレイ時の補正が不明なため暫定値を使用。そのうち検証する
                baseRollCountRatio *= 1 + (playerCount - 1) * 0.5f;                            // 仮: 1人あたり0.5倍増加と仮定
                //baseRollCount = Math.Pow(1.5f * baseRollCount, playerCount);    // 仮: プレイヤー人数に応じて指数関数的に増加すると仮定
                dropRange *= baseRollCountRatio;
            }

            // 正しい抽選枠数の表示
            correctRollRange = dropRange.IsSingleValue ?
                dropRange.Min.ToString() :
                $"{dropRange.Min * baseRollCountRatio} - {dropRange.Max * baseRollCountRatio}";
            normalRollCountLabel.Text = correctRollRange;

            double ratePerLootRoll = dropRange.GetAverage();
            int correctedRollCount = Convert.ToInt32(Math.Round(ratePerLootRoll));
            if (correctedRollCount > 36) correctedRollCount = 36;

            // 釣り確率補正
            double rollperDrop = item.RollPerDrop;
            if (tableAction is TableAction.FishingLoot)
            {
                rollperDrop *= 0.6 + (double)(increasedChanceToGetFishLoot.Value - increasedChanceToGetFish.Value) / 100f;
            }
            else if (tableAction is TableAction.FishingFish)
            {
                rollperDrop *= 0.4 + (double)(increasedChanceToGetFish.Value - increasedChanceToGetFishLoot.Value) / 100f;
            }

            // 保証抽選枠の有無で計算式を変更する
            if (item.IsOneOfGuaranteedToDrop)
            {
                CalculateChancePerRoll = (rollCount) =>
                {
                    // 1回のRollで少なくとも1個取得できる確率
                    double ratePerLoot = 1 - (Math.Pow(1 - rollperDrop, correctedRollCount - 1) * (1 - item.GuaranteedRollPerDrop));
                    // n回のRollで少なくとも1個取得できる確率
                    double ratePerRoll = 1 - Math.Pow(1 - ratePerLoot, rollCount);
                    return ratePerRoll;
                };
            }
            else
            {
                CalculateChancePerRoll = (rollCount) =>
                {
                    // 1回のRollで少なくとも1個取得できる確率
                    double ratePerLoot = 1 - Math.Pow(1 - rollperDrop, correctedRollCount);
                    // n回のRollで少なくとも1個取得できる確率
                    double ratePerRoll = 1 - Math.Pow(1 - ratePerLoot, rollCount);
                    return ratePerRoll;
                };
            }

            int trialCount = 1;
            double ratePerRoll = 0;
            List<(int Count, double Rate)> trialRateCounts = [];
            tryRollsDataGridView.Rows.Clear();
            do
            {
                ratePerRoll = CalculateChancePerRoll(trialCount);
                tryRollsDataGridView.Rows.Add(trialCount, ratePerRoll * 100f);
                trialRateCounts.Add((trialCount, ratePerRoll * 100f));
                trialCount++;
            } while (ratePerRoll < DropUpperLimit && trialCount < 10000);
            expectedCountForDrop25Label.Text = $"{trialRateCounts.FirstOrDefault(t => t.Rate >= 25f).Count}回";
            expectedCountForDrop50Label.Text = $"{trialRateCounts.FirstOrDefault(t => t.Rate >= 50f).Count}回";
            expectedCountForDrop70Label.Text = $"{trialRateCounts.FirstOrDefault(t => t.Rate >= 70f).Count}回";
            expectedCountForDrop90Label.Text = $"{trialRateCounts.FirstOrDefault(t => t.Rate >= 90f).Count}回";
        }

        private void tryRollsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var colName = tryRollsDataGridView.Columns[e.ColumnIndex];
            switch (colName)
            {
                case { Name: "TryCount" }:
                    tryRollsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case { Name: "Rate" }:
                    tryRollsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    e.Value = ((double)e.Value!).ToString($"F{decimalPlacesNumericUpDown.Value.ToString()}");    //hack 小数点以下の表示変更を受け入れられるようにする
                    break;
                default:
                    break;
            }
        }

        private void decimalPlacesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            lootDataGridView.Refresh();
            tryRollsDataGridView.Refresh();
        }

        private void lootTableListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string itemText = (string)lootTableListBox.Items[e.Index];
            string fileName = itemText;

            // 表示名の日本語化
            var trancelation = _fileManager.LocalizationData.Values.ToDictionary();
            if (trancelation.TryGetValue(itemText, out string? displayName))
            {
                itemText = displayName;
            }
            else if (LootTableHelper.AdditionalTableNameDic.TryGetValue(itemText, out var additionalDisplayName))
            {
                itemText = additionalDisplayName;
            }

            // その他の情報取得
            (Biome Biome, TableAction Action) otherLootInfo = LootTableHelper.TableDetails[fileName];

            // 背景色を設定する
            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // 選択中のアイテムの背景色を変更
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.HighlightText, e.Bounds);
            }
            else
            {
                switch (otherLootInfo.Biome)
                {
                    case Biome.Dirt:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Dirt], e.Bounds);
                        break;
                    case Biome.Clay:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Clay], e.Bounds);
                        break;
                    case Biome.Larva:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Larva], e.Bounds);
                        break;
                    case Biome.Stone:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Stone], e.Bounds);
                        break;
                    case Biome.Nature:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Nature], e.Bounds);
                        break;
                    case Biome.Mold:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Mold], e.Bounds);
                        break;
                    case Biome.Sea:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Sea], e.Bounds);
                        break;
                    case Biome.City:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.City], e.Bounds);
                        break;
                    case Biome.Desert:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Desert], e.Bounds);
                        break;
                    case Biome.Temple:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Temple], e.Bounds);
                        break;
                    case Biome.Oasis:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Oasis], e.Bounds);
                        break;
                    case Biome.Lava:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Lava], e.Bounds);
                        break;
                    case Biome.Crystal:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Crystal], e.Bounds);
                        break;
                    case Biome.Passage:
                        e.Graphics.FillRectangle(LootTableHelper.BiomeColor[Biome.Passage], e.Bounds);
                        break;
                    // 通常のアイテムの背景色を変更
                    default:
                        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                        e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);
                        break;
                }
                e.Graphics.DrawString(itemText, e.Font!, SystemBrushes.ControlText, e.Bounds);

            }
            e.DrawFocusRectangle();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/KujoYuki/CoreKeeperCharaDataEditor/blob/main/Document/dropItems.md") { UseShellExecute = true });
        }
    }
}
