namespace CKCharaDataEditor
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            label1 = new Label();
            openSevePathDialogButton = new Button();
            savePathTextBox = new TextBox();
            openInstallPathDialogButton = new Button();
            installPathTextBox = new TextBox();
            label2 = new Label();
            saveFolderBrowserDialog = new FolderBrowserDialog();
            installFolderBrowserDialog = new FolderBrowserDialog();
            okButton = new Button();
            cancelButton = new Button();
            saveDataPathExampleLabel = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 23);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 0;
            label1.Text = "セーブデータパス";
            // 
            // openSevePathDialogButton
            // 
            openSevePathDialogButton.Location = new Point(634, 40);
            openSevePathDialogButton.Name = "openSevePathDialogButton";
            openSevePathDialogButton.Size = new Size(75, 23);
            openSevePathDialogButton.TabIndex = 7;
            openSevePathDialogButton.Text = "パス指定...";
            openSevePathDialogButton.UseVisualStyleBackColor = true;
            openSevePathDialogButton.Click += openSevePathDialogButton_Click;
            // 
            // savePathTextBox
            // 
            savePathTextBox.Location = new Point(22, 41);
            savePathTextBox.Name = "savePathTextBox";
            savePathTextBox.Size = new Size(593, 23);
            savePathTextBox.TabIndex = 6;
            savePathTextBox.Leave += savePathTextBox_Leave;
            // 
            // openInstallPathDialogButton
            // 
            openInstallPathDialogButton.Location = new Point(632, 109);
            openInstallPathDialogButton.Name = "openInstallPathDialogButton";
            openInstallPathDialogButton.Size = new Size(75, 23);
            openInstallPathDialogButton.TabIndex = 10;
            openInstallPathDialogButton.Text = "パス指定...";
            openInstallPathDialogButton.UseVisualStyleBackColor = true;
            openInstallPathDialogButton.Click += openInstallPathDialogButton_Click;
            // 
            // installPathTextBox
            // 
            installPathTextBox.Location = new Point(22, 110);
            installPathTextBox.Name = "installPathTextBox";
            installPathTextBox.Size = new Size(593, 23);
            installPathTextBox.TabIndex = 9;
            installPathTextBox.Leave += installPathTextBox_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 92);
            label2.Name = "label2";
            label2.Size = new Size(289, 15);
            label2.TabIndex = 8;
            label2.Text = "インストールフォルダパス（翻訳リソースの取得に仕様します）";
            // 
            // saveFolderBrowserDialog
            // 
            saveFolderBrowserDialog.Description = "セーブデータのフォルダパスを選択してください。";
            // 
            // installFolderBrowserDialog
            // 
            installFolderBrowserDialog.Description = "ゲームのインストールフォルダを指定してください。";
            // 
            // okButton
            // 
            okButton.Location = new Point(511, 176);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 11;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(621, 176);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 12;
            cancelButton.Text = "キャンセル";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // saveDataPathExampleLabel
            // 
            saveDataPathExampleLabel.AutoSize = true;
            saveDataPathExampleLabel.Location = new Point(39, 67);
            saveDataPathExampleLabel.Name = "saveDataPathExampleLabel";
            saveDataPathExampleLabel.Size = new Size(466, 15);
            saveDataPathExampleLabel.TabIndex = 13;
            saveDataPathExampleLabel.Text = "(例) C:\\Users\\UserName\\AppData\\LocalLow\\Pugstorm\\Core Keeper\\Steam\\xxxxxxxxx";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(39, 136);
            label4.Name = "label4";
            label4.Size = new Size(368, 15);
            label4.TabIndex = 14;
            label4.Text = "(例) C:\\Program Files (x86)\\Steam\\steamapps\\common\\Core Keeper";
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(732, 222);
            Controls.Add(label4);
            Controls.Add(saveDataPathExampleLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(openInstallPathDialogButton);
            Controls.Add(installPathTextBox);
            Controls.Add(label2);
            Controls.Add(openSevePathDialogButton);
            Controls.Add(savePathTextBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingForm";
            Text = "ファイルパス設定";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button openSevePathDialogButton;
        private TextBox savePathTextBox;
        private Button openInstallPathDialogButton;
        private TextBox installPathTextBox;
        private Label label2;
        private FolderBrowserDialog saveFolderBrowserDialog;
        private FolderBrowserDialog installFolderBrowserDialog;
        private Button okButton;
        private Button cancelButton;
        private Label saveDataPathExampleLabel;
        private Label label4;
    }
}