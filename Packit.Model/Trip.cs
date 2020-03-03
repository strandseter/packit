using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class Trip : BaseInformation
    {
        public int TripId { get; set; }
        public string Destination { get; set; }
        public DateTime DepatureDate { get; set; }
        public virtual ICollection<BackpackTrip> Backpacks { get;}

        public Trip() { }

        public Trip(string title, string description, string imageStringName, User user)
           : base(title, description, imageStringName, user)
        {
            Backpacks = new List<BackpackTrip>();
        }

        public override string ToString()
        {
            return $"{Title}, ";
        }
    }
}
