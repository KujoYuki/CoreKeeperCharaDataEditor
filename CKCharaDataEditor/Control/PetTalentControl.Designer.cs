namespace CKCharaDataEditor.Control
{
    partial class PetTalentControl
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
            petTalentCheckBox = new CheckBox();
            petTalentComboBox = new ComboBox();
            SuspendLayout();
            // 
            // petTalentCheckBox
            // 
            petTalentCheckBox.AutoSize = true;
            petTalentCheckBox.Location = new Point(3, 3);
            petTalentCheckBox.Name = "petTalentCheckBox";
            petTalentCheckBox.Size = new Size(54, 19);
            petTalentCheckBox.TabIndex = 0;
            petTalentCheckBox.Text = "スキル";
            petTalentCheckBox.UseVisualStyleBackColor = true;
            petTalentCheckBox.CheckedChanged += petTalentCheckBox_CheckedChanged;
            // 
            // petTalentComboBox
            // 
            petTalentComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            petTalentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            petTalentComboBox.DropDownWidth = 520;
            petTalentComboBox.FormattingEnabled = true;
            petTalentComboBox.Location = new Point(3, 28);
            petTalentComboBox.Name = "petTalentComboBox";
            petTalentComboBox.Size = new Size(110, 23);
            petTalentComboBox.TabIndex = 1;
            petTalentComboBox.SelectedIndexChanged += petTalentComboBox_SelectedIndexChanged;
            // 
            // PetTalentControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(petTalentComboBox);
            Controls.Add(petTalentCheckBox);
            Name = "PetTalentControl";
            Size = new Size(116, 55);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox petTalentCheckBox;
        private ComboBox petTalentComboBox;
    }
}
