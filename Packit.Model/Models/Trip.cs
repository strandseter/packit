using Packit.Model.Models;
using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Trip : BaseInformation, ICloneable
    {
        private string destination;
        private DateTime depatureDate;

        public int TripId { get; set; }
        public string Destination 
        { 
            get => destination;
            set
            {
                if (value == destination) return;
                destination = value;
                OnPropertyChanged(nameof(Destination));
            }
        }
        public DateTime DepatureDate 
        { 
            get => depatureDate;
            set
            {
                if (value == depatureDate) return;
                depatureDate = value;
                OnPropertyChanged(nameof(DepatureDate));
            }
        }

        public virtual ICollection<BackpackTrip> Backpacks { get; } = new List<BackpackTrip>();

        public Trip()
        {
        }

        public override int GetId() => TripId;

        public override void SetId(int value) => TripId = value;

        public override string ToString() => $"{Title}, ";

        public object Clone() => MemberwiseClone();
    }
}
