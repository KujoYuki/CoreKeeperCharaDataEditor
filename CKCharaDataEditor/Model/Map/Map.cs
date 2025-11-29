using System.Text.Json.Serialization;

namespace CKCharaDataEditor.Model.Map
{
    /// <summary>
    /// マップパーツの座標を表すクラス
    /// </summary>
    public class MapPartKey
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    /// <summary>
    /// マップパーツの画像データを表すクラス
    /// </summary>
    public class MapPartValue
    {
        [JsonPropertyName("png")]
        public int[]? Png { get; set; }

        [JsonPropertyName("timestampPng")]
        public int[]? TimestampPng { get; set; }

        /// <summary>
        /// PNG画像データをバイト配列として取得
        /// </summary>
        public byte[] GetPngBytes()
        {
            if (Png == null) return Array.Empty<byte>();

            byte[] result = new byte[Png.Length];
            for (int i = 0; i < Png.Length; i++)
            {
                result[i] = (byte)Png[i];
            }
            return result;
        }

        /// <summary>
        /// タイムスタンプPNG画像データをバイト配列として取得
        /// </summary>
        public byte[] GetTimestampPngBytes()
        {
            if (TimestampPng == null) return Array.Empty<byte>();

            byte[] result = new byte[TimestampPng.Length];
            for (int i = 0; i < TimestampPng.Length; i++)
            {
                result[i] = (byte)TimestampPng[i];
            }
            return result;
        }
    }

    /// <summary>
    /// マップパーツのコレクションを表すクラス
    /// </summary>
    public class MapParts
    {
        [JsonPropertyName("keys")]
        public List<MapPartKey> Keys { get; set; } = new List<MapPartKey>();

        [JsonPropertyName("values")]
        public List<MapPartValue> Values { get; set; } = new List<MapPartValue>();

        /// <summary>
        /// マップパーツの数を取得
        /// </summary>
        public int Count => Math.Min(Keys.Count, Values.Count);

        /// <summary>
        /// 指定したインデックスのマップパーツを取得
        /// </summary>
        public (MapPartKey Key, MapPartValue Value) GetMapPart(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return (Keys[index], Values[index]);
        }
    }

    /// <summary>
    /// Core Keeperマップデータのルートクラス
    /// </summary>
    public class CoreKeeperMapData
    {
        [JsonPropertyName("mapParts")]
        public MapParts MapParts { get; set; } = new MapParts();
    }
}
