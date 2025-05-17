using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKCharaDataEditor.Model.Items
{
    [Flags]
    public enum IngredientRoots
    {
        None,
        Harvest,
        Fish,
        Cooked,
        /// <summary>
        /// 削除済みアイテム
        /// </summary>
        Deprecated,
    }
}
