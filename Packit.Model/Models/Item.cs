using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Item : BaseInformation, IDatabaseExistable
    {
        public int ItemId { get; set; }
        public virtual ICollection<ItemBackpack> Backpacks { get; }

        public Item() { }

        public Item(string title, string description, string imageStringName, User user)
            :base(title, description, imageStringName, user)
        {
            Backpacks = new List<ItemBackpack>();
        }

        public override string ToString()
        {
            return $"{Title} {ItemId}";
        }

        public int Id()
        {
            return ItemId;
        }
    }
}
