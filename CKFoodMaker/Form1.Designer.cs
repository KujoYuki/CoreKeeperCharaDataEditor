namespace CKFoodMaker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            itemEditTabControl = new TabControl();
            foodTab = new TabPage();
            rarityComboBox = new ComboBox();
            cookedCategoryComboBox = new ComboBox();
            label12 = new Label();
            label11 = new Label();
            button1 = new Button();
            toMaxButton = new Button();
            label10 = new Label();
            createdNumericNo = new NumericUpDown();
            materialComboBoxB = new ComboBox();
            label5 = new Label();
            materialComboBoxA = new ComboBox();
            label4 = new Label();
            advancedTab = new TabPage();
            SetDefaultButton = new Button();
            label3 = new Label();
            internalNameTextBox = new TextBox();
            objectIdsLinkLabel = new LinkLabel();
            label6 = new Label();
            variationUpdateCountTextBox = new TextBox();
            objectIdTextBox = new TextBox();
            label7 = new Label();
            label9 = new Label();
            amoutTextBox = new TextBox();
            label8 = new Label();
            variationTextBox = new TextBox();
            label1 = new Label();
            saveSlotNoComboBox = new ComboBox();
            label2 = new Label();
            savePathTextBox = new TextBox();
            saveFolderBrowserDialog = new FolderBrowserDialog();
            openSevePathDialogButton = new Button();
            itemSlotLabel = new Label();
            inventoryIndexComboBox = new ComboBox();
            createButton = new Button();
            itemSlotToolTip = new ToolTip(components);
            resultLabel = new Label();
            previousItemButton = new Button();
            nextItemButton = new Button();
            amountRangeCheckBox = new CheckBox();
            amountRangeDown = new NumericUpDown();
            amountRangeUp = new NumericUpDown();
            label13 = new Label();
            itemEditTabControl.SuspendLayout();
            foodTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).BeginInit();
            advancedTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)amountRangeDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)amountRangeUp).BeginInit();
            SuspendLayout();
            // 
            // itemEditTabControl
            // 
            itemEditTabControl.Controls.Add(foodTab);
            itemEditTabControl.Controls.Add(advancedTab);
            itemEditTabControl.Location = new Point(13, 110);
            itemEditTabControl.Name = "itemEditTabControl";
            itemEditTabControl.SelectedIndex = 0;
            itemEditTabControl.Size = new Size(511, 196);
            itemEditTabControl.TabIndex = 0;
            // 
            // foodTab
            // 
            foodTab.Controls.Add(rarityComboBox);
            foodTab.Controls.Add(cookedCategoryComboBox);
            foodTab.Controls.Add(label12);
            foodTab.Controls.Add(label11);
            foodTab.Controls.Add(button1);
            foodTab.Controls.Add(toMaxButton);
            foodTab.Controls.Add(label10);
            foodTab.Controls.Add(createdNumericNo);
            foodTab.Controls.Add(materialComboBoxB);
            foodTab.Controls.Add(label5);
            foodTab.Controls.Add(materialComboBoxA);
            foodTab.Controls.Add(label4);
            foodTab.Location = new Point(4, 24);
            foodTab.Name = "foodTab";
            foodTab.Padding = new Padding(3);
            foodTab.Size = new Size(503, 168);
            foodTab.TabIndex = 0;
            foodTab.Text = "料理作成";
            foodTab.UseVisualStyleBackColor = true;
            // 
            // rarityComboBox
            // 
            rarityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            rarityComboBox.FormattingEnabled = true;
            rarityComboBox.Items.AddRange(new object[] { "コモン", "レア", "エピック" });
            rarityComboBox.Location = new Point(256, 72);
            rarityComboBox.Name = "rarityComboBox";
            rarityComboBox.Size = new Size(221, 23);
            rarityComboBox.TabIndex = 17;
            // 
            // cookedCategoryComboBox
            // 
            cookedCategoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cookedCategoryComboBox.FormattingEnabled = true;
            cookedCategoryComboBox.Location = new Point(256, 21);
            cookedCategoryComboBox.Name = "cookedCategoryComboBox";
            cookedCategoryComboBox.Size = new Size(221, 23);
            cookedCategoryComboBox.TabIndex = 16;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(256, 54);
            label12.Name = "label12";
            label12.Size = new Size(37, 15);
            label12.TabIndex = 15;
            label12.Text = "レア度";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(256, 3);
            label11.Name = "label11";
            label11.Size = new Size(78, 15);
            label11.TabIndex = 14;
            label11.Text = "調理後カテゴリ";
            // 
            // button1
            // 
            button1.Location = new Point(200, 124);
            button1.Name = "button1";
            button1.Size = new Size(53, 23);
            button1.TabIndex = 13;
            button1.Text = "-1個";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // toMaxButton
            // 
            toMaxButton.Location = new Point(141, 124);
            toMaxButton.Name = "toMaxButton";
            toMaxButton.Size = new Size(53, 23);
            toMaxButton.TabIndex = 12;
            toMaxButton.Text = "999個";
            toMaxButton.UseVisualStyleBackColor = true;
            toMaxButton.Click += toMaxButton_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(7, 108);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 9;
            label10.Text = "作成個数";
            // 
            // createdNumericNo
            // 
            createdNumericNo.Location = new Point(7, 126);
            createdNumericNo.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            createdNumericNo.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            createdNumericNo.Name = "createdNumericNo";
            createdNumericNo.Size = new Size(120, 23);
            createdNumericNo.TabIndex = 8;
            createdNumericNo.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // materialComboBoxB
            // 
            materialComboBoxB.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBoxB.FormattingEnabled = true;
            materialComboBoxB.Location = new Point(6, 72);
            materialComboBoxB.Name = "materialComboBoxB";
            materialComboBoxB.Size = new Size(221, 23);
            materialComboBoxB.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 54);
            label5.Name = "label5";
            label5.Size = new Size(56, 15);
            label5.TabIndex = 10;
            label5.Text = "食材その2";
            // 
            // materialComboBoxA
            // 
            materialComboBoxA.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBoxA.FormattingEnabled = true;
            materialComboBoxA.Location = new Point(6, 21);
            materialComboBoxA.Name = "materialComboBoxA";
            materialComboBoxA.Size = new Size(221, 23);
            materialComboBoxA.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 3);
            label4.Name = "label4";
            label4.Size = new Size(56, 15);
            label4.TabIndex = 8;
            label4.Text = "食材その1";
            // 
            // advancedTab
            // 
            advancedTab.Controls.Add(label13);
            advancedTab.Controls.Add(amountRangeUp);
            advancedTab.Controls.Add(amountRangeDown);
            advancedTab.Controls.Add(amountRangeCheckBox);
            advancedTab.Controls.Add(SetDefaultButton);
            advancedTab.Controls.Add(label3);
            advancedTab.Controls.Add(internalNameTextBox);
            advancedTab.Controls.Add(objectIdsLinkLabel);
            advancedTab.Controls.Add(label6);
            advancedTab.Controls.Add(variationUpdateCountTextBox);
            advancedTab.Controls.Add(objectIdTextBox);
            advancedTab.Controls.Add(label7);
            advancedTab.Controls.Add(label9);
            advancedTab.Controls.Add(amoutTextBox);
            advancedTab.Controls.Add(label8);
            advancedTab.Controls.Add(variationTextBox);
            advancedTab.Location = new Point(4, 24);
            advancedTab.Name = "advancedTab";
            advancedTab.Padding = new Padding(3);
            advancedTab.Size = new Size(503, 168);
            advancedTab.TabIndex = 1;
            advancedTab.Text = "上級者向け";
            advancedTab.UseVisualStyleBackColor = true;
            // 
            // SetDefaultButton
            // 
            SetDefaultButton.Location = new Point(136, 119);
            SetDefaultButton.Name = "SetDefaultButton";
            SetDefaultButton.Size = new Size(152, 23);
            SetDefaultButton.TabIndex = 18;
            SetDefaultButton.Text = "デフォルト値(空アイテム)セット";
            SetDefaultButton.UseVisualStyleBackColor = true;
            SetDefaultButton.Click += SetDefaultButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(271, 9);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 16;
            label3.Text = "internalName";
            // 
            // internalNameTextBox
            // 
            internalNameTextBox.Location = new Point(271, 27);
            internalNameTextBox.Name = "internalNameTextBox";
            internalNameTextBox.Size = new Size(209, 23);
            internalNameTextBox.TabIndex = 17;
            // 
            // objectIdsLinkLabel
            // 
            objectIdsLinkLabel.AutoSize = true;
            objectIdsLinkLabel.Location = new Point(9, 123);
            objectIdsLinkLabel.Name = "objectIdsLinkLabel";
            objectIdsLinkLabel.Size = new Size(87, 15);
            objectIdsLinkLabel.TabIndex = 14;
            objectIdsLinkLabel.TabStop = true;
            objectIdsLinkLabel.Text = "ObjectIDs(wiki)";
            itemSlotToolTip.SetToolTip(objectIdsLinkLabel, "海外wikiのURLを開きます。");
            objectIdsLinkLabel.LinkClicked += objectIdsLinkLabel_LinkClicked;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 9);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 8;
            label6.Text = "objectID";
            // 
            // variationUpdateCountTextBox
            // 
            variationUpdateCountTextBox.Location = new Point(136, 75);
            variationUpdateCountTextBox.Name = "variationUpdateCountTextBox";
            variationUpdateCountTextBox.Size = new Size(100, 23);
            variationUpdateCountTextBox.TabIndex = 15;
            // 
            // objectIdTextBox
            // 
            objectIdTextBox.Location = new Point(9, 27);
            objectIdTextBox.Name = "objectIdTextBox";
            objectIdTextBox.Size = new Size(100, 23);
            objectIdTextBox.TabIndex = 9;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 57);
            label7.Name = "label7";
            label7.Size = new Size(48, 15);
            label7.TabIndex = 10;
            label7.Text = "amount";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(136, 57);
            label9.Name = "label9";
            label9.Size = new Size(123, 15);
            label9.TabIndex = 14;
            label9.Text = "variationUpdateCount";
            // 
            // amoutTextBox
            // 
            amoutTextBox.Location = new Point(9, 75);
            amoutTextBox.Name = "amoutTextBox";
            amoutTextBox.Size = new Size(100, 23);
            amoutTextBox.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(136, 9);
            label8.Name = "label8";
            label8.Size = new Size(53, 15);
            label8.TabIndex = 12;
            label8.Text = "variation";
            // 
            // variationTextBox
            // 
            variationTextBox.Location = new Point(136, 27);
            variationTextBox.Name = "variationTextBox";
            variationTextBox.Size = new Size(100, 23);
            variationTextBox.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 63);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 1;
            label1.Text = "セーブスロットNo";
            // 
            // saveSlotNoComboBox
            // 
            saveSlotNoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            saveSlotNoComboBox.FormattingEnabled = true;
            saveSlotNoComboBox.Location = new Point(12, 81);
            saveSlotNoComboBox.Name = "saveSlotNoComboBox";
            saveSlotNoComboBox.Size = new Size(60, 23);
            saveSlotNoComboBox.TabIndex = 2;
            saveSlotNoComboBox.SelectedIndexChanged += saveSlotNoComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 3;
            label2.Text = "セーブデータフォルダ";
            // 
            // savePathTextBox
            // 
            savePathTextBox.Location = new Point(12, 27);
            savePathTextBox.Name = "savePathTextBox";
            savePathTextBox.Size = new Size(431, 23);
            savePathTextBox.TabIndex = 4;
            savePathTextBox.TextChanged += savePathTextBox_TextChanged;
            // 
            // saveFolderBrowserDialog
            // 
            saveFolderBrowserDialog.RootFolder = Environment.SpecialFolder.LocalApplicationData;
            // 
            // openSevePathDialogButton
            // 
            openSevePathDialogButton.Location = new Point(449, 26);
            openSevePathDialogButton.Name = "openSevePathDialogButton";
            openSevePathDialogButton.Size = new Size(75, 23);
            openSevePathDialogButton.TabIndex = 5;
            openSevePathDialogButton.Text = "パス指定...";
            openSevePathDialogButton.UseVisualStyleBackColor = true;
            openSevePathDialogButton.Click += openSevePathDialogButton_Click;
            // 
            // itemSlotLabel
            // 
            itemSlotLabel.AutoSize = true;
            itemSlotLabel.Location = new Point(127, 63);
            itemSlotLabel.Name = "itemSlotLabel";
            itemSlotLabel.Size = new Size(89, 15);
            itemSlotLabel.TabIndex = 6;
            itemSlotLabel.Text = "アイテムスロット枠";
            // 
            // inventoryIndexComboBox
            // 
            inventoryIndexComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            inventoryIndexComboBox.FormattingEnabled = true;
            inventoryIndexComboBox.Location = new Point(127, 81);
            inventoryIndexComboBox.Name = "inventoryIndexComboBox";
            inventoryIndexComboBox.Size = new Size(239, 23);
            inventoryIndexComboBox.TabIndex = 7;
            inventoryIndexComboBox.TextChanged += inventoryIndexComboBox_TextChanged;
            // 
            // createButton
            // 
            createButton.Location = new Point(13, 312);
            createButton.Name = "createButton";
            createButton.Size = new Size(147, 45);
            createButton.TabIndex = 13;
            createButton.Text = "作成";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(172, 327);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(64, 15);
            resultLabel.TabIndex = 14;
            resultLabel.Text = "resultLabel";
            resultLabel.Visible = false;
            // 
            // previousItemButton
            // 
            previousItemButton.Location = new Point(385, 80);
            previousItemButton.Name = "previousItemButton";
            previousItemButton.Size = new Size(58, 23);
            previousItemButton.TabIndex = 15;
            previousItemButton.Text = "◀";
            previousItemButton.UseVisualStyleBackColor = true;
            previousItemButton.Click += previousItemButton_Click;
            // 
            // nextItemButton
            // 
            nextItemButton.Location = new Point(449, 80);
            nextItemButton.Name = "nextItemButton";
            nextItemButton.Size = new Size(58, 23);
            nextItemButton.TabIndex = 16;
            nextItemButton.Text = "▶";
            nextItemButton.UseVisualStyleBackColor = true;
            nextItemButton.Click += nextItemButton_Click;
            // 
            // amountRangeCheckBox
            // 
            amountRangeCheckBox.AutoSize = true;
            amountRangeCheckBox.Location = new Point(320, 79);
            amountRangeCheckBox.Name = "amountRangeCheckBox";
            amountRangeCheckBox.Size = new Size(144, 19);
            amountRangeCheckBox.TabIndex = 20;
            amountRangeCheckBox.Text = "amount random range";
            amountRangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // amountRangeDown
            // 
            amountRangeDown.Location = new Point(320, 119);
            amountRangeDown.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            amountRangeDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            amountRangeDown.Name = "amountRangeDown";
            amountRangeDown.Size = new Size(62, 23);
            amountRangeDown.TabIndex = 21;
            amountRangeDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // amountRangeUp
            // 
            amountRangeUp.Location = new Point(409, 119);
            amountRangeUp.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            amountRangeUp.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            amountRangeUp.Name = "amountRangeUp";
            amountRangeUp.Size = new Size(62, 23);
            amountRangeUp.TabIndex = 22;
            amountRangeUp.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(389, 122);
            label13.Name = "label13";
            label13.Size = new Size(12, 15);
            label13.TabIndex = 23;
            label13.Text = "-";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 378);
            Controls.Add(nextItemButton);
            Controls.Add(previousItemButton);
            Controls.Add(resultLabel);
            Controls.Add(createButton);
            Controls.Add(inventoryIndexComboBox);
            Controls.Add(itemSlotLabel);
            Controls.Add(openSevePathDialogButton);
            Controls.Add(savePathTextBox);
            Controls.Add(label2);
            Controls.Add(saveSlotNoComboBox);
            Controls.Add(label1);
            Controls.Add(itemEditTabControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "CKFoodMaker";
            FormClosing += Form1_FormClosing;
            itemEditTabControl.ResumeLayout(false);
            foodTab.ResumeLayout(false);
            foodTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).EndInit();
            advancedTab.ResumeLayout(false);
            advancedTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)amountRangeDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)amountRangeUp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl itemEditTabControl;
        private TabPage foodTab;
        private TabPage advancedTab;
        private Label label1;
        private ComboBox saveSlotNoComboBox;
        private Label label2;
        private TextBox savePathTextBox;
        private FolderBrowserDialog saveFolderBrowserDialog;
        private Button openSevePathDialogButton;
        private Label itemSlotLabel;
        private ComboBox inventoryIndexComboBox;
        private ComboBox materialComboBoxB;
        private Label label5;
        private ComboBox materialComboBoxA;
        private Label label4;
        private TextBox objectIdTextBox;
        private Label label6;
        private TextBox variationTextBox;
        private Label label8;
        private TextBox amoutTextBox;
        private Label label7;
        private Button toMaxButton;
        private Label label10;
        private NumericUpDown createdNumericNo;
        private TextBox variationUpdateCountTextBox;
        private Label label9;
        private Button createButton;
        private LinkLabel objectIdsLinkLabel;
        private Button button1;
        private ComboBox rarityComboBox;
        private ComboBox cookedCategoryComboBox;
        private Label label12;
        private Label label11;
        private ToolTip itemSlotToolTip;
        private Label label3;
        private TextBox internalNameTextBox;
        private Button SetDefaultButton;
        private Label resultLabel;
        private Button previousItemButton;
        private Button nextItemButton;
        private NumericUpDown amountRangeDown;
        private CheckBox amountRangeCheckBox;
        private NumericUpDown amountRangeUp;
        private Label label13;
    }
}
