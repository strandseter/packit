using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageFilePath { get; set; }
        public User User { get; set; }
        public virtual ICollection<ItemBackpack> Backpacks { get; }

        public Item()
        {
            Backpacks = new List<ItemBackpack>();
        }

        public override string ToString()
        {
            return $"{Title} {ItemId}";
        }
    }
}
