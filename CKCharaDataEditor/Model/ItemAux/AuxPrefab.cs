namespace CKCharaDataEditor.Model.ItemAux
{
    public record AuxPrefab
    {
        public AuxPrefab(ulong prefabHash, List<AuxStableType> types)
        {
            this.prefabHash = prefabHash;
            this.types = types;
        }

        public ulong prefabHash { get; set; }
        public List<AuxStableType> types { get; set; }
    }
}
