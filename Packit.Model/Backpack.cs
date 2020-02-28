using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class Backpack
    {
        public int BackpackId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public virtual ICollection<ItemBackpack> Items { get;}
        public virtual ICollection<BackpackTrip> Trips { get; }

        public Backpack()
        {
            Items = new List<ItemBackpack>();
            Trips = new List<BackpackTrip>();
        }

        public override string ToString()
        {
            return $"{Title}, {BackpackId}";
        }
    }
}
