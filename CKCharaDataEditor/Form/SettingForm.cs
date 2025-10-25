namespace CKCharaDataEditor
{
    public partial class SettingForm : Form
    {
        private FileManager _fileManager = FileManager.Instance;

        private string _saveFolderPath = string.Empty;
        public string SaveFolderPath
        {
            get => _saveFolderPath;
            set
            {
                if (Directory.Exists(value))
                {
                    _saveFolderPath = value;
                    savePathTextBox.Text = value;
                }
                else
                {
                    MessageBox.Show($"指定されたフォルダが存在しません。正しいパスを入力してください。\nPath:{value}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string _installFolderPath = string.Empty;
        public string InstallFolderPath
        {
            get => _installFolderPath;
            set
            {
                try
                {
                    _installFolderPath = value;
                    installPathTextBox.Text = value;
                }
                catch (Exception)
                {
                    MessageBox.Show($"指定されたフォルダ内にリソースが存在しません。正しいパスを入力してください。\nPath:{value}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void savePathTextBox_Leave(object sender, EventArgs e)
        {
            SaveFolderPath = savePathTextBox.Text;
        }

        private void installPathTextBox_Leave(object sender, EventArgs e)
        {
            InstallFolderPath = installPathTextBox.Text;
        }

        public SettingForm()
        {
            InitializeComponent();
            InitializeFolderPaths();
            saveDataPathExampleLabel.Text = saveDataPathExampleLabel.Text.Replace("UserName", Environment.UserName);
        }

        private void InitializeFolderPaths()
        {
            saveFolderBrowserDialog.SelectedPath = _fileManager.SaveFolder;
            savePathTextBox.Text = _fileManager.SaveFolder;

            installFolderBrowserDialog.SelectedPath = _fileManager.InstallFolder;
            installPathTextBox.Text = _fileManager.InstallFolder;
        }

        private void openSevePathDialogButton_Click(object sender, EventArgs e)
        {
            saveFolderBrowserDialog.SelectedPath = _fileManager.SaveFolder;
            if (saveFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFolderPath = saveFolderBrowserDialog.SelectedPath;
                savePathTextBox.Text = saveFolderBrowserDialog.SelectedPath;
            }
        }

        private void openInstallPathDialogButton_Click(object sender, EventArgs e)
        {
            installFolderBrowserDialog.SelectedPath = _fileManager.InstallFolder;
            if (installFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                InstallFolderPath = installFolderBrowserDialog.SelectedPath;
                installPathTextBox.Text = installFolderBrowserDialog.SelectedPath;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
