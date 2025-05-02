using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace CKCharaDataEditor
{
    public static class UpdateChecker
    {
        private const string _currentVersion = "1.4.3"; // todo 新バージョンリリース時に手動で更新すること
        private const string _repoOwner = "KujoYuki";
        private const string _repoName = "CoreKeeperCharaDataEditor";
        private const string _uri = $"https://api.github.com/repos/{_repoOwner}/{_repoName}/releases/latest";

        public static async Task<(bool newerRelease, string version, int download_count)> CheckLatestVersionAsync()
        {
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(_uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CKFoodMaker", "1.0"));

                var response = await client.GetAsync(_uri, HttpCompletionOption.ResponseHeadersRead);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var latestRelease = JsonNode.Parse(json)!.AsObject();
                    string latestVersion = latestRelease["tag_name"]?.GetValue<string>()!;
                    int download_count = latestRelease["assets"]![0]!["download_count"]!.GetValue<int>();

                    bool newerRelease = IsNewerVersion(_currentVersion, latestVersion);
                    return (newerRelease, latestVersion, download_count);
                }
                else
                {
                    // todo globalなロギング
                    return (false, _currentVersion, 0);
                    //throw new HttpRequestException($"バージョンチェックに失敗しました。ステータスコード: {response.StatusCode}");
                }
            }
        }

        private static bool IsNewerVersion(string currentVersion, string latestVersion)
        {
            var currentParts = currentVersion.Split('.').Select(int.Parse).ToArray();
            var latestParts = latestVersion.Split('.').Select(int.Parse).ToArray();

            foreach (var (currentPart, latestPart) in currentParts.Zip(latestParts, (c, l) => (c, l)))
            {
                if (latestPart > currentPart)
                {
                    return true;
                }
                else if (latestPart < currentPart)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
