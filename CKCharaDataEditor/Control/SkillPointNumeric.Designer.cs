namespace CKCharaDataEditor.Control
{
    partial class SkillPointNumeric
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
            skillNameLabel = new Label();
            skillPointNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)skillPointNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // skillNameLabel
            // 
            skillNameLabel.AutoSize = true;
            skillNameLabel.Location = new Point(3, 0);
            skillNameLabel.Name = "skillNameLabel";
            skillNameLabel.Size = new Size(38, 15);
            skillNameLabel.TabIndex = 0;
            skillNameLabel.Text = "label1";
            // 
            // skillPointNumericUpDown
            // 
            skillPointNumericUpDown.Location = new Point(3, 18);
            skillPointNumericUpDown.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            skillPointNumericUpDown.Name = "skillPointNumericUpDown";
            skillPointNumericUpDown.Size = new Size(120, 23);
            skillPointNumericUpDown.TabIndex = 1;
            // 
            // SkillPointNumeric
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(skillPointNumericUpDown);
            Controls.Add(skillNameLabel);
            Name = "SkillPointNumeric";
            Size = new Size(129, 48);
            ((System.ComponentModel.ISupportInitialize)skillPointNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label skillNameLabel;
        private NumericUpDown skillPointNumericUpDown;
    }
}
