using CKCharaDataEditor.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKCharaDataEditor.Model.Pet
{
    public record Pet
    {
        // undone ペット関連の情報を一意に持つモデル
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

        public int Exp { get; set; } = 0;

        public PetTalent[] Talents { get; set; } = new PetTalent[9];

        public int AuxIndex { get; set; } = 0;

        public Pet()
        {
            Id = PetId.PetDog;
            Color = PetColor.Color_0;
            Name = string.Empty;
            Talents = new PetTalent[9];
        }

        public Pet(Item item)
        {
            // todo アイテムからペットを生成する
            for (int i = 0; i < 9; i++)
            {
                Talents[i] = PetTalent.Default;
            }
            throw new NotImplementedException();
        }
        public Item ToItem()
        {
            // todo ペットをアイテムに変換する
            throw new NotImplementedException();
        }
    }
}
