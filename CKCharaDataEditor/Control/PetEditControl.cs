using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Resource;
using System.Data;

namespace CKCharaDataEditor.Control
{
    public partial class PetEditControl : UserControl
    {
        private FileManager _fileManager = FileManager.Instance;

        public PetEditControl()
        {
            InitializeComponent();
            LoadPetKind();
        }

        private PetBattleType _battleType = PetBattleType.Undefined;

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

        public void LoadPetKind()
        {
            petKindComboBox.BeginUpdate();
            var petKinds = Enum.GetValues<PetType>();

            petKindComboBox.Items.Clear();
            for (int i = 0; i < petKinds.Length; i++)
            {
                int objectId = (int)petKinds[i];
                if (_fileManager.LocalizationData.TryGetValue(objectId, out string[]? translateResources))  //hack 初期化時点では辞書初期化がまだ実行されていない
                {
                    petKindComboBox.Items.Add(translateResources[1]);
                }
                else
                {
                    petKindComboBox.Items.Add(petKinds[i].ToString());
                }
            }
            petKindComboBox.EndUpdate();
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
                PetItem = Pet.Default;
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

        private void petKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            List<(int objectID, string DisplayName)> colorPet = [];
            foreach (int ID in PetResource.ColorSelectablePets.Select(k => (int)k).ToArray())
            {
                string displayName = _fileManager.LocalizationData.TryGetValue(ID, out string[]? translateResources)
                    ? translateResources[1]
                    : PetResource.PetDic[ID].objectName;
                colorPet.Add((ID, displayName));
            }
            int selectedPetId = colorPet.SingleOrDefault(pet=>pet.DisplayName == petKindComboBox.SelectedItem?.ToString()).objectID;
            if (colorPet.Select(cp=>cp.objectID).Contains(selectedPetId))
            {
                foreach (var color in Enum.GetValues<PetColor>())
                {
                    petColorComboBox.Items.Add(PetResource.ColorDict[(allPetType[petKindComboBox.SelectedIndex], color)]);
                }
            }
            else
            {
                // スライムJrなど一色しかない系
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
            if (sender is TextBox textBox)
            {
                StaticResource.SanitizeTextBoxText(textBox);
            }
        }
    }
}
