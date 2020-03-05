using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class ItemBackpack : IManyToManyJoinable
    {
        public virtual int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual int BackpackId { get; set; }
        public virtual Backpack Backpack { get; set; }

        public void Id1(int id)
        {
            ItemId = id;
        }

        public void Id2(int id)
        {
            BackpackId = id;
        }

    }
}
