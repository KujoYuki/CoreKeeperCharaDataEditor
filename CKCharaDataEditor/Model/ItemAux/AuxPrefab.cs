namespace CKCharaDataEditor.Model.ItemAux
{
    public record AuxPrefab
    {
        public AuxPrefab(ulong prefabHash, IEnumerable<AuxStableType> types)
        {
            this.prefabHash = prefabHash;
            this.types = types;
        }

        public ulong prefabHash { get; set; }
        public IEnumerable<AuxStableType> types { get; set; }
    }
}
