using System.Text.Json;
using System.Text.Json.Serialization;

namespace CKCharaDataEditor.Model.Pet
{
    public record PetTalent
    {
        public int Talent { get; set; } = 0;
        public int Points { get; set; } = 0;

        public static PetTalent Default => new(0, 0);

        [JsonConstructor]
        public PetTalent(int talent, int points)
        {
            Talent = talent;
            Points = points;
        }

        public PetTalent(string combinedJson)
        {
            var deserializedTalent = JsonSerializer.Deserialize<PetTalent>(combinedJson);
            if (deserializedTalent is null)
            {
                throw new ArgumentException("Invalid PetTalentFormat");
            }
            Talent = deserializedTalent.Talent;
            Points = deserializedTalent.Points;
        }

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
