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
            foreach (Control control in buttonsTableLayoutPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Click += textButton_Click;
                }
            }
            if (Program.IsDeveloper)
            {
                fillRandomButton.Visible = true;
            }
        }

        private void textButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button button
                && button.Tag is string tag)
            {
                specialTextBox.Text += GetUnicodeString(button);
            }
        }

        private void specialTextBox_TextChanged(object sender, EventArgs e)
        {
            StaticResource.SanitizeTextBoxText(sender, e);
        }

        private string GetUnicodeString(Button button)
        {
            if (button.Tag is string tag)
            {
                // U+XXXX形式の文字コード対応
                if (tag.StartsWith("U+", StringComparison.OrdinalIgnoreCase)) tag = tag.Substring(2);

                if (int.TryParse(tag, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int code)
                    && code >= 0 && code <= 0x10FFFF)
                {
                    return char.ConvertFromUtf32(code);
                }
            }
            return string.Empty;
        }

        private void fillRandomButton_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (Control control in buttonsTableLayoutPanel.Controls)
            {
                if (control is Button button) 
                {
                    list.Add(GetUnicodeString(button));
                }
            }
            var random = new Random();
            specialTextBox.Text = string.Empty;
            var hitIndex = new List<int>();
            while (hitIndex.Count < 21) 
            {
                int index = random.Next(list.Count);
                if (hitIndex.Contains(index)) continue;
                hitIndex.Add(index);
                specialTextBox.Text += list[index];
            }
            specialTextBox.SelectAll();
        }
    }
}
