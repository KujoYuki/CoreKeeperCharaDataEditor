using CKCharaDataEditor.Resource;
using System.Text.Json.Nodes;

namespace CKCharaDataEditor.Model.ItemAux
{
    public record ItemAuxData
    {
        public AuxPrefabManager AuxPrefabManager;

        #region Property
        public int index { get; set; }
        public string data
        {
            get
            {
                if (AuxPrefabManager == AuxPrefabManager.Default)
                {
                    return string.Empty;
                }
                return AuxPrefabManager.ToInnerJsonString(StaticResource.SerializerOption);
            }
            set
            {
                try
                {
                    AuxPrefabManager = new AuxPrefabManager(JsonNode.Parse(value)!.AsObject());
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"補助データのフォーマットが違います。\n{value}\n{ex.StackTrace}");
                }
            }
        }
        #endregion

        public static readonly ItemAuxData Default = new(0, "");

        public ItemAuxData(int index, string data)
        {
            this.index = index;
            AuxPrefabManager = (data is "") ? AuxPrefabManager.Default.DeepCopy()
                : new AuxPrefabManager(JsonNode.Parse(data)!.AsObject());
        }

        public ItemAuxData(int index, AuxPrefabManager prefabManager)
        {
            this.index = index;
            AuxPrefabManager = prefabManager;
            this.data = prefabManager.GetJsonString();
        }
    }
}
