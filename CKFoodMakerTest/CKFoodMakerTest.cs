using CKFoodMaker;

namespace CKFoodMakerTest
{
    [TestClass]
    public class CKFoodMakerTest
    {
        [TestMethod]
        public void VariationAlgorithmTest()
        {
            // ���F�̔����`���[���b�v + �X�s���b�g�x�[��
            string calcResult = Form1.CalcrateVariation(8101, 9717).ToString();
            Assert.AreEqual(530916853.ToString(), calcResult);

            // �~�̃N�b�L�[ + �C�r���A�C�L�����f�B�[
            calcResult = Form1.CalcrateVariation(9613, 9608).ToString();
            Assert.AreEqual(630007176.ToString(), calcResult);
        }
    }
}
