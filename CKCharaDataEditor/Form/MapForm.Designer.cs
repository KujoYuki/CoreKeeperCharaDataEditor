namespace CKCharaDataEditor
{
    partial class MapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
            mapListBox = new ListBox();
            worldIdTextLabel = new Label();
            worldIdLabel = new Label();
            mapPictureBox = new PictureBox();
            saveMapAsImageButton = new Button();
            deleteSelectedMapButton = new Button();
            panel1 = new Panel();
            saveMapFileDialog = new SaveFileDialog();
            flowLayoutPanel = new FlowLayoutPanel();
            oreGroupCheckBox = new CheckBox();
            oreBoulderListBox = new CheckedListBox();
            highLightGroupBox = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)mapPictureBox).BeginInit();
            panel1.SuspendLayout();
            flowLayoutPanel.SuspendLayout();
            highLightGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // mapListBox
            // 
            mapListBox.FormattingEnabled = true;
            mapListBox.ItemHeight = 15;
            mapListBox.Location = new Point(12, 12);
            mapListBox.Name = "mapListBox";
            mapListBox.Size = new Size(177, 559);
            mapListBox.TabIndex = 1;
            mapListBox.SelectedIndexChanged += mapListBox_SelectedIndexChanged;
            // 
            // worldIdTextLabel
            // 
            worldIdTextLabel.AutoSize = true;
            worldIdTextLabel.Location = new Point(205, 26);
            worldIdTextLabel.Name = "worldIdTextLabel";
            worldIdTextLabel.Size = new Size(70, 15);
            worldIdTextLabel.TabIndex = 2;
            worldIdTextLabel.Text = "World Guid:";
            // 
            // worldIdLabel
            // 
            worldIdLabel.AutoSize = true;
            worldIdLabel.Location = new Point(329, 26);
            worldIdLabel.Name = "worldIdLabel";
            worldIdLabel.Size = new Size(127, 15);
            worldIdLabel.TabIndex = 3;
            worldIdLabel.Text = "There is the world guid";
            worldIdLabel.Click += worldIdLabel_Click;
            // 
            // mapPictureBox
            // 
            mapPictureBox.Location = new Point(13, 13);
            mapPictureBox.Name = "mapPictureBox";
            mapPictureBox.Size = new Size(732, 490);
            mapPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            mapPictureBox.TabIndex = 6;
            mapPictureBox.TabStop = false;
            mapPictureBox.MouseDown += mapPictureBox_MouseDown;
            mapPictureBox.MouseMove += mapPictureBox_MouseMove;
            // 
            // saveMapAsImageButton
            // 
            saveMapAsImageButton.Location = new Point(200, 44);
            saveMapAsImageButton.Name = "saveMapAsImageButton";
            saveMapAsImageButton.Size = new Size(118, 23);
            saveMapAsImageButton.TabIndex = 7;
            saveMapAsImageButton.Text = "画像で保存する";
            saveMapAsImageButton.UseVisualStyleBackColor = true;
            saveMapAsImageButton.Click += saveMapAsImageButton_Click;
            // 
            // deleteSelectedMapButton
            // 
            deleteSelectedMapButton.Location = new Point(329, 44);
            deleteSelectedMapButton.Name = "deleteSelectedMapButton";
            deleteSelectedMapButton.Size = new Size(118, 23);
            deleteSelectedMapButton.TabIndex = 8;
            deleteSelectedMapButton.Text = "このマップを削除する";
            deleteSelectedMapButton.UseVisualStyleBackColor = true;
            deleteSelectedMapButton.Click += deleteSelectedMapButton_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Controls.Add(mapPictureBox);
            panel1.Location = new Point(195, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(791, 520);
            panel1.TabIndex = 9;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(oreGroupCheckBox);
            flowLayoutPanel.Controls.Add(oreBoulderListBox);
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.Location = new Point(3, 19);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(201, 9);
            flowLayoutPanel.TabIndex = 10;
            // 
            // oreGroupCheckBox
            // 
            oreGroupCheckBox.AutoSize = true;
            oreGroupCheckBox.Location = new Point(3, 3);
            oreGroupCheckBox.Name = "oreGroupCheckBox";
            oreGroupCheckBox.Size = new Size(72, 19);
            oreGroupCheckBox.TabIndex = 0;
            oreGroupCheckBox.Text = "鉱石の岩";
            oreGroupCheckBox.UseVisualStyleBackColor = true;
            oreGroupCheckBox.CheckedChanged += oreGroupCheckBox_CheckedChanged;
            // 
            // oreBoulderListBox
            // 
            oreBoulderListBox.FormattingEnabled = true;
            oreBoulderListBox.Items.AddRange(new object[] { "銅の鉱石の岩", "スズの鉱石の岩", "鉄の鉱石の岩", "真紅石の鉱石の岩", "オクタリンの鉱石の岩", "ガラクサイトの鉱石の岩", "ソラライトの鉱石の岩", "パンドリウムの鉱石の岩" });
            oreBoulderListBox.Location = new Point(81, 3);
            oreBoulderListBox.Name = "oreBoulderListBox";
            oreBoulderListBox.Size = new Size(195, 148);
            oreBoulderListBox.TabIndex = 7;
            oreBoulderListBox.Visible = false;
            // 
            // highLightGroupBox
            // 
            highLightGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            highLightGroupBox.Controls.Add(flowLayoutPanel);
            highLightGroupBox.Location = new Point(12, 571);
            highLightGroupBox.Name = "highLightGroupBox";
            highLightGroupBox.Size = new Size(207, 31);
            highLightGroupBox.TabIndex = 0;
            highLightGroupBox.TabStop = false;
            highLightGroupBox.Text = "ハイライト表示";
            highLightGroupBox.Visible = false;
            // 
            // MapForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 614);
            Controls.Add(highLightGroupBox);
            Controls.Add(panel1);
            Controls.Add(deleteSelectedMapButton);
            Controls.Add(saveMapAsImageButton);
            Controls.Add(worldIdLabel);
            Controls.Add(worldIdTextLabel);
            Controls.Add(mapListBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MapForm";
            Text = "マップ閲覧";
            ((System.ComponentModel.ISupportInitialize)mapPictureBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel.ResumeLayout(false);
            flowLayoutPanel.PerformLayout();
            highLightGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox mapListBox;
        private Label worldIdTextLabel;
        private Label worldIdLabel;
        private PictureBox mapPictureBox;
        private Button saveMapAsImageButton;
        private Button deleteSelectedMapButton;
        private Panel panel1;
        private SaveFileDialog saveMapFileDialog;
        private FlowLayoutPanel flowLayoutPanel;
        private GroupBox highLightGroupBox;
        private CheckBox copperOreCheckBox;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private CheckBox checkBox6;
        private CheckBox oreGroupCheckBox;
        private FlowLayoutPanel oreFlowLayoutPanel;
        private CheckedListBox oreBoulderListBox;
    }
}