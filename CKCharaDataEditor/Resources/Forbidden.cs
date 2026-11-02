namespace CKCharaDataEditor.Resources
{
    public static partial class Forbidden
    {
        public static string[] Users { get; private set; } = Array.Empty<string>();

        static Forbidden()
        {
            SetLocalUsers();
        }

        // 必要に応じて、別のファイルで実装される。
        static partial void SetLocalUsers();
    }
}
