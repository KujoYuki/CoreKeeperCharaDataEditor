using CKCharaDataEditor.Model.ItemAux;

namespace CKCharaDataEditor.Model.Items
{
    public record Item : ItemInfo
    {
        public virtual string objectName { get; set; }
        public ItemAuxData Aux { get; set; }
        public bool Locked { get; set; }

        /// <summary>
        /// 日本語リソース表示
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (Aux.AuxPrefabManager.TryGetData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash, out var _))
                {
                    return Aux.AuxPrefabManager.GetData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash)!.FirstOrDefault()!;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                // 空文字ならPrefabを削除する
                if (string.IsNullOrEmpty(value))
                {
                    Aux.AuxPrefabManager.DeletePrefab(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash);
                    return;
                }

                // 元が無ければPrefab追加する
                if (!Aux.AuxPrefabManager.HasData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash))
                {
                    Aux.AuxPrefabManager.AddPrefab([new AuxPrefab(AuxHash.ItemNameGroupHash, [new AuxStableType(AuxHash.ItemNameHash, [value])])]);
                    return;
                }

                // 既に存在する場合は更新する
                Aux.AuxPrefabManager.UpdateData(AuxHash.ItemNameGroupHash, AuxHash.ItemNameHash, [value]);
            }
        }

        public Item(int objectID, int amount, int variation, int variationUpdateCount, string objectName, ItemAuxData aux, bool locked = false)
            : base(objectID, amount, variation, variationUpdateCount)
        {
            this.objectName = objectName;
            Aux = aux;
            Locked = locked;
        }

        public static new Item Default = new(0, 0, 0, 0, string.Empty, ItemAuxData.Default);
    }
}
