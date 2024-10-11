using CKFoodMaker.Model;
using System.Data;
using System.Diagnostics;

namespace CKFoodMaker
{
    public partial class ConditionForm : Form
    {
        readonly SaveDataManager _saveDataManager = SaveDataManager.Instance;
        static readonly List<(int ID, string Description)> ConditionDescriptions = LoadConditionDescriptions();

        private List<Condition> Conditions = [];

        public ConditionForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = Cursor.Position;
            Conditions = new(_saveDataManager.GetConditions().OrderBy(c => c.Id));
            ((DataGridViewComboBoxColumn)dataGridView.Columns["Description"]).Items
                .AddRange(ConditionDescriptions.Select(c => c.Description).ToArray());
            LoadConditionsToGrid();
        }

        private static List<(int ID, string Description)> LoadConditionDescriptions()
        {
            string conditionDescriptionFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "ConditionDescription.csv")
                ?? throw new FileNotFoundException("ConditionDescription.csvが見つかりません。");

            var dic = File.ReadLines(conditionDescriptionFilePath)
                .Select(line =>
                {
                    var sprit = line.Split(',');
                    return (int.Parse(sprit[0]), $"[{int.Parse(sprit[0])},{sprit[1]}] : {sprit[2]}");
                })
                .ToList();
            return new(dic);
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
                                      ConditionDescriptions.Single(d => d.ID == condition.Id).Description);
            }
        }

        private void aboutConditionIdLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/KujoYuki/CoreKeeperFoodEditor/blob/main/Document/conditions.md") { UseShellExecute = true });
        }

        private void conditionListLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/KujoYuki/CoreKeeperFoodEditor/blob/main/CKFoodMaker/Resource/ConditionDescription.csv") { UseShellExecute = true });
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
                Conditions = new(condtions);
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
            string headerName = dataGridView.Columns[e.ColumnIndex].Name;
            string formattedValue = e.FormattedValue?.ToString()!;
            string formatErrorText = string.Empty;

            switch (headerName)
            {
                case "Value":
                    if (!int.TryParse(formattedValue, out _))
                    {
                        formatErrorText = "Valueは整数値でなければなりません。";
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
                            formatErrorText = "Infinityがfalseの場合、Durationは数値でなければなりません。";
                        }
                    }
                    break;
                case "Timer":
                    if (!double.TryParse(formattedValue, out _))
                    {
                        formatErrorText = "Timerは数値でなければなりません。";
                        e.Cancel = true;
                    }
                    break;
                case "Description":
                    var tmp = dataGridView.Rows[e.RowIndex].Cells["ConditionId"].Value =
                        ConditionDescriptions.Single(d =>
                        d.Description == dataGridView.Rows[e.RowIndex].Cells["Description"].EditedFormattedValue.ToString()).ID;
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
            var newConditions = RetrieveConditionsFromGrid();
            _saveDataManager.OverrideConditions(newConditions);

            resultLabel.Text = $"ConditionListを更新しました。{Conditions.Count} -> {newConditions.Count}";
            Conditions = newConditions;
            ShowResultLabel();

            var deleteRows = dataGridView.Rows.Cast<DataGridViewRow>()
                .Where(r => (int)r.Cells["ConditionId"].Value == Condition.Default.Id)
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
                .Take(dataGridView.Rows.Count - 1)
                .Select(r =>
                {
                    try
                    {
                        int id = int.Parse(r.Cells["ConditionId"].Value?.ToString()!);
                        int value = int.Parse(r.Cells["Value"].Value?.ToString()!);
                        double duration = (bool)(r.Cells["Infinity"].Value ?? false) ? double.PositiveInfinity : double.Parse(r.Cells["Duration"].Value.ToString()!);
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
            resultLabel.Visible = true;
            await Task.Delay(3000);
            resultLabel.Visible = false;
        }

        /// <summary>
        /// 現在編集中のセルが含まれる行を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteSelectedRowButton_Click(object sender, EventArgs e)
        {

            if (dataGridView.CurrentCell is not null)
            {
                int rowIndex = dataGridView.CurrentCell.RowIndex;
                if (rowIndex >= 0 && rowIndex < dataGridView.Rows.Count &&
                    rowIndex != dataGridView.NewRowIndex)
                {
                    dataGridView.Rows.RemoveAt(rowIndex);
                }
            }
        }
    }
}
