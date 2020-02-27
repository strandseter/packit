using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public DateTime DepatureDate { get; set; }
        public string ImageFileName { get; set; }
        public User User { get; set; }
        public virtual ICollection<PackingList> PackingLists { get; set; }

        public Trip()
        {
            PackingLists = new List<PackingList>();
        }

        public override string ToString()
        {
            return $"{Title}, ";
        }
    }
}
