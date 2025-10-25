namespace CKCharaDataEditor
{
    partial class WorldSetteingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldSetteingForm));
            label1 = new Label();
            worldComboBox = new ComboBox();
            label2 = new Label();
            worldIdLabel = new Label();
            ModeGroupBox = new GroupBox();
            HardCreativeRadioButton = new RadioButton();
            CreativeRadioButton = new RadioButton();
            HardRadioButton = new RadioButton();
            NormalRadioButton = new RadioButton();
            CasualRadioButton = new RadioButton();
            label3 = new Label();
            seedStringLabel = new Label();
            label5 = new Label();
            worldNameTextBox = new TextBox();
            applyButton = new Button();
            cancelButton = new Button();
            ModeGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 0;
            label1.Text = "ワールド一覧";
            // 
            // worldComboBox
            // 
            worldComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            worldComboBox.FormattingEnabled = true;
            worldComboBox.Location = new Point(12, 36);
            worldComboBox.Name = "worldComboBox";
            worldComboBox.Size = new Size(155, 23);
            worldComboBox.TabIndex = 1;
            worldComboBox.SelectedIndexChanged += worldComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 99);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 2;
            label2.Text = "World GUID : ";
            // 
            // worldIdLabel
            // 
            worldIdLabel.AutoSize = true;
            worldIdLabel.Location = new Point(85, 99);
            worldIdLabel.Name = "worldIdLabel";
            worldIdLabel.Size = new Size(27, 15);
            worldIdLabel.TabIndex = 3;
            worldIdLabel.Text = "----";
            worldIdLabel.Click += worldIdLabel_Click;
            // 
            // ModeGroupBox
            // 
            ModeGroupBox.Controls.Add(HardCreativeRadioButton);
            ModeGroupBox.Controls.Add(CreativeRadioButton);
            ModeGroupBox.Controls.Add(HardRadioButton);
            ModeGroupBox.Controls.Add(NormalRadioButton);
            ModeGroupBox.Controls.Add(CasualRadioButton);
            ModeGroupBox.Location = new Point(12, 163);
            ModeGroupBox.Name = "ModeGroupBox";
            ModeGroupBox.Size = new Size(297, 81);
            ModeGroupBox.TabIndex = 4;
            ModeGroupBox.TabStop = false;
            ModeGroupBox.Text = "モード設定";
            // 
            // HardCreativeRadioButton
            // 
            HardCreativeRadioButton.AutoSize = true;
            HardCreativeRadioButton.Location = new Point(114, 49);
            HardCreativeRadioButton.Name = "HardCreativeRadioButton";
            HardCreativeRadioButton.Size = new Size(119, 19);
            HardCreativeRadioButton.TabIndex = 4;
            HardCreativeRadioButton.TabStop = true;
            HardCreativeRadioButton.Tag = "3";
            HardCreativeRadioButton.Text = "Hard and Creative";
            HardCreativeRadioButton.UseVisualStyleBackColor = true;
            // 
            // CreativeRadioButton
            // 
            CreativeRadioButton.AutoSize = true;
            CreativeRadioButton.Location = new Point(15, 49);
            CreativeRadioButton.Name = "CreativeRadioButton";
            CreativeRadioButton.Size = new Size(67, 19);
            CreativeRadioButton.TabIndex = 3;
            CreativeRadioButton.TabStop = true;
            CreativeRadioButton.Tag = "2";
            CreativeRadioButton.Text = "Creative";
            CreativeRadioButton.UseVisualStyleBackColor = true;
            // 
            // HardRadioButton
            // 
            HardRadioButton.AutoSize = true;
            HardRadioButton.Location = new Point(224, 24);
            HardRadioButton.Name = "HardRadioButton";
            HardRadioButton.Size = new Size(51, 19);
            HardRadioButton.TabIndex = 2;
            HardRadioButton.TabStop = true;
            HardRadioButton.Tag = "1";
            HardRadioButton.Text = "Hard";
            HardRadioButton.UseVisualStyleBackColor = true;
            // 
            // NormalRadioButton
            // 
            NormalRadioButton.AutoSize = true;
            NormalRadioButton.Location = new Point(114, 24);
            NormalRadioButton.Name = "NormalRadioButton";
            NormalRadioButton.Size = new Size(64, 19);
            NormalRadioButton.TabIndex = 1;
            NormalRadioButton.TabStop = true;
            NormalRadioButton.Tag = "0";
            NormalRadioButton.Text = "Normal";
            NormalRadioButton.UseVisualStyleBackColor = true;
            // 
            // CasualRadioButton
            // 
            CasualRadioButton.AutoSize = true;
            CasualRadioButton.Location = new Point(15, 24);
            CasualRadioButton.Name = "CasualRadioButton";
            CasualRadioButton.Size = new Size(59, 19);
            CasualRadioButton.TabIndex = 0;
            CasualRadioButton.TabStop = true;
            CasualRadioButton.Tag = "4";
            CasualRadioButton.Text = "Casual";
            CasualRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 72);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 5;
            label3.Text = "SeedString :";
            // 
            // seedStringLabel
            // 
            seedStringLabel.AutoSize = true;
            seedStringLabel.Location = new Point(87, 72);
            seedStringLabel.Name = "seedStringLabel";
            seedStringLabel.Size = new Size(27, 15);
            seedStringLabel.TabIndex = 6;
            seedStringLabel.Text = "----";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 126);
            label5.Name = "label5";
            label5.Size = new Size(54, 15);
            label5.TabIndex = 7;
            label5.Text = "ワールド名";
            // 
            // worldNameTextBox
            // 
            worldNameTextBox.Location = new Point(72, 123);
            worldNameTextBox.Name = "worldNameTextBox";
            worldNameTextBox.Size = new Size(237, 23);
            worldNameTextBox.TabIndex = 8;
            // 
            // applyButton
            // 
            applyButton.Location = new Point(61, 259);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(75, 23);
            applyButton.TabIndex = 9;
            applyButton.Text = "適用する";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(183, 259);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 10;
            cancelButton.Text = "キャンセル";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // WorldSetteingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(321, 294);
            Controls.Add(cancelButton);
            Controls.Add(applyButton);
            Controls.Add(worldNameTextBox);
            Controls.Add(label5);
            Controls.Add(seedStringLabel);
            Controls.Add(label3);
            Controls.Add(ModeGroupBox);
            Controls.Add(worldIdLabel);
            Controls.Add(label2);
            Controls.Add(worldComboBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WorldSetteingForm";
            Text = "ワールド設定変更";
            ModeGroupBox.ResumeLayout(false);
            ModeGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox worldComboBox;
        private Label label2;
        private Label worldIdLabel;
        private GroupBox ModeGroupBox;
        private RadioButton CasualRadioButton;
        private RadioButton HardCreativeRadioButton;
        private RadioButton CreativeRadioButton;
        private RadioButton HardRadioButton;
        private RadioButton NormalRadioButton;
        private Label label3;
        private Label seedStringLabel;
        private Label label5;
        private TextBox worldNameTextBox;
        private Button applyButton;
        private Button cancelButton;
    }
}