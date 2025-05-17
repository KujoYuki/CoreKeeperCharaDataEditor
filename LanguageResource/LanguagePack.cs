using System.Text;

namespace LanguageResource
{
    /// <summary>
    /// Core KeeperのローカライズデータをTSV形式で出力するプログラム
    /// </summary>
    public class LanguagePack
    {
        /// <summary>
        /// 動作確認用に単体でリソースを合成し、tsvファイルにします。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string installPath = @"G:\SteamLibrary\steamapps\common\Core Keeper";

            var dic = CreateLanguageDictionary(installPath);
            var outputNumericLines = dic.Select(pair => $"{pair.Key}\t{pair.Value[0]}\t{pair.Value[1]}").ToList();

            // 新規TSVファイルに出力
            string outputPath = Path.Combine(Environment.CurrentDirectory, "LanguageResource.tsv");
            
            //依存パッケージが増えるのでShift-JISは使わないことにする
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // JISエンコードを追加 
            //File.WriteAllLines(outputPath, outputNumericLines, Encoding.GetEncoding("Shift-JIS"));
            
            File.WriteAllLines(outputPath, outputNumericLines, Encoding.UTF8);
            Console.WriteLine($"TSVファイルが出力されました: {outputPath}");
        }

        /// <summary>
        /// 日本語のリソースを取得します。
        /// </summary>
        /// <param name="installPath"></param>
        /// <returns>KeyはobjectId, Valueは[0]でobjectName, [1]で日本語表示名</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static IReadOnlyDictionary<string, string[]> CreateLanguageDictionary(string installPath)
        {
            string localizationPath = Path.Combine(installPath, @"localization\Localization Template.csv");
            string objectIdPath = Path.Combine(installPath, @"CoreKeeper_Data\StreamingAssets\Conf\ID\ObjectID.json");

            if (!File.Exists(localizationPath) || !File.Exists(objectIdPath))
            {
                // hack 例外をロガーに投げる
                //string errorMessage = $"インストールフォルダ内の指定されたファイルが見つかりません。\n" +
                //    $"パスを確認するかゲームを再インストールしてください。";
                
                // 空の辞書を返す
                return new Dictionary<string, string[]>();
            }

            var objectIdDic = File.ReadAllText(objectIdPath).Trim()
                .Replace("{", string.Empty).Replace("}", string.Empty).Replace(":", ",").Replace("\"", string.Empty).Replace("\t", string.Empty)
                .Split("\n")
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Split(","))
                .Skip(1) // Noneをスキップ
                .ToDictionary(line => line[0], line => int.Parse(line[1]));

            var trancelation = File.ReadAllLines(localizationPath)
                .Skip(1) // ヘッダーをスキップ
                .Select(line => line.Split('\t'))
                .Where(words =>
                {
                    string key = words[0];
                    if (words.Length < 16 || !key.StartsWith("Items/") || key.EndsWith("Desc")) return false;
                    return true;
                })
                .Distinct()
                .Select(words =>
                {
                    string key = words[0].Replace("Items/", string.Empty).Replace("Desc", string.Empty);
                    string objectId = objectIdDic.TryGetValue(key, out int id) ? id.ToString() : $"{key}"; // IDが見つからない場合はKeyを使用
                    // 本来はobjectIdとvariationのタプルでキーを参照しに良くのが正しいが、開発チームの消し忘れリソースも多いの。
                    // それにvariation違いでkeyが変わるアイテムが少数過ぎるし、コアキ側の実装がEAの頃から中途半端に変わってるのが悪い。
                    string language = words[8]; // 8 is Japanese translation column
                    return new string[] { objectId, key, language };
                })
                .ToDictionary(words => words[0], words => new string[] { words[1], words[2] });

            var outputDic = trancelation
                .Where(line => int.TryParse(line.Key, out _))
                .OrderBy(line => int.Parse(line.Key))
                //.Concat(trancelation  // variation値込みで出力する必要があるものや、リソースの切れたKeyを見たい時だけ使う
                //    .Where(line => !int.TryParse(line.Key, out _))
                //    .OrderBy(line => line.Key))
                .ToDictionary();

            return outputDic;
        }
    }
}
