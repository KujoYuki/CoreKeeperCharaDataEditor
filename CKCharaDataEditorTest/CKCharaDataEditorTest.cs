using CKCharaDataEditor;
using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Pet;
using System.Text.Json.Nodes;

namespace CKCharaDataEditorTest
{
    [TestClass]
    public class CKCharaDataEditorTest
    {
        const string LawPetData = "{\"prefabs\":[{\"prefabHash\":1145011307,\"types\":[{\"stableTypeHash\":13695103918181693450,\"data\":[\"0\"]},{\"stableTypeHash\":16038764625220822319,\"data\":[\"{\\\"Talent\\\":2,\\\"Points\\\":0}\",\"{\\\"Talent\\\":3,\\\"Points\\\":0}\",\"{\\\"Talent\\\":17,\\\"Points\\\":1}\",\"{\\\"Talent\\\":0,\\\"Points\\\":0}\",\"{\\\"Talent\\\":3,\\\"Points\\\":1}\",\"{\\\"Talent\\\":16,\\\"Points\\\":1}\",\"{\\\"Talent\\\":16,\\\"Points\\\":1}\",\"{\\\"Talent\\\":3,\\\"Points\\\":1}\",\"{\\\"Talent\\\":3,\\\"Points\\\":0}\"]}]},{\"prefabHash\":2811915185,\"types\":[{\"stableTypeHash\":9923282613123898873,\"data\":[\"BlueSlimePet\"]}]}]}";

        [TestMethod]
        public void VariationAlgorithmTest()
        {
            // �n�[�g�x���[ + ���e�s�[�}��
            int calcResult = CKCharaDataEditor.Model.Food.Recipe.CalculateVariation(8003, 8009);
            Assert.AreEqual(524885827, calcResult);

            // �n�[�g�x���[ + ���e�s�[�}�� �tver
            calcResult = CKCharaDataEditor.Model.Food.Recipe.CalculateVariation(8009, 8003);
            Assert.AreEqual(524885827, calcResult);

            // ���F�̔����`���[���b�v + �X�s���b�g�x�[��
            calcResult = CKCharaDataEditor.Model.Food.Recipe.CalculateVariation(8101, 9717);
            Assert.AreEqual(636821413, calcResult);

            // �~�̃N�b�L�[ + �C�r���A�C�L�����f�B�[
            calcResult = CKCharaDataEditor.Model.Food.Recipe.CalculateVariation(9613, 9608);
            Assert.AreEqual(630007176, calcResult);
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
            var name = manager.GetData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash).Single(); ;
            var talents = manager.GetData(AuxHash.PetGroupHash, AuxHash.PetTalentsHash)
                .Select(str => new PetTalent(str))
                .ToList();
            Assert.AreEqual(9, talents.Count);
            Assert.AreEqual(5, talents.Count(t => t.Points == 1));
        }
    }
}
