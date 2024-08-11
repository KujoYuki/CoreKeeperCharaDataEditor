using CKFoodMaker;

namespace CKFoodMakerTest
{
    [TestClass]
    public class CKFoodMakerTest
    {
        [TestMethod]
        public void VariationAlgorithmTest()
        {
            // 金色の発光チューリップ + スピリットベール
            string calcResult = Form1.CalcrateVariation(8101, 9717).ToString();
            Assert.AreEqual(530916853.ToString(), calcResult);

            // 冬のクッキー + イビルアイキャンディー
            calcResult = Form1.CalcrateVariation(9613, 9608).ToString();
            Assert.AreEqual(630007176.ToString(), calcResult);
        }
    }
}
