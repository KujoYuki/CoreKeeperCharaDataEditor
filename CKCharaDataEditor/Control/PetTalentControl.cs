using CKCharaDataEditor.Model.Pet;

namespace CKCharaDataEditor.Control
{
    public partial class PetTalentControl : UserControl
    {
        const char _separator = ',';

        public PetTalentControl()
        {
            InitializeComponent();
            LoadTalentList();
        }

        private int _slotNo;
        public int SlotNo
        {
            get { return _slotNo; }
            set
            {
                _slotNo = value;
                petTalentCheckBox.Text = $"タレント {value + 1}";
            }
        }

        private PetTalent _petTalent = PetTalent.Default;
        public PetTalent Talent
        {
            get
            {
                return _petTalent;
            }
            set
            {
                _petTalent = value;
                petTalentComboBox.SelectedIndex = value.Talent;
                petTalentCheckBox.Checked = value.Points is 1;
            }
        }

        static PetBattleType _battleType = PetBattleType.Undefined;
        public PetBattleType BattleType
        {
            get => _battleType;
            set
            {
                _battleType = value;
                LoadTalentList();
            }
        }

        public void LoadTalentList()
        {
            List<string> validTalentIds = [];
            switch (_battleType)
            {
                case PetBattleType.Melee or PetBattleType.Range:
                    validTalentIds.AddRange(
                        PetResource.TalentDescriptionDict
                        .Select(set => 
                        {
                            var list = new string[] { set.Id.ToString(), set.MeleeRangeDisplayName, set.MeleeRangeDescription };
                            return string.Join(_separator, list);
                        })
                        .ToList());
                    break;
                case PetBattleType.Buff:
                    validTalentIds.AddRange(
                        PetResource.TalentDescriptionDict
                        .Select(set =>
                        {
                            var list = new string[] { set.Id.ToString(), set.BuffDisplayName, set.BuffDescription };
                            return string.Join(_separator, list);
                        })
                        .ToList());
                    break;
                default:
                    break;
            }
            int selectedIndex = petTalentComboBox.SelectedIndex;
            petTalentComboBox.Items.Clear();
            petTalentComboBox.Items.AddRange(validTalentIds.ToArray());
            petTalentComboBox.SelectedIndex = selectedIndex;
        }

        public void Reset()
        {
            _battleType = PetBattleType.Undefined;
            petTalentComboBox.Items.Clear();
            petTalentCheckBox.Checked = false;
            petTalentComboBox.SelectedIndex = -1;
        }

        private void petTalentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _petTalent.Points = petTalentCheckBox.Checked ? 1 : 0;
        }

        private void petTalentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTalentId = petTalentComboBox.SelectedItem?.ToString()?.Split(_separator).First() ?? string.Empty;
            if (int.TryParse(selectedTalentId, out int talentId))
            {
                _petTalent.Talent = talentId;
            }
            else
            {
                // 何も選択されてない場合は規定値を0とする
                _petTalent.Talent = 0;
            }
        }
    }
}
