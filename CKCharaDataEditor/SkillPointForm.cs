using CKCharaDataEditor.Control;
using CKCharaDataEditor.Model;
using System.Data;

namespace CKCharaDataEditor
{
    public partial class SkillPointForm : Form
    {
        readonly SaveDataManager _saveDataManager = SaveDataManager.Instance;

        public SkillPointForm()
        {
            InitializeComponent();
            LoadSkillPoint();
        }

        private void LoadSkillPoint()
        {
            var skills = _saveDataManager.LoadSkillPoint();
            foreach (var skillPointControl in skillTableLayoutPanel.Controls.Cast<SkillPointNumeric>())
            {
                int id = skillPointControl.SkillID;
                skillPointControl.Point = skills.SingleOrDefault(x => x.skillID == id)!.value;
            }
        }

        private void applySkillPointButton_Click(object sender, EventArgs e)
        {
            var newSkillPoints = skillTableLayoutPanel.Controls.Cast<SkillPointNumeric>()
                .Select(x => new Skill
                {
                    skillID = x.SkillID,
                    value = x.Point
                })
                .ToList();
            _saveDataManager.WriteSkillPoint(newSkillPoints);
            EnableResultMessage("スキルポイントを適用しました。");
        }

        private async void EnableResultMessage(string message)
        {
            resultLabel.Text = message;
            resultLabel.Visible = true;
            await Task.Delay(3000);
            resultLabel.Visible = false;
        }
    }
}
