using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class Backpack : BaseInformation, IDatabaseExistable
    {
        public int BackpackId { get; set; }
        public virtual ICollection<ItemBackpack> Items { get;}
        public virtual ICollection<BackpackTrip> Trips { get; }

        public Backpack() { }

        public Backpack(string title, string description, string imageStringName, User user)
            : base(title, description, imageStringName, user)
        {
            Items = new List<ItemBackpack>();
            Trips = new List<BackpackTrip>();
        }
        public override string ToString()
        {
            return $"{Title}, {BackpackId}";
        }

        public int Id()
        {
            return BackpackId;
        }
    }
}
