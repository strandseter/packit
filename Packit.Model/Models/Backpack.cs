using Packit.Model.Models;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Backpack : BaseInformation, IDatabase
    {
        public int BackpackId { get; set; }
        public SharedBackpack SharedBackpack { get; set; }
        public virtual ICollection<ItemBackpack> Items { get; set; }
        public virtual ICollection<BackpackTrip> Trips { get; set; }

        public Backpack() 
        {
            Items = new List<ItemBackpack>();
            Trips = new List<BackpackTrip>();
        }

        public Backpack(string title, string description, string imageStringName, User user)
            : base(title, description, imageStringName, user)
        {
            Items = new List<ItemBackpack>();
            Trips = new List<BackpackTrip>();
        }

        public override string ToString() => $"{Title}, {BackpackId}";
        public int GetId() => BackpackId;
    }
}
