using CKFoodMaker;

namespace CKFoodMakerTest
{
    [TestClass]
    public class CKFoodMakerTest
    {
        [TestMethod]
        public void VariationAlgorithmTest()
        {
            // �n�[�g�x���[ + ���e�s�[�}��
            string calcResult = Form1.CalcrateVariation(8003, 8009).ToString();
            Assert.AreEqual(524885827.ToString(), calcResult);

            // �n�[�g�x���[ + ���e�s�[�}�� �tver
            calcResult = Form1.CalcrateVariation(8009, 8003).ToString();
            Assert.AreEqual(524885827.ToString(), calcResult);

            // ���F�̔����`���[���b�v + �X�s���b�g�x�[��
            calcResult = Form1.CalcrateVariation(8101, 9717).ToString();
            Assert.AreEqual(636821413.ToString(), calcResult);

            // �~�̃N�b�L�[ + �C�r���A�C�L�����f�B�[
            calcResult = Form1.CalcrateVariation(9613, 9608).ToString();
            Assert.AreEqual(630007176.ToString(), calcResult);
        }
    }
}
