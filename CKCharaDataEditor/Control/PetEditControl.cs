using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Pet;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace CKCharaDataEditor.Control
{
    public partial class PetEditControl : UserControl, INotifyPropertyChanged
    {
        private FileManager _fileManager = FileManager.Instance;

        public PetEditControl()
        {
            InitializeComponent();
            InitControl();
        }

        private PetBattleType _battleType = PetBattleType.Undefined;

        public event PropertyChangedEventHandler? PropertyChanged;

        private Pet _pet = Pet.Default;
        public Pet PetItem
        {
            get
            {
                _pet = AssemblePetParameters();
                return _pet;
            }
            set
            {
                _pet = value;
                LoadPet(value!);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitControl()
        {
            var petKinds = Enum.GetValues<PetType>();
            for (int i = 0; i < petKinds.Length; i++)
            {
                string objectId = ((int)petKinds[i]).ToString();
                //hack LocalizationDataを取得する前に初期化してしまうから一生初期化できない
                if (_fileManager.LocalizationData.TryGetValue(objectId, out string[]? translateResources))
                {
                    petKindComboBox.Items.Add(translateResources[1]);
                }
                else
                {
                    petKindComboBox.Items.Add(petKinds[i].ToString());
                }
            }
        }

        public void LoadPet(Pet petItem)
        {
            if (Pet.IsPet(petItem.objectID))
            {
                PetType petType = petItem.Type;

                _battleType = PetResource.BattleType[petType];
                battleTypeLabel.Text = _battleType switch
                {
                    PetBattleType.Melee => "近接",
                    PetBattleType.Range => "遠距離",
                    PetBattleType.Buff => "補助",
                    _ => throw new ArgumentException(),
                };
                petKindComboBox.SelectedIndex = Array.IndexOf(Enum.GetValues<PetType>(), petItem.Type);
                petExpNumeric.Value = petItem.Exp;

                petColorComboBox.SelectedIndex = Convert.ToInt32(petItem.Color);
                petNameTextBox.Text = petItem.Name;

                LoadPetTalents(_battleType, petItem.Talents);
            }
            else
            {
                ResetPetTab();
            }

        }

        private void LoadPetTalents(PetBattleType battleType, IEnumerable<PetTalent> talents)
        {
            var petTalentContrls = petTalentTableLayoutPanel.Controls.Cast<PetTalentControl>()
                .OrderBy(control => control.SlotNo)
                .ToList();
            foreach (var (control, talent) in petTalentContrls.Zip(talents))
            {
                control.BattleType = _battleType;
                control.Talent = talent;
            }
        }

        public void ResetPetTab()
        {
            petColorComboBox.SelectedIndex = -1;
            petExpNumeric.Value = 0;
            petNameTextBox.Text = string.Empty;
            battleTypeLabel.Text = string.Empty;

            var petTalentControls = petTalentTableLayoutPanel.Controls.Cast<PetTalentControl>()
                .OrderBy(control => control.SlotNo)
                .ToList();
            foreach (var control in petTalentControls)
            {
                control.Reset();
            }
            petKindComboBox.SelectedIndex = -1;
        }

        private void petKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex is -1)
            {
                battleTypeLabel.Text = "--";
                return;
            }

            var allPetType = Enum.GetValues<PetType>();
            Dictionary<int, PetType> petTabDic = Enumerable.Range(0, allPetType.Length)
                .Select(i => (i, allPetType[i]))
                .ToDictionary();
            var colorDic = Enumerable.Range(0, Enum.GetValues<PetColor>().Length - 1)
                .Select(i => (i, Enum.GetValues<PetColor>()[i]))
                .ToDictionary();
            petColorComboBox.Items.Clear();

            // ペット種別で戦闘タイプ更新
            _battleType = PetResource.BattleType[petTabDic[petKindComboBox.SelectedIndex]];
            battleTypeLabel.Text = _battleType switch
            {
                PetBattleType.Melee => "近接",
                PetBattleType.Range => "遠距離",
                PetBattleType.Buff => "補助",
                _ => throw new InvalidOperationException()
            };

            // タレント表示内容更新
            var talents = petTalentTableLayoutPanel.Controls.Cast<PetTalentControl>()
                .OrderBy(control => control.SlotNo);
            foreach (var control in talents)
            {
                control.BattleType = _battleType;
            }

            // 多色ペット判定
            var colorPets = PetResource.ColorSelectablePets.Select(k => k.ToString());
            if (colorPets.Contains(petKindComboBox.SelectedItem?.ToString()))
            {
                foreach (var color in Enum.GetValues<PetColor>())
                {
                    petColorComboBox.Items.Add(PetResource.ColorDict[(allPetType[petKindComboBox.SelectedIndex], color)]);
                }
            }
            else
            {
                // スライム系
                petColorComboBox.Items.Add(PetResource.ColorDict[(allPetType[petKindComboBox.SelectedIndex], PetColor.Color_0)]);
            }
            petColorComboBox.SelectedIndex = 0;

        }

        private List<PetTalent> GeneratePetTalentLists()
        {
            var talents = petTalentTableLayoutPanel.Controls.Cast<PetTalentControl>()
                .OrderBy(control => control.SlotNo)
                .Select(control =>
                {
                    var talent = control.Talent;
                    // Controlのindexが未選択の時は0として扱う
                    if (talent.Talent is -1)
                    {
                        talent.Talent = 0;
                    }
                    return talent;
                })
                .ToList();
            return talents;
        }

        private Pet AssemblePetParameters()
        {
            var allPetTypes = Enum.GetValues<PetType>();
            var allPetColors = Enum.GetValues<PetColor>();

            int auxIndex = _pet.Aux.index;
            int objectID = (int)allPetTypes[petKindComboBox.SelectedIndex];
            int amount = (int)petExpNumeric.Value;
            string objectName = Enum.GetNames<PetType>()[petKindComboBox.SelectedIndex];
            var petTalents = GeneratePetTalentLists();
            ItemAuxData auxData = new ItemAuxData(auxIndex, AuxPrefabManager.CreatePet(petNameTextBox.Text, petColorComboBox.SelectedIndex, petTalents));

            return new(new Item(objectID, amount, 0, 0, objectName, auxData));
        }

        private void petNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = petNameTextBox.Text;
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            // ペット名が64バイトを超えたら溢れた分を削除する
            if (bytes.Length > 64)
            {
                while (Encoding.UTF8.GetByteCount(text) > 64)
                {
                    text = text.Substring(0, text.Length - 1);
                }
                petNameTextBox.Text = text;
                petNameTextBox.SelectionStart = text.Length; // キャレット位置を末尾に設定
            }
        }
    }
}
