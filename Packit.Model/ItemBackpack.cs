using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class ItemBackpack
    {
        public virtual int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual int BackpackId { get; set; }
        public virtual Backpack Backpack { get; set; }
    }
}
