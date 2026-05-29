using CKCharaDataEditor.Resources;
using System.ComponentModel;
using System.Globalization;

namespace CKCharaDataEditor.Forms
{
    public partial class SpecialStringInputForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SpecialText
        {
            get { return specialTextBox.Text; }
            set { specialTextBox.Text = value; }
        }

        public SpecialStringInputForm()
        {
            InitializeComponent();
            InitControls();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void InitControls()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button button)
                {
                    button.Click += textButton_Click;
                }
            }
        }

        private void textButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button button
                && button.Tag is string tag)
            {
                // U+XXXX形式の文字コード対応
                if (tag.StartsWith("U+", StringComparison.OrdinalIgnoreCase)) tag = tag.Substring(2);
    
                if (int.TryParse(tag, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int code)
                    && code >= 0 && code <= 0x10FFFF)
                {
                    specialTextBox.Text += char.ConvertFromUtf32(code);
                }
            }
        }

        private void specialTextBox_TextChanged(object sender, EventArgs e)
        {
            StaticResource.SanitizeTextBoxText(sender, e);
        }
    }
}
