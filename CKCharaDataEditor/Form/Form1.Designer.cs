namespace CKCharaDataEditor
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
            advancedTab = new TabPage();
            itemDataGroupBox = new GroupBox();
            objectIdNumericUpDown = new NumericUpDown();
            variationNumericUpDown = new NumericUpDown();
            labelVariation = new Label();
            label9 = new Label();
            labelAmount = new Label();
            auxIndexNumericUpDown = new NumericUpDown();
            label6 = new Label();
            keyNameTextBox = new TextBox();
            variationUpdateCountNumericUpDown = new NumericUpDown();
            label3 = new Label();
            amountNumericUpDown = new NumericUpDown();
            amountConstCheckBox = new CheckBox();
            objectLockedCheckBox = new CheckBox();
            amountConst = new NumericUpDown();
            label17 = new Label();
            labelAuxData = new Label();
            auxDataTextBox = new TextBox();
            dupeEquipmentEachLv = new Button();
            DisplayNameTextBox = new TextBox();
            label18 = new Label();
            inventryPasteButton = new Button();
            inventryCopyButton = new Button();
            parameterlinkLabel = new LinkLabel();
            itemPasteButton = new Button();
            itemCopyButton = new Button();
            SetDefaultButton = new Button();
            objectIdsLinkLabel = new LinkLabel();
            foodTab = new TabPage();
            groupBox1 = new GroupBox();
            addAllRecipeButton = new Button();
            deleteDiscoveredReciepesButton = new Button();
            listUncreatedRecipesButton = new Button();
            rarityComboBox = new ComboBox();
            cookedCategoryComboBox = new ComboBox();
            label12 = new Label();
            label11 = new Label();
            toMinusOneButton = new Button();
            toMaxButton = new Button();
            label10 = new Label();
            createdNumericNo = new NumericUpDown();
            secondaryIngredientComboBox = new ComboBox();
            label5 = new Label();
            primaryIngredientComboBox = new ComboBox();
            label4 = new Label();
            petTab = new TabPage();
            cattleTab = new TabPage();
            mealNumericUpDown = new NumericUpDown();
            label2 = new Label();
            cattleColorVariationComboBox = new ComboBox();
            label16 = new Label();
            cattleComboBox = new ComboBox();
            label8 = new Label();
            breedingCheckBox = new CheckBox();
            stomachNumericUpDown = new NumericUpDown();
            cattleNameTextBox = new TextBox();
            label15 = new Label();
            label7 = new Label();
            otherTab = new TabPage();
            inventoryDupeButton = new Button();
            groupBox3 = new GroupBox();
            label19 = new Label();
            itemDescEnTextBox = new TextBox();
            IdOrKeyTextBox = new TextBox();
            label20 = new Label();
            itemDescJpTextBox = new TextBox();
            itemNameJpTextBox = new TextBox();
            label21 = new Label();
            itemNameEnTextBox = new TextBox();
            groupBox2 = new GroupBox();
            ListupUnobtainedEquipButton = new Button();
            exportTrancelateButton = new Button();
            changeAddResourceButton = new Button();
            label1 = new Label();
            saveSlotNoComboBox = new ComboBox();
            itemSlotLabel = new Label();
            createButton = new Button();
            resultLabel = new Label();
            openConditionsButton = new Button();
            openSkillbutton = new Button();
            slotReloadbutton = new Button();
            label13 = new Label();
            dataFormatLabel = new Label();
            itemListBox = new ListBox();
            label14 = new Label();
            clearedFlagLabel = new Label();
            toolTipConstAmount = new ToolTip(components);
            toolTipKeyName = new ToolTip(components);
            toolTipAmount = new ToolTip(components);
            toolTipAuxData = new ToolTip(components);
            toolTipVariation = new ToolTip(components);
            toolTipDataFormatVersion = new ToolTip(components);
            toolTipLockedObject = new ToolTip(components);
            menuStrip = new MenuStrip();
            SettingToolStripMenuItem = new ToolStripMenuItem();
            FilePathToolStripMenuItem = new ToolStripMenuItem();
            AboutToolStripMenuItem = new ToolStripMenuItem();
            toolTipCattleStomach = new ToolTip(components);
            toolTipCattleMeal = new ToolTip(components);
            lastActivatedSessionWorld = new Label();
            worldEditButton = new Button();
            mapButton = new Button();
            dropButton = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            itemEditTabControl.SuspendLayout();
            advancedTab.SuspendLayout();
            itemDataGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectIdNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)variationNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)auxIndexNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)variationUpdateCountNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)amountNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)amountConst).BeginInit();
            foodTab.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).BeginInit();
            cattleTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mealNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stomachNumericUpDown).BeginInit();
            otherTab.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // itemEditTabControl
            // 
            itemEditTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            itemEditTabControl.Controls.Add(advancedTab);
            itemEditTabControl.Controls.Add(foodTab);
            itemEditTabControl.Controls.Add(petTab);
            itemEditTabControl.Controls.Add(cattleTab);
            itemEditTabControl.Controls.Add(otherTab);
            itemEditTabControl.Location = new Point(304, 146);
            itemEditTabControl.MinimumSize = new Size(650, 300);
            itemEditTabControl.Name = "itemEditTabControl";
            itemEditTabControl.SelectedIndex = 0;
            itemEditTabControl.Size = new Size(658, 357);
            itemEditTabControl.TabIndex = 0;
            // 
            // advancedTab
            // 
            advancedTab.Controls.Add(itemDataGroupBox);
            advancedTab.Controls.Add(dupeEquipmentEachLv);
            advancedTab.Controls.Add(DisplayNameTextBox);
            advancedTab.Controls.Add(label18);
            advancedTab.Controls.Add(inventryPasteButton);
            advancedTab.Controls.Add(inventryCopyButton);
            advancedTab.Controls.Add(parameterlinkLabel);
            advancedTab.Controls.Add(itemPasteButton);
            advancedTab.Controls.Add(itemCopyButton);
            advancedTab.Controls.Add(SetDefaultButton);
            advancedTab.Controls.Add(objectIdsLinkLabel);
            advancedTab.Location = new Point(4, 24);
            advancedTab.Name = "advancedTab";
            advancedTab.Padding = new Padding(3);
            advancedTab.Size = new Size(650, 329);
            advancedTab.TabIndex = 1;
            advancedTab.Text = "上級者向け";
            advancedTab.UseVisualStyleBackColor = true;
            // 
            // itemDataGroupBox
            // 
            itemDataGroupBox.Controls.Add(objectIdNumericUpDown);
            itemDataGroupBox.Controls.Add(variationNumericUpDown);
            itemDataGroupBox.Controls.Add(labelVariation);
            itemDataGroupBox.Controls.Add(label9);
            itemDataGroupBox.Controls.Add(labelAmount);
            itemDataGroupBox.Controls.Add(auxIndexNumericUpDown);
            itemDataGroupBox.Controls.Add(label6);
            itemDataGroupBox.Controls.Add(keyNameTextBox);
            itemDataGroupBox.Controls.Add(variationUpdateCountNumericUpDown);
            itemDataGroupBox.Controls.Add(label3);
            itemDataGroupBox.Controls.Add(amountNumericUpDown);
            itemDataGroupBox.Controls.Add(amountConstCheckBox);
            itemDataGroupBox.Controls.Add(objectLockedCheckBox);
            itemDataGroupBox.Controls.Add(amountConst);
            itemDataGroupBox.Controls.Add(label17);
            itemDataGroupBox.Controls.Add(labelAuxData);
            itemDataGroupBox.Controls.Add(auxDataTextBox);
            itemDataGroupBox.Location = new Point(8, 3);
            itemDataGroupBox.Name = "itemDataGroupBox";
            itemDataGroupBox.Size = new Size(632, 198);
            itemDataGroupBox.TabIndex = 38;
            itemDataGroupBox.TabStop = false;
            itemDataGroupBox.Text = "データ部";
            // 
            // objectIdNumericUpDown
            // 
            objectIdNumericUpDown.Location = new Point(14, 44);
            objectIdNumericUpDown.Maximum = new decimal(new int[] { 65536, 0, 0, 0 });
            objectIdNumericUpDown.Name = "objectIdNumericUpDown";
            objectIdNumericUpDown.Size = new Size(99, 23);
            objectIdNumericUpDown.TabIndex = 37;
            objectIdNumericUpDown.ValueChanged += objectIdNumericUpDown_ValueChanged;
            // 
            // variationNumericUpDown
            // 
            variationNumericUpDown.Location = new Point(127, 44);
            variationNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            variationNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            variationNumericUpDown.Name = "variationNumericUpDown";
            variationNumericUpDown.Size = new Size(136, 23);
            variationNumericUpDown.TabIndex = 34;
            // 
            // labelVariation
            // 
            labelVariation.AutoSize = true;
            labelVariation.Location = new Point(125, 25);
            labelVariation.Name = "labelVariation";
            labelVariation.Size = new Size(53, 15);
            labelVariation.TabIndex = 12;
            labelVariation.Text = "variation";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(493, 25);
            label9.Name = "label9";
            label9.Size = new Size(123, 15);
            label9.TabIndex = 14;
            label9.Text = "variationUpdateCount";
            // 
            // labelAmount
            // 
            labelAmount.AutoSize = true;
            labelAmount.Location = new Point(15, 85);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(48, 15);
            labelAmount.TabIndex = 10;
            labelAmount.Text = "amount";
            // 
            // auxIndexNumericUpDown
            // 
            auxIndexNumericUpDown.Location = new Point(493, 105);
            auxIndexNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            auxIndexNumericUpDown.Name = "auxIndexNumericUpDown";
            auxIndexNumericUpDown.ReadOnly = true;
            auxIndexNumericUpDown.Size = new Size(123, 23);
            auxIndexNumericUpDown.TabIndex = 35;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 25);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 8;
            label6.Text = "objectID";
            // 
            // keyNameTextBox
            // 
            keyNameTextBox.Location = new Point(278, 43);
            keyNameTextBox.Name = "keyNameTextBox";
            keyNameTextBox.ReadOnly = true;
            keyNameTextBox.Size = new Size(191, 23);
            keyNameTextBox.TabIndex = 17;
            // 
            // variationUpdateCountNumericUpDown
            // 
            variationUpdateCountNumericUpDown.Location = new Point(493, 44);
            variationUpdateCountNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            variationUpdateCountNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            variationUpdateCountNumericUpDown.Name = "variationUpdateCountNumericUpDown";
            variationUpdateCountNumericUpDown.ReadOnly = true;
            variationUpdateCountNumericUpDown.Size = new Size(123, 23);
            variationUpdateCountNumericUpDown.TabIndex = 33;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(278, 25);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 16;
            label3.Text = "Key Name";
            // 
            // amountNumericUpDown
            // 
            amountNumericUpDown.Location = new Point(14, 104);
            amountNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            amountNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            amountNumericUpDown.Name = "amountNumericUpDown";
            amountNumericUpDown.Size = new Size(100, 23);
            amountNumericUpDown.TabIndex = 32;
            // 
            // amountConstCheckBox
            // 
            amountConstCheckBox.AutoSize = true;
            amountConstCheckBox.Location = new Point(126, 85);
            amountConstCheckBox.Name = "amountConstCheckBox";
            amountConstCheckBox.Size = new Size(99, 19);
            amountConstCheckBox.TabIndex = 20;
            amountConstCheckBox.Text = "const amount";
            amountConstCheckBox.UseVisualStyleBackColor = true;
            // 
            // objectLockedCheckBox
            // 
            objectLockedCheckBox.AutoSize = true;
            objectLockedCheckBox.Location = new Point(278, 106);
            objectLockedCheckBox.Name = "objectLockedCheckBox";
            objectLockedCheckBox.Size = new Size(83, 19);
            objectLockedCheckBox.TabIndex = 31;
            objectLockedCheckBox.Text = "locked flag";
            objectLockedCheckBox.UseVisualStyleBackColor = true;
            // 
            // amountConst
            // 
            amountConst.Location = new Point(125, 105);
            amountConst.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            amountConst.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            amountConst.Name = "amountConst";
            amountConst.Size = new Size(136, 23);
            amountConst.TabIndex = 21;
            amountConst.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(493, 85);
            label17.Name = "label17";
            label17.Size = new Size(55, 15);
            label17.TabIndex = 22;
            label17.Text = "auxIndex";
            // 
            // labelAuxData
            // 
            labelAuxData.AutoSize = true;
            labelAuxData.Location = new Point(14, 143);
            labelAuxData.Name = "labelAuxData";
            labelAuxData.Size = new Size(50, 15);
            labelAuxData.TabIndex = 24;
            labelAuxData.Text = "auxData";
            // 
            // auxDataTextBox
            // 
            auxDataTextBox.Location = new Point(15, 161);
            auxDataTextBox.Name = "auxDataTextBox";
            auxDataTextBox.ReadOnly = true;
            auxDataTextBox.Size = new Size(601, 23);
            auxDataTextBox.TabIndex = 25;
            // 
            // dupeEquipmentEachLv
            // 
            dupeEquipmentEachLv.Location = new Point(121, 293);
            dupeEquipmentEachLv.Name = "dupeEquipmentEachLv";
            dupeEquipmentEachLv.Size = new Size(152, 23);
            dupeEquipmentEachLv.TabIndex = 36;
            dupeEquipmentEachLv.Text = "装備のLv別制作（４）";
            dupeEquipmentEachLv.UseVisualStyleBackColor = true;
            dupeEquipmentEachLv.Visible = false;
            dupeEquipmentEachLv.Click += dupeEquipmentEachLv_Click;
            // 
            // DisplayNameTextBox
            // 
            DisplayNameTextBox.Location = new Point(10, 228);
            DisplayNameTextBox.Name = "DisplayNameTextBox";
            DisplayNameTextBox.Size = new Size(333, 23);
            DisplayNameTextBox.TabIndex = 1;
            DisplayNameTextBox.TextChanged += DisplayNameTextBox_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(10, 210);
            label18.Name = "label18";
            label18.Size = new Size(149, 15);
            label18.TabIndex = 0;
            label18.Text = "ユーザー定義のアイテム表示名";
            // 
            // inventryPasteButton
            // 
            inventryPasteButton.Location = new Point(463, 293);
            inventryPasteButton.Name = "inventryPasteButton";
            inventryPasteButton.Size = new Size(148, 23);
            inventryPasteButton.TabIndex = 30;
            inventryPasteButton.Text = "インベントリ全体のペースト";
            inventryPasteButton.UseVisualStyleBackColor = true;
            inventryPasteButton.Click += inventryPasteButton_Click;
            // 
            // inventryCopyButton
            // 
            inventryCopyButton.Location = new Point(463, 257);
            inventryCopyButton.Name = "inventryCopyButton";
            inventryCopyButton.Size = new Size(148, 23);
            inventryCopyButton.TabIndex = 29;
            inventryCopyButton.Text = "インベントリ全体のコピー";
            inventryCopyButton.UseVisualStyleBackColor = true;
            inventryCopyButton.Click += inventryCopyButton_Click;
            // 
            // parameterlinkLabel
            // 
            parameterlinkLabel.AutoSize = true;
            parameterlinkLabel.Location = new Point(9, 265);
            parameterlinkLabel.Name = "parameterlinkLabel";
            parameterlinkLabel.Size = new Size(87, 15);
            parameterlinkLabel.TabIndex = 28;
            parameterlinkLabel.TabStop = true;
            parameterlinkLabel.Text = "パラメータについて";
            parameterlinkLabel.LinkClicked += linkLabel1_LinkClicked;
            // 
            // itemPasteButton
            // 
            itemPasteButton.Location = new Point(305, 293);
            itemPasteButton.Name = "itemPasteButton";
            itemPasteButton.Size = new Size(130, 23);
            itemPasteButton.TabIndex = 27;
            itemPasteButton.Text = "アイテムのペースト(&V)";
            itemPasteButton.UseVisualStyleBackColor = true;
            itemPasteButton.Click += PasteButton_Click;
            // 
            // itemCopyButton
            // 
            itemCopyButton.Location = new Point(305, 257);
            itemCopyButton.Name = "itemCopyButton";
            itemCopyButton.Size = new Size(130, 23);
            itemCopyButton.TabIndex = 26;
            itemCopyButton.Text = "アイテムのコピー(&C)";
            itemCopyButton.UseVisualStyleBackColor = true;
            itemCopyButton.Click += CopyButton_Click;
            // 
            // SetDefaultButton
            // 
            SetDefaultButton.Location = new Point(122, 257);
            SetDefaultButton.Name = "SetDefaultButton";
            SetDefaultButton.Size = new Size(152, 23);
            SetDefaultButton.TabIndex = 18;
            SetDefaultButton.Text = "デフォルト値(空アイテム)セット";
            SetDefaultButton.UseVisualStyleBackColor = true;
            SetDefaultButton.Click += SetDefaultButton_Click;
            // 
            // objectIdsLinkLabel
            // 
            objectIdsLinkLabel.AutoSize = true;
            objectIdsLinkLabel.Location = new Point(9, 297);
            objectIdsLinkLabel.Name = "objectIdsLinkLabel";
            objectIdsLinkLabel.Size = new Size(58, 15);
            objectIdsLinkLabel.TabIndex = 14;
            objectIdsLinkLabel.TabStop = true;
            objectIdsLinkLabel.Text = "ObjectIDs";
            objectIdsLinkLabel.LinkClicked += objectIdsLinkLabel_LinkClicked;
            // 
            // foodTab
            // 
            foodTab.Controls.Add(groupBox1);
            foodTab.Controls.Add(rarityComboBox);
            foodTab.Controls.Add(cookedCategoryComboBox);
            foodTab.Controls.Add(label12);
            foodTab.Controls.Add(label11);
            foodTab.Controls.Add(toMinusOneButton);
            foodTab.Controls.Add(toMaxButton);
            foodTab.Controls.Add(label10);
            foodTab.Controls.Add(createdNumericNo);
            foodTab.Controls.Add(secondaryIngredientComboBox);
            foodTab.Controls.Add(label5);
            foodTab.Controls.Add(primaryIngredientComboBox);
            foodTab.Controls.Add(label4);
            foodTab.Location = new Point(4, 24);
            foodTab.Name = "foodTab";
            foodTab.Padding = new Padding(3);
            foodTab.Size = new Size(650, 329);
            foodTab.TabIndex = 0;
            foodTab.Text = "料理作成";
            foodTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(addAllRecipeButton);
            groupBox1.Controls.Add(deleteDiscoveredReciepesButton);
            groupBox1.Controls.Add(listUncreatedRecipesButton);
            groupBox1.Location = new Point(346, 121);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(184, 140);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            groupBox1.Text = "レシピ";
            // 
            // addAllRecipeButton
            // 
            addAllRecipeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addAllRecipeButton.Location = new Point(21, 90);
            addAllRecipeButton.Name = "addAllRecipeButton";
            addAllRecipeButton.Size = new Size(147, 23);
            addAllRecipeButton.TabIndex = 20;
            addAllRecipeButton.Text = "全てのレシピを追加";
            addAllRecipeButton.UseVisualStyleBackColor = true;
            addAllRecipeButton.Click += addAllRecipeButton_Click;
            // 
            // deleteDiscoveredReciepesButton
            // 
            deleteDiscoveredReciepesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteDiscoveredReciepesButton.Location = new Point(21, 61);
            deleteDiscoveredReciepesButton.Name = "deleteDiscoveredReciepesButton";
            deleteDiscoveredReciepesButton.Size = new Size(147, 23);
            deleteDiscoveredReciepesButton.TabIndex = 19;
            deleteDiscoveredReciepesButton.Text = "発見済みレシピの初期化";
            deleteDiscoveredReciepesButton.UseVisualStyleBackColor = true;
            deleteDiscoveredReciepesButton.Click += deleteDiscoveredReciepesButton_Click;
            // 
            // listUncreatedRecipesButton
            // 
            listUncreatedRecipesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            listUncreatedRecipesButton.Location = new Point(21, 29);
            listUncreatedRecipesButton.Name = "listUncreatedRecipesButton";
            listUncreatedRecipesButton.Size = new Size(147, 23);
            listUncreatedRecipesButton.TabIndex = 18;
            listUncreatedRecipesButton.Text = "未作成のレシピを列挙";
            listUncreatedRecipesButton.UseVisualStyleBackColor = true;
            listUncreatedRecipesButton.Click += listUncreatedRecipesButton_Click;
            // 
            // rarityComboBox
            // 
            rarityComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            rarityComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            rarityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            rarityComboBox.FormattingEnabled = true;
            rarityComboBox.Items.AddRange(new object[] { "コモン", "レア", "エピック" });
            rarityComboBox.Location = new Point(297, 81);
            rarityComboBox.Name = "rarityComboBox";
            rarityComboBox.Size = new Size(241, 24);
            rarityComboBox.TabIndex = 17;
            rarityComboBox.DrawItem += rarityComboBox_DrawItem;
            // 
            // cookedCategoryComboBox
            // 
            cookedCategoryComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            cookedCategoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cookedCategoryComboBox.FormattingEnabled = true;
            cookedCategoryComboBox.Location = new Point(297, 30);
            cookedCategoryComboBox.Name = "cookedCategoryComboBox";
            cookedCategoryComboBox.Size = new Size(241, 23);
            cookedCategoryComboBox.TabIndex = 16;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Location = new Point(297, 63);
            label12.Name = "label12";
            label12.Size = new Size(37, 15);
            label12.TabIndex = 15;
            label12.Text = "レア度";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new Point(297, 12);
            label11.Name = "label11";
            label11.Size = new Size(78, 15);
            label11.TabIndex = 14;
            label11.Text = "調理後カテゴリ";
            // 
            // toMinusOneButton
            // 
            toMinusOneButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            toMinusOneButton.Location = new Point(7, 260);
            toMinusOneButton.Name = "toMinusOneButton";
            toMinusOneButton.Size = new Size(86, 23);
            toMinusOneButton.TabIndex = 13;
            toMinusOneButton.Text = "-1個";
            toMinusOneButton.UseVisualStyleBackColor = true;
            toMinusOneButton.Visible = false;
            toMinusOneButton.Click += toMinusOneButton_Click;
            // 
            // toMaxButton
            // 
            toMaxButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            toMaxButton.Location = new Point(7, 231);
            toMaxButton.Name = "toMaxButton";
            toMaxButton.Size = new Size(86, 23);
            toMaxButton.TabIndex = 12;
            toMaxButton.Text = "9999個";
            toMaxButton.UseVisualStyleBackColor = true;
            toMaxButton.Click += toMaxButton_Click;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Location = new Point(7, 174);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 9;
            label10.Text = "作成個数";
            // 
            // createdNumericNo
            // 
            createdNumericNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createdNumericNo.Location = new Point(7, 192);
            createdNumericNo.Maximum = new decimal(new int[] { 869778, 0, 0, 0 });
            createdNumericNo.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            createdNumericNo.Name = "createdNumericNo";
            createdNumericNo.Size = new Size(149, 23);
            createdNumericNo.TabIndex = 8;
            createdNumericNo.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // secondaryIngredientComboBox
            // 
            secondaryIngredientComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            secondaryIngredientComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            secondaryIngredientComboBox.FormattingEnabled = true;
            secondaryIngredientComboBox.Location = new Point(6, 81);
            secondaryIngredientComboBox.Name = "secondaryIngredientComboBox";
            secondaryIngredientComboBox.Size = new Size(252, 24);
            secondaryIngredientComboBox.TabIndex = 9;
            secondaryIngredientComboBox.DrawItem += ingredientComboBoxB_DrawItem;
            secondaryIngredientComboBox.SelectedIndexChanged += ingredientComboBox_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 63);
            label5.Name = "label5";
            label5.Size = new Size(56, 15);
            label5.TabIndex = 10;
            label5.Text = "食材その2";
            // 
            // primaryIngredientComboBox
            // 
            primaryIngredientComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            primaryIngredientComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            primaryIngredientComboBox.FormattingEnabled = true;
            primaryIngredientComboBox.Location = new Point(6, 30);
            primaryIngredientComboBox.Name = "primaryIngredientComboBox";
            primaryIngredientComboBox.Size = new Size(252, 24);
            primaryIngredientComboBox.TabIndex = 8;
            primaryIngredientComboBox.DrawItem += ingredientComboBoxA_DrawItem;
            primaryIngredientComboBox.SelectedIndexChanged += ingredientComboBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 12);
            label4.Name = "label4";
            label4.Size = new Size(56, 15);
            label4.TabIndex = 8;
            label4.Text = "食材その1";
            // 
            // petTab
            // 
            petTab.Location = new Point(4, 24);
            petTab.Name = "petTab";
            petTab.Padding = new Padding(3);
            petTab.Size = new Size(650, 329);
            petTab.TabIndex = 2;
            petTab.Text = "ペット";
            petTab.UseVisualStyleBackColor = true;
            // 
            // cattleTab
            // 
            cattleTab.Controls.Add(mealNumericUpDown);
            cattleTab.Controls.Add(label2);
            cattleTab.Controls.Add(cattleColorVariationComboBox);
            cattleTab.Controls.Add(label16);
            cattleTab.Controls.Add(cattleComboBox);
            cattleTab.Controls.Add(label8);
            cattleTab.Controls.Add(breedingCheckBox);
            cattleTab.Controls.Add(stomachNumericUpDown);
            cattleTab.Controls.Add(cattleNameTextBox);
            cattleTab.Controls.Add(label15);
            cattleTab.Controls.Add(label7);
            cattleTab.Location = new Point(4, 24);
            cattleTab.Name = "cattleTab";
            cattleTab.Size = new Size(650, 329);
            cattleTab.TabIndex = 3;
            cattleTab.Text = "家畜";
            cattleTab.UseVisualStyleBackColor = true;
            // 
            // mealNumericUpDown
            // 
            mealNumericUpDown.Location = new Point(180, 137);
            mealNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            mealNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            mealNumericUpDown.Name = "mealNumericUpDown";
            mealNumericUpDown.Size = new Size(120, 23);
            mealNumericUpDown.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(180, 119);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 10;
            label2.Text = "食事回数";
            // 
            // cattleColorVariationComboBox
            // 
            cattleColorVariationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cattleColorVariationComboBox.FormattingEnabled = true;
            cattleColorVariationComboBox.Items.AddRange(new object[] { "Color_0", "Color_1", "Color_2", "Color_3", "Color_4" });
            cattleColorVariationComboBox.Location = new Point(252, 16);
            cattleColorVariationComboBox.Name = "cattleColorVariationComboBox";
            cattleColorVariationComboBox.Size = new Size(122, 23);
            cattleColorVariationComboBox.TabIndex = 9;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(205, 19);
            label16.Name = "label16";
            label16.Size = new Size(41, 15);
            label16.TabIndex = 8;
            label16.Text = "色違い";
            // 
            // cattleComboBox
            // 
            cattleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cattleComboBox.FormattingEnabled = true;
            cattleComboBox.Location = new Point(46, 16);
            cattleComboBox.Name = "cattleComboBox";
            cattleComboBox.Size = new Size(122, 23);
            cattleComboBox.TabIndex = 7;
            cattleComboBox.SelectedIndexChanged += cattleComboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 19);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 6;
            label8.Text = "種別";
            // 
            // breedingCheckBox
            // 
            breedingCheckBox.AutoSize = true;
            breedingCheckBox.Location = new Point(9, 187);
            breedingCheckBox.Name = "breedingCheckBox";
            breedingCheckBox.Size = new Size(119, 19);
            breedingCheckBox.TabIndex = 5;
            breedingCheckBox.Text = "繁殖（大人のみ）";
            breedingCheckBox.UseVisualStyleBackColor = true;
            // 
            // stomachNumericUpDown
            // 
            stomachNumericUpDown.Location = new Point(9, 137);
            stomachNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            stomachNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            stomachNumericUpDown.Name = "stomachNumericUpDown";
            stomachNumericUpDown.Size = new Size(120, 23);
            stomachNumericUpDown.TabIndex = 4;
            // 
            // cattleNameTextBox
            // 
            cattleNameTextBox.Location = new Point(9, 74);
            cattleNameTextBox.Name = "cattleNameTextBox";
            cattleNameTextBox.Size = new Size(333, 23);
            cattleNameTextBox.TabIndex = 3;
            cattleNameTextBox.TextChanged += cattleNameTextBox_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(9, 119);
            label15.Name = "label15";
            label15.Size = new Size(43, 15);
            label15.TabIndex = 2;
            label15.Text = "満腹度";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 56);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 0;
            label7.Text = "名前";
            // 
            // otherTab
            // 
            otherTab.Controls.Add(inventoryDupeButton);
            otherTab.Controls.Add(groupBox3);
            otherTab.Controls.Add(groupBox2);
            otherTab.Location = new Point(4, 24);
            otherTab.Name = "otherTab";
            otherTab.Padding = new Padding(3);
            otherTab.Size = new Size(650, 329);
            otherTab.TabIndex = 4;
            otherTab.Text = "その他";
            otherTab.UseVisualStyleBackColor = true;
            // 
            // inventoryDupeButton
            // 
            inventoryDupeButton.Location = new Point(16, 100);
            inventoryDupeButton.Name = "inventoryDupeButton";
            inventoryDupeButton.Size = new Size(147, 23);
            inventoryDupeButton.TabIndex = 4;
            inventoryDupeButton.Text = "倉庫用アイテム複製";
            inventoryDupeButton.UseVisualStyleBackColor = true;
            inventoryDupeButton.Visible = false;
            inventoryDupeButton.Click += InventoryDupeButton_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label19);
            groupBox3.Controls.Add(itemDescEnTextBox);
            groupBox3.Controls.Add(IdOrKeyTextBox);
            groupBox3.Controls.Add(changeAddResourceButton);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(itemDescJpTextBox);
            groupBox3.Controls.Add(itemNameJpTextBox);
            groupBox3.Controls.Add(label21);
            groupBox3.Controls.Add(itemNameEnTextBox);
            groupBox3.Location = new Point(229, 34);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(447, 268);
            groupBox3.TabIndex = 14;
            groupBox3.TabStop = false;
            groupBox3.Text = "2.0で実装する";
            groupBox3.Visible = false;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(10, 19);
            label19.Name = "label19";
            label19.Size = new Size(74, 15);
            label19.TabIndex = 4;
            label19.Text = "objectId/Key";
            // 
            // itemDescEnTextBox
            // 
            itemDescEnTextBox.Location = new Point(10, 232);
            itemDescEnTextBox.Multiline = true;
            itemDescEnTextBox.Name = "itemDescEnTextBox";
            itemDescEnTextBox.Size = new Size(416, 32);
            itemDescEnTextBox.TabIndex = 13;
            // 
            // IdOrKeyTextBox
            // 
            IdOrKeyTextBox.Location = new Point(10, 37);
            IdOrKeyTextBox.Name = "IdOrKeyTextBox";
            IdOrKeyTextBox.Size = new Size(100, 23);
            IdOrKeyTextBox.TabIndex = 5;
            IdOrKeyTextBox.TextChanged += IdOrKeyTextBox_TextChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(10, 76);
            label20.Name = "label20";
            label20.Size = new Size(55, 15);
            label20.TabIndex = 6;
            label20.Text = "アイテム名";
            // 
            // itemDescJpTextBox
            // 
            itemDescJpTextBox.Location = new Point(10, 194);
            itemDescJpTextBox.Multiline = true;
            itemDescJpTextBox.Name = "itemDescJpTextBox";
            itemDescJpTextBox.Size = new Size(416, 32);
            itemDescJpTextBox.TabIndex = 11;
            // 
            // itemNameJpTextBox
            // 
            itemNameJpTextBox.Location = new Point(10, 94);
            itemNameJpTextBox.Name = "itemNameJpTextBox";
            itemNameJpTextBox.Size = new Size(194, 23);
            itemNameJpTextBox.TabIndex = 8;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(10, 176);
            label21.Name = "label21";
            label21.Size = new Size(67, 15);
            label21.TabIndex = 10;
            label21.Text = "アイテム説明";
            // 
            // itemNameEnTextBox
            // 
            itemNameEnTextBox.Location = new Point(10, 123);
            itemNameEnTextBox.Name = "itemNameEnTextBox";
            itemNameEnTextBox.Size = new Size(194, 23);
            itemNameEnTextBox.TabIndex = 9;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ListupUnobtainedEquipButton);
            groupBox2.Controls.Add(exportTrancelateButton);
            groupBox2.Location = new Point(10, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(161, 82);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "出力";
            // 
            // ListupUnobtainedEquipButton
            // 
            ListupUnobtainedEquipButton.Location = new Point(6, 22);
            ListupUnobtainedEquipButton.Name = "ListupUnobtainedEquipButton";
            ListupUnobtainedEquipButton.Size = new Size(147, 23);
            ListupUnobtainedEquipButton.TabIndex = 2;
            ListupUnobtainedEquipButton.Text = "未発見アイテム一覧";
            ListupUnobtainedEquipButton.UseVisualStyleBackColor = true;
            ListupUnobtainedEquipButton.Click += ListupUnobtainedEquipButton_Click;
            // 
            // exportTrancelateButton
            // 
            exportTrancelateButton.Location = new Point(6, 51);
            exportTrancelateButton.Name = "exportTrancelateButton";
            exportTrancelateButton.Size = new Size(147, 23);
            exportTrancelateButton.TabIndex = 3;
            exportTrancelateButton.Text = "日本語リソース抽出";
            exportTrancelateButton.UseVisualStyleBackColor = true;
            exportTrancelateButton.Click += exportTrancelateButton_Click;
            // 
            // changeAddResourceButton
            // 
            changeAddResourceButton.Location = new Point(317, 37);
            changeAddResourceButton.Name = "changeAddResourceButton";
            changeAddResourceButton.Size = new Size(75, 23);
            changeAddResourceButton.TabIndex = 7;
            changeAddResourceButton.Text = "変更/追加";
            changeAddResourceButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 32);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 1;
            label1.Text = "セーブスロットNo";
            // 
            // saveSlotNoComboBox
            // 
            saveSlotNoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            saveSlotNoComboBox.FormattingEnabled = true;
            saveSlotNoComboBox.Location = new Point(12, 56);
            saveSlotNoComboBox.Name = "saveSlotNoComboBox";
            saveSlotNoComboBox.Size = new Size(148, 23);
            saveSlotNoComboBox.TabIndex = 2;
            saveSlotNoComboBox.SelectedIndexChanged += saveSlotNoComboBox_SelectedIndexChanged;
            // 
            // itemSlotLabel
            // 
            itemSlotLabel.AutoSize = true;
            itemSlotLabel.Location = new Point(13, 127);
            itemSlotLabel.Name = "itemSlotLabel";
            itemSlotLabel.Size = new Size(72, 15);
            itemSlotLabel.TabIndex = 6;
            itemSlotLabel.Text = "インベントリ枠";
            // 
            // createButton
            // 
            createButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createButton.Location = new Point(306, 502);
            createButton.Name = "createButton";
            createButton.Size = new Size(147, 45);
            createButton.TabIndex = 13;
            createButton.Text = "作成";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // resultLabel
            // 
            resultLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(465, 517);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(64, 15);
            resultLabel.TabIndex = 14;
            resultLabel.Text = "resultLabel";
            resultLabel.Visible = false;
            // 
            // openConditionsButton
            // 
            openConditionsButton.BackColor = Color.Cyan;
            openConditionsButton.Location = new Point(12, 85);
            openConditionsButton.Name = "openConditionsButton";
            openConditionsButton.Size = new Size(138, 23);
            openConditionsButton.TabIndex = 17;
            openConditionsButton.Text = "コンディション値編集";
            openConditionsButton.UseVisualStyleBackColor = false;
            openConditionsButton.Click += openConditionsButton_Click;
            // 
            // openSkillbutton
            // 
            openSkillbutton.BackColor = Color.Transparent;
            openSkillbutton.Location = new Point(166, 85);
            openSkillbutton.Name = "openSkillbutton";
            openSkillbutton.Size = new Size(138, 23);
            openSkillbutton.TabIndex = 18;
            openSkillbutton.Text = "スキルポイント編集";
            openSkillbutton.UseVisualStyleBackColor = false;
            openSkillbutton.Click += openSkillButton_Click;
            // 
            // slotReloadbutton
            // 
            slotReloadbutton.Location = new Point(167, 55);
            slotReloadbutton.Name = "slotReloadbutton";
            slotReloadbutton.Size = new Size(90, 23);
            slotReloadbutton.TabIndex = 19;
            slotReloadbutton.Text = "再読み込み";
            slotReloadbutton.UseVisualStyleBackColor = true;
            slotReloadbutton.Click += slotReloadbutton_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(280, 59);
            label13.Name = "label13";
            label13.Size = new Size(107, 15);
            label13.TabIndex = 20;
            label13.Text = "データ書式バージョン :";
            // 
            // dataFormatLabel
            // 
            dataFormatLabel.AutoSize = true;
            dataFormatLabel.ForeColor = Color.Purple;
            dataFormatLabel.Location = new Point(382, 59);
            dataFormatLabel.Name = "dataFormatLabel";
            dataFormatLabel.Size = new Size(45, 15);
            dataFormatLabel.TabIndex = 21;
            dataFormatLabel.Text = "version";
            // 
            // itemListBox
            // 
            itemListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            itemListBox.DrawMode = DrawMode.OwnerDrawFixed;
            itemListBox.FormattingEnabled = true;
            itemListBox.ItemHeight = 15;
            itemListBox.Location = new Point(12, 146);
            itemListBox.Name = "itemListBox";
            itemListBox.Size = new Size(286, 394);
            itemListBox.TabIndex = 22;
            itemListBox.DrawItem += itemListBox_DrawItem;
            itemListBox.SelectedIndexChanged += itemListBox_TextChanged;
            itemListBox.KeyDown += itemListBox_KeyDown;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(428, 59);
            label14.Name = "label14";
            label14.Size = new Size(63, 15);
            label14.TabIndex = 23;
            label14.Text = "クリア状況 :";
            // 
            // clearedFlagLabel
            // 
            clearedFlagLabel.AutoSize = true;
            clearedFlagLabel.ForeColor = Color.Purple;
            clearedFlagLabel.Location = new Point(488, 59);
            clearedFlagLabel.Name = "clearedFlagLabel";
            clearedFlagLabel.Size = new Size(56, 15);
            clearedFlagLabel.TabIndex = 24;
            clearedFlagLabel.Text = "clear_flag";
            // 
            // toolTipConstAmount
            // 
            toolTipConstAmount.AutoPopDelay = 5000;
            toolTipConstAmount.InitialDelay = 100;
            toolTipConstAmount.ReshowDelay = 100;
            // 
            // toolTipKeyName
            // 
            toolTipKeyName.AutoPopDelay = 5000;
            toolTipKeyName.InitialDelay = 100;
            toolTipKeyName.ReshowDelay = 100;
            // 
            // toolTipAmount
            // 
            toolTipAmount.AutoPopDelay = 5000;
            toolTipAmount.InitialDelay = 100;
            toolTipAmount.ReshowDelay = 100;
            // 
            // toolTipAuxData
            // 
            toolTipAuxData.AutoPopDelay = 5000;
            toolTipAuxData.InitialDelay = 100;
            toolTipAuxData.ReshowDelay = 100;
            // 
            // toolTipVariation
            // 
            toolTipVariation.AutoPopDelay = 5000;
            toolTipVariation.InitialDelay = 100;
            toolTipVariation.ReshowDelay = 100;
            // 
            // toolTipDataFormatVersion
            // 
            toolTipDataFormatVersion.AutoPopDelay = 5000;
            toolTipDataFormatVersion.InitialDelay = 100;
            toolTipDataFormatVersion.ReshowDelay = 100;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { SettingToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(972, 24);
            menuStrip.TabIndex = 25;
            menuStrip.Text = "menuStrip1";
            // 
            // SettingToolStripMenuItem
            // 
            SettingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { FilePathToolStripMenuItem, AboutToolStripMenuItem });
            SettingToolStripMenuItem.Name = "SettingToolStripMenuItem";
            SettingToolStripMenuItem.Size = new Size(57, 20);
            SettingToolStripMenuItem.Text = "設定(&S)";
            // 
            // FilePathToolStripMenuItem
            // 
            FilePathToolStripMenuItem.Name = "FilePathToolStripMenuItem";
            FilePathToolStripMenuItem.Size = new Size(174, 22);
            FilePathToolStripMenuItem.Text = "ファイルパス設定...(&F)";
            FilePathToolStripMenuItem.Click += FilePathToolStripMenuItem_Click;
            // 
            // AboutToolStripMenuItem
            // 
            AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            AboutToolStripMenuItem.Size = new Size(174, 22);
            AboutToolStripMenuItem.Text = "バージョン情報(&A)";
            AboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // lastActivatedSessionWorld
            // 
            lastActivatedSessionWorld.AutoSize = true;
            lastActivatedSessionWorld.ForeColor = Color.Purple;
            lastActivatedSessionWorld.Location = new Point(592, 59);
            lastActivatedSessionWorld.Name = "lastActivatedSessionWorld";
            lastActivatedSessionWorld.Size = new Size(149, 15);
            lastActivatedSessionWorld.TabIndex = 26;
            lastActivatedSessionWorld.Text = "AbortedSessionWorldLabel";
            lastActivatedSessionWorld.Visible = false;
            lastActivatedSessionWorld.Click += lastActivatedSessionWorldLabel_Click;
            // 
            // worldEditButton
            // 
            worldEditButton.Location = new Point(318, 85);
            worldEditButton.Name = "worldEditButton";
            worldEditButton.Size = new Size(137, 23);
            worldEditButton.TabIndex = 27;
            worldEditButton.Text = "ワールド設定変更";
            worldEditButton.UseVisualStyleBackColor = true;
            worldEditButton.Click += worldEditButton_Click;
            // 
            // mapButton
            // 
            mapButton.Location = new Point(465, 85);
            mapButton.Name = "mapButton";
            mapButton.Size = new Size(137, 23);
            mapButton.TabIndex = 28;
            mapButton.Text = "マップ閲覧";
            mapButton.UseVisualStyleBackColor = true;
            mapButton.Click += mapButton_Click;
            // 
            // dropButton
            // 
            dropButton.Location = new Point(613, 85);
            dropButton.Name = "dropButton";
            dropButton.Size = new Size(137, 23);
            dropButton.TabIndex = 29;
            dropButton.Text = "ドロップ率計算機";
            dropButton.UseVisualStyleBackColor = true;
            dropButton.Click += dropButton_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(972, 561);
            Controls.Add(dropButton);
            Controls.Add(mapButton);
            Controls.Add(worldEditButton);
            Controls.Add(lastActivatedSessionWorld);
            Controls.Add(clearedFlagLabel);
            Controls.Add(label14);
            Controls.Add(itemListBox);
            Controls.Add(dataFormatLabel);
            Controls.Add(label13);
            Controls.Add(slotReloadbutton);
            Controls.Add(openSkillbutton);
            Controls.Add(openConditionsButton);
            Controls.Add(resultLabel);
            Controls.Add(createButton);
            Controls.Add(itemSlotLabel);
            Controls.Add(saveSlotNoComboBox);
            Controls.Add(label1);
            Controls.Add(itemEditTabControl);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            MinimumSize = new Size(690, 100);
            Name = "Form1";
            Text = "CKCharaDataEditor";
            FormClosing += Form1_FormClosing;
            itemEditTabControl.ResumeLayout(false);
            advancedTab.ResumeLayout(false);
            advancedTab.PerformLayout();
            itemDataGroupBox.ResumeLayout(false);
            itemDataGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectIdNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)variationNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)auxIndexNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)variationUpdateCountNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)amountNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)amountConst).EndInit();
            foodTab.ResumeLayout(false);
            foodTab.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).EndInit();
            cattleTab.ResumeLayout(false);
            cattleTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mealNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stomachNumericUpDown).EndInit();
            otherTab.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl itemEditTabControl;
        private TabPage foodTab;
        private TabPage advancedTab;
        private Label label1;
        private ComboBox saveSlotNoComboBox;
        private Label itemSlotLabel;
        private ComboBox secondaryIngredientComboBox;
        private Label label5;
        private ComboBox primaryIngredientComboBox;
        private Label label4;
        private Label label6;
        private Label labelVariation;
        private Label labelAmount;
        private Button toMaxButton;
        private Label label10;
        private NumericUpDown createdNumericNo;
        private Label label9;
        private Button createButton;
        private LinkLabel objectIdsLinkLabel;
        private Button toMinusOneButton;
        private ComboBox rarityComboBox;
        private ComboBox cookedCategoryComboBox;
        private Label label12;
        private Label label11;
        private Label label3;
        private TextBox keyNameTextBox;
        private Button SetDefaultButton;
        private Label resultLabel;
        private NumericUpDown amountConst;
        private CheckBox amountConstCheckBox;
        private TabPage petTab;
        private Button listUncreatedRecipesButton;
        private TextBox auxDataTextBox;
        private Label labelAuxData;
        private Label label17;
        private Button openConditionsButton;
        private Button itemPasteButton;
        private Button itemCopyButton;
        private LinkLabel parameterlinkLabel;
        private Button inventryPasteButton;
        private Button inventryCopyButton;
        private Button openSkillbutton;
        private Button deleteDiscoveredReciepesButton;
        private Control.PetEditControl petEditControl;
        private Button slotReloadbutton;
        private Label label13;
        private Label dataFormatLabel;
        private ListBox itemListBox;
        private TabPage cattleTab;
        private Label label14;
        private Label clearedFlagLabel;
        private CheckBox objectLockedCheckBox;
        private ToolTip toolTipConstAmount;
        private ToolTip toolTipKeyName;
        private ToolTip toolTipAmount;
        private ToolTip toolTipAuxData;
        private ToolTip toolTipVariation;
        private ToolTip toolTipDataFormatVersion;
        private ToolTip toolTipLockedObject;
        private MenuStrip menuStrip;
        private ToolStripMenuItem SettingToolStripMenuItem;
        private ToolStripMenuItem FilePathToolStripMenuItem;
        private ToolStripMenuItem AboutToolStripMenuItem;
        private TextBox cattleNameTextBox;
        private Label label15;
        private Label label7;
        private NumericUpDown stomachNumericUpDown;
        private CheckBox breedingCheckBox;
        private ComboBox cattleColorVariationComboBox;
        private Label label16;
        private ComboBox cattleComboBox;
        private Label label8;
        private NumericUpDown mealNumericUpDown;
        private Label label2;
        private ToolTip toolTipCattleStomach;
        private ToolTip toolTipCattleMeal;
        private NumericUpDown amountNumericUpDown;
        private NumericUpDown variationUpdateCountNumericUpDown;
        private NumericUpDown variationNumericUpDown;
        private NumericUpDown auxIndexNumericUpDown;
        private Button dupeEquipmentEachLv;
        private TabPage otherTab;
        private TextBox DisplayNameTextBox;
        private Label label18;
        private Label lastActivatedSessionWorld;
        private Button worldEditButton;
        private Button ListupUnobtainedEquipButton;
        private Button mapButton;
        private Button dropButton;
        private Button addAllRecipeButton;
        private GroupBox groupBox1;
        private NumericUpDown objectIdNumericUpDown;
        private Button exportTrancelateButton;
        private GroupBox itemDataGroupBox;
        private FolderBrowserDialog folderBrowserDialog;
        private Label label20;
        private TextBox IdOrKeyTextBox;
        private Label label19;
        private Button changeAddResourceButton;
        private TextBox itemNameJpTextBox;
        private Label label21;
        private TextBox itemNameEnTextBox;
        private GroupBox groupBox2;
        private TextBox itemDescJpTextBox;
        private TextBox itemDescEnTextBox;
        private Button inventoryDupeButton;
        private GroupBox groupBox3;
    }
}
