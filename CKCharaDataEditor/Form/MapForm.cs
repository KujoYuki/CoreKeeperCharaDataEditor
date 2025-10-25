using CKCharaDataEditor.Model.Map;
using System.Data;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace CKCharaDataEditor
{
    public partial class MapForm : Form
    {
        //todo マウスホイールでマップの拡大縮小
        //todo 読み込み後に(0, 0)の左下が中心に来るように自動スクロールする

        private FileManager _fileManager = FileManager.Instance;
        private SaveDataManager _saveDataManager = SaveDataManager.Instance;
        private int _charaFileIndex;
        private List<(FileInfo fileInfo, Guid guid)> CharaMapData = [];
        private Bitmap? _originalMap = null;
        private Point _mouseDownPosition;
        private Point _scrollPosition;

        public MapForm()
        {
            InitializeComponent();
            LoadMapInfo();
        }

        private void LoadMapInfo()
        {
            string charaFileName = Path.GetFileNameWithoutExtension(_saveDataManager.SaveDataPath);
            _charaFileIndex = Convert.ToInt32(charaFileName);
            IEnumerable<FileInfo> charaMapFilePaths = _fileManager.GetValidCharaMapFilePaths(_charaFileIndex);
            IEnumerable<Guid> mapGuids = _saveDataManager.GetJoinedMapIds();
            CharaMapData = charaMapFilePaths.Zip(mapGuids, (fileInfo, guid) => (fileInfo: fileInfo, guid: guid)).ToList();
            var setNames = CharaMapData
                .Select(data =>
                {
                    string no = Path.GetFileNameWithoutExtension(data.fileInfo.Name).Replace(".mapparts", "");
                    string guidFirstBlock = data.guid.ToString("N")[..16] + "...";
                    return $"{no}, {guidFirstBlock}";
                })
                .ToArray();
            mapListBox.Items.AddRange(setNames);
        }

        private void mapListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = mapListBox.SelectedIndex;
            worldIdLabel.Text = CharaMapData[selectedIndex].guid.ToString();
            string filePath = CharaMapData[selectedIndex].fileInfo.FullName;
            LoadMapData(filePath);
        }

        private void LoadMapData(string filePath)
        {
            using GZipStream fs = new(File.OpenRead(filePath), CompressionMode.Decompress);
            using StreamReader reader = new(fs, Encoding.UTF8);
            string json = reader.ReadToEnd();

            var mapparts = JsonSerializer.Deserialize<CoreKeeperMapData>(json);
            if (mapparts == null)
            {
                MessageBox.Show("マップデータの読み込みに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int mapCount = mapparts.MapParts.Count;
            // 画像の組み立て
            // タイムスタンプ画像の実体として時刻フォーマット未解明のため、基本使用しない
            var keyMaps = MapRestorer.CreateBitmapsFromMapData(mapparts);
            Bitmap map = MapRestorer.ComposeMap(keyMaps);
            mapPictureBox.Image = map;
            _originalMap = (Bitmap)map.Clone();
        }

        private void mapPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownPosition = e.Location;
                _scrollPosition = panel1.AutoScrollPosition;
            }
        }

        private void mapPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = _mouseDownPosition.X - e.X;
                int dy = _mouseDownPosition.Y - e.Y;
                panel1.AutoScrollPosition = new Point(
                    -(_scrollPosition.X + dx),
                    -(_scrollPosition.Y + dy)
                );
            }
        }

        private void saveMapAsImageButton_Click(object sender, EventArgs e)
        {
            // png画像として保存する
            if (mapPictureBox.Image == null)
            {
                return;
            }

            saveMapFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            saveMapFileDialog.FileName = $"map_{_charaFileIndex}_{mapListBox.SelectedItem!.ToString()!.Split(",")[0]}_{worldIdLabel.Text}.png";

            if (saveMapFileDialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = saveMapFileDialog.FileName;
                try
                {
                    mapPictureBox.Image.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"マップ画像の保存に失敗しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void worldIdLabel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(worldIdLabel.Text);
        }

        private void deleteSelectedMapButton_Click(object sender, EventArgs e)
        {
            if (mapListBox.SelectedIndex == -1)
            {
                MessageBox.Show("マップが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _saveDataManager.DeleteMapId(new Guid(worldIdLabel.Text));
            _fileManager.DeleteMapFile(CharaMapData.Select(v => v.fileInfo).ToArray(), mapListBox.SelectedIndex);

            MessageBox.Show("選択したマップファイルを削除し、マップ番号を詰めました。");

            // 再読み込み
            int topindex = mapListBox.TopIndex;
            int selectedIndex = mapListBox.SelectedIndex;
            mapListBox.Items.Clear();
            LoadMapInfo();
            mapListBox.TopIndex = topindex;
            if (selectedIndex >= mapListBox.Items.Count)
            {
                selectedIndex = mapListBox.Items.Count - 1;
            }
            else
            {
                mapListBox.SelectedIndex = selectedIndex;
            }
        }

        private void highLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mapPictureBox.Image is null) return;

            List<Color> targetColors = new List<Color>();
            if (oreGroupCheckBox.Checked)
            {
                foreach (CheckBox checkBox in oreFlowLayoutPanel.Controls.OfType<CheckBox>())
                {
                    if (checkBox.Checked && checkBox.Tag is not null)
                    {
                        string colorCode = checkBox.Tag.ToString()!;
                        int r = Convert.ToInt32(colorCode[..2], 16);
                        int g = Convert.ToInt32(colorCode[2..4], 16);
                        int b = Convert.ToInt32(colorCode[4..], 16);
                        Color color = Color.FromArgb(r, g, b);
                        targetColors.Add(color);
                    }
                }
            }
            // todo 他のグループもここに追加可能

            DrawHighLight(targetColors);

        }

        private IEnumerable<Point> DrawHighLight(List<Color> targetColors)
        {
            Bitmap originalMap = (Bitmap)mapPictureBox.Image!;
            // 一括で半透明にする
            //MakeTransparent(originalMap, 0.5f);

            var points = new List<Point>();
            for (int y = 0; y < originalMap.Height - 1; y++)
            {
                for (int x = 0; x < originalMap.Width - 1; x++)
                {
                    Color currentPixel = originalMap.GetPixel(x, y);
                    if (currentPixel.A is 0 or 255)
                    {
                        originalMap.SetPixel(x, y, Color.FromArgb(128, currentPixel));
                        continue; // 未探査か岩確定ピクセルはスキップ
                    }

                    if (targetColors.Contains(currentPixel))    //hack 255だと直前に切られてるので来ない
                    {
                        // 大きな岩か検証
                        Color neighborPixel = originalMap.GetPixel(x + 1, y);
                        Color belowPixel = originalMap.GetPixel(x, y + 1);
                        Color diagonalPixel = originalMap.GetPixel(x + 1, y + 1);
                        if (currentPixel.Equals(neighborPixel) || currentPixel.Equals(belowPixel) || currentPixel.Equals(diagonalPixel))
                        {
                            points.Add(new Point(x, y));
                            Color notTransparent = Color.FromArgb(255, currentPixel);
                            originalMap.SetPixel(x, y, notTransparent);
                            originalMap.SetPixel(x + 1, y, notTransparent);
                            originalMap.SetPixel(x, y + 1, notTransparent);
                            originalMap.SetPixel(x + 1, y + 1, notTransparent);
                        }
                    }
                }
            }
            mapPictureBox.Image = originalMap;
            return points;
        }

        //public static Bitmap MakeTransparent(Bitmap bitmap, float alpha)
        //{
        //    using (Graphics g = Graphics.FromImage(bitmap))
        //    {
        //        var matrix = new System.Drawing.Imaging.ColorMatrix
        //        {
        //            Matrix33 = alpha // 0.0～1.0
        //        };
        //        var attributes = new System.Drawing.Imaging.ImageAttributes();
        //        attributes.SetColorMatrix(matrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
        //        g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);
        //    }
        //    return bitmap;
        //}

        private void oreGroupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox)!.Checked;
            oreBoulderListBox.Visible = isChecked;
        }
    }
}
