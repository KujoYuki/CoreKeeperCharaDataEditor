namespace CKCharaDataEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            try
            {
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogException(e.Exception);
            MessageBox.Show("スレッド例外が発生しました。\n" + e.Exception.Message);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                LogException(ex);
                MessageBox.Show("未処理の例外が発生しました。\n" + ex.Message);
            }
        }

        private static void LogException(Exception ex)
        {
            // ログファイルに追記
            File.AppendAllText("ErrorStackTrace.txt", DateTime.Now + Environment.NewLine + ex.ToString() + Environment.NewLine);
        }

        static Program()
        {
            string[] args = Environment.GetCommandLineArgs();
            IsDeveloper = args.Contains("--Usagi");
        }

        public static bool IsDeveloper { get; private set; }
    }
}