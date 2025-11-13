using System.ComponentModel;

namespace CKCharaDataEditor.Control
{
    public partial class SkillPointNumeric : UserControl
    {
        public SkillPointNumeric()
        {
            InitializeComponent();
            skillPointNumericUpDown.Maximum = int.MaxValue;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SkillID { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SkillName
        {
            get => skillNameLabel.Text;
            set => skillNameLabel.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Point
        {
            get => (int)skillPointNumericUpDown.Value;
            set => skillPointNumericUpDown.Value = value;
        }
    }
}
