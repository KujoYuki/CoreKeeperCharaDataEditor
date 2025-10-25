using System.Diagnostics;
using System.Reflection;

namespace CKCharaDataEditor
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            Text = String.Format("バージョン情報");
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("バージョン {0}", FileVersion);
            labelGameVersion.Text = String.Format("リリース時のゲームバージョン {0}", "1.1.2.5");
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
                //return Assembly.GetExecutingAssembly().GetName().Version.ToString();
                // 実行ファイルのパスを取得
                var exePath = Assembly.GetExecutingAssembly().Location;
                // ファイルバージョン情報を取得
                var fileVersion = FileVersionInfo.GetVersionInfo(exePath).FileVersion;
                return fileVersion ?? "";
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
