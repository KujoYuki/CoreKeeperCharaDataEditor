using System.Text;

namespace CKCharaDataEditor
{
    public class LanguageLoader
    {
		const string YukisInstallPath = @"G:\SteamLibrary\steamapps\common\Core Keeper";
		static int ResourceColumnsCount = 0; // リソースファイルのカラム数
		static int JapaneseColumnIndex = 0; // 日本語リソースのカラムインデックス

		/// <summary>
		/// 動作確認用に単体でリソースを合成し、tsvファイルにします。
		/// </summary>
		/// <param name="args"></param>
		public static void OutputVisibleDictionary()
		{
			var dic = CreateLanguageDictionary(YukisInstallPath, out var _);
			List<string> outputNumericLines = dic.Select(pair => $"{pair.Key}\t{pair.Value.key}\t{pair.Value.displayString}").ToList();
			// 新規TSVファイルに出力
			string outputPath = Path.Combine(Environment.CurrentDirectory, "LanguageResource.tsv");

			File.WriteAllLines(outputPath, outputNumericLines, Encoding.UTF8);
			OutPutJapaneseResource();                                           // 全ての言語リソースの日英を抽出。
			Console.WriteLine($"TSVファイルが出力されました: {outputPath}");
		}

		/// <summary>
		/// 日本語のリソースを取得します。
		/// </summary>
		/// <param name="installPath"></param>
		/// <returns>KeyはobjectId, Valueは[0]でkeyName, [1]で日本語表示名</returns>
		/// <exception cref="FileNotFoundException"></exception>
		public static IReadOnlyDictionary<int, (string key, string displayString)> CreateLanguageDictionary(string installPath,
			out IReadOnlyDictionary<int, string> objectIdWithKeyDic)
		{
			string originObjectIdPath = Path.Combine(installPath, @"CoreKeeper_Data\StreamingAssets\Conf\ID\ObjectID.json");
			string bundledObjectIdPath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "ObjectID.json");
			string originLocalizationPath = Path.Combine(installPath, @"localization\Localization Template.csv");
			string bundledLocalizationPath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "Localization Template.csv");
			
			string objectIdPath = File.Exists(originObjectIdPath) ? originObjectIdPath : bundledObjectIdPath;
			string localizationPath = File.Exists(originLocalizationPath) ? originLocalizationPath : bundledLocalizationPath;

			objectIdWithKeyDic = CreateIdWithKey(objectIdPath);

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
				.Select(line => line.Split('\t'))
				.ToList();

			string[] header = languageResourceOrigin.First();
			ResourceColumnsCount = header.Length;
			JapaneseColumnIndex = Array.IndexOf(header, "Japanese");

			var trancelation = FixLineFeed(languageResourceOrigin)
				.Skip(1) // ヘッダーをスキップ
				.Where(words =>
				{
					string key = words[0];
					if (words.Length < ResourceColumnsCount || !key.StartsWith("Items/") || key.EndsWith("Desc")) return false;
					return true;
				})
				.Distinct()
				.Select(words =>
				{
					string key = words[0].Replace("Items/", string.Empty).Replace("Desc", string.Empty);
					string objectId = objectIdDic.TryGetValue(key, out int id) ? id.ToString() : $"{key}"; // IDが見つからない場合はKeyを使用
																										   // 本来はobjectIdとvariationのタプルでキーを参照しに良くのが正しいが、開発チームの消し忘れリソースも多いの。
																										   // それにvariation違いでkeyが変わるアイテムが少数過ぎるし、コアキ側の実装がEAの頃から中途半端に変わってるのが悪い。
					string displayString = words[JapaneseColumnIndex];
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
				.ToDictionary(key => int.Parse(key.Key), value => value.Value);

			return outputDic;
		}

        private static Dictionary<int, string> CreateIdWithKey(string filePath)
        {
			return File.ReadAllText(filePath).Trim()
				.Replace("{", string.Empty).Replace("}", string.Empty).Replace(":", ",").Replace("\"", string.Empty).Replace("\t", string.Empty)
				.Split("\n")
				.Where(line => !string.IsNullOrEmpty(line))
				.Select(line => line.Split(","))
				.Skip(1) // Noneをスキップ
				.ToDictionary(line => int.Parse(line[1]), line => line[0]);
		}

        /// <summary>
        /// リソースファイル中の無意味な改行コードの混入を修正します。
        /// </summary>
        /// <param name="languageResources"></param>
        /// <returns></returns>
        private static List<string[]> FixLineFeed(List<string[]> languageResources)
		{
			string lastKey = @"youAreInGuestMode";
			for (int i = 0; i < languageResources.Count; i++)
			{
				string[] keyResource = languageResources[i];
				if (keyResource[0] == lastKey)
				{
					break; // 最後の行に到達したら終了
				}
				if (keyResource.Length < ResourceColumnsCount)
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

		/// <summary>
		/// 翻訳用リソースのみを抽出、ただしアイテムに限らず全てのリソースを対象とする。
		/// </summary>
		private static void OutPutJapaneseResource()
		{
			string localizationPath = Path.Combine(YukisInstallPath, @"localization\Localization.csv");
			List<string[]> languageResourceTwo = File.ReadAllLines(localizationPath)
				.Select(line => line.Split('\t'))
				.ToList();

			List<string> trancelation = FixLineFeed(languageResourceTwo)
				.Select(words => string.Join("\t", words[0], words[JapaneseColumnIndex], words[3]))
				.ToList();

			string outputPath = Path.Combine(Environment.CurrentDirectory, "TwoLanguageResource.tsv");
			File.WriteAllLines(outputPath, trancelation, Encoding.UTF8);
			Console.WriteLine($"TSVファイルが出力されました: {outputPath}");
		}
	}
}
