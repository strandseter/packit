using Packit.Model.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class Backpack : BaseInformation, IOneToManyAble, IDatabase
    {
        public int BackpackId { get; set; }
        public SharedBackpack SharedBackpack { get; set; }
        public User User { get; set; }
        public virtual ICollection<ItemBackpack> Items { get; set; }
        public virtual ICollection<BackpackTrip> Trips { get; set; }

        public Backpack() 
        {
            Items = new List<ItemBackpack>();
            Trips = new List<BackpackTrip>();
        }

        public Backpack(string title, string description, string imageStringName)
            : base(title, description, imageStringName)
        {
            Items = new List<ItemBackpack>();
            Trips = new List<BackpackTrip>();
        }

        public override string ToString() => $"{Title}, {BackpackId}";

        public int Id() => BackpackId;

        public int GetId() => BackpackId;

        public IList GetEntities()
        {
            return (IList)Items;
        }
    }
}
