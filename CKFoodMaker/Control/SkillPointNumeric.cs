namespace CKFoodMaker.Control
{
    public partial class SkillPointNumeric : UserControl
    {
        public SkillPointNumeric()
        {
            InitializeComponent();
            skillPointNumericUpDown.Maximum = int.MaxValue;
        }

        public int SkillID { get; set; }

        public string SkillName
        {
            get => skillNameLabel.Text;
            set => skillNameLabel.Text = value;
        }
        public int Point
        {
            get => (int)skillPointNumericUpDown.Value;
            set => skillPointNumericUpDown.Value = value;
        }
    }
}
