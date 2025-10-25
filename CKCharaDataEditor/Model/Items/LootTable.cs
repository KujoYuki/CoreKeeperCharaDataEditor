using System.Text.Json.Serialization;

namespace CKCharaDataEditor.Model.Items
{
    /// <summary>
    /// Core Keeper の戦利品テーブル設定を表すクラス
    /// </summary>
    public record LootTable
    {
        /// <summary>
        /// 戦利品テーブルのID
        /// </summary>
        [JsonPropertyName("lootTableID")]
        public int LootTableID { get; set; }

        /// <summary>
        /// ユニークドロップの設定
        /// </summary>
        [JsonPropertyName("uniqueDrops")]
        public DropRange UniqueDrops { get; set; }

        /// <summary>
        /// 重複を許可しないかどうか
        /// </summary>
        [JsonPropertyName("dontAllowDuplicates")]
        public bool DontAllowDuplicates { get; set; }

        /// <summary>
        /// 戦利品のリスト
        /// </summary>
        [JsonPropertyName("loots")]
        public List<LootItem> Loots { get; set; } = new List<LootItem>();

        /// <summary>
        /// 保証抽選テーブル内でのドロップ確率を計算する
        /// </summary>
        public void ComputeGuaranteedRoll()
        {
            // ドロップ確率の計算
            double totalWeight = Loots
                .Where(item => item.IsOneOfGuaranteedToDrop)
                .Sum(item => item.Weight);
            foreach (LootItem item in Loots.Where(item => item.IsOneOfGuaranteedToDrop))
            {
                item.GuaranteedRollPerDrop = item.Weight / totalWeight;
            }
        }

        /// <summary>
        /// 一般抽選テーブル内でのドロップ確率を計算する
        /// </summary>
        public void ComputeRoll()
        {
            double totalWeight = Loots.Sum(item => item.Weight);
            foreach (LootItem item in Loots)
            {
                item.RollPerDrop = item.Weight / totalWeight;
            }
        }
    }

    /// <summary>
    /// ドロップ範囲を表すクラス
    /// </summary>
    public record DropRange
    {
        [JsonIgnore]
        const int MaxDropLimit = 36;
        [JsonIgnore]
        static readonly string MaxDropLimitString = MaxDropLimit.ToString();

        /// <summary>
        /// 最小値
        /// </summary>
        [JsonPropertyName("min")]
        public int Min { get; set; }

        /// <summary>
        /// 最大値
        /// </summary>
        [JsonPropertyName("max")]
        public int Max { get; set; }

        /// <summary>
        /// 最小値と最大値が等しい（固定値の範囲）かどうか
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        public bool IsSingleValue => Min == Max;

        public override string ToString()
        {
            if (IsSingleValue)
                return Min > MaxDropLimit ? MaxDropLimitString : Min.ToString();
            else
            {
                string max = Max > MaxDropLimit ? MaxDropLimitString : Max.ToString();
                string min = Min > MaxDropLimit ? MaxDropLimitString : Min.ToString();
                if (max == MaxDropLimitString && min == MaxDropLimitString)
                    return MaxDropLimitString;
                return $"{Min} - {Max}";
            }
        }

        public double GetAverage()
        {
            double average = (Min + Max) / 2.0;
            if (average > MaxDropLimit) average = MaxDropLimit;
            return average;
        }

        public DropRange Copy()
        {
            return new DropRange
            {
                Min = this.Min,
                Max = this.Max,
            };
        }

        /// <summary>
        /// 指定した倍率で範囲をスケールした新しい DropRange を返します。
        /// 小数はJIS丸めする。
        /// </summary>
        /// <param name="range">元の範囲（null は許容しません）</param>
        /// <param name="multiplier">倍率（double）</param>
        /// <returns>スケールされた新しい DropRange</returns>
        public static DropRange operator *(DropRange range, double multiplier)
        {
            if (range is null) throw new ArgumentNullException(nameof(range));
            // 四捨五入して整数に変換。負値は 0 にクランプする。
            int newMin = (int)Math.Round(range.Min * multiplier);
            int newMax = (int)Math.Round(range.Max * multiplier);
            return new DropRange { Min = Math.Min(newMin,36), Max = Math.Min(newMax,36) };
        }
    }

    /// <summary>
    /// 戦利品アイテムを表すクラス
    /// </summary>
    public record LootItem
    {
        /// <summary>
        /// オブジェクトID
        /// </summary>
        [JsonPropertyName("objectID")]
        public int ObjectID { get; set; }

        /// <summary>
        /// ドロップの重み（確率）
        /// </summary>
        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// ドロップ数の範囲
        /// </summary>
        [JsonPropertyName("amount")]
        public DropRange Amount { get; set; }

        /// <summary>
        /// 保証ドロップグループの一つかどうか
        /// </summary>
        [JsonPropertyName("isOneOfGuaranteedToDrop")]
        public bool IsOneOfGuaranteedToDrop { get; set; }

        /// <summary>
        /// 特定のバイオームでのみドロップするか
        /// </summary>
        [JsonPropertyName("onlyDropsInBiome")]
        public int OnlyDropsInBiome { get; set; }

        /// <summary>
        /// 保証抽選枠ドロップ率
        /// </summary>
        [JsonIgnore]
        public double GuaranteedRollPerDrop { get; set; } = 0;

        /// <summary>
        /// 一般抽選枠ドロップ率
        /// </summary>
        [JsonIgnore]
        public double RollPerDrop { get; set; } // 表示用に計算されたドロップ確率
    }
}
