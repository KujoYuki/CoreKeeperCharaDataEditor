using CKFoodMaker;

namespace CKFoodMakerTest
{
    [TestClass]
    public class CKFoodMakerTest
    {
        [TestMethod]
        public void VariationAlgorithmTest()
        {
            // ハートベリー + 爆弾ピーマン
            string calcResult = Form1.CalcrateVariation(8003, 8009).ToString();
            Assert.AreEqual(524885827.ToString(), calcResult);

            // ハートベリー + 爆弾ピーマン 逆ver
            calcResult = Form1.CalcrateVariation(8009, 8003).ToString();
            Assert.AreEqual(524885827.ToString(), calcResult);

            // 金色の発光チューリップ + スピリットベール
            calcResult = Form1.CalcrateVariation(8101, 9717).ToString();
            Assert.AreEqual(636821413.ToString(), calcResult);

            // 冬のクッキー + イビルアイキャンディー
            calcResult = Form1.CalcrateVariation(9613, 9608).ToString();
            Assert.AreEqual(630007176.ToString(), calcResult);
        }
    }
}
