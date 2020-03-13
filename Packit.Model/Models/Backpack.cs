using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class Backpack : BaseInformation, IOneToManyAble
    {
        public int BackpackId { get; set; }
        public SharedBackpack SharedBackpack { get; set; }
        public virtual ICollection<ItemBackpack> Items { get; set; }
        public virtual ICollection<BackpackTrip> Trips { get; set; }

        public Backpack() 
        {
        }

        public Backpack(string title, string description, string imageStringName)
            : base(title, description, imageStringName)
        {
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
