using CKCharaDataEditor.Model;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Resource;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace CKCharaDataEditor.Control
{
    public partial class PetEditControl : UserControl, INotifyPropertyChanged
    {
        static readonly List<PetId> _colorSelectablePets = [PetId.PetDog, PetId.PetCat, PetId.PetBird, PetId.PetBunny, PetId.PetMoth, PetId.PetTardigrade, PetId.PetWarlock];

        public PetEditControl()
        {
            InitializeComponent();
            InitControl();
        }

        private PetBattleType _battleType = PetBattleType.Undefined;

        public event PropertyChangedEventHandler? PropertyChanged;

        private Item? _petItem = Item.Default;
        public Item? PetItem
        {
            get
            {
                _petItem = AssemblePetParameters();
                return _petItem;
            }
            set
            {
                _petItem = value;
                LoadPet(value!);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitControl()
        {
            var petKinds = Enum.GetNames<PetId>();
            petKindComboBox.Items.AddRange(petKinds);
            var petColors = Enum.GetNames<PetColor>();
            petColorComboBox.Items.AddRange(petColors);
        }

        private static readonly IEnumerable<int> allPetIds = Enum.GetValues(typeof(PetId)).Cast<int>();

        public void LoadPet(Item petItem)
        {
            if (allPetIds.Contains(_petItem!.Info.objectID))
            {
                int objectId = petItem.Info.objectID;
                PetId petId = (PetId)objectId;

                _battleType = PetResource.BattleType[petId];
                battleTypeLabel.Text = _battleType switch
                {
                    PetBattleType.Melee => "近接",
                    PetBattleType.Range => "遠距離",
                    PetBattleType.Buff => "補助",
                    _ => string.Empty,
                };
                petKindComboBox.SelectedIndex = Array.IndexOf(
                    Enum.GetValues(typeof(PetId)).Cast<int>().ToArray(), objectId);
                petExpNumeric.Value = petItem.Info.amount;

                petItem.Aux.GetPetData(out var name, out var color, out List<PetTalent> talents);
                petColorComboBox.SelectedIndex = color;
                petNameTextBox.Text = name;

                LoadPetTalents(_battleType, talents);
            }
            else
            {
                ResetPetTab();
            }

        }

        private void LoadPetTalents(PetBattleType battleType, List<PetTalent> talents)
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

            var allPetType = Enum.GetValues<PetId>();
            Dictionary<int, PetId> petTabDic = Enumerable.Range(0, allPetType.Length)
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
            var colorPets = _colorSelectablePets.Select(k => k.ToString());
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

        private Item? AssemblePetParameters()
        {
            var allPetTypes = Enum.GetValues<PetId>();
            var allPetColors = (PetColor[])Enum.GetValues(typeof(PetColor));

            //元アイテムがペットでない場合はauxIndexはデフォルト値で与える
            int auxIndex = _petItem?.Aux.index ?? 0;

            ItemInfo itemInfo = new(objectID: (int)allPetTypes[petKindComboBox.SelectedIndex],
                amount: (int)petExpNumeric.Value,
                variation: 0);
            string objectName = Enum.GetNames(typeof(PetId))[petKindComboBox.SelectedIndex];
            var petTalents = GeneratePetTalentLists();
            ItemAuxData auxData = new ItemAuxData(auxIndex, AuxPrefabManager.CreatePet(petNameTextBox.Text, petColorComboBox.SelectedIndex, petTalents));

            return new Item(itemInfo, objectName, auxData);
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
