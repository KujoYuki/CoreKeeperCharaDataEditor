using CKFoodMaker;
using CKFoodMaker.Model;
using CKFoodMaker.Model.ItemAux;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CKFoodMakerTest
{
    [TestClass]
    public class CKFoodMakerTest
    {
        const string LawPetData = "{\"prefabs\":[{\"prefabHash\":1145011307,\"types\":[{\"stableTypeHash\":13695103918181693450,\"data\":[\"0\"]},{\"stableTypeHash\":16038764625220822319,\"data\":[\"{\\\"Talent\\\":2,\\\"Points\\\":0}\",\"{\\\"Talent\\\":3,\\\"Points\\\":0}\",\"{\\\"Talent\\\":17,\\\"Points\\\":1}\",\"{\\\"Talent\\\":0,\\\"Points\\\":0}\",\"{\\\"Talent\\\":3,\\\"Points\\\":1}\",\"{\\\"Talent\\\":16,\\\"Points\\\":1}\",\"{\\\"Talent\\\":16,\\\"Points\\\":1}\",\"{\\\"Talent\\\":3,\\\"Points\\\":1}\",\"{\\\"Talent\\\":3,\\\"Points\\\":0}\"]}]},{\"prefabHash\":2811915185,\"types\":[{\"stableTypeHash\":9923282613123898873,\"data\":[\"BlueSlimePet\"]}]}]}";

        [TestMethod]
        public void VariationAlgorithmTest()
        {
            // �n�[�g�x���[ + ���e�s�[�}��
            string calcResult = Form1.CalculateVariation(8003, 8009).ToString();
            Assert.AreEqual(524885827.ToString(), calcResult);

            // �n�[�g�x���[ + ���e�s�[�}�� �tver
            calcResult = Form1.CalculateVariation(8009, 8003).ToString();
            Assert.AreEqual(524885827.ToString(), calcResult);

            // ���F�̔����`���[���b�v + �X�s���b�g�x�[��
            calcResult = Form1.CalculateVariation(8101, 9717).ToString();
            Assert.AreEqual(636821413.ToString(), calcResult);

            // �~�̃N�b�L�[ + �C�r���A�C�L�����f�B�[
            calcResult = Form1.CalculateVariation(9613, 9608).ToString();
            Assert.AreEqual(630007176.ToString(), calcResult);
        }

        [TestMethod]
        public void ReadPetDataTest()
        {
            // �X�L����������d�G�X�P�[�v����Ă�̂𐮌`
            string petData = Regex.Unescape(LawPetData);
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Unescaped.json"), petData);
            petData = petData.Replace("\"{\"", "{\"").Replace("}\"", "}");
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Formated.json"), petData);

            AuxPrefabManager manager = new(JsonNode.Parse(LawPetData)!.AsObject());
            var color = int.Parse(manager.GetData(AuxHash.PetGroupHash, AuxHash.PetColorHash).Single());
            var name = manager.GetData(AuxHash.PetNameGroupHash, AuxHash.PetNameHash).Single(); ;
            var talents = manager.GetData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash)
                .Select(str => new PetTalent(str))
                .ToList();
            Assert.AreEqual(9, talents.Count);
            Assert.AreEqual(5, talents.Count(t => t.Points == 1));
        }

        [TestMethod]
        public void AssemblePetDataTest()
        {
            var aux = new ItemAuxData(10, LawPetData);  //index�͔C�ӂ̒l
            aux.GetPetData(out string name, out int color, out var Talents);

            var manager = new AuxPrefabManager();
            manager.AddPrefab(new AuxPrefab(AuxHash.PetGroupHash, new List<AuxStableType>()));
            manager.AddPrefab(new AuxPrefab(AuxHash.PetNameGroupHash, new List<AuxStableType>()));
            var petColor = new AuxStableType(AuxHash.PetColorHash, new[] { color.ToString() });
            var petTalent = new AuxStableType(AuxHash.PetTalentsHash, Talents.Select(t => t.ToJsonString()));
            var petName = new AuxStableType(AuxHash.PetNameHash, new[] { name });
            manager.AddStableType(AuxHash.PetGroupHash, petColor);
            manager.AddStableType(AuxHash.PetGroupHash, petTalent);
            manager.AddStableType(AuxHash.PetNameGroupHash, petName);
            
            JsonSerializerOptions serializerOptionWrite = new()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            string assemblePetData = manager.ToInnerJsonString(serializerOptionWrite);

            Assert.AreEqual(LawPetData, assemblePetData);
        }
    }
}
