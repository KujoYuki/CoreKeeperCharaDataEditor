namespace CKCharaDataEditor
{
    partial class DropForm
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DropForm));
            lootTableListBox = new ListBox();
            label1 = new Label();
            label2 = new Label();
            lootDataGridView = new DataGridView();
            objectID = new DataGridViewTextBoxColumn();
            ItemName = new DataGridViewTextBoxColumn();
            Amount = new DataGridViewTextBoxColumn();
            Weight = new DataGridViewTextBoxColumn();
            RollPerDop = new DataGridViewTextBoxColumn();
            GuaranteedRoll = new DataGridViewTextBoxColumn();
            label3 = new Label();
            groupBox1 = new GroupBox();
            playerCountNumericUpDown = new NumericUpDown();
            increasedChanceToGetFish = new NumericUpDown();
            label13 = new Label();
            increasedChanceToGetFishLoot = new NumericUpDown();
            notFishLabel = new Label();
            guaranteedRollCountLabel = new Label();
            label20 = new Label();
            normalRollCountLabel = new Label();
            label15 = new Label();
            linkLabel1 = new LinkLabel();
            expectedCountForDrop90Label = new Label();
            expectedCountForDrop70Label = new Label();
            expectedCountForDrop50Label = new Label();
            expectedCountForDrop25Label = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            decimalPlacesNumericUpDown = new NumericUpDown();
            label6 = new Label();
            tryRollsDataGridView = new DataGridView();
            TryCount = new DataGridViewTextBoxColumn();
            Rate = new DataGridViewTextBoxColumn();
            label5 = new Label();
            worldModeComboBox = new ComboBox();
            label4 = new Label();
            lootIdExplaneLabel = new Label();
            lootIdLabel = new Label();
            skillNotFishToolTip = new ToolTip(components);
            copyTableButton = new Button();
            label12 = new Label();
            searchIdTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)lootDataGridView).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)playerCountNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)increasedChanceToGetFish).BeginInit();
            ((System.ComponentModel.ISupportInitialize)increasedChanceToGetFishLoot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)decimalPlacesNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tryRollsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // lootTableListBox
            // 
            lootTableListBox.DrawMode = DrawMode.OwnerDrawFixed;
            lootTableListBox.FormattingEnabled = true;
            lootTableListBox.ItemHeight = 15;
            lootTableListBox.Location = new Point(12, 27);
            lootTableListBox.Name = "lootTableListBox";
            lootTableListBox.Size = new Size(183, 589);
            lootTableListBox.TabIndex = 0;
            lootTableListBox.DrawItem += lootTableListBox_DrawItem;
            lootTableListBox.SelectedIndexChanged += lootTableListBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 1;
            label1.Text = "テーブル選択";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(201, 9);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 2;
            label2.Text = "テーブル内容";
            // 
            // lootDataGridView
            // 
            lootDataGridView.AllowUserToAddRows = false;
            lootDataGridView.AllowUserToDeleteRows = false;
            lootDataGridView.AllowUserToResizeRows = false;
            lootDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            lootDataGridView.Columns.AddRange(new DataGridViewColumn[] { objectID, ItemName, Amount, Weight, RollPerDop, GuaranteedRoll });
            lootDataGridView.Location = new Point(201, 27);
            lootDataGridView.MultiSelect = false;
            lootDataGridView.Name = "lootDataGridView";
            lootDataGridView.ReadOnly = true;
            lootDataGridView.RowHeadersVisible = false;
            lootDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lootDataGridView.Size = new Size(744, 359);
            lootDataGridView.TabIndex = 3;
            lootDataGridView.CellEnter += lootDataGridView_CellEnter;
            lootDataGridView.CellFormatting += lootDataGridView_CellFormatting;
            // 
            // objectID
            // 
            objectID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            objectID.DefaultCellStyle = dataGridViewCellStyle1;
            objectID.HeaderText = "objectID";
            objectID.Name = "objectID";
            objectID.ReadOnly = true;
            objectID.Width = 76;
            // 
            // ItemName
            // 
            ItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ItemName.HeaderText = "アイテム名";
            ItemName.Name = "ItemName";
            ItemName.ReadOnly = true;
            ItemName.Width = 63;
            // 
            // Amount
            // 
            Amount.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            Amount.DefaultCellStyle = dataGridViewCellStyle2;
            Amount.HeaderText = "個数";
            Amount.Name = "Amount";
            Amount.ReadOnly = true;
            Amount.Width = 52;
            // 
            // Weight
            // 
            Weight.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            Weight.DefaultCellStyle = dataGridViewCellStyle3;
            Weight.HeaderText = "ウェイト";
            Weight.Name = "Weight";
            Weight.ReadOnly = true;
            Weight.Width = 53;
            // 
            // RollPerDop
            // 
            RollPerDop.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            RollPerDop.DefaultCellStyle = dataGridViewCellStyle4;
            RollPerDop.HeaderText = "通常抽選枠確率[%]";
            RollPerDop.Name = "RollPerDop";
            RollPerDop.ReadOnly = true;
            RollPerDop.Width = 96;
            // 
            // GuaranteedRoll
            // 
            GuaranteedRoll.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            GuaranteedRoll.DefaultCellStyle = dataGridViewCellStyle5;
            GuaranteedRoll.HeaderText = "保証抽選枠確率[%]";
            GuaranteedRoll.Name = "GuaranteedRoll";
            GuaranteedRoll.ReadOnly = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(169, 360);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(playerCountNumericUpDown);
            groupBox1.Controls.Add(increasedChanceToGetFish);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(increasedChanceToGetFishLoot);
            groupBox1.Controls.Add(notFishLabel);
            groupBox1.Controls.Add(guaranteedRollCountLabel);
            groupBox1.Controls.Add(label20);
            groupBox1.Controls.Add(normalRollCountLabel);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(linkLabel1);
            groupBox1.Controls.Add(expectedCountForDrop90Label);
            groupBox1.Controls.Add(expectedCountForDrop70Label);
            groupBox1.Controls.Add(expectedCountForDrop50Label);
            groupBox1.Controls.Add(expectedCountForDrop25Label);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(decimalPlacesNumericUpDown);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(tryRollsDataGridView);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(worldModeComboBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(201, 392);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(744, 224);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "試行回数シミュレート";
            // 
            // playerCountNumericUpDown
            // 
            playerCountNumericUpDown.Location = new Point(276, 60);
            playerCountNumericUpDown.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            playerCountNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            playerCountNumericUpDown.Name = "playerCountNumericUpDown";
            playerCountNumericUpDown.Size = new Size(47, 23);
            playerCountNumericUpDown.TabIndex = 26;
            playerCountNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            playerCountNumericUpDown.ValueChanged += playerCountNumericUpDown_ValueChanged;
            // 
            // increasedChanceToGetFish
            // 
            increasedChanceToGetFish.Location = new Point(276, 126);
            increasedChanceToGetFish.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            increasedChanceToGetFish.Name = "increasedChanceToGetFish";
            increasedChanceToGetFish.Size = new Size(48, 23);
            increasedChanceToGetFish.TabIndex = 25;
            increasedChanceToGetFish.ValueChanged += increasedChanceToGetFish_ValueChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(167, 132);
            label13.Name = "label13";
            label13.Size = new Size(103, 15);
            label13.TabIndex = 24;
            label13.Text = "魚が針にかかる確率";
            // 
            // increasedChanceToGetFishLoot
            // 
            increasedChanceToGetFishLoot.Location = new Point(276, 97);
            increasedChanceToGetFishLoot.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
            increasedChanceToGetFishLoot.Name = "increasedChanceToGetFishLoot";
            increasedChanceToGetFishLoot.Size = new Size(48, 23);
            increasedChanceToGetFishLoot.TabIndex = 23;
            increasedChanceToGetFishLoot.ValueChanged += increasedChanceToGetFishLoot_ValueChanged;
            // 
            // notFishLabel
            // 
            notFishLabel.AutoSize = true;
            notFishLabel.Location = new Point(15, 102);
            notFishLabel.Name = "notFishLabel";
            notFishLabel.Size = new Size(255, 15);
            notFishLabel.TabIndex = 22;
            notFishLabel.Text = "釣りで貴重または魚以外のアイテムが針にかかる確率";
            // 
            // guaranteedRollCountLabel
            // 
            guaranteedRollCountLabel.AutoSize = true;
            guaranteedRollCountLabel.Location = new Point(283, 31);
            guaranteedRollCountLabel.Name = "guaranteedRollCountLabel";
            guaranteedRollCountLabel.Size = new Size(13, 15);
            guaranteedRollCountLabel.TabIndex = 21;
            guaranteedRollCountLabel.Text = "0";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(195, 31);
            label20.Name = "label20";
            label20.Size = new Size(82, 15);
            label20.TabIndex = 20;
            label20.Text = "保証抽選枠数:";
            // 
            // normalRollCountLabel
            // 
            normalRollCountLabel.AutoSize = true;
            normalRollCountLabel.Location = new Point(101, 31);
            normalRollCountLabel.Name = "normalRollCountLabel";
            normalRollCountLabel.Size = new Size(13, 15);
            normalRollCountLabel.TabIndex = 17;
            normalRollCountLabel.Text = "0";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(13, 31);
            label15.Name = "label15";
            label15.Size = new Size(82, 15);
            label15.TabIndex = 16;
            label15.Text = "通常抽選枠数:";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(549, 189);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(76, 15);
            linkLabel1.TabIndex = 15;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "計算の仕組み";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // expectedCountForDrop90Label
            // 
            expectedCountForDrop90Label.AutoSize = true;
            expectedCountForDrop90Label.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            expectedCountForDrop90Label.Location = new Point(675, 140);
            expectedCountForDrop90Label.Name = "expectedCountForDrop90Label";
            expectedCountForDrop90Label.Size = new Size(50, 21);
            expectedCountForDrop90Label.TabIndex = 14;
            expectedCountForDrop90Label.Text = "None";
            // 
            // expectedCountForDrop70Label
            // 
            expectedCountForDrop70Label.AutoSize = true;
            expectedCountForDrop70Label.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            expectedCountForDrop70Label.Location = new Point(675, 102);
            expectedCountForDrop70Label.Name = "expectedCountForDrop70Label";
            expectedCountForDrop70Label.Size = new Size(50, 21);
            expectedCountForDrop70Label.TabIndex = 13;
            expectedCountForDrop70Label.Text = "None";
            // 
            // expectedCountForDrop50Label
            // 
            expectedCountForDrop50Label.AutoSize = true;
            expectedCountForDrop50Label.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            expectedCountForDrop50Label.Location = new Point(675, 63);
            expectedCountForDrop50Label.Name = "expectedCountForDrop50Label";
            expectedCountForDrop50Label.Size = new Size(50, 21);
            expectedCountForDrop50Label.TabIndex = 12;
            expectedCountForDrop50Label.Text = "None";
            // 
            // expectedCountForDrop25Label
            // 
            expectedCountForDrop25Label.AutoSize = true;
            expectedCountForDrop25Label.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            expectedCountForDrop25Label.Location = new Point(675, 22);
            expectedCountForDrop25Label.Name = "expectedCountForDrop25Label";
            expectedCountForDrop25Label.Size = new Size(50, 21);
            expectedCountForDrop25Label.TabIndex = 11;
            expectedCountForDrop25Label.Text = "None";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Yu Gothic UI", 12F);
            label10.Location = new Point(549, 140);
            label10.Name = "label10";
            label10.Size = new Size(129, 21);
            label10.TabIndex = 10;
            label10.Text = "90%を超える回数:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Yu Gothic UI", 12F);
            label9.Location = new Point(549, 102);
            label9.Name = "label9";
            label9.Size = new Size(129, 21);
            label9.TabIndex = 9;
            label9.Text = "70%を超える回数:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Yu Gothic UI", 12F);
            label8.Location = new Point(549, 63);
            label8.Name = "label8";
            label8.Size = new Size(129, 21);
            label8.TabIndex = 8;
            label8.Text = "50%を超える回数:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI", 12F);
            label7.Location = new Point(549, 22);
            label7.Name = "label7";
            label7.Size = new Size(129, 21);
            label7.TabIndex = 7;
            label7.Text = "25%を超える回数:";
            // 
            // decimalPlacesNumericUpDown
            // 
            decimalPlacesNumericUpDown.Location = new Point(143, 163);
            decimalPlacesNumericUpDown.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            decimalPlacesNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            decimalPlacesNumericUpDown.Name = "decimalPlacesNumericUpDown";
            decimalPlacesNumericUpDown.Size = new Size(54, 23);
            decimalPlacesNumericUpDown.TabIndex = 6;
            decimalPlacesNumericUpDown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            decimalPlacesNumericUpDown.ValueChanged += decimalPlacesNumericUpDown_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 166);
            label6.Name = "label6";
            label6.Size = new Size(125, 15);
            label6.TabIndex = 5;
            label6.Text = "小数点以下の表示桁数";
            // 
            // tryRollsDataGridView
            // 
            tryRollsDataGridView.AllowUserToAddRows = false;
            tryRollsDataGridView.AllowUserToDeleteRows = false;
            tryRollsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tryRollsDataGridView.Columns.AddRange(new DataGridViewColumn[] { TryCount, Rate });
            tryRollsDataGridView.Location = new Point(365, 22);
            tryRollsDataGridView.MultiSelect = false;
            tryRollsDataGridView.Name = "tryRollsDataGridView";
            tryRollsDataGridView.ReadOnly = true;
            tryRollsDataGridView.RowHeadersVisible = false;
            tryRollsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tryRollsDataGridView.Size = new Size(170, 182);
            tryRollsDataGridView.TabIndex = 4;
            tryRollsDataGridView.CellFormatting += tryRollsDataGridView_CellFormatting;
            // 
            // TryCount
            // 
            TryCount.HeaderText = "回数";
            TryCount.Name = "TryCount";
            TryCount.ReadOnly = true;
            TryCount.SortMode = DataGridViewColumnSortMode.NotSortable;
            TryCount.Width = 50;
            // 
            // Rate
            // 
            Rate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Rate.HeaderText = "確率[%]";
            Rate.Name = "Rate";
            Rate.ReadOnly = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(195, 63);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 2;
            label5.Text = "セッション人数";
            // 
            // worldModeComboBox
            // 
            worldModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            worldModeComboBox.FormattingEnabled = true;
            worldModeComboBox.Items.AddRange(new object[] { "Hard以外", "Hard" });
            worldModeComboBox.Location = new Point(101, 60);
            worldModeComboBox.Name = "worldModeComboBox";
            worldModeComboBox.Size = new Size(76, 23);
            worldModeComboBox.TabIndex = 1;
            worldModeComboBox.SelectedIndexChanged += worldModeComboBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 63);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 0;
            label4.Text = "ワールド難易度";
            // 
            // lootIdExplaneLabel
            // 
            lootIdExplaneLabel.AutoSize = true;
            lootIdExplaneLabel.Location = new Point(201, 627);
            lootIdExplaneLabel.Name = "lootIdExplaneLabel";
            lootIdExplaneLabel.Size = new Size(45, 15);
            lootIdExplaneLabel.TabIndex = 7;
            lootIdExplaneLabel.Text = "LootID:";
            lootIdExplaneLabel.Visible = false;
            // 
            // lootIdLabel
            // 
            lootIdLabel.AutoSize = true;
            lootIdLabel.Location = new Point(252, 627);
            lootIdLabel.Name = "lootIdLabel";
            lootIdLabel.Size = new Size(36, 15);
            lootIdLabel.TabIndex = 8;
            lootIdLabel.Text = "None";
            lootIdLabel.Visible = false;
            // 
            // skillNotFishToolTip
            // 
            skillNotFishToolTip.AutomaticDelay = 100;
            skillNotFishToolTip.AutoPopDelay = 5000;
            skillNotFishToolTip.InitialDelay = 100;
            skillNotFishToolTip.ReshowDelay = 20;
            // 
            // copyTableButton
            // 
            copyTableButton.Location = new Point(408, 619);
            copyTableButton.Name = "copyTableButton";
            copyTableButton.Size = new Size(140, 23);
            copyTableButton.TabIndex = 9;
            copyTableButton.Text = "テーブルコピー(wiki用)";
            copyTableButton.UseVisualStyleBackColor = true;
            copyTableButton.Visible = false;
            copyTableButton.Click += copyTableButton_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(12, 627);
            label12.Name = "label12";
            label12.Size = new Size(88, 15);
            label12.TabIndex = 10;
            label12.Text = "objectIDで検索:";
            // 
            // searchIdTextBox
            // 
            searchIdTextBox.Location = new Point(95, 624);
            searchIdTextBox.Name = "searchIdTextBox";
            searchIdTextBox.Size = new Size(100, 23);
            searchIdTextBox.TabIndex = 11;
            searchIdTextBox.TextChanged += searchIdTextBox_TextChanged;
            // 
            // DropForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 664);
            Controls.Add(searchIdTextBox);
            Controls.Add(label12);
            Controls.Add(copyTableButton);
            Controls.Add(lootIdLabel);
            Controls.Add(lootIdExplaneLabel);
            Controls.Add(groupBox1);
            Controls.Add(label3);
            Controls.Add(lootDataGridView);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lootTableListBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "DropForm";
            Text = "ドロップ率計算";
            ((System.ComponentModel.ISupportInitialize)lootDataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)playerCountNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)increasedChanceToGetFish).EndInit();
            ((System.ComponentModel.ISupportInitialize)increasedChanceToGetFishLoot).EndInit();
            ((System.ComponentModel.ISupportInitialize)decimalPlacesNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)tryRollsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lootTableListBox;
        private Label label1;
        private Label label2;
        private DataGridView lootDataGridView;
        private Label label3;
        private GroupBox groupBox1;
        private Label label4;
        private ComboBox worldModeComboBox;
        private Label label5;
        private DataGridView tryRollsDataGridView;
        private Label label7;
        private NumericUpDown decimalPlacesNumericUpDown;
        private Label label6;
        private Label expectedCountForDrop90Label;
        private Label expectedCountForDrop70Label;
        private Label expectedCountForDrop50Label;
        private Label expectedCountForDrop25Label;
        private Label label10;
        private Label label9;
        private Label label8;
        private LinkLabel linkLabel1;
        private Label normalRollCountLabel;
        private Label label15;
        private Label guaranteedRollCountLabel;
        private Label label20;
        private DataGridViewTextBoxColumn TryCount;
        private DataGridViewTextBoxColumn Rate;
        private Label lootIdExplaneLabel;
        private Label lootIdLabel;
        private Label notFishLabel;
        private Label label13;
        private NumericUpDown increasedChanceToGetFishLoot;
        private NumericUpDown increasedChanceToGetFish;
        private ToolTip skillNotFishToolTip;
        private Button copyTableButton;
        private Label label12;
        private TextBox searchIdTextBox;
        private NumericUpDown playerCountNumericUpDown;
        private DataGridViewTextBoxColumn objectID;
        private DataGridViewTextBoxColumn ItemName;
        private DataGridViewTextBoxColumn Amount;
        private DataGridViewTextBoxColumn Weight;
        private DataGridViewTextBoxColumn RollPerDop;
        private DataGridViewTextBoxColumn GuaranteedRoll;
    }
}