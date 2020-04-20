using Packit.Model.Models;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Backpack : BaseInformation
    {
        public int BackpackId { get; set; }
        public bool IsShared { get; set; }
        public virtual ICollection<ItemBackpack> Items { get; } = new List<ItemBackpack>();
        public virtual ICollection<BackpackTrip> Trips { get; } = new List<BackpackTrip>();

        public Backpack() 
        {
        }

        public override int GetId()
        {
            return BackpackId;
        }

        public override void SetId(int value)
        {
            BackpackId = value;
        }

        public override string ToString() => $"{Title}, {BackpackId}";
    }
}
