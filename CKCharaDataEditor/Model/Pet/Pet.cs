using CKCharaDataEditor.Model.ItemAux;
using CKCharaDataEditor.Model.Items;
using CKCharaDataEditor.Resource;

namespace CKCharaDataEditor.Model.Pet
{
    public record Pet : Item
    {
        public PetId Id { get; set; }

        public PetBattleType BattleType
        {
            get => PetResource.BattleType[Id];
        }

        public PetColor Color { get; set; }

        public string ColorDisplay
        {
            get => PetResource.ColorDict[(Id, Color)];
        }

        public string Name { get; set; } = string.Empty;

        public PetTalent[] Talents { get; set; }

        protected Pet(PetId petId, int exp, ItemAuxData auxData)
            : base((int)petId, exp, 0, 0, petId.ToString(), auxData)
        {
            Id = petId;
            Aux = auxData;
            Aux.GetPetData(out string name, out int color, out var talents);
            Color = (PetColor)color;
            Name = name;
            Talents = talents.ToArray();
        }

        // hack ペットごとの特質が必要な場合にこの辞書を参照するように既存ロジックを変更する
        public static readonly Dictionary<int, (PetId PetId, PetBattleType PetBattleType, bool HasMultiColor)> PetDic = new()
        {
            { 1222, (PetId.PetDog, PetBattleType.Melee,true) },
            { 1225, (PetId.PetCat, PetBattleType.Range,true) },
            { 1228, (PetId.PetBird, PetBattleType.Buff,true) },
            { 1231, (PetId.PetSlimeBlob, PetBattleType.Melee, false) },
            { 1234, (PetId.PetBunny, PetBattleType.Range, true) },
            { 1237, (PetId.PetSlipperySlimeBlob, PetBattleType.Melee, false) },
            { 1240, (PetId.PetPoisonSlimeBlob, PetBattleType.Melee, false) },
            { 1243, (PetId.PetLavaSlimeBlob, PetBattleType.Melee, false) },
            { 1246, (PetId.PetPrinceSlimeBlob, PetBattleType.Melee, false) },
            { 1247, (PetId.PetMoth, PetBattleType.Buff, true) },
            { 1252, (PetId.PetTardigrade, PetBattleType.Melee, true) },
            { 1253, (PetId.PetMagic, PetBattleType.Buff, false) },
            { 1258, (PetId.PetElectric, PetBattleType.Melee, false) },
            { 1261, (PetId.PetWarlock, PetBattleType.Buff, true) },
        };
    }
}
