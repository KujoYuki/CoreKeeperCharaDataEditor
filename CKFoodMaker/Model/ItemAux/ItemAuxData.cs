using System.Text.Json.Nodes;

namespace CKFoodMaker.Model.ItemAux
{
    public record ItemAuxData
    {
        private JsonObject _formattedData = new();
        public AuxPrefabManager? auxPrefabManager = null;

        #region Property
        public int index { get; set; }
        public string data { get; set; }
        #endregion

        public static readonly ItemAuxData Default = new(0,"");

        public ItemAuxData(int index, string data)
        {
            this.index = index;
            this.data = data;
            if (data != "")
            {
                auxPrefabManager = new(JsonNode.Parse(data)!.AsObject());
            }
        }

        public void GetPetData(out string Name, out int Color, out List<PetTalent> Talents)
        {
            if (auxPrefabManager is null)
            {
                throw new NullReferenceException("aux data is empty.");
            }
            Color = int.Parse(auxPrefabManager.GetData(AuxHash.PetGroupHash, AuxHash.PetColorHash).Single());
            Name = auxPrefabManager.GetData(AuxHash.PetNameGroupHash, AuxHash.PetNameHash).Single(); ;
            Talents = auxPrefabManager.GetData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash)
                .Select(str => new PetTalent(str))
                .ToList();
        }
    }
}
