using System.Diagnostics;
using System.Reflection;

namespace CKCharaDataEditor
{
    partial class AboutBox : Form
    {
        public static string GameVersion = "1.1.2.9";
        public static string ApplicationVersion = "1.5.6";
		public AboutBox()
        {
            InitializeComponent();
            Text = String.Format("バージョン情報");
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("バージョン {0}", FileVersion);
            labelGameVersion.Text = String.Format("リリース時のゲームバージョン {0}", GameVersion);
            labelCopyright.Text = AssemblyCopyright;
            linkLabelRepositoryUrl.Links.Add(0, linkLabelRepositoryUrl.Text.Length, "https://github.com/KujoYuki/CoreKeeperCharaDataEditor");
            textBoxDescription.Text = "詳細な使い方や仕様についてはリポジトリのドキュメントをチェックしてください。\r\n" +
                "マルチプレイでの使用はホストに確認してください。\r\n" +
                "不具合/要望があれば作者までどうぞ。\r\n" +
                "\r\nDiscord : kujoyuki\r\n" +
                "X : @KujoYuki_vr\r\n" +
                "Twitch : kujoyuki_vr";
        }

        #region アセンブリ属性アクセサー

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public static string FileVersion
        {
            get
            {
				// 優先: EntryAssembly (実行ファイル)、次に ExecutingAssembly
				var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

				// まず実行ファイルパスから取得（単一ファイル公開などで空の場合がある）
				var exePath = assembly.Location;
				if (!string.IsNullOrEmpty(exePath))
				{
					try
					{
						var fileVersion = FileVersionInfo.GetVersionInfo(exePath).FileVersion;
						if (!string.IsNullOrEmpty(fileVersion))
						{
							return fileVersion;
						}
					}
					catch
					{
						// 取得失敗時は無視して次へ
					}
				}
                return ApplicationVersion;
			}
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion

        private void linkLabelRepositoryUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link!.LinkData is string url)
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
    }
}
