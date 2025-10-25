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
            dupeEquipmentEachLv = new Button();
            auxIndexNumericUpDown = new NumericUpDown();
            variationNumericUpDown = new NumericUpDown();
            variationUpdateCountNumericUpDown = new NumericUpDown();
            amountNumericUpDown = new NumericUpDown();
            objectLockedCheckBox = new CheckBox();
            inventryPasteButton = new Button();
            inventryCopyButton = new Button();
            parameterlinkLabel = new LinkLabel();
            itemPasteButton = new Button();
            itemCopyButton = new Button();
            auxDataTextBox = new TextBox();
            labelAuxData = new Label();
            label17 = new Label();
            amountConst = new NumericUpDown();
            amountConstCheckBox = new CheckBox();
            SetDefaultButton = new Button();
            label3 = new Label();
            objectNameTextBox = new TextBox();
            objectIdsLinkLabel = new LinkLabel();
            label6 = new Label();
            objectIdTextBox = new TextBox();
            labelAmount = new Label();
            label9 = new Label();
            labelVariation = new Label();
            foodTab = new TabPage();
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
            ingredientComboBoxB = new ComboBox();
            label5 = new Label();
            ingredientComboBoxA = new ComboBox();
            label4 = new Label();
            petTab = new TabPage();
            petEditControl = new CKCharaDataEditor.Control.PetEditControl();
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
            ListupUnobtainedEquipButton = new Button();
            DisplayNameTextBox = new TextBox();
            label18 = new Label();
            label1 = new Label();
            saveSlotNoComboBox = new ComboBox();
            saveFolderBrowserDialog = new FolderBrowserDialog();
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
            toolTipObjectName = new ToolTip(components);
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
            lastConnectedWorldLabel = new Label();
            worldEditButton = new Button();
            mapButton = new Button();
            dropButton = new Button();
            itemEditTabControl.SuspendLayout();
            advancedTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)auxIndexNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)variationNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)variationUpdateCountNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)amountNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)amountConst).BeginInit();
            foodTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).BeginInit();
            petTab.SuspendLayout();
            cattleTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mealNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stomachNumericUpDown).BeginInit();
            otherTab.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // itemEditTabControl
            // 
            itemEditTabControl.Controls.Add(advancedTab);
            itemEditTabControl.Controls.Add(foodTab);
            itemEditTabControl.Controls.Add(petTab);
            itemEditTabControl.Controls.Add(cattleTab);
            itemEditTabControl.Controls.Add(otherTab);
            itemEditTabControl.Location = new Point(304, 146);
            itemEditTabControl.MinimumSize = new Size(650, 300);
            itemEditTabControl.Name = "itemEditTabControl";
            itemEditTabControl.SelectedIndex = 0;
            itemEditTabControl.Size = new Size(650, 304);
            itemEditTabControl.TabIndex = 0;
            // 
            // advancedTab
            // 
            advancedTab.Controls.Add(dupeEquipmentEachLv);
            advancedTab.Controls.Add(auxIndexNumericUpDown);
            advancedTab.Controls.Add(variationNumericUpDown);
            advancedTab.Controls.Add(variationUpdateCountNumericUpDown);
            advancedTab.Controls.Add(amountNumericUpDown);
            advancedTab.Controls.Add(objectLockedCheckBox);
            advancedTab.Controls.Add(inventryPasteButton);
            advancedTab.Controls.Add(inventryCopyButton);
            advancedTab.Controls.Add(parameterlinkLabel);
            advancedTab.Controls.Add(itemPasteButton);
            advancedTab.Controls.Add(itemCopyButton);
            advancedTab.Controls.Add(auxDataTextBox);
            advancedTab.Controls.Add(labelAuxData);
            advancedTab.Controls.Add(label17);
            advancedTab.Controls.Add(amountConst);
            advancedTab.Controls.Add(amountConstCheckBox);
            advancedTab.Controls.Add(SetDefaultButton);
            advancedTab.Controls.Add(label3);
            advancedTab.Controls.Add(objectNameTextBox);
            advancedTab.Controls.Add(objectIdsLinkLabel);
            advancedTab.Controls.Add(label6);
            advancedTab.Controls.Add(objectIdTextBox);
            advancedTab.Controls.Add(labelAmount);
            advancedTab.Controls.Add(label9);
            advancedTab.Controls.Add(labelVariation);
            advancedTab.Location = new Point(4, 24);
            advancedTab.Name = "advancedTab";
            advancedTab.Padding = new Padding(3);
            advancedTab.Size = new Size(642, 276);
            advancedTab.TabIndex = 1;
            advancedTab.Text = "上級者向け";
            advancedTab.UseVisualStyleBackColor = true;
            // 
            // dupeEquipmentEachLv
            // 
            dupeEquipmentEachLv.Location = new Point(121, 216);
            dupeEquipmentEachLv.Name = "dupeEquipmentEachLv";
            dupeEquipmentEachLv.Size = new Size(152, 23);
            dupeEquipmentEachLv.TabIndex = 36;
            dupeEquipmentEachLv.Text = "装備のLv別制作（４）";
            dupeEquipmentEachLv.UseVisualStyleBackColor = true;
            dupeEquipmentEachLv.Visible = false;
            dupeEquipmentEachLv.Click += dupeEquipmentEachLv_Click;
            // 
            // auxIndexNumericUpDown
            // 
            auxIndexNumericUpDown.Location = new Point(488, 89);
            auxIndexNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            auxIndexNumericUpDown.Name = "auxIndexNumericUpDown";
            auxIndexNumericUpDown.ReadOnly = true;
            auxIndexNumericUpDown.Size = new Size(123, 23);
            auxIndexNumericUpDown.TabIndex = 35;
            // 
            // variationNumericUpDown
            // 
            variationNumericUpDown.Location = new Point(122, 28);
            variationNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            variationNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            variationNumericUpDown.Name = "variationNumericUpDown";
            variationNumericUpDown.Size = new Size(136, 23);
            variationNumericUpDown.TabIndex = 34;
            // 
            // variationUpdateCountNumericUpDown
            // 
            variationUpdateCountNumericUpDown.Location = new Point(488, 28);
            variationUpdateCountNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            variationUpdateCountNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            variationUpdateCountNumericUpDown.Name = "variationUpdateCountNumericUpDown";
            variationUpdateCountNumericUpDown.ReadOnly = true;
            variationUpdateCountNumericUpDown.Size = new Size(123, 23);
            variationUpdateCountNumericUpDown.TabIndex = 33;
            // 
            // amountNumericUpDown
            // 
            amountNumericUpDown.Location = new Point(9, 88);
            amountNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            amountNumericUpDown.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            amountNumericUpDown.Name = "amountNumericUpDown";
            amountNumericUpDown.Size = new Size(100, 23);
            amountNumericUpDown.TabIndex = 32;
            // 
            // objectLockedCheckBox
            // 
            objectLockedCheckBox.AutoSize = true;
            objectLockedCheckBox.Location = new Point(273, 90);
            objectLockedCheckBox.Name = "objectLockedCheckBox";
            objectLockedCheckBox.Size = new Size(83, 19);
            objectLockedCheckBox.TabIndex = 31;
            objectLockedCheckBox.Text = "locked flag";
            objectLockedCheckBox.UseVisualStyleBackColor = true;
            // 
            // inventryPasteButton
            // 
            inventryPasteButton.Location = new Point(463, 216);
            inventryPasteButton.Name = "inventryPasteButton";
            inventryPasteButton.Size = new Size(148, 23);
            inventryPasteButton.TabIndex = 30;
            inventryPasteButton.Text = "インベントリ全体のペースト";
            inventryPasteButton.UseVisualStyleBackColor = true;
            inventryPasteButton.Click += inventryPasteButton_Click;
            // 
            // inventryCopyButton
            // 
            inventryCopyButton.Location = new Point(463, 180);
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
            parameterlinkLabel.Location = new Point(9, 188);
            parameterlinkLabel.Name = "parameterlinkLabel";
            parameterlinkLabel.Size = new Size(87, 15);
            parameterlinkLabel.TabIndex = 28;
            parameterlinkLabel.TabStop = true;
            parameterlinkLabel.Text = "パラメータについて";
            parameterlinkLabel.LinkClicked += linkLabel1_LinkClicked;
            // 
            // itemPasteButton
            // 
            itemPasteButton.Location = new Point(305, 216);
            itemPasteButton.Name = "itemPasteButton";
            itemPasteButton.Size = new Size(130, 23);
            itemPasteButton.TabIndex = 27;
            itemPasteButton.Text = "アイテムのペースト(&V)";
            itemPasteButton.UseVisualStyleBackColor = true;
            itemPasteButton.Click += PasteButton_Click;
            // 
            // itemCopyButton
            // 
            itemCopyButton.Location = new Point(305, 180);
            itemCopyButton.Name = "itemCopyButton";
            itemCopyButton.Size = new Size(130, 23);
            itemCopyButton.TabIndex = 26;
            itemCopyButton.Text = "アイテムのコピー(&C)";
            itemCopyButton.UseVisualStyleBackColor = true;
            itemCopyButton.Click += CopyButton_Click;
            // 
            // auxDataTextBox
            // 
            auxDataTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            auxDataTextBox.Location = new Point(10, 145);
            auxDataTextBox.Name = "auxDataTextBox";
            auxDataTextBox.ReadOnly = true;
            auxDataTextBox.Size = new Size(601, 23);
            auxDataTextBox.TabIndex = 25;
            // 
            // labelAuxData
            // 
            labelAuxData.AutoSize = true;
            labelAuxData.Location = new Point(9, 127);
            labelAuxData.Name = "labelAuxData";
            labelAuxData.Size = new Size(50, 15);
            labelAuxData.TabIndex = 24;
            labelAuxData.Text = "auxData";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(488, 69);
            label17.Name = "label17";
            label17.Size = new Size(55, 15);
            label17.TabIndex = 22;
            label17.Text = "auxIndex";
            // 
            // amountConst
            // 
            amountConst.Location = new Point(120, 89);
            amountConst.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            amountConst.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            amountConst.Name = "amountConst";
            amountConst.Size = new Size(136, 23);
            amountConst.TabIndex = 21;
            amountConst.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // amountConstCheckBox
            // 
            amountConstCheckBox.AutoSize = true;
            amountConstCheckBox.Location = new Point(121, 69);
            amountConstCheckBox.Name = "amountConstCheckBox";
            amountConstCheckBox.Size = new Size(99, 19);
            amountConstCheckBox.TabIndex = 20;
            amountConstCheckBox.Text = "const amount";
            amountConstCheckBox.UseVisualStyleBackColor = true;
            // 
            // SetDefaultButton
            // 
            SetDefaultButton.Location = new Point(122, 180);
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
            label3.Location = new Point(273, 9);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 16;
            label3.Text = "objectName";
            // 
            // objectNameTextBox
            // 
            objectNameTextBox.Location = new Point(273, 27);
            objectNameTextBox.Name = "objectNameTextBox";
            objectNameTextBox.Size = new Size(191, 23);
            objectNameTextBox.TabIndex = 17;
            // 
            // objectIdsLinkLabel
            // 
            objectIdsLinkLabel.AutoSize = true;
            objectIdsLinkLabel.Location = new Point(9, 220);
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
            // objectIdTextBox
            // 
            objectIdTextBox.Location = new Point(9, 27);
            objectIdTextBox.Name = "objectIdTextBox";
            objectIdTextBox.Size = new Size(100, 23);
            objectIdTextBox.TabIndex = 9;
            // 
            // labelAmount
            // 
            labelAmount.AutoSize = true;
            labelAmount.Location = new Point(10, 69);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(48, 15);
            labelAmount.TabIndex = 10;
            labelAmount.Text = "amount";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(488, 9);
            label9.Name = "label9";
            label9.Size = new Size(123, 15);
            label9.TabIndex = 14;
            label9.Text = "variationUpdateCount";
            // 
            // labelVariation
            // 
            labelVariation.AutoSize = true;
            labelVariation.Location = new Point(120, 9);
            labelVariation.Name = "labelVariation";
            labelVariation.Size = new Size(53, 15);
            labelVariation.TabIndex = 12;
            labelVariation.Text = "variation";
            // 
            // foodTab
            // 
            foodTab.Controls.Add(deleteDiscoveredReciepesButton);
            foodTab.Controls.Add(listUncreatedRecipesButton);
            foodTab.Controls.Add(rarityComboBox);
            foodTab.Controls.Add(cookedCategoryComboBox);
            foodTab.Controls.Add(label12);
            foodTab.Controls.Add(label11);
            foodTab.Controls.Add(toMinusOneButton);
            foodTab.Controls.Add(toMaxButton);
            foodTab.Controls.Add(label10);
            foodTab.Controls.Add(createdNumericNo);
            foodTab.Controls.Add(ingredientComboBoxB);
            foodTab.Controls.Add(label5);
            foodTab.Controls.Add(ingredientComboBoxA);
            foodTab.Controls.Add(label4);
            foodTab.Location = new Point(4, 24);
            foodTab.Name = "foodTab";
            foodTab.Padding = new Padding(3);
            foodTab.Size = new Size(642, 276);
            foodTab.TabIndex = 0;
            foodTab.Text = "料理作成";
            foodTab.UseVisualStyleBackColor = true;
            // 
            // deleteDiscoveredReciepesButton
            // 
            deleteDiscoveredReciepesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteDiscoveredReciepesButton.Location = new Point(307, 166);
            deleteDiscoveredReciepesButton.Name = "deleteDiscoveredReciepesButton";
            deleteDiscoveredReciepesButton.Size = new Size(223, 23);
            deleteDiscoveredReciepesButton.TabIndex = 19;
            deleteDiscoveredReciepesButton.Text = "発見済みレシピの初期化";
            deleteDiscoveredReciepesButton.UseVisualStyleBackColor = true;
            deleteDiscoveredReciepesButton.Click += deleteDiscoveredReciepesButton_Click;
            // 
            // listUncreatedRecipesButton
            // 
            listUncreatedRecipesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            listUncreatedRecipesButton.Location = new Point(307, 137);
            listUncreatedRecipesButton.Name = "listUncreatedRecipesButton";
            listUncreatedRecipesButton.Size = new Size(223, 23);
            listUncreatedRecipesButton.TabIndex = 18;
            listUncreatedRecipesButton.Text = "未作成料理の組み合わせ調査";
            listUncreatedRecipesButton.UseVisualStyleBackColor = true;
            listUncreatedRecipesButton.Click += listUncreatedRecipesButton_Click;
            // 
            // rarityComboBox
            // 
            rarityComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            rarityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            rarityComboBox.FormattingEnabled = true;
            rarityComboBox.Items.AddRange(new object[] { "コモン", "レア", "エピック" });
            rarityComboBox.Location = new Point(289, 81);
            rarityComboBox.Name = "rarityComboBox";
            rarityComboBox.Size = new Size(241, 23);
            rarityComboBox.TabIndex = 17;
            // 
            // cookedCategoryComboBox
            // 
            cookedCategoryComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            cookedCategoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cookedCategoryComboBox.FormattingEnabled = true;
            cookedCategoryComboBox.Location = new Point(289, 30);
            cookedCategoryComboBox.Name = "cookedCategoryComboBox";
            cookedCategoryComboBox.Size = new Size(241, 23);
            cookedCategoryComboBox.TabIndex = 16;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Location = new Point(289, 63);
            label12.Name = "label12";
            label12.Size = new Size(37, 15);
            label12.TabIndex = 15;
            label12.Text = "レア度";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new Point(289, 12);
            label11.Name = "label11";
            label11.Size = new Size(78, 15);
            label11.TabIndex = 14;
            label11.Text = "調理後カテゴリ";
            // 
            // toMinusOneButton
            // 
            toMinusOneButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            toMinusOneButton.Location = new Point(7, 207);
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
            toMaxButton.Location = new Point(7, 178);
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
            label10.Location = new Point(7, 121);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 9;
            label10.Text = "作成個数";
            // 
            // createdNumericNo
            // 
            createdNumericNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createdNumericNo.Location = new Point(7, 139);
            createdNumericNo.Maximum = new decimal(new int[] { 869778, 0, 0, 0 });
            createdNumericNo.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            createdNumericNo.Name = "createdNumericNo";
            createdNumericNo.Size = new Size(149, 23);
            createdNumericNo.TabIndex = 8;
            createdNumericNo.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // ingredientComboBoxB
            // 
            ingredientComboBoxB.DrawMode = DrawMode.OwnerDrawFixed;
            ingredientComboBoxB.DropDownStyle = ComboBoxStyle.DropDownList;
            ingredientComboBoxB.FormattingEnabled = true;
            ingredientComboBoxB.Location = new Point(6, 81);
            ingredientComboBoxB.Name = "ingredientComboBoxB";
            ingredientComboBoxB.Size = new Size(252, 24);
            ingredientComboBoxB.TabIndex = 9;
            ingredientComboBoxB.DrawItem += ingredientComboBoxB_DrawItem;
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
            // ingredientComboBoxA
            // 
            ingredientComboBoxA.DrawMode = DrawMode.OwnerDrawFixed;
            ingredientComboBoxA.DropDownStyle = ComboBoxStyle.DropDownList;
            ingredientComboBoxA.FormattingEnabled = true;
            ingredientComboBoxA.Location = new Point(6, 30);
            ingredientComboBoxA.Name = "ingredientComboBoxA";
            ingredientComboBoxA.Size = new Size(252, 24);
            ingredientComboBoxA.TabIndex = 8;
            ingredientComboBoxA.DrawItem += ingredientComboBoxA_DrawItem;
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
            petTab.Controls.Add(petEditControl);
            petTab.Location = new Point(4, 24);
            petTab.Name = "petTab";
            petTab.Padding = new Padding(3);
            petTab.Size = new Size(642, 276);
            petTab.TabIndex = 2;
            petTab.Text = "ペット";
            petTab.UseVisualStyleBackColor = true;
            // 
            // petEditControl
            // 
            petEditControl.Dock = DockStyle.Fill;
            petEditControl.Location = new Point(3, 3);
            petEditControl.Margin = new Padding(4, 5, 4, 5);
            petEditControl.Name = "petEditControl";
            petEditControl.Size = new Size(636, 270);
            petEditControl.TabIndex = 0;
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
            cattleTab.Size = new Size(642, 276);
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
            otherTab.Controls.Add(ListupUnobtainedEquipButton);
            otherTab.Controls.Add(DisplayNameTextBox);
            otherTab.Controls.Add(label18);
            otherTab.Location = new Point(4, 24);
            otherTab.Name = "otherTab";
            otherTab.Padding = new Padding(3);
            otherTab.Size = new Size(642, 276);
            otherTab.TabIndex = 4;
            otherTab.Text = "その他";
            otherTab.UseVisualStyleBackColor = true;
            // 
            // ListupUnobtainedEquipButton
            // 
            ListupUnobtainedEquipButton.Location = new Point(17, 105);
            ListupUnobtainedEquipButton.Name = "ListupUnobtainedEquipButton";
            ListupUnobtainedEquipButton.Size = new Size(149, 23);
            ListupUnobtainedEquipButton.TabIndex = 2;
            ListupUnobtainedEquipButton.Text = "未発見アイテム一覧を出力";
            ListupUnobtainedEquipButton.UseVisualStyleBackColor = true;
            ListupUnobtainedEquipButton.Click += ListupUnobtainedEquipButton_Click;
            // 
            // DisplayNameTextBox
            // 
            DisplayNameTextBox.Location = new Point(17, 38);
            DisplayNameTextBox.Name = "DisplayNameTextBox";
            DisplayNameTextBox.Size = new Size(333, 23);
            DisplayNameTextBox.TabIndex = 1;
            DisplayNameTextBox.TextChanged += DisplayNameTextBox_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(17, 20);
            label18.Name = "label18";
            label18.Size = new Size(113, 15);
            label18.TabIndex = 0;
            label18.Text = "ユーザー定義の表示名";
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
            // saveFolderBrowserDialog
            // 
            saveFolderBrowserDialog.RootFolder = Environment.SpecialFolder.LocalApplicationData;
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
            createButton.Location = new Point(306, 449);
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
            resultLabel.Location = new Point(465, 464);
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
            itemListBox.DrawMode = DrawMode.OwnerDrawFixed;
            itemListBox.FormattingEnabled = true;
            itemListBox.ItemHeight = 15;
            itemListBox.Location = new Point(12, 146);
            itemListBox.Name = "itemListBox";
            itemListBox.Size = new Size(286, 349);
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
            // toolTipObjectName
            // 
            toolTipObjectName.AutoPopDelay = 5000;
            toolTipObjectName.InitialDelay = 100;
            toolTipObjectName.ReshowDelay = 100;
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
            menuStrip.Size = new Size(967, 24);
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
            // lastConnectedWorldLabel
            // 
            lastConnectedWorldLabel.AutoSize = true;
            lastConnectedWorldLabel.ForeColor = Color.Purple;
            lastConnectedWorldLabel.Location = new Point(592, 59);
            lastConnectedWorldLabel.Name = "lastConnectedWorldLabel";
            lastConnectedWorldLabel.Size = new Size(142, 15);
            lastConnectedWorldLabel.TabIndex = 26;
            lastConnectedWorldLabel.Text = "lastConnectedWorldLabel";
            lastConnectedWorldLabel.Visible = false;
            lastConnectedWorldLabel.Click += lastConnectedWorldLabel_Click;
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
            ClientSize = new Size(967, 508);
            Controls.Add(dropButton);
            Controls.Add(mapButton);
            Controls.Add(worldEditButton);
            Controls.Add(lastConnectedWorldLabel);
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
            ((System.ComponentModel.ISupportInitialize)auxIndexNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)variationNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)variationUpdateCountNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)amountNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)amountConst).EndInit();
            foodTab.ResumeLayout(false);
            foodTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).EndInit();
            petTab.ResumeLayout(false);
            cattleTab.ResumeLayout(false);
            cattleTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mealNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stomachNumericUpDown).EndInit();
            otherTab.ResumeLayout(false);
            otherTab.PerformLayout();
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
        private FolderBrowserDialog saveFolderBrowserDialog;
        private Label itemSlotLabel;
        private ComboBox ingredientComboBoxB;
        private Label label5;
        private ComboBox ingredientComboBoxA;
        private Label label4;
        private TextBox objectIdTextBox;
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
        private TextBox objectNameTextBox;
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
        private ToolTip toolTipObjectName;
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
        private Label lastConnectedWorldLabel;
        private Button worldEditButton;
        private Button ListupUnobtainedEquipButton;
        private Button mapButton;
        private Button dropButton;
    }
}
