using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Resource;
using System.ComponentModel;
using System.Data;

namespace CKCharaDataEditor.Control
{
    public partial class PetEditControl : UserControl
    {
        private FileManager _fileManager = FileManager.Instance;
        private const int TalentSlotCount = 9;

        public PetEditControl()
        {
            InitializeComponent();
            LoadPetKind();
        }

        private PetBattleType _battleType = PetBattleType.Undefined;

        private Pet _pet = Pet.Default;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pet PetItem
        {
            get
            {
                return AssemblePetParameters();
            }
            set
            {
                _pet = value;
                LoadPet(value!);
            }
        }

        private void InitializePetKindComboBox()
        {
            petKindComboBox.Items.Clear();
            var petKinds = Enum.GetValues<PetType>();
            foreach (var petKind in petKinds)
            {
                int objectId = (int)petKind;
                petKindComboBox.Items.Add(
                    _fileManager.LocalizationData.TryGetValue(objectId, out var translateResources)
                        ? translateResources.DisplayName
                        : petKind.ToString()
                );
            }
        }

        private void InitializePetColorComboBox(PetType petType)
        {
            petColorComboBox.Items.Clear();
            if (PetResource.ColorSelectablePets.Contains(petType))
            {
                foreach (var color in Enum.GetValues<PetColor>())
                {
                    petColorComboBox.Items.Add(PetResource.ColorDict[(petType, color)]);
                }
            }
            else
            {
                petColorComboBox.Items.Add(PetResource.ColorDict[(petType, PetColor.Color_0)]);
            }
            petColorComboBox.SelectedIndex = 0;
        }

        private void UpdateBattleType(PetType petType)
        {
            _battleType = PetResource.BattleType[petType];
            battleTypeLabel.Text = _battleType switch
            {
                PetBattleType.Melee => "近接",
                PetBattleType.Range => "遠距離",
                PetBattleType.Buff => "補助",
                _ => throw new InvalidOperationException("不正なPetBattleTypeです"),
            };
        }

        private List<PetTalentControl> GetOrderedTalentControls()
        {
            return petTalentTableLayoutPanel.Controls.OfType<PetTalentControl>()
                .OrderBy(c => c.SlotNo)
                .ToList();
        }

        private void UpdateTalentControlsBattleType()
        {
            foreach (var talentControl in GetOrderedTalentControls())
            {
                talentControl.BattleType = _battleType;
            }
        }

        public void LoadPetKind()
        {
            petKindComboBox.BeginUpdate();
            InitializePetKindComboBox();
            petKindComboBox.EndUpdate();
        }

        public void LoadPet(Pet petItem)
        {
            if (Pet.IsPet(petItem.objectID))
            {
                PetType petType = petItem.Type;

                UpdateBattleType(petType);
                petKindComboBox.SelectedIndex = Array.IndexOf(Enum.GetValues<PetType>(), petItem.Type);
                petExpNumeric.Value = petItem.Exp;

                InitializePetColorComboBox(petType);
                petColorComboBox.SelectedIndex = Convert.ToInt32(petItem.Color);
                petNameTextBox.Text = petItem.Name;

                LoadPetTalents(petItem.Talents);
            }
            else
            {
                PetItem = Pet.Default;
            }
        }

        private void LoadPetTalents(List<PetTalent> talents)
        {
            var talentControls = GetOrderedTalentControls();
            for (int i = 0; i < TalentSlotCount; i++)
            {
                talentControls[i].BattleType = _battleType;
                talentControls[i].Talent = talents[i];
            }
        }

        private void petKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var allPetTypes = Enum.GetValues<PetType>();
            var selectedPetType = allPetTypes[petKindComboBox.SelectedIndex];

            UpdateBattleType(selectedPetType);
            UpdateTalentControlsBattleType();
            InitializePetColorComboBox(selectedPetType);
        }

        private List<PetTalent> GeneratePetTalentLists()
        {
            return GetOrderedTalentControls()
                .Select(control =>
                {
                    var talent = control.Talent;
                    talent.Talent = (talent.Talent == -1) ? 2 : talent.Talent;
                    return talent;
                })
                .ToList();
        }

        private Pet AssemblePetParameters()
        {
            var allPetTypes = Enum.GetValues<PetType>();

            int auxIndex = _pet.Aux.index;
            int objectID = (int)allPetTypes[petKindComboBox.SelectedIndex];
            int petExp = (int)petExpNumeric.Value;
            string objectName = Enum.GetNames<PetType>()[petKindComboBox.SelectedIndex];
            var petTalents = GeneratePetTalentLists();
            ItemAuxData auxData = new(auxIndex, AuxPrefabManager.CreatePet(petNameTextBox.Text, petColorComboBox.SelectedIndex, petTalents));

            return new(new Item(objectID, petExp, 0, 0, objectName, auxData));
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
