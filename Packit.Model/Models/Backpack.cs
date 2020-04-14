using Packit.Model.Models;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Backpack : BaseInformation, IDatabase
    {
        public int BackpackId { get; set; }
        public SharedBackpack SharedBackpack { get; set; }
        public virtual ICollection<ItemBackpack> Items { get; } = new List<ItemBackpack>();
        public virtual ICollection<BackpackTrip> Trips { get; } = new List<BackpackTrip>();

        public Backpack() 
        {
        }

        public int GetId()
        {
            return BackpackId;
        }

        public void SetId(int value)
        {
            BackpackId = value;
        }

        public override string ToString() => $"{Title}, {BackpackId}";
    }
}
