using System.Text;

namespace LanguageResource
{
    /// <summary>
    /// Core KeeperのローカライズデータをTSV形式で出力するプログラム
    /// </summary>
    public class LanguagePack
    {
        const string YukisInstallPath = @"G:\SteamLibrary\steamapps\common\Core Keeper";

        /// <summary>
        /// 動作確認用に単体でリソースを合成し、tsvファイルにします。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var dic = CreateLanguageDictionary(YukisInstallPath);
            var outputNumericLines = dic.Select(pair => $"{pair.Key}\t{pair.Value.key}\t{pair.Value.displayString}").ToList();

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
        public static IReadOnlyDictionary<int, (string key, string displayString)> CreateLanguageDictionary(string installPath)
        {
            string objectIdPath = Path.Combine(installPath, @"CoreKeeper_Data\StreamingAssets\Conf\ID\ObjectID.json");
            string localizationPath = Path.Combine(installPath, @"localization\Localization Template.csv");

            if (!File.Exists(localizationPath) || !File.Exists(objectIdPath))
            {
                // リソースファイルが見つからない場合は、空の辞書を返す
                return new Dictionary<int, (string, string)>();
            }

            Dictionary<string, int> objectIdDic = File.ReadAllText(objectIdPath).Trim()
                .Replace("{", string.Empty).Replace("}", string.Empty).Replace(":", ",").Replace("\"", string.Empty).Replace("\t", string.Empty)
                .Split("\n")
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Split(","))
                .Skip(1) // Noneをスキップ
                .ToDictionary(line => line[0], line => int.Parse(line[1]));

            List<string[]> languageResourceOrigin = File.ReadAllLines(localizationPath)
                .Skip(1)
                .Select(line => line.Split('\t'))
                .ToList(); // ヘッダーをスキップ

            var trancelation = FixLineFeed(languageResourceOrigin)
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
                    string displayString = words[8]; // 8 is Japanese translation column
                    return (objectId, (key, displayString));
                })
                .ToDictionary();

            // 例外的に、大人の事情でリソースが完全に使いまわされているアイテムを手動で追加する
            (string id, string key, string originResourceId)[] remakedItem = [("5502", "GiantMushroom2", "5501"), ("5503", "AmberLarva2", "5607")];
            foreach (var item in remakedItem)
            {
                string displayString = trancelation[item.originResourceId].displayString;
                trancelation.Add(item.id, (item.key, displayString));
            }

            var outputDic = trancelation
                .Where(line => int.TryParse(line.Key, out _))
                .OrderBy(line => int.Parse(line.Key))
                //.Concat(trancelation  // variation値込みで出力する必要があるものや、リソースの切れたKeyを見たい時だけ使う
                //    .Where(line => !int.TryParse(line.Key, out _))
                //    .OrderBy(line => line.Key))
                .ToDictionary(key => int.Parse(key.Key), value=> value.Value);

            return outputDic;
        }

        /// <summary>
        /// リソースファイル中の無意味な改行コードの混入を修正します。
        /// </summary>
        /// <param name="languageResources"></param>
        /// <returns></returns>
        private static List<string[]> FixLineFeed(List<string[]> languageResources)
        {
            string lastKey = @"youAreInGuestMode";
            for(int i = 0; i < languageResources.Count; i++)
            {
                string[] keyResource = languageResources[i];
                if (keyResource[0]==lastKey)
                {
                    break; // 最後の行に到達したら終了
                }
                if (keyResource.Length < 16)
                {
                    if (keyResource.Length is 1 ||
                        (keyResource.Length >= 2 && keyResource[1] is not "Text"))
                    {
                        // 単一で改行された行、もしくは行中に複数のカラムが残っているが先頭から始まっていない場合は前の行と連結する
                        string fixedLine = string.Join('\t', languageResources[i - 1]) + string.Join('\t', languageResources[i]);
                        languageResources[i - 1] = fixedLine.Split('\t');
                        // 前の行に連結完了した現在行は削除する
                        languageResources.RemoveAt(i);
                        i--;
                    }
                    else if (keyResource.Length >= 2 && keyResource[1] is "Text")
                    {
                        // 行の途中での改行は次の行と連結する
                        string fixedLine = string.Join('\t', languageResources[i]) + string.Join('\t', languageResources[i + 1]);
                        languageResources[i] = fixedLine.Split('\t');
                        // 現在の行に連結完了した次の行は削除する
                        languageResources.RemoveAt(i + 1);
                    }
                }
            }
            return languageResources;
        }
    }
}
