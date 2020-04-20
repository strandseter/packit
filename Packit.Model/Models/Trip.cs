using Packit.Model.Models;
using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Trip : BaseInformation
    {
        public int TripId { get; set; }
        public string Destination { get; set; }
        public DateTime DepatureDate { get; set; }
        public virtual ICollection<BackpackTrip> Backpacks { get; } = new List<BackpackTrip>();

        public Trip() 
        {
        }

        public override int GetId()
        {
            return TripId;
        }

        public override void SetId(int value)
        {
            TripId = value;
        }

        public override string ToString() => $"{Title}, ";
    }
}
