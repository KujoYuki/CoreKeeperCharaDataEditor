using CKCharaDataEditor.Resource;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.Json;

namespace CKCharaDataEditor.Model.Map
{
    /// <summary>
    /// Core Keeperマップの復元と合成を行うクラス
    /// </summary>
    public class MapRestorer
    {
        /// <summary>
        /// JSONファイルからマップデータを読み込む
        /// </summary>
        /// <param name="jsonFilePath">JSONファイルのパス</param>
        /// <returns>マップデータ</returns>
        public static CoreKeeperMapData LoadFromJsonAsync(string jsonContent)   //hack mapの読み込み場所で非同期処理を使う
        {
            try
            {
                var mapData = JsonSerializer.Deserialize<CoreKeeperMapData>(jsonContent, StaticResource.SerializerOption);
                if (mapData == null)
                    throw new InvalidOperationException("JSONデータの解析に失敗しました");

                Console.WriteLine($"マップデータを読み込みました: {mapData.MapParts.Count}個のパーツ");
                return mapData;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"JSONファイルの解析エラー: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// バイト配列からBitmapを作成する
        /// </summary>
        /// <param name="pngBytes">PNGデータのバイト配列</param>
        /// <returns>Bitmap画像</returns>
        public static Bitmap CreateBitmapFromBytes(byte[] pngBytes)
        {
            if (pngBytes == null || pngBytes.Length == 0)
                throw new ArgumentException("PNGデータが空です");

            try
            {
                using (var memoryStream = new MemoryStream(pngBytes))
                {
                    return new Bitmap(memoryStream);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"PNG画像の復元に失敗しました: {ex.Message}", ex);
            }
        }

        public static DateTime[] GetTimeStampsFromBytes(byte[] pngBytes)
        {
            using var memoryStream = new MemoryStream(pngBytes);
            using var bitmap = new Bitmap(memoryStream);

            int width = bitmap.Width;
            int height = bitmap.Height;
            int[] unixAddTime = new int[width * height];

            var rect = new Rectangle(0, 0, width, height);
            var bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            try
            {
                unsafe
                {
                    byte* ptr = (byte*)bmpData.Scan0;
                    int stride = bmpData.Stride;
                    for (int y = 0; y < height; y++)
                    {
                        byte* row = ptr + y * stride;
                        for (int x = 0; x < width; x++)
                        {
                            // RGBA順に取得し、各要素のビットを逆転
                            byte b = row[x * 4 + 0];
                            byte g = row[x * 4 + 1];
                            byte r = row[x * 4 + 2];
                            byte a = row[x * 4 + 3];
                            int value =
                                (a << 24) |
                                (r << 16) |
                                (g << 8) |
                                (b);
                            if (value is not 0)
                            {
                                Debug.WriteLine($" {Convert.ToString(value, 2).PadLeft(32, '0')}");
                            }
                            unixAddTime[y * width + x] = value;
                        }
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(bmpData);
            }

            // Unixエポックからの現在時刻までの秒数を参考で取得
            uint elapsedSeconds = (uint)(DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;
            Debug.WriteLine($"現在のUnixタイムスタンプ: {elapsedSeconds}");
            Debug.WriteLine($"現在のUnixタイムスタンプ(バイナリ): {Convert.ToString(elapsedSeconds, 2).PadLeft(32, '0')}");

            DateTime[] resultTimes = new DateTime[unixAddTime.Length];
            for (int i = 0; i < unixAddTime.Length; i++)
            {
                resultTimes[i] = DateTime.UnixEpoch.AddSeconds(unixAddTime[i]);
            }
            Debug.WriteLine($"タイムスタンプ(バイナリ): {Convert.ToString(unixAddTime[0], 2).PadLeft(32, '0')}");
            Debug.WriteLine($"未探査タイムスタンプ(バイナリ): {Convert.ToString(unixAddTime[128], 2).PadLeft(32, '0')}"); //未探査マップは全て0になっているはず？

            //デバッグ用に2038年問題をチェック
            Debug.WriteLine($"\nint.Max = {int.MaxValue}");
            Debug.WriteLine($"2038年問題の閾値: {DateTime.UnixEpoch.AddSeconds(int.MaxValue)}");
            Debug.WriteLine($"int.Maxを32bitでバイナリ表示: {Convert.ToString(int.MaxValue, 2).PadLeft(32, '0')}");

            return resultTimes;
        }

        /// <summary>
        /// マップパーツからBitmapのリストを作成する
        /// </summary>
        /// <param name="mapData">マップデータ</param>
        /// <param name="useTimestamp">タイムスタンプ画像を使用するかどうか</param>
        /// <returns>座標とBitmapのペアのリスト</returns>
        public static List<(MapPartKey Key, Bitmap Image)> CreateBitmapsFromMapData(
            CoreKeeperMapData mapData,
            bool useTimestamp = false)
        {
            var bitmaps = new List<(MapPartKey, Bitmap)>();

            for (int i = 0; i < mapData.MapParts.Count; i++)
            {
                try
                {
                    var (key, value) = mapData.MapParts.GetMapPart(i);

                    // 使用する画像データを選択
                    byte[] pngBytes = useTimestamp ? value.GetTimestampPngBytes() : value.GetPngBytes();

                    if (pngBytes.Length == 0)
                    {
                        Console.WriteLine($"パーツ {i} ({key}) にはPNGデータがありません");
                        continue;
                    }

                    var bitmap = CreateBitmapFromBytes(pngBytes);

                    // デバッグ用にピクセルごとのタイムスタンプを確認
                    //var timeStamps = GetTimeStampsFromBytes(pngBytes);

                    // デバッグ用にキーを描画
                    //DrawKeyOnBitmap(bitmap, key);

                    bitmaps.Add((key, bitmap));

                    Console.WriteLine($"パーツ {i}: {key} - {bitmap.Width}x{bitmap.Height}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"パーツ {i} の読み込みに失敗: {ex.Message}");
                }
            }

            Console.WriteLine($"{bitmaps.Count}個のパーツを正常に読み込みました");
            return bitmaps;
        }

        /// <summary>
        /// Bitmap画像にkeyを(x, y)形式で描画する（デバッグ用）
        /// </summary>
        /// <param name="bitmap">描画対象のBitmap</param>
        /// <param name="key">描画するキー</param>
        /// <param name="font">使用するフォント（省略可）</param>
        /// <param name="color">文字色（省略可）</param>
        public static void DrawKeyOnBitmap(Bitmap bitmap, MapPartKey key, Font? font = null, Color? color = null)
        {
            if (bitmap == null || key == null) return;

            using var g = Graphics.FromImage(bitmap);
            font ??= new Font("Consolas", 48, FontStyle.Bold, GraphicsUnit.Pixel);
            color ??= Color.Red;

            // 1. 薄い白色の枠線を描画（α=128, 太さ2px）
            using (var pen = new Pen(Color.FromArgb(128, 255, 255, 255), 2))
            {
                // 枠線がBitmapの外にはみ出さないように-1
                g.DrawRectangle(pen, 1, 1, bitmap.Width - 3, bitmap.Height - 3);
            }

            string text = $"({key.X}, {key.Y})";
            var textPosition = new PointF(32, 96); // 左上に描画（必要に応じて調整）

            var brush = new SolidBrush(color.Value);
            var outlineBrush = new SolidBrush(Color.White);

            // 簡易的な縁取り
            g.DrawString(text, font, outlineBrush, textPosition.X + 1, textPosition.Y + 1);
            g.DrawString(text, font, brush, textPosition);
        }

        /// <summary>
        /// マップの境界を計算する
        /// </summary>
        /// <param name="bitmaps">マップパーツのリスト</param>
        /// <returns>最小座標と最大座標</returns>
        public static (Point Min, Point Max, Size TileSize) CalculateMapBounds(
            List<(MapPartKey Key, Bitmap Image)> bitmaps)
        {
            if (!bitmaps.Any())
                throw new ArgumentException("マップパーツが空です");

            int minX = bitmaps.Min(b => b.Key.X);
            int maxX = bitmaps.Max(b => b.Key.X);
            int minY = bitmaps.Min(b => b.Key.Y);
            int maxY = bitmaps.Max(b => b.Key.Y);

            // 最初のタイルからサイズを取得
            var firstTile = bitmaps.First().Image;
            var tileSize = new Size(firstTile.Width, firstTile.Height);

            return (new Point(minX, minY), new Point(maxX, maxY), tileSize);
        }

        /// <summary>
        /// マップパーツを合成して完全なマップ画像を作成する
        /// </summary>
        /// <param name="bitmaps">マップパーツのリスト</param>
        /// <returns>合成されたマップ画像</returns>
        public static Bitmap ComposeMap(List<(MapPartKey Key, Bitmap Image)> bitmaps)
        {
            var (minPoint, maxPoint, tileSize) = CalculateMapBounds(bitmaps);

            // 合成画像のサイズを計算
            int mapWidth = (maxPoint.X - minPoint.X + 1) * tileSize.Width;
            int mapHeight = (maxPoint.Y - minPoint.Y + 1) * tileSize.Height;

            Debug.WriteLine($"合成マップサイズ: {mapWidth}x{mapHeight}");
            Debug.WriteLine($"座標範囲: X({minPoint.X} ～ {maxPoint.X}), Y({minPoint.Y} ～ {maxPoint.Y})");
            Debug.WriteLine($"タイルサイズ: {tileSize.Width}x{tileSize.Height}");

            // 合成用のBitmapを作成
            var composedMap = new Bitmap(mapWidth, mapHeight, PixelFormat.Format32bppArgb);

            using (var graphics = Graphics.FromImage(composedMap))
            {
                // 背景を透明に設定
                graphics.Clear(Color.Transparent);

                // ピクセル向けの描画設定
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                // 各マップパーツを適切な位置に描画
                foreach (var (key, image) in bitmaps.OrderBy(kv => kv.Key.X).ThenBy(kv => kv.Key.Y))
                {
                    int x = (key.X - minPoint.X) * tileSize.Width;
                    int y = (maxPoint.Y - key.Y) * tileSize.Height;

                    graphics.DrawImage(image, x, y, tileSize.Width, tileSize.Height);
                    Debug.WriteLine($"パーツ {key} を位置 ({x / tileSize.Width}, {y / tileSize.Height}) に描画");
                }
            }

            return composedMap;
        }

        /// <summary>
        /// Bitmapリソースを解放する
        /// </summary>
        /// <param name="bitmaps">解放するBitmapのリスト</param>
        public static void DisposeBitmaps(List<(MapPartKey Key, Bitmap Image)> bitmaps)
        {
            foreach (var (_, image) in bitmaps)
            {
                image?.Dispose();
            }
        }

        /// <summary>
        /// 合成されたマップをPNGファイルとして保存する
        /// </summary>
        /// <param name="bitmap">保存するBitmap</param>
        /// <param name="outputPath">出力ファイルパス</param>
        public static void SaveMapAsPng(Bitmap bitmap, string outputPath)
        {
            try
            {
                // 出力ディレクトリが存在しない場合は作成
                string? directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                bitmap.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"マップを保存しました: {outputPath}");

                // ファイル情報を表示
                var fileInfo = new FileInfo(outputPath);
                Console.WriteLine($"ファイルサイズ: {fileInfo.Length / 1024.0 / 1024.0:F2} MB");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"ファイルの保存に失敗しました: {ex.Message}", ex);
            }
        }
    }
}
