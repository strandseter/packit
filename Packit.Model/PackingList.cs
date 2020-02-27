using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class PackingList
    {
        public int PackingListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public PackingList()
        {
            Items = new List<Item>();
        }

        public override string ToString()
        {
            return $"{Title}, {PackingListId}";
        }
    }
}
