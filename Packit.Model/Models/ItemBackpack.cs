using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class ItemBackpack : IManyToMany
    {
        public virtual int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual int BackpackId { get; set; }
        public virtual Backpack Backpack { get; set; }

        public void SetLeftId(int id)
        {
            ItemId = id;
        }

        public int GetLeftId()
        {
            return ItemId;
        }

        public void SetRightId(int id)
        {
            BackpackId = id;
        }

        public int GetRightId()
        {
            return BackpackId;
        }
    }
}
