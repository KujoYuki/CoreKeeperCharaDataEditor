using CKCharaDataEditor.Resource;
using System.ComponentModel;
using System.Data;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CKCharaDataEditor
{
    public partial class WorldSetteingForm : Form
    {
        private FileManager _fileManager = FileManager.Instance;
        private JsonObject _saveData = [];

        private string _saveDataPath = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SaveDataPath
        {
            get => _saveDataPath;
            set
            {
                if (File.Exists(value))
                {
                    _saveDataPath = value;
                    _saveData = JsonNode.Parse(File.ReadAllText(value))?.AsObject()
                        ?? throw new ArgumentException("Failed parse worldinfo.");
                }
                else
                {
                    throw new FileNotFoundException("Save data file not found.", value);
                }
            }
        }

        public WorldSetteingForm()
        {
            InitializeComponent();
            LoadSlots();
        }

        private string GetSeedString(string selectedFilePath)
        {
            string seedString = _saveData["seedString"]!.ToString();
            if (seedString == string.Empty)
            {
                return "None";
            }
            else return seedString;
        }

        private Guid GetWorldId(string selectedFilePath)
        {
            return new(_saveData["guid"]!.GetValue<string>());
        }

        private string GetWorldName(string path)
        {
            return _saveData["name"]!.GetValue<string>();
        }

        private int GetWorldMode()
        {
            return _saveData["mode"]!.GetValue<int>();
        }

        private void LoadSlots()
        {
            worldComboBox.Items.Clear();
            foreach (var filePath in _fileManager.WorldFilePaths)
            {
                SaveDataPath = filePath.FullName;
                string worldName = GetWorldName(filePath.FullName);
                var fileName = Path.GetFileNameWithoutExtension(filePath.FullName);
                if (int.TryParse(fileName, out int slotNo))
                {
                    // ゲーム内スロット表記に合わせて1を加える
                    worldComboBox.Items.Add($"{slotNo + 1}, {worldName}");
                }
                else
                {
                    worldComboBox.Items.Add($"{fileName}, {worldName}");
                }
            }
            if (worldComboBox.Items.Count > 0)
            {
                worldComboBox.SelectedIndex = 0;
            }
            LoadWorldInfo();
        }

        public void LoadWorldInfo()
        {
            seedStringLabel.Text = GetSeedString(SaveDataPath);
            worldIdLabel.Text = GetWorldId(SaveDataPath).ToString();
            worldNameTextBox.Text = GetWorldName(SaveDataPath);
            int mode = GetWorldMode();
            foreach (var control in ModeGroupBox.Controls.OfType<RadioButton>())
            {
                control.Checked = Convert.ToInt32(control.Tag) is int tag && tag == mode;
            }
        }

        private void worldComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDataPath = _fileManager.WorldFilePaths[worldComboBox.SelectedIndex].FullName;
            LoadWorldInfo();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            // 入力値でワールド名を更新
            string newWorldName = worldNameTextBox.Text;
            _saveData["name"] = newWorldName;
            // Mode設定を更新
            int newMode = ModeGroupBox.Controls.OfType<RadioButton>()
                .Where(control => control.Checked is true)
                .Select(control => Convert.ToInt32(control.Tag!))
                .FirstOrDefault();
            _saveData["mode"] = newMode;
            string changedJson = JsonSerializer.Serialize(_saveData, StaticResource.SerializerOption);
            File.WriteAllText(SaveDataPath, changedJson);
            Close();
        }

        private void worldIdLabel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(worldIdLabel.Text);
            MessageBox.Show("World ID copied to clipboard.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
