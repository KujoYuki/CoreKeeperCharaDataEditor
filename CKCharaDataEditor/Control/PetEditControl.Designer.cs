namespace CKCharaDataEditor.Control
{
    partial class PetEditControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            battleTypeLabel = new Label();
            label19 = new Label();
            petTalentTableLayoutPanel = new TableLayoutPanel();
            petTalentControl0 = new PetTalentControl();
            petTalentControl8 = new PetTalentControl();
            petTalentControl7 = new PetTalentControl();
            petTalentControl6 = new PetTalentControl();
            petTalentControl5 = new PetTalentControl();
            petTalentControl4 = new PetTalentControl();
            petTalentControl3 = new PetTalentControl();
            petTalentControl2 = new PetTalentControl();
            petTalentControl1 = new PetTalentControl();
            petNameTextBox = new TextBox();
            label16 = new Label();
            petExpNumeric = new NumericUpDown();
            label15 = new Label();
            petKindComboBox = new ComboBox();
            label14 = new Label();
            petColorComboBox = new ComboBox();
            label13 = new Label();
            petTalentTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)petExpNumeric).BeginInit();
            SuspendLayout();
            // 
            // battleTypeLabel
            // 
            battleTypeLabel.AutoSize = true;
            battleTypeLabel.Location = new Point(274, 7);
            battleTypeLabel.Name = "battleTypeLabel";
            battleTypeLabel.Size = new Size(17, 15);
            battleTypeLabel.TabIndex = 39;
            battleTypeLabel.Text = "--";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(205, 7);
            label19.Name = "label19";
            label19.Size = new Size(70, 15);
            label19.TabIndex = 38;
            label19.Text = "戦闘タイプ：";
            // 
            // petTalentTableLayoutPanel
            // 
            petTalentTableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentTableLayoutPanel.ColumnCount = 3;
            petTalentTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33222F));
            petTalentTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.335556F));
            petTalentTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33222F));
            petTalentTableLayoutPanel.Controls.Add(petTalentControl0, 0, 0);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl8, 2, 2);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl7, 1, 2);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl6, 0, 2);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl5, 2, 1);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl4, 1, 1);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl3, 0, 1);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl2, 2, 0);
            petTalentTableLayoutPanel.Controls.Add(petTalentControl1, 1, 0);
            petTalentTableLayoutPanel.Location = new Point(8, 82);
            petTalentTableLayoutPanel.Name = "petTalentTableLayoutPanel";
            petTalentTableLayoutPanel.RowCount = 3;
            petTalentTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            petTalentTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            petTalentTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            petTalentTableLayoutPanel.Size = new Size(626, 183);
            petTalentTableLayoutPanel.TabIndex = 35;
            // 
            // petTalentControl0
            // 
            petTalentControl0.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl0.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl0.Location = new Point(3, 3);
            petTalentControl0.Name = "petTalentControl0";
            petTalentControl0.Size = new Size(202, 55);
            petTalentControl0.SlotNo = 0;
            petTalentControl0.TabIndex = 9;
            // 
            // petTalentControl8
            // 
            petTalentControl8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl8.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl8.Location = new Point(419, 125);
            petTalentControl8.Name = "petTalentControl8";
            petTalentControl8.Size = new Size(204, 55);
            petTalentControl8.SlotNo = 8;
            petTalentControl8.TabIndex = 8;
            // 
            // petTalentControl7
            // 
            petTalentControl7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl7.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl7.Location = new Point(211, 125);
            petTalentControl7.Name = "petTalentControl7";
            petTalentControl7.Size = new Size(202, 55);
            petTalentControl7.SlotNo = 7;
            petTalentControl7.TabIndex = 7;
            // 
            // petTalentControl6
            // 
            petTalentControl6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl6.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl6.Location = new Point(3, 125);
            petTalentControl6.Name = "petTalentControl6";
            petTalentControl6.Size = new Size(202, 55);
            petTalentControl6.SlotNo = 6;
            petTalentControl6.TabIndex = 6;
            // 
            // petTalentControl5
            // 
            petTalentControl5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl5.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl5.Location = new Point(419, 64);
            petTalentControl5.Name = "petTalentControl5";
            petTalentControl5.Size = new Size(204, 55);
            petTalentControl5.SlotNo = 5;
            petTalentControl5.TabIndex = 5;
            // 
            // petTalentControl4
            // 
            petTalentControl4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl4.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl4.Location = new Point(211, 64);
            petTalentControl4.Name = "petTalentControl4";
            petTalentControl4.Size = new Size(202, 55);
            petTalentControl4.SlotNo = 4;
            petTalentControl4.TabIndex = 4;
            // 
            // petTalentControl3
            // 
            petTalentControl3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl3.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl3.Location = new Point(3, 64);
            petTalentControl3.Name = "petTalentControl3";
            petTalentControl3.Size = new Size(202, 55);
            petTalentControl3.SlotNo = 3;
            petTalentControl3.TabIndex = 3;
            // 
            // petTalentControl2
            // 
            petTalentControl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl2.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl2.Location = new Point(419, 3);
            petTalentControl2.Name = "petTalentControl2";
            petTalentControl2.Size = new Size(204, 55);
            petTalentControl2.SlotNo = 2;
            petTalentControl2.TabIndex = 2;
            // 
            // petTalentControl1
            // 
            petTalentControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            petTalentControl1.BattleType = Model.Pet.PetBattleType.Undefined;
            petTalentControl1.Location = new Point(211, 3);
            petTalentControl1.Name = "petTalentControl1";
            petTalentControl1.Size = new Size(202, 55);
            petTalentControl1.SlotNo = 1;
            petTalentControl1.TabIndex = 1;
            // 
            // petNameTextBox
            // 
            petNameTextBox.Location = new Point(43, 38);
            petNameTextBox.Name = "petNameTextBox";
            petNameTextBox.Size = new Size(277, 23);
            petNameTextBox.TabIndex = 37;
            petNameTextBox.TextChanged += petNameTextBox_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 42);
            label16.Name = "label16";
            label16.Size = new Size(31, 15);
            label16.TabIndex = 36;
            label16.Text = "名前";
            // 
            // petExpNumeric
            // 
            petExpNumeric.Location = new Point(386, 39);
            petExpNumeric.Maximum = new decimal(new int[] { 107000, 0, 0, 0 });
            petExpNumeric.Name = "petExpNumeric";
            petExpNumeric.Size = new Size(160, 23);
            petExpNumeric.TabIndex = 34;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(337, 42);
            label15.Name = "label15";
            label15.Size = new Size(43, 15);
            label15.TabIndex = 33;
            label15.Text = "経験値";
            // 
            // petKindComboBox
            // 
            petKindComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petKindComboBox.FormattingEnabled = true;
            petKindComboBox.Location = new Point(43, 4);
            petKindComboBox.Name = "petKindComboBox";
            petKindComboBox.Size = new Size(140, 23);
            petKindComboBox.TabIndex = 32;
            petKindComboBox.SelectedIndexChanged += petKindComboBox_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 7);
            label14.Name = "label14";
            label14.Size = new Size(31, 15);
            label14.TabIndex = 31;
            label14.Text = "種類";
            // 
            // petColorComboBox
            // 
            petColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petColorComboBox.FormattingEnabled = true;
            petColorComboBox.Location = new Point(449, 4);
            petColorComboBox.Name = "petColorComboBox";
            petColorComboBox.Size = new Size(159, 23);
            petColorComboBox.TabIndex = 30;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(430, 7);
            label13.Name = "label13";
            label13.Size = new Size(19, 15);
            label13.TabIndex = 29;
            label13.Text = "色";
            // 
            // PetEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(battleTypeLabel);
            Controls.Add(label19);
            Controls.Add(petTalentTableLayoutPanel);
            Controls.Add(petNameTextBox);
            Controls.Add(label16);
            Controls.Add(petExpNumeric);
            Controls.Add(label15);
            Controls.Add(petKindComboBox);
            Controls.Add(label14);
            Controls.Add(petColorComboBox);
            Controls.Add(label13);
            Name = "PetEditControl";
            Size = new Size(639, 270);
            petTalentTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)petExpNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label battleTypeLabel;
        private Label label19;
        private TableLayoutPanel petTalentTableLayoutPanel;
        private PetTalentControl petTalentControl0;
        private PetTalentControl petTalentControl8;
        private PetTalentControl petTalentControl7;
        private PetTalentControl petTalentControl6;
        private PetTalentControl petTalentControl5;
        private PetTalentControl petTalentControl4;
        private PetTalentControl petTalentControl3;
        private PetTalentControl petTalentControl2;
        private PetTalentControl petTalentControl1;
        private TextBox petNameTextBox;
        private Label label16;
        private NumericUpDown petExpNumeric;
        private Label label15;
        private ComboBox petKindComboBox;
        private Label label14;
        private ComboBox petColorComboBox;
        private Label label13;
    }
}
