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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            itemEditTabControl = new TabControl();
            foodTab = new TabPage();
            listUncreatedRecipesButton = new Button();
            rarityComboBox = new ComboBox();
            cookedCategoryComboBox = new ComboBox();
            label12 = new Label();
            label11 = new Label();
            toMinusOneButton = new Button();
            toMaxButton = new Button();
            label10 = new Label();
            createdNumericNo = new NumericUpDown();
            materialComboBoxB = new ComboBox();
            label5 = new Label();
            materialComboBoxA = new ComboBox();
            label4 = new Label();
            petTab = new TabPage();
            petNameTextBox = new TextBox();
            label16 = new Label();
            petTalent9ComboBox = new ComboBox();
            petTalent9ValidCheckBox = new CheckBox();
            petTalent8ComboBox = new ComboBox();
            petTalent8ValidCheckBox = new CheckBox();
            petTalent7ComboBox = new ComboBox();
            petTalent7ValidCheckBox = new CheckBox();
            petTalent6ComboBox = new ComboBox();
            petTalent6ValidCheckBox = new CheckBox();
            petTalent5ComboBox = new ComboBox();
            petTalent5ValidCheckBox = new CheckBox();
            petTalent4ComboBox = new ComboBox();
            petTalent4ValidCheckBox = new CheckBox();
            petTalent3ComboBox = new ComboBox();
            petTalent3ValidCheckBox = new CheckBox();
            petTalent2ComboBox = new ComboBox();
            petTalent2ValidCheckBox = new CheckBox();
            petTalent1ComboBox = new ComboBox();
            petTalent1ValidCheckBox = new CheckBox();
            petExpNumeric = new NumericUpDown();
            label15 = new Label();
            petKindComboBox = new ComboBox();
            label14 = new Label();
            petColorComboBox = new ComboBox();
            label13 = new Label();
            advancedTab = new TabPage();
            PaeteButton = new Button();
            CopyButton = new Button();
            auxDataTextBox = new TextBox();
            label18 = new Label();
            auxIndexTextBox = new TextBox();
            label17 = new Label();
            amountConst = new NumericUpDown();
            amountConstCheckBox = new CheckBox();
            SetDefaultButton = new Button();
            label3 = new Label();
            objectNameTextBox = new TextBox();
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
            resultLabel = new Label();
            previousItemButton = new Button();
            nextItemButton = new Button();
            openConditionsButton = new Button();
            linkLabel1 = new LinkLabel();
            itemEditTabControl.SuspendLayout();
            foodTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).BeginInit();
            petTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)petExpNumeric).BeginInit();
            advancedTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)amountConst).BeginInit();
            SuspendLayout();
            // 
            // itemEditTabControl
            // 
            itemEditTabControl.Controls.Add(foodTab);
            itemEditTabControl.Controls.Add(petTab);
            itemEditTabControl.Controls.Add(advancedTab);
            itemEditTabControl.Location = new Point(13, 110);
            itemEditTabControl.Name = "itemEditTabControl";
            itemEditTabControl.SelectedIndex = 0;
            itemEditTabControl.Size = new Size(635, 177);
            itemEditTabControl.TabIndex = 0;
            // 
            // foodTab
            // 
            foodTab.Controls.Add(listUncreatedRecipesButton);
            foodTab.Controls.Add(rarityComboBox);
            foodTab.Controls.Add(cookedCategoryComboBox);
            foodTab.Controls.Add(label12);
            foodTab.Controls.Add(label11);
            foodTab.Controls.Add(toMinusOneButton);
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
            foodTab.Size = new Size(627, 149);
            foodTab.TabIndex = 0;
            foodTab.Text = "料理作成";
            foodTab.UseVisualStyleBackColor = true;
            // 
            // listUncreatedRecipesButton
            // 
            listUncreatedRecipesButton.Location = new Point(274, 124);
            listUncreatedRecipesButton.Name = "listUncreatedRecipesButton";
            listUncreatedRecipesButton.Size = new Size(203, 23);
            listUncreatedRecipesButton.TabIndex = 18;
            listUncreatedRecipesButton.Text = "未作成の食材組み合わせ調査";
            listUncreatedRecipesButton.UseVisualStyleBackColor = true;
            listUncreatedRecipesButton.Click += listUncreatedRecipesButton_Click;
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
            // toMinusOneButton
            // 
            toMinusOneButton.Location = new Point(200, 124);
            toMinusOneButton.Name = "toMinusOneButton";
            toMinusOneButton.Size = new Size(53, 23);
            toMinusOneButton.TabIndex = 13;
            toMinusOneButton.Text = "-1個";
            toMinusOneButton.UseVisualStyleBackColor = true;
            toMinusOneButton.Visible = false;
            toMinusOneButton.Click += toMinusOneButton_Click;
            // 
            // toMaxButton
            // 
            toMaxButton.Location = new Point(141, 124);
            toMaxButton.Name = "toMaxButton";
            toMaxButton.Size = new Size(53, 23);
            toMaxButton.TabIndex = 12;
            toMaxButton.Text = "9999個";
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
            createdNumericNo.Maximum = new decimal(new int[] { 869778, 0, 0, 0 });
            createdNumericNo.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            createdNumericNo.Name = "createdNumericNo";
            createdNumericNo.Size = new Size(120, 23);
            createdNumericNo.TabIndex = 8;
            createdNumericNo.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // materialComboBoxB
            // 
            materialComboBoxB.DrawMode = DrawMode.OwnerDrawFixed;
            materialComboBoxB.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBoxB.FormattingEnabled = true;
            materialComboBoxB.Location = new Point(6, 72);
            materialComboBoxB.Name = "materialComboBoxB";
            materialComboBoxB.Size = new Size(221, 24);
            materialComboBoxB.TabIndex = 9;
            materialComboBoxB.DrawItem += materialComboBoxB_DrawItem;
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
            materialComboBoxA.DrawMode = DrawMode.OwnerDrawFixed;
            materialComboBoxA.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBoxA.FormattingEnabled = true;
            materialComboBoxA.Location = new Point(6, 21);
            materialComboBoxA.Name = "materialComboBoxA";
            materialComboBoxA.Size = new Size(221, 24);
            materialComboBoxA.TabIndex = 8;
            materialComboBoxA.DrawItem += materialComboBoxA_DrawItem;
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
            // petTab
            // 
            petTab.Controls.Add(petNameTextBox);
            petTab.Controls.Add(label16);
            petTab.Controls.Add(petTalent9ComboBox);
            petTab.Controls.Add(petTalent9ValidCheckBox);
            petTab.Controls.Add(petTalent8ComboBox);
            petTab.Controls.Add(petTalent8ValidCheckBox);
            petTab.Controls.Add(petTalent7ComboBox);
            petTab.Controls.Add(petTalent7ValidCheckBox);
            petTab.Controls.Add(petTalent6ComboBox);
            petTab.Controls.Add(petTalent6ValidCheckBox);
            petTab.Controls.Add(petTalent5ComboBox);
            petTab.Controls.Add(petTalent5ValidCheckBox);
            petTab.Controls.Add(petTalent4ComboBox);
            petTab.Controls.Add(petTalent4ValidCheckBox);
            petTab.Controls.Add(petTalent3ComboBox);
            petTab.Controls.Add(petTalent3ValidCheckBox);
            petTab.Controls.Add(petTalent2ComboBox);
            petTab.Controls.Add(petTalent2ValidCheckBox);
            petTab.Controls.Add(petTalent1ComboBox);
            petTab.Controls.Add(petTalent1ValidCheckBox);
            petTab.Controls.Add(petExpNumeric);
            petTab.Controls.Add(label15);
            petTab.Controls.Add(petKindComboBox);
            petTab.Controls.Add(label14);
            petTab.Controls.Add(petColorComboBox);
            petTab.Controls.Add(label13);
            petTab.Location = new Point(4, 24);
            petTab.Name = "petTab";
            petTab.Padding = new Padding(3);
            petTab.Size = new Size(627, 149);
            petTab.TabIndex = 2;
            petTab.Text = "ペット";
            petTab.UseVisualStyleBackColor = true;
            // 
            // petNameTextBox
            // 
            petNameTextBox.Location = new Point(496, 8);
            petNameTextBox.Name = "petNameTextBox";
            petNameTextBox.Size = new Size(100, 23);
            petNameTextBox.TabIndex = 26;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(459, 12);
            label16.Name = "label16";
            label16.Size = new Size(31, 15);
            label16.TabIndex = 25;
            label16.Text = "名前";
            // 
            // petTalent9ComboBox
            // 
            petTalent9ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent9ComboBox.DropDownWidth = 400;
            petTalent9ComboBox.FormattingEnabled = true;
            petTalent9ComboBox.Location = new Point(492, 104);
            petTalent9ComboBox.Name = "petTalent9ComboBox";
            petTalent9ComboBox.Size = new Size(129, 23);
            petTalent9ComboBox.TabIndex = 24;
            // 
            // petTalent9ValidCheckBox
            // 
            petTalent9ValidCheckBox.AutoSize = true;
            petTalent9ValidCheckBox.Location = new Point(426, 106);
            petTalent9ValidCheckBox.Name = "petTalent9ValidCheckBox";
            petTalent9ValidCheckBox.Size = new Size(60, 19);
            petTalent9ValidCheckBox.TabIndex = 23;
            petTalent9ValidCheckBox.Text = "スキル9";
            petTalent9ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent8ComboBox
            // 
            petTalent8ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent8ComboBox.DropDownWidth = 400;
            petTalent8ComboBox.FormattingEnabled = true;
            petTalent8ComboBox.Location = new Point(283, 104);
            petTalent8ComboBox.Name = "petTalent8ComboBox";
            petTalent8ComboBox.Size = new Size(129, 23);
            petTalent8ComboBox.TabIndex = 22;
            // 
            // petTalent8ValidCheckBox
            // 
            petTalent8ValidCheckBox.AutoSize = true;
            petTalent8ValidCheckBox.Location = new Point(217, 106);
            petTalent8ValidCheckBox.Name = "petTalent8ValidCheckBox";
            petTalent8ValidCheckBox.Size = new Size(60, 19);
            petTalent8ValidCheckBox.TabIndex = 21;
            petTalent8ValidCheckBox.Text = "スキル8";
            petTalent8ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent7ComboBox
            // 
            petTalent7ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent7ComboBox.DropDownWidth = 400;
            petTalent7ComboBox.FormattingEnabled = true;
            petTalent7ComboBox.Location = new Point(73, 104);
            petTalent7ComboBox.Name = "petTalent7ComboBox";
            petTalent7ComboBox.Size = new Size(129, 23);
            petTalent7ComboBox.TabIndex = 20;
            // 
            // petTalent7ValidCheckBox
            // 
            petTalent7ValidCheckBox.AutoSize = true;
            petTalent7ValidCheckBox.Location = new Point(7, 106);
            petTalent7ValidCheckBox.Name = "petTalent7ValidCheckBox";
            petTalent7ValidCheckBox.Size = new Size(60, 19);
            petTalent7ValidCheckBox.TabIndex = 19;
            petTalent7ValidCheckBox.Text = "スキル7";
            petTalent7ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent6ComboBox
            // 
            petTalent6ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent6ComboBox.DropDownWidth = 400;
            petTalent6ComboBox.FormattingEnabled = true;
            petTalent6ComboBox.Location = new Point(492, 79);
            petTalent6ComboBox.Name = "petTalent6ComboBox";
            petTalent6ComboBox.Size = new Size(129, 23);
            petTalent6ComboBox.TabIndex = 18;
            // 
            // petTalent6ValidCheckBox
            // 
            petTalent6ValidCheckBox.AutoSize = true;
            petTalent6ValidCheckBox.Location = new Point(426, 81);
            petTalent6ValidCheckBox.Name = "petTalent6ValidCheckBox";
            petTalent6ValidCheckBox.Size = new Size(60, 19);
            petTalent6ValidCheckBox.TabIndex = 17;
            petTalent6ValidCheckBox.Text = "スキル6";
            petTalent6ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent5ComboBox
            // 
            petTalent5ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent5ComboBox.DropDownWidth = 400;
            petTalent5ComboBox.FormattingEnabled = true;
            petTalent5ComboBox.Location = new Point(283, 79);
            petTalent5ComboBox.Name = "petTalent5ComboBox";
            petTalent5ComboBox.Size = new Size(129, 23);
            petTalent5ComboBox.TabIndex = 16;
            // 
            // petTalent5ValidCheckBox
            // 
            petTalent5ValidCheckBox.AutoSize = true;
            petTalent5ValidCheckBox.Location = new Point(217, 81);
            petTalent5ValidCheckBox.Name = "petTalent5ValidCheckBox";
            petTalent5ValidCheckBox.Size = new Size(60, 19);
            petTalent5ValidCheckBox.TabIndex = 15;
            petTalent5ValidCheckBox.Text = "スキル5";
            petTalent5ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent4ComboBox
            // 
            petTalent4ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent4ComboBox.DropDownWidth = 400;
            petTalent4ComboBox.FormattingEnabled = true;
            petTalent4ComboBox.Location = new Point(73, 79);
            petTalent4ComboBox.Name = "petTalent4ComboBox";
            petTalent4ComboBox.Size = new Size(129, 23);
            petTalent4ComboBox.TabIndex = 14;
            // 
            // petTalent4ValidCheckBox
            // 
            petTalent4ValidCheckBox.AutoSize = true;
            petTalent4ValidCheckBox.Location = new Point(7, 81);
            petTalent4ValidCheckBox.Name = "petTalent4ValidCheckBox";
            petTalent4ValidCheckBox.Size = new Size(60, 19);
            petTalent4ValidCheckBox.TabIndex = 13;
            petTalent4ValidCheckBox.Text = "スキル4";
            petTalent4ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent3ComboBox
            // 
            petTalent3ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent3ComboBox.DropDownWidth = 400;
            petTalent3ComboBox.FormattingEnabled = true;
            petTalent3ComboBox.Location = new Point(492, 54);
            petTalent3ComboBox.Name = "petTalent3ComboBox";
            petTalent3ComboBox.Size = new Size(129, 23);
            petTalent3ComboBox.TabIndex = 12;
            // 
            // petTalent3ValidCheckBox
            // 
            petTalent3ValidCheckBox.AutoSize = true;
            petTalent3ValidCheckBox.Location = new Point(426, 56);
            petTalent3ValidCheckBox.Name = "petTalent3ValidCheckBox";
            petTalent3ValidCheckBox.Size = new Size(60, 19);
            petTalent3ValidCheckBox.TabIndex = 11;
            petTalent3ValidCheckBox.Text = "スキル3";
            petTalent3ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent2ComboBox
            // 
            petTalent2ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent2ComboBox.DropDownWidth = 400;
            petTalent2ComboBox.FormattingEnabled = true;
            petTalent2ComboBox.Location = new Point(283, 54);
            petTalent2ComboBox.Name = "petTalent2ComboBox";
            petTalent2ComboBox.Size = new Size(129, 23);
            petTalent2ComboBox.TabIndex = 10;
            // 
            // petTalent2ValidCheckBox
            // 
            petTalent2ValidCheckBox.AutoSize = true;
            petTalent2ValidCheckBox.Location = new Point(217, 56);
            petTalent2ValidCheckBox.Name = "petTalent2ValidCheckBox";
            petTalent2ValidCheckBox.Size = new Size(60, 19);
            petTalent2ValidCheckBox.TabIndex = 9;
            petTalent2ValidCheckBox.Text = "スキル2";
            petTalent2ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petTalent1ComboBox
            // 
            petTalent1ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalent1ComboBox.DropDownWidth = 400;
            petTalent1ComboBox.FormattingEnabled = true;
            petTalent1ComboBox.Location = new Point(73, 54);
            petTalent1ComboBox.Name = "petTalent1ComboBox";
            petTalent1ComboBox.Size = new Size(129, 23);
            petTalent1ComboBox.TabIndex = 8;
            // 
            // petTalent1ValidCheckBox
            // 
            petTalent1ValidCheckBox.AutoSize = true;
            petTalent1ValidCheckBox.Location = new Point(7, 56);
            petTalent1ValidCheckBox.Name = "petTalent1ValidCheckBox";
            petTalent1ValidCheckBox.Size = new Size(60, 19);
            petTalent1ValidCheckBox.TabIndex = 7;
            petTalent1ValidCheckBox.Text = "スキル1";
            petTalent1ValidCheckBox.UseVisualStyleBackColor = true;
            // 
            // petExpNumeric
            // 
            petExpNumeric.Location = new Point(354, 9);
            petExpNumeric.Maximum = new decimal(new int[] { 107000, 0, 0, 0 });
            petExpNumeric.Name = "petExpNumeric";
            petExpNumeric.Size = new Size(83, 23);
            petExpNumeric.TabIndex = 6;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(305, 12);
            label15.Name = "label15";
            label15.Size = new Size(43, 15);
            label15.TabIndex = 5;
            label15.Text = "経験値";
            // 
            // petKindComboBox
            // 
            petKindComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petKindComboBox.FormattingEnabled = true;
            petKindComboBox.Location = new Point(44, 9);
            petKindComboBox.Name = "petKindComboBox";
            petKindComboBox.Size = new Size(140, 23);
            petKindComboBox.TabIndex = 4;
            petKindComboBox.SelectedIndexChanged += petKindComboBox_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(7, 12);
            label14.Name = "label14";
            label14.Size = new Size(31, 15);
            label14.TabIndex = 3;
            label14.Text = "種類";
            // 
            // petColorComboBox
            // 
            petColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petColorComboBox.FormattingEnabled = true;
            petColorComboBox.Location = new Point(224, 9);
            petColorComboBox.Name = "petColorComboBox";
            petColorComboBox.Size = new Size(64, 23);
            petColorComboBox.TabIndex = 2;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(205, 12);
            label13.Name = "label13";
            label13.Size = new Size(19, 15);
            label13.TabIndex = 0;
            label13.Text = "色";
            // 
            // advancedTab
            // 
            advancedTab.Controls.Add(linkLabel1);
            advancedTab.Controls.Add(PaeteButton);
            advancedTab.Controls.Add(CopyButton);
            advancedTab.Controls.Add(auxDataTextBox);
            advancedTab.Controls.Add(label18);
            advancedTab.Controls.Add(auxIndexTextBox);
            advancedTab.Controls.Add(label17);
            advancedTab.Controls.Add(amountConst);
            advancedTab.Controls.Add(amountConstCheckBox);
            advancedTab.Controls.Add(SetDefaultButton);
            advancedTab.Controls.Add(label3);
            advancedTab.Controls.Add(objectNameTextBox);
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
            advancedTab.Size = new Size(627, 149);
            advancedTab.TabIndex = 1;
            advancedTab.Text = "上級者向け";
            advancedTab.UseVisualStyleBackColor = true;
            // 
            // PaeteButton
            // 
            PaeteButton.Location = new Point(452, 119);
            PaeteButton.Name = "PaeteButton";
            PaeteButton.Size = new Size(79, 23);
            PaeteButton.TabIndex = 27;
            PaeteButton.Text = "ペースト";
            PaeteButton.UseVisualStyleBackColor = true;
            PaeteButton.Click += PaeteButton_Click;
            // 
            // CopyButton
            // 
            CopyButton.Location = new Point(316, 119);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(130, 23);
            CopyButton.TabIndex = 26;
            CopyButton.Text = "アイテム情報のコピー";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += CopyButton_Click;
            // 
            // auxDataTextBox
            // 
            auxDataTextBox.Location = new Point(368, 74);
            auxDataTextBox.Name = "auxDataTextBox";
            auxDataTextBox.ReadOnly = true;
            auxDataTextBox.Size = new Size(243, 23);
            auxDataTextBox.TabIndex = 25;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(368, 56);
            label18.Name = "label18";
            label18.Size = new Size(50, 15);
            label18.TabIndex = 24;
            label18.Text = "auxData";
            // 
            // auxIndexTextBox
            // 
            auxIndexTextBox.Location = new Point(446, 27);
            auxIndexTextBox.Name = "auxIndexTextBox";
            auxIndexTextBox.Size = new Size(100, 23);
            auxIndexTextBox.TabIndex = 23;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(446, 9);
            label17.Name = "label17";
            label17.Size = new Size(55, 15);
            label17.TabIndex = 22;
            label17.Text = "auxIndex";
            // 
            // amountConst
            // 
            amountConst.Location = new Point(121, 74);
            amountConst.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            amountConst.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            amountConst.Name = "amountConst";
            amountConst.Size = new Size(62, 23);
            amountConst.TabIndex = 21;
            amountConst.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // amountConstCheckBox
            // 
            amountConstCheckBox.AutoSize = true;
            amountConstCheckBox.Location = new Point(121, 56);
            amountConstCheckBox.Name = "amountConstCheckBox";
            amountConstCheckBox.Size = new Size(99, 19);
            amountConstCheckBox.TabIndex = 20;
            amountConstCheckBox.Text = "const amount";
            amountConstCheckBox.UseVisualStyleBackColor = true;
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
            label3.Location = new Point(226, 9);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 16;
            label3.Text = "objectName";
            // 
            // objectNameTextBox
            // 
            objectNameTextBox.Location = new Point(226, 27);
            objectNameTextBox.Name = "objectNameTextBox";
            objectNameTextBox.Size = new Size(209, 23);
            objectNameTextBox.TabIndex = 17;
            // 
            // objectIdsLinkLabel
            // 
            objectIdsLinkLabel.AutoSize = true;
            objectIdsLinkLabel.Location = new Point(9, 127);
            objectIdsLinkLabel.Name = "objectIdsLinkLabel";
            objectIdsLinkLabel.Size = new Size(87, 15);
            objectIdsLinkLabel.TabIndex = 14;
            objectIdsLinkLabel.TabStop = true;
            objectIdsLinkLabel.Text = "ObjectIDs(wiki)";
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
            variationUpdateCountTextBox.Location = new Point(226, 73);
            variationUpdateCountTextBox.Name = "variationUpdateCountTextBox";
            variationUpdateCountTextBox.ReadOnly = true;
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
            label9.Location = new Point(226, 57);
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
            label8.Location = new Point(120, 9);
            label8.Name = "label8";
            label8.Size = new Size(53, 15);
            label8.TabIndex = 12;
            label8.Text = "variation";
            // 
            // variationTextBox
            // 
            variationTextBox.Location = new Point(120, 27);
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
            savePathTextBox.Size = new Size(551, 23);
            savePathTextBox.TabIndex = 4;
            savePathTextBox.Validating += savePathTextBox_Validating;
            // 
            // saveFolderBrowserDialog
            // 
            saveFolderBrowserDialog.RootFolder = Environment.SpecialFolder.LocalApplicationData;
            // 
            // openSevePathDialogButton
            // 
            openSevePathDialogButton.Location = new Point(569, 26);
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
            itemSlotLabel.Size = new Size(72, 15);
            itemSlotLabel.TabIndex = 6;
            itemSlotLabel.Text = "インベントリ枠";
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
            // openConditionsButton
            // 
            openConditionsButton.BackColor = Color.Cyan;
            openConditionsButton.Location = new Point(529, 81);
            openConditionsButton.Name = "openConditionsButton";
            openConditionsButton.Size = new Size(104, 23);
            openConditionsButton.TabIndex = 17;
            openConditionsButton.Text = "コンディション値";
            openConditionsButton.UseVisualStyleBackColor = false;
            openConditionsButton.Click += openConditionsButton_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(9, 108);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(87, 15);
            linkLabel1.TabIndex = 28;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "パラメータについて";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(660, 374);
            Controls.Add(openConditionsButton);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "CKFoodMaker";
            FormClosing += Form1_FormClosing;
            itemEditTabControl.ResumeLayout(false);
            foodTab.ResumeLayout(false);
            foodTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).EndInit();
            petTab.ResumeLayout(false);
            petTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)petExpNumeric).EndInit();
            advancedTab.ResumeLayout(false);
            advancedTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)amountConst).EndInit();
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
        private Button toMinusOneButton;
        private ComboBox rarityComboBox;
        private ComboBox cookedCategoryComboBox;
        private Label label12;
        private Label label11;
        private Label label3;
        private TextBox objectNameTextBox;
        private Button SetDefaultButton;
        private Label resultLabel;
        private Button previousItemButton;
        private Button nextItemButton;
        private NumericUpDown amountConst;
        private CheckBox amountConstCheckBox;
        private TabPage petTab;
        private Label label13;
        private ComboBox petKindComboBox;
        private Label label14;
        private ComboBox petColorComboBox;
        private NumericUpDown petExpNumeric;
        private Label label15;
        private ComboBox petTalent1ComboBox;
        private CheckBox petTalent1ValidCheckBox;
        private ComboBox petTalent9ComboBox;
        private CheckBox petTalent9ValidCheckBox;
        private ComboBox petTalent8ComboBox;
        private CheckBox petTalent8ValidCheckBox;
        private ComboBox petTalent7ComboBox;
        private CheckBox petTalent7ValidCheckBox;
        private ComboBox petTalent6ComboBox;
        private CheckBox petTalent6ValidCheckBox;
        private ComboBox petTalent5ComboBox;
        private CheckBox petTalent5ValidCheckBox;
        private ComboBox petTalent4ComboBox;
        private CheckBox petTalent4ValidCheckBox;
        private ComboBox petTalent3ComboBox;
        private CheckBox petTalent3ValidCheckBox;
        private ComboBox petTalent2ComboBox;
        private CheckBox petTalent2ValidCheckBox;
        private TextBox petNameTextBox;
        private Label label16;
        private Button listUncreatedRecipesButton;
        private TextBox auxDataTextBox;
        private Label label18;
        private TextBox auxIndexTextBox;
        private Label label17;
        private Button openConditionsButton;
        private Button PaeteButton;
        private Button CopyButton;
        private LinkLabel linkLabel1;
    }
}
