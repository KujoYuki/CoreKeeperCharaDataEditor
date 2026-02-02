using CKCharaDataEditor.Model;
using CKCharaDataEditor.Properties;
using System.Data;
using System.Diagnostics;

namespace CKCharaDataEditor
{
    public partial class ConditionForm : Form
    {
        readonly SaveDataManager _saveDataManager = SaveDataManager.Instance;
        static List<(int ID, string Description)> ConditionDescriptions = LoadConditionDescriptions();
        private List<Condition> Conditions = [];
        public static List<int> IncreaseHealthMax = [16, 199, 200, 201, 212, 218, 242, 284, 300, 301];

        public ConditionForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = Cursor.Position;
            if (Settings.Default.ConditonFormSizeHeight is not 0 &&
                Settings.Default.ConditonFormSizeWidth is not 0)
            {
                var size = this.Size;
                size.Width = Settings.Default.ConditonFormSizeWidth;
                size.Height = Settings.Default.ConditonFormSizeHeight;
                this.Size = size;
            }
            InitConditons();
        }

        private void InitConditons()
        {
            Conditions = new(_saveDataManager.GetConditions().OrderBy(c => c.Id));
            int maxConditionId = Conditions.Max(c => c.Id);
            if (maxConditionId >= ConditionDescriptions.Last().ID)
            {
                MessageBox.Show($"v{AboutBox.GameVersion}に無いConditionIdを読み込んでいます\n" +
                    "本ツールのバージョンが更新されたら再ダウンロードしてください",
                    "ゲームが更新されています", MessageBoxButtons.OK, MessageBoxIcon.Warning);

				// todo CondtionDescription.csvに無いIDを追加する処理
                string descriptionPath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "ConditionDescription.csv")
                    ?? throw new FileNotFoundException("ConditionDescription.csvが見つかりません。");
                List<string> appendConditons = Enumerable.Range(ConditionDescriptions.Last().ID + 1, maxConditionId - ConditionDescriptions.Last().ID)
                    .Select(id => $"{id},Unknown,new condition. Read GitHub.")
                    .ToList();

				File.AppendAllLines(descriptionPath,appendConditons);
				ConditionDescriptions = LoadConditionDescriptions();
			}
			((DataGridViewComboBoxColumn)dataGridView.Columns["Description"]!).Items
                .AddRange(ConditionDescriptions.Select(c => c.Description).ToArray());
            LoadConditionsToGrid();
        }

        private static List<(int ID, string Description)> LoadConditionDescriptions()
        {
            string conditionDescriptionFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "ConditionDescription.csv")
                ?? throw new FileNotFoundException("ConditionDescription.csvが見つかりません。");

            var dic = File.ReadLines(conditionDescriptionFilePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
				.Select(line =>
                {
                    var sprit = line.Split(',');
                    return (int.Parse(sprit[0]), $"[{int.Parse(sprit[0])},{sprit[1]}] : {sprit[2]}");
                })
                .ToList();
            return dic;
        }

        private void LoadConditionsToGrid()
        {
            dataGridView.Rows.Clear();
			foreach (var condition in Conditions)
            {
                bool infinityDuration = double.IsInfinity(condition.Duration);
                dataGridView.Rows.Add(condition.Id,
                                      condition.Value,
                                      infinityDuration,
                                      infinityDuration ? "--" : condition.Duration,
                                      condition.Timer,
                                      ConditionDescriptions.SingleOrDefault(d => d.ID == condition.Id).Description);
            }
        }

        private void aboutConditionIdLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/KujoYuki/CoreKeeperCharaDataEditor/blob/main/Document/conditions.md") { UseShellExecute = true });
        }

        private void conditionListLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/KujoYuki/CoreKeeperCharaDataEditor/blob/main/CKCharaDataEditor/Resource/ConditionDescription.csv") { UseShellExecute = true });
        }

        private void backUpConditionsButton_Click(object sender, EventArgs e)
        {
            var result = saveFileDialog.ShowDialog();
            if (result is DialogResult.OK)
            {
                SaveDataManager.BackUpConditions(RetrieveConditionsFromGrid(), saveFileDialog.FileName);
            }
        }

        private void loadConditionsButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result is DialogResult.OK)
            {
                var condtions = SaveDataManager.LoadConditions(openFileDialog.FileName);
                Conditions = condtions;
                LoadConditionsToGrid();
            }
        }

        private void addNewRowButton_Click(object sender, EventArgs e)
        {
            var defaultCondition = Condition.Default;
            dataGridView.Rows.Add(Condition.Default.Id,
                                  Condition.Default.Value,
                                  false,
                                  "--",
                                  Condition.Default.Timer,
                                  ConditionDescriptions.Single(d => d.ID == Condition.Default.Id).Description);
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string headerName = dataGridView.Columns[e.ColumnIndex].Name;
            string formattedValue = e.FormattedValue?.ToString()!;
            string formatErrorText = string.Empty;

            switch (headerName)
            {
                case "Value":
                    if (!int.TryParse(formattedValue, out _))
                    {
                        formatErrorText = "効果量は整数値でなければなりません。";
                    }
                    break;
                case "Infinity":
                    bool isInfinityDuration = dataGridView.Rows[e.RowIndex].Cells["Infinity"]?.Value as bool? ?? false;
                    if (isInfinityDuration)
                    {
                        dataGridView.Rows[e.RowIndex].Cells["Duration"].Value = "--";
                        dataGridView.Rows[e.RowIndex].Cells["Duration"].ReadOnly = true;
                    }
                    else
                    {
                        dataGridView.Rows[e.RowIndex].Cells["Duration"].ReadOnly = false;
                    }
                    break;
                case "Duration":
                    isInfinityDuration = dataGridView.Rows[e.RowIndex].Cells["Infinity"]?.Value as bool? ?? false;
                    if (isInfinityDuration)
                    {
                        if (formattedValue != "--")
                        {
                            dataGridView.Rows[e.RowIndex].Cells["Duration"].Value = "--";
                        }
                    }
                    else
                    {
                        if (!double.TryParse(formattedValue, out _))
                        {
                            dataGridView.Rows[e.RowIndex].Cells["Duration"].ReadOnly = false;
                            formatErrorText = "永続化が無効の場合、持続時間は数値でなければなりません。";
                        }
                    }
                    break;
                case "Timer":
                    if (!double.TryParse(formattedValue, out _))
                    {
                        formatErrorText = "残り時間は数値でなければなりません。";
                        e.Cancel = true;
                    }
                    break;
                case "Description":
                    // Descriptionカラムが変更された場合、対応するConditionIdを更新
                    dataGridView.Rows[e.RowIndex].Cells["ConditionId"].Value =
                        ConditionDescriptions.Single(d =>
                        d.Description == dataGridView.Rows[e.RowIndex].Cells["Description"].EditedFormattedValue!.ToString()).ID;
                    break;
                default:
                    // 他のカラムに対する検証が必要な場合はここに追加
                    break;
            }
            if (!string.IsNullOrEmpty(formatErrorText))
            {
                dataGridView.Rows[e.RowIndex].ErrorText = formatErrorText;
                dataGridView[e.ColumnIndex, e.RowIndex].ToolTipText = formatErrorText;
                DataGridErrorTextLabel.Text = formatErrorText;
                DataGridErrorTextLabel.Visible = true;
                e.Cancel = true;
            }
            else
            {
                DataGridErrorTextLabel.Visible = false;
            }
        }

        private void overrideConditionsButton_Click(object sender, EventArgs e)
        {
            List<Condition> newConditions = RetrieveConditionsFromGrid();
            _saveDataManager.OverrideConditions(newConditions);

            updateConditionsLabel.Text = $"ConditionListを更新しました。\n" +
                $"{Conditions.Count} -> {newConditions.Count}";
            Conditions = newConditions;
            ShowResultLabel();

            var deleteRows = dataGridView.Rows.Cast<DataGridViewRow>()
                .Where(r => (int)r.Cells["ConditionId"].Value! == Condition.Default.Id)
                .ToList();
            foreach (var row in deleteRows)
            {
                dataGridView.Rows.Remove(row);
            }
        }

        private List<Condition> RetrieveConditionsFromGrid()
        {
            return dataGridView.Rows
                .Cast<DataGridViewRow>()
                .Select(r =>
                {
                    try
                    {
                        int id = int.Parse(r.Cells["ConditionId"].Value?.ToString()!);
                        int value = int.Parse(r.Cells["Value"].Value?.ToString()!);
                        double duration = (bool)(r.Cells["Infinity"].Value ?? false) ? double.PositiveInfinity
                        : double.Parse(r.Cells["Duration"].Value!.ToString()!);
                        double timer = double.Parse(r.Cells["Timer"].Value?.ToString()!);
                        return new Condition(id, value, duration, timer);
                    }
                    catch
                    {
                        return Condition.Default;
                    }
                })
                .Where(c => c.Id != Condition.Default.Id)
                .ToList();
        }

        private async void ShowResultLabel()
        {
            updateConditionsLabel.Visible = true;
            await Task.Delay(3000);
            updateConditionsLabel.Visible = false;
        }

        /// <summary>
        /// 現在編集中の行を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteSelectedRowButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    dataGridView.Rows.Remove(row);
                }
            }
        }

        private void ConditionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.ConditonFormSizeWidth = this.Size.Width;
            Settings.Default.ConditonFormSizeHeight = this.Size.Height;
            Settings.Default.Save();
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dataGridView.Columns[e.ColumnIndex];
            double value;
            switch (col)
            {
                case { Name: "Duration" }:
                    if (e.Value!.ToString() == "--") break;
                    double.TryParse(e.Value!.ToString(), out value);
                    e.Value = value.ToString("F1");
                    break;
                case { Name: "Timer" }:
                    double.TryParse(e.Value!.ToString(), out value);
                    e.Value = value.ToString("F1");
                    break;
                default:
                    break;
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var colName = dataGridView.Columns[e.ColumnIndex];
            var row = dataGridView.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];
            switch (colName)
            {
                case { Name: "Infinity" }:
                    if ((bool)cell.Value!)
                    {
                        // 永続化が有効な場合にDurationを自動で"--"にする
                        row.Cells["Duration"].Value = "--";
                        row.Cells["Timer"].Value = -1d;
                    }
                    else
                    {
                        // 永続化が無効な場合にDurationを0にする                      
                        row.Cells["Duration"].Value = 10d;
                        row.Cells["Timer"].Value = 10d;
                    }
                    break;
                default:
                    break;
            }
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!Program.IsDeveloper)
            {
                // 体力最大値増加系のコンディションは編集不可にする
                int conditionId = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ConditionId"].Value);
                if (IncreaseHealthMax.Contains(conditionId))
                {
                    dataGridView.Rows[e.RowIndex].ReadOnly = true;
                }
            }
        }
    }
}