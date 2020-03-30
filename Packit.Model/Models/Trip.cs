using Packit.Model.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Backpacks = new List<BackpackTrip>();
        }

        public Trip(string title, string description, string imageStringName)
           : base(title, description, imageStringName)
        {
            Backpacks = new List<BackpackTrip>();
        }

        public override string ToString() => $"{Title}, ";

        public int Id() => TripId;

        public int GetId() => TripId;
    }
}
