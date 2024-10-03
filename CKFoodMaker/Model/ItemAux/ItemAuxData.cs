using CKFoodMaker.Resource;
using System.Text.Json.Nodes;

namespace CKFoodMaker.Model.ItemAux
{
    public record ItemAuxData
    {
        public AuxPrefabManager? AuxPrefabManager = null;

        #region Property
        public int index { get; set; }
        public string data 
        {
            get 
            {
                if (AuxPrefabManager is null)
                {
                    return "";
                }
                return AuxPrefabManager.ToInnerJsonString(StaticResource.SerializerOption);
            }
            set { }
        }
        #endregion

        public static readonly ItemAuxData Default = new(0,"");

        public ItemAuxData(int index, string data)
        {
            this.index = index;
            this.data = data;
            if (data != "")
            {
                AuxPrefabManager = new(JsonNode.Parse(data)!.AsObject());
            }
        }

        public void GetPetData(out string Name, out int Color, out List<PetTalent> Talents)
        {
            if (AuxPrefabManager is null)
            {
                throw new NullReferenceException("aux data is empty.");
            }
            Color = int.Parse(AuxPrefabManager.GetData(AuxHash.PetGroupHash, AuxHash.PetColorHash).Single());
            Name = AuxPrefabManager.GetData(AuxHash.PetNameGroupHash, AuxHash.PetNameHash).Single(); ;
            Talents = AuxPrefabManager.GetData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash)
                .Select(str => new PetTalent(str))
                .ToList();
        }
    }
}
