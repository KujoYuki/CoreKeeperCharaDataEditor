using CKCharaDataEditor.Model.Pet;
using CKCharaDataEditor.Resource;
using System.Text.Json.Nodes;

namespace CKCharaDataEditor.Model.ItemAux
{
    public record ItemAuxData
    {
        public AuxPrefabManager AuxPrefabManager;

        #region Property
        public int index { get; set; }
        public string data
        {
            get
            {
                if (AuxPrefabManager == AuxPrefabManager.Default)
                {
                    return string.Empty;
                }
                return AuxPrefabManager.ToInnerJsonString(StaticResource.SerializerOption);
            }
            set
            {
                try
                {
                    AuxPrefabManager = new AuxPrefabManager(JsonNode.Parse(value)!.AsObject());
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"補助データのフォーマットが違います。\n{value}\n{ex.StackTrace}");
                }
            }
        }
        #endregion

        public static readonly ItemAuxData Default = new(0, "");

        public ItemAuxData(int index, string data)
        {
            this.index = index;
            AuxPrefabManager = (data is "") ? AuxPrefabManager.Default.DeepCopy()
                : new AuxPrefabManager(JsonNode.Parse(data)!.AsObject());
        }

        public ItemAuxData(int index, AuxPrefabManager prefabManager)
        {
            this.index = index;
            AuxPrefabManager = prefabManager;
            this.data = prefabManager.GetJsonString();
        }

        [Obsolete("Petクラスの責務であるため、代替のちに廃止する")]
        public void GetPetData(out string Name, out int Color, out List<PetTalent> Talents)
        {
            if (AuxPrefabManager is null)
            {
                throw new NullReferenceException("aux data is empty.");
            }
            Name = AuxPrefabManager.GetData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash)!.Single();
            Color = int.Parse(AuxPrefabManager.GetData(AuxHash.PetGroupHash, AuxHash.PetColorHash)!.Single());

            Talents = AuxPrefabManager.GetData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash)!
                .Select(str => new PetTalent(str))
                .ToList();
        }
    }
}
