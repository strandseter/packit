using Packit.Model.Models;
using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Trip : BaseInformation, ICloneable
    {
        private string destination;
        private DateTimeOffset depatureDate = DateTimeOffset.Now;

        public int TripId { get; set; }
        public virtual ICollection<BackpackTrip> Backpacks { get; } = new List<BackpackTrip>();

        public string Destination 
        { 
            get => destination;
            set => Set(ref destination, value);
        }

        public DateTimeOffset DepatureDate 
        { 
            get => depatureDate;
            set => Set(ref depatureDate, value);
        }

        public Trip()
        {
        }

        public override int GetId() => TripId;

        public override void SetId(int value) => TripId = value;

        public override string ToString() => $"{Title}, ";

        public object Clone() => MemberwiseClone();
    }
}
