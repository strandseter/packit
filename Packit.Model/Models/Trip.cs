using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class Trip : BaseInformation, IOneToManyAble, IDatabase
    {
        public int TripId { get; set; }
        public string Destination { get; set; }
        public DateTime DepatureDate { get; set; }
        public User user { get; set; }
        public virtual ICollection<BackpackTrip> Backpacks { get; set; }

        public Trip() 
        {
        }

        public Trip(string title, string description, string imageStringName)
           : base(title, description, imageStringName)
        {
        }

        public override string ToString() => $"{Title}, ";

        public int Id() => TripId;

        public int GetId() => TripId;
    }
}
