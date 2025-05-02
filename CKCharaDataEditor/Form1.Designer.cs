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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            itemEditTabControl = new TabControl();
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
            advancedTab = new TabPage();
            inventryPasteButton = new Button();
            inventryCopyButton = new Button();
            parameterlinkLabel = new LinkLabel();
            itemPasteButton = new Button();
            itemCopyButton = new Button();
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
            openSkillbutton = new Button();
            slotReloadbutton = new Button();
            label13 = new Label();
            itemEditTabControl.SuspendLayout();
            foodTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).BeginInit();
            petTab.SuspendLayout();
            advancedTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)amountConst).BeginInit();
            SuspendLayout();
            // 
            // itemEditTabControl
            // 
            itemEditTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            itemEditTabControl.Controls.Add(foodTab);
            itemEditTabControl.Controls.Add(petTab);
            itemEditTabControl.Controls.Add(advancedTab);
            itemEditTabControl.Location = new Point(13, 170);
            itemEditTabControl.MinimumSize = new Size(650, 300);
            itemEditTabControl.Name = "itemEditTabControl";
            itemEditTabControl.SelectedIndex = 0;
            itemEditTabControl.Size = new Size(650, 300);
            itemEditTabControl.TabIndex = 0;
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
            foodTab.Size = new Size(642, 272);
            foodTab.TabIndex = 0;
            foodTab.Text = "料理作成";
            foodTab.UseVisualStyleBackColor = true;
            // 
            // deleteDiscoveredReciepesButton
            // 
            deleteDiscoveredReciepesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteDiscoveredReciepesButton.Location = new Point(307, 162);
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
            listUncreatedRecipesButton.Location = new Point(307, 133);
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
            toMinusOneButton.Location = new Point(7, 203);
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
            toMaxButton.Location = new Point(7, 174);
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
            label10.Location = new Point(7, 117);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 9;
            label10.Text = "作成個数";
            // 
            // createdNumericNo
            // 
            createdNumericNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createdNumericNo.Location = new Point(7, 135);
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
            petTab.Size = new Size(642, 272);
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
            petEditControl.Size = new Size(636, 266);
            petEditControl.TabIndex = 0;
            // 
            // advancedTab
            // 
            advancedTab.Controls.Add(inventryPasteButton);
            advancedTab.Controls.Add(inventryCopyButton);
            advancedTab.Controls.Add(parameterlinkLabel);
            advancedTab.Controls.Add(itemPasteButton);
            advancedTab.Controls.Add(itemCopyButton);
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
            advancedTab.Size = new Size(642, 272);
            advancedTab.TabIndex = 1;
            advancedTab.Text = "上級者向け";
            advancedTab.UseVisualStyleBackColor = true;
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
            itemPasteButton.Text = "アイテム情報のペースト";
            itemPasteButton.UseVisualStyleBackColor = true;
            itemPasteButton.Click += PasteButton_Click;
            // 
            // itemCopyButton
            // 
            itemCopyButton.Location = new Point(305, 180);
            itemCopyButton.Name = "itemCopyButton";
            itemCopyButton.Size = new Size(130, 23);
            itemCopyButton.TabIndex = 26;
            itemCopyButton.Text = "アイテム情報のコピー";
            itemCopyButton.UseVisualStyleBackColor = true;
            itemCopyButton.Click += CopyButton_Click;
            // 
            // auxDataTextBox
            // 
            auxDataTextBox.Location = new Point(10, 145);
            auxDataTextBox.Name = "auxDataTextBox";
            auxDataTextBox.ReadOnly = true;
            auxDataTextBox.Size = new Size(601, 23);
            auxDataTextBox.TabIndex = 25;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(9, 127);
            label18.Name = "label18";
            label18.Size = new Size(50, 15);
            label18.TabIndex = 24;
            label18.Text = "auxData";
            // 
            // auxIndexTextBox
            // 
            auxIndexTextBox.Location = new Point(455, 88);
            auxIndexTextBox.Name = "auxIndexTextBox";
            auxIndexTextBox.ReadOnly = true;
            auxIndexTextBox.Size = new Size(100, 23);
            auxIndexTextBox.TabIndex = 23;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(455, 72);
            label17.Name = "label17";
            label17.Size = new Size(55, 15);
            label17.TabIndex = 22;
            label17.Text = "auxIndex";
            // 
            // amountConst
            // 
            amountConst.Location = new Point(120, 89);
            amountConst.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            amountConst.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            amountConst.Name = "amountConst";
            amountConst.Size = new Size(99, 23);
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
            // variationUpdateCountTextBox
            // 
            variationUpdateCountTextBox.Location = new Point(455, 27);
            variationUpdateCountTextBox.Name = "variationUpdateCountTextBox";
            variationUpdateCountTextBox.ReadOnly = true;
            variationUpdateCountTextBox.Size = new Size(156, 23);
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
            label7.Location = new Point(10, 70);
            label7.Name = "label7";
            label7.Size = new Size(48, 15);
            label7.TabIndex = 10;
            label7.Text = "amount";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(455, 11);
            label9.Name = "label9";
            label9.Size = new Size(123, 15);
            label9.TabIndex = 14;
            label9.Text = "variationUpdateCount";
            // 
            // amoutTextBox
            // 
            amoutTextBox.Location = new Point(9, 88);
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
            saveSlotNoComboBox.Location = new Point(12, 87);
            saveSlotNoComboBox.Name = "saveSlotNoComboBox";
            saveSlotNoComboBox.Size = new Size(148, 23);
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
            savePathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            savePathTextBox.Location = new Point(12, 27);
            savePathTextBox.Name = "savePathTextBox";
            savePathTextBox.Size = new Size(565, 23);
            savePathTextBox.TabIndex = 4;
            savePathTextBox.Validating += savePathTextBox_Validating;
            // 
            // saveFolderBrowserDialog
            // 
            saveFolderBrowserDialog.RootFolder = Environment.SpecialFolder.LocalApplicationData;
            // 
            // openSevePathDialogButton
            // 
            openSevePathDialogButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            openSevePathDialogButton.Location = new Point(583, 26);
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
            itemSlotLabel.Location = new Point(13, 123);
            itemSlotLabel.Name = "itemSlotLabel";
            itemSlotLabel.Size = new Size(72, 15);
            itemSlotLabel.TabIndex = 6;
            itemSlotLabel.Text = "インベントリ枠";
            // 
            // inventoryIndexComboBox
            // 
            inventoryIndexComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inventoryIndexComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            inventoryIndexComboBox.FormattingEnabled = true;
            inventoryIndexComboBox.Location = new Point(13, 141);
            inventoryIndexComboBox.Name = "inventoryIndexComboBox";
            inventoryIndexComboBox.Size = new Size(253, 23);
            inventoryIndexComboBox.TabIndex = 7;
            inventoryIndexComboBox.TextChanged += inventoryIndexComboBox_TextChanged;
            // 
            // createButton
            // 
            createButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createButton.Location = new Point(13, 479);
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
            resultLabel.Location = new Point(172, 494);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(64, 15);
            resultLabel.TabIndex = 14;
            resultLabel.Text = "resultLabel";
            resultLabel.Visible = false;
            // 
            // previousItemButton
            // 
            previousItemButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            previousItemButton.Location = new Point(285, 140);
            previousItemButton.Name = "previousItemButton";
            previousItemButton.Size = new Size(58, 23);
            previousItemButton.TabIndex = 15;
            previousItemButton.Text = "◀";
            previousItemButton.UseVisualStyleBackColor = true;
            previousItemButton.Click += previousItemButton_Click;
            // 
            // nextItemButton
            // 
            nextItemButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nextItemButton.Location = new Point(349, 140);
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
            openConditionsButton.Location = new Point(295, 86);
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
            openSkillbutton.Location = new Point(449, 86);
            openSkillbutton.Name = "openSkillbutton";
            openSkillbutton.Size = new Size(138, 23);
            openSkillbutton.TabIndex = 18;
            openSkillbutton.Text = "スキルポイント編集";
            openSkillbutton.UseVisualStyleBackColor = false;
            openSkillbutton.Click += openSkillButton_Click;
            // 
            // slotReloadbutton
            // 
            slotReloadbutton.Location = new Point(167, 86);
            slotReloadbutton.Name = "slotReloadbutton";
            slotReloadbutton.Size = new Size(90, 23);
            slotReloadbutton.TabIndex = 19;
            slotReloadbutton.Text = "再読み込み";
            slotReloadbutton.UseVisualStyleBackColor = true;
            slotReloadbutton.Click += slotReloadbutton_Click;
            // 
            // label13
            // 
            label13.ForeColor = Color.Red;
            label13.Location = new Point(328, 479);
            label13.Name = "label13";
            label13.Size = new Size(334, 45);
            label13.TabIndex = 20;
            label13.Text = "編集が無効化される可能性があります。作成後のゲーム起動時は毎回新規ワールドを作成して入り、編集内容を確定させてください。詳しくはreadmeへ。";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(674, 535);
            Controls.Add(label13);
            Controls.Add(slotReloadbutton);
            Controls.Add(openSkillbutton);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(690, 574);
            Name = "Form1";
            Text = "CKCharaDataEditor";
            FormClosing += Form1_FormClosing;
            itemEditTabControl.ResumeLayout(false);
            foodTab.ResumeLayout(false);
            foodTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)createdNumericNo).EndInit();
            petTab.ResumeLayout(false);
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
        private ComboBox ingredientComboBoxB;
        private Label label5;
        private ComboBox ingredientComboBoxA;
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
        private Button listUncreatedRecipesButton;
        private TextBox auxDataTextBox;
        private Label label18;
        private TextBox auxIndexTextBox;
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
    }
}
