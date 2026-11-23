namespace CKCharaDataEditor.Forms
{
    partial class SearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            closeButton = new Button();
            resultListBox = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            searchComboBox = new ComboBox();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Location = new Point(359, 369);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 1;
            closeButton.Text = "閉じる";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // resultListBox
            // 
            resultListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            resultListBox.Font = new Font("BIZ UDゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            resultListBox.FormattingEnabled = true;
            resultListBox.Location = new Point(12, 89);
            resultListBox.Name = "resultListBox";
            resultListBox.Size = new Size(422, 260);
            resultListBox.TabIndex = 2;
            resultListBox.SelectedIndexChanged += resultListBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 3;
            label1.Text = "検索ワード";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 4;
            label2.Text = "検索結果";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(13, 352);
            label3.Name = "label3";
            label3.Size = new Size(182, 15);
            label3.TabIndex = 5;
            label3.Text = "選択したobjectのIDが挿入されます。";
            // 
            // searchComboBox
            // 
            searchComboBox.FormattingEnabled = true;
            searchComboBox.Location = new Point(12, 45);
            searchComboBox.Name = "searchComboBox";
            searchComboBox.Size = new Size(224, 23);
            searchComboBox.TabIndex = 6;
            searchComboBox.TextUpdate += searchComboBox_TextUpdate;
            // 
            // SearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(451, 402);
            Controls.Add(searchComboBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(resultListBox);
            Controls.Add(closeButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SearchForm";
            Text = "objectID検索";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button closeButton;
        private ListBox resultListBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox searchComboBox;
    }
}