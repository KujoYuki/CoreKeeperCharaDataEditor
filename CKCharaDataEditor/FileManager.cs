using CKCharaDataEditor.Properties;
using LanguageResource;
using System.Text.RegularExpressions;

namespace CKCharaDataEditor
{
    // Singleton pattern
    public sealed class FileManager : IDisposable
    {
        private static FileManager? _instance;
        public static FileManager Instance => _instance ??= new FileManager();
        public IReadOnlyDictionary<int, string[]> LocalizationData = new Dictionary<int, string[]>();
        private FileManager()
        {
            _saveFolder = Settings.Default.SaveFolderPath;
            if (_saveFolder == string.Empty || !Directory.Exists(_saveFolder))
            {
                SaveFolder = InitSaveFolderPath();
            }

            _installFolder = Settings.Default.InstallFolderPath;
            if (_installFolder == string.Empty || !Directory.Exists(_installFolder))
            {
                InstallFolder = InitInstallFolderPath();
            }
        }

        private string _saveFolder = string.Empty;
        public string SaveFolder
        {
            get => _saveFolder;
            set
            {
                if (Directory.Exists(value))
                {
                    _saveFolder = value;
                    Settings.Default.SaveFolderPath = value;
                }
            }
        }

        public List<FileInfo> CharacterFilePaths => GetValidCharaFilePaths();

        public List<FileInfo> WorldFilePaths => GetValidWorldFilePaths();

        private string _installFolder = string.Empty;
        public string InstallFolder
        {
            get => _installFolder;
            set
            {
                if (Directory.Exists(value))
                {
                    _installFolder = value;
                    Settings.Default.InstallFolderPath = value;
                    // 言語リソースの初期化
                    LocalizationData = LanguagePack.CreateLanguageDictionary(value);
                }
            }
        }

        public void Dispose()
        {
            Settings.Default.Save();
        }

        private static string InitSaveFolderPath()
        {
            try
            {
                string appDataPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))!.FullName;
                string generalPath = Path.Combine(appDataPath, @"LocalLow\Pugstorm\Core Keeper\Steam");

                string saveDataFolderPath;
                if (!Path.Exists(generalPath) || Directory.GetDirectories(generalPath).Length is 0)
                {
                    // フォルダ見つからない場合はLocalLowフォルダで止め置く
                    saveDataFolderPath = Path.Combine(appDataPath, "LocalLow");
                }
                else
                {
                    // 複数のSteamアカウントでCoreKeeperのセーブデータがそれぞれある場合、最初のディレクトリを選択する
                    saveDataFolderPath = Directory.GetDirectories(generalPath).FirstOrDefault()!;
                }
                return saveDataFolderPath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string InitInstallFolderPath()
        {
            //@"G:\SteamLibrary\steamapps\common\Core Keeper"; //Yukiさんのインストール先
            //@"C:\Program Files (x86)\Steam\steamapps\common\Core Keeper";   //一般的なインストール先
            string installFolderPath = @"C:\Program Files (x86)\Steam\steamapps\common\Core Keeper";
            if (Directory.Exists(installFolderPath))
            {
                return installFolderPath;
            }
            else
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            }
        }

        private List<FileInfo> GetValidCharaFilePaths()
        {
            if (string.IsNullOrEmpty(SaveFolder) || Directory.GetDirectories(SaveFolder).Length is 0)
            {
                // セーブデータが存在しない場合は空リストを返す
                return [];
            }
            try
            {
                // セーブデータフォルダが存在する場合、セーブデータ一覧を取得
                Regex regex = new(@"^\d{1,2}|debug$", RegexOptions.Compiled);
                List<FileInfo> saveFiles = new DirectoryInfo(Path.Combine(SaveFolder, "saves")).GetFiles(@"*.json")
                    .Where(file => regex.IsMatch(Path.GetFileNameWithoutExtension(file.Name)))
                    .OrderBy(file => file.Name)
                    .ToList();

                List<FileInfo> nonNumericFiles = saveFiles
                    .Where(file => !int.TryParse(Path.GetFileNameWithoutExtension(file.Name), out _))
                    .ToList();

                List<FileInfo> sortedSaveFiles = saveFiles
                    .Where(file => int.TryParse(Path.GetFileNameWithoutExtension(file.Name), out _))
                    .OrderBy(file => int.Parse(Path.GetFileNameWithoutExtension(file.Name)))
                    .Concat(nonNumericFiles)
                    .ToList();

                return sortedSaveFiles;
            }
            catch (Exception)
            {
                _saveFolder = string.Empty;
                return [];
            }
        }

        private List<FileInfo> GetValidWorldFilePaths()
        {
            if (string.IsNullOrEmpty(SaveFolder) || Directory.GetDirectories(SaveFolder).Length is 0)
            {
                // セーブデータが存在しない場合は空リストを返す
                return [];
            }
            try
            {
                // セーブデータフォルダが存在する場合、セーブデータ一覧を取得
                Regex regex = new(@"^\d{1,2}|debug$", RegexOptions.Compiled);
                List<FileInfo> saveFiles = new DirectoryInfo(Path.Combine(SaveFolder, "worldinfos")).GetFiles(@"*.worldinfo")
                    .Where(file => regex.IsMatch(Path.GetFileNameWithoutExtension(file.Name)))
                    .OrderBy(file => file.Name)
                    .ToList();

                List<FileInfo> nonNumericFiles = saveFiles
                    .Where(file => !int.TryParse(Path.GetFileNameWithoutExtension(file.Name), out _))
                    .ToList();

                List<FileInfo> sortedSaveFiles = saveFiles
                    .Where(file => int.TryParse(Path.GetFileNameWithoutExtension(file.Name), out _))
                    .OrderBy(file => int.Parse(Path.GetFileNameWithoutExtension(file.Name)))
                    .Concat(nonNumericFiles)
                    .ToList();

                return sortedSaveFiles;
            }
            catch (Exception)
            {
                _saveFolder = string.Empty;
                return [];
            }
        }
    }
}
