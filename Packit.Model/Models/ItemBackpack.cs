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

        public void SetLeftId(int id) => ItemId = id;
        public int GetLeftId() => ItemId;
        public void SetRightId(int id) => BackpackId = id;
        public int GetRightId() => BackpackId;
        public override string ToString() => $"LeftId: {ItemId}, RightId: {BackpackId}";
    }
}
