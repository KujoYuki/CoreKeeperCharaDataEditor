using System.Data;

namespace CKCharaDataEditor.Forms
{
    public partial class SearchForm : Form
    {
        const string FORMAT_SEARCH_RESULT = "{0,5}, {1}";
        const string FORMAT_NO_RESULT = "上記検索結果に一致するアイテムはありませんでした。";
        private const int MAX_SEARCH_WORD_HISTORY_COUNT = 20;
        private FileManager _fileManager = FileManager.Instance;
        private readonly Action<int> _updateObjectId;

        private Dictionary<int, string> _localizationDic = new Dictionary<int, string>();

        private Queue<string> _searchWordHistories = new Queue<string>(MAX_SEARCH_WORD_HISTORY_COUNT);

        public int SelectedObjectId { get; private set; } = 0;

        public SearchForm(Action<int> updateObjectId)
        {
            _updateObjectId = updateObjectId;
            InitializeComponent();

            _localizationDic = _fileManager.LocalizationData.ToDictionary(p => p.Key, p => p.Value.DisplayName);

            // フォームロード時に検索結果を初期化(全てのアイテムを表示)
            string[] results = SearchObjectIds(string.Empty);
            resultListBox.Items.AddRange(results);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void searchComboBox_TextUpdate(object sender, EventArgs e)
        {
            resultListBox.Items.Clear();
            string searchText = (sender as ComboBox)!.Text;
            string[] results = SearchObjectIds(searchText);
            resultListBox.Items.AddRange(results);
        }

        private string[] SearchObjectIds(string searchText)
        {
            // 検索は部分一致で行う
            var searchResults = _localizationDic.Where(p => p.Value.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Key)
                .Select(p => string.Format(FORMAT_SEARCH_RESULT, p.Key, p.Value))
                .ToArray();

            if (searchResults.Length == 0)
            {
                searchResults = [FORMAT_NO_RESULT];
            }

            // 結果を返す
            return searchResults;
        }

        private void resultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resultListBox.SelectedItem is null ||
                (resultListBox.Items.Count == 1 && resultListBox.Items[0].ToString() == FORMAT_NO_RESULT))
            {
                return;
            }

            string selectedObject = resultListBox.SelectedItem.ToString()!;
            SelectedObjectId = int.Parse(selectedObject.Split(',')[0].Trim());
            _updateObjectId(SelectedObjectId);

            // 検索結果の選択が変更されたときに、検索履歴に追加する
            if (!string.IsNullOrWhiteSpace(searchComboBox.Text) && !_searchWordHistories.Contains(searchComboBox.Text))
            {
                if (_searchWordHistories.Count == MAX_SEARCH_WORD_HISTORY_COUNT)
                {
                    _searchWordHistories.Dequeue();
                }
                _searchWordHistories.Enqueue(searchComboBox.Text);
                // コンボボックスに履歴を反映する
                searchComboBox.Items.Clear();
                searchComboBox.Items.AddRange(_searchWordHistories.ToArray());
            }
        }
    }
}
