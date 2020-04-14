using Packit.Model.Models;
using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Trip : BaseInformation, IDatabase
    {
        public int TripId { get; set; }
        public string Destination { get; set; }
        public DateTime DepatureDate { get; set; }
        public virtual ICollection<BackpackTrip> Backpacks { get; } = new List<BackpackTrip>();

        public Trip() 
        {
        }

        public int GetId()
        {
            return TripId;
        }

        public void SetId(int value)
        {
            TripId = value;
        }

        public override string ToString() => $"{Title}, ";
    }
}
