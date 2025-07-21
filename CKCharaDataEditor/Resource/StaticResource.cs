using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CKCharaDataEditor.Resource
{
    public static class StaticResource
    {
        public static readonly JsonSerializerOptions SerializerOption = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        };

        /// <summary>
        /// 固有スロット名と色の辞書。
        /// </summary>
        public static IReadOnlyDictionary<int, (string Segment, Brush Color)> ExtendSlotName
            = new Dictionary<int, (string Segment, Brush Color)>
            {
                {51, ("Cursor", Brushes.Gray)},
                {52, ("Set1,Helm", Brushes.LawnGreen)},
                {53, ("Set1,Necklace", Brushes.LawnGreen)},
                {54, ("Set1,Breast", Brushes.LawnGreen)},
                {55, ("Set1,Pants", Brushes.LawnGreen)},
                {56, ("Set1,RingA", Brushes.LawnGreen)},
                {57, ("Set1,RingB", Brushes.LawnGreen)},
                {58, ("Set1,OffHand", Brushes.LawnGreen)},
                {59, ("Set1,Bag", Brushes.LawnGreen)},
                {60, ("Sell,Slot1", Brushes.Gray)},
                {61, ("Sell,Slot2", Brushes.Gray)},
                {62, ("Sell,Slot3", Brushes.Gray)},
                {63, ("Sell,Slot4", Brushes.Gray)},
                {64, ("Sell,Slot5", Brushes.Gray)},
                {65, ("Sell,Slot6", Brushes.Gray)},
                {66, ("TrashCan", Brushes.Gray)},
                {67, ("Dresser,Helm", Brushes.Peru)},
                {68, ("Dresser,Breast", Brushes.Peru)},
                {69, ("Dresser,Pants", Brushes.Peru)},
                {70, ("Set2,Helm", Brushes.SkyBlue)},
                {71, ("Set2,Necklace", Brushes.SkyBlue)},
                {72, ("Set2,Breast", Brushes.SkyBlue)},
                {73, ("Set2,Pants", Brushes.SkyBlue)},
                {74, ("Set2,RingA", Brushes.SkyBlue)},
                {75, ("Set2,RingB", Brushes.SkyBlue)},
                {76, ("Set2,OffHand", Brushes.SkyBlue)},
                {77, ("Set3,Helm", Brushes.LightCoral)},
                {78, ("Set3,Necklace", Brushes.LightCoral)},
                {79, ("Set3,Breast", Brushes.LightCoral)},
                {80, ("Set3,Pants", Brushes.LightCoral)},
                {81, ("Set3,RingA", Brushes.LightCoral)},
                {82, ("Set3,RingB", Brushes.LightCoral)},
                {83, ("Set3,OffHand", Brushes.LightCoral)},
                {84, ("Pet", Brushes.BurlyWood)},
                {85, ("Lantan", Brushes.BurlyWood)},
                {86, ("Upgrade", Brushes.Gray)},
                {87, ("Pouch_A", Brushes.BurlyWood)},
                {98, ("Pouch_B", Brushes.BurlyWood)},
                {109, ("Pouch_C", Brushes.BurlyWood)},
                {120, ("Pouch_D", Brushes.BurlyWood)},
            };

        /// <summary>
        /// TextBoxのTextが64バイトを超えていたら末尾から削除して64バイト以内に収める
        /// </summary>
        public static void SanitizeTextBoxText(TextBox textBox)
        {
            string text = textBox.Text;
            while (Encoding.UTF8.GetByteCount(text) > 64)
            {
                text = text[..^1];
            }
            if (textBox.Text != text)
            {
                textBox.Text = text;
                textBox.SelectionStart = text.Length; // キャレット位置を末尾に設定
            }
        }
    }
}
