using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class BackpackTrip
    {
        public virtual int BackpackId { get; set; }
        public virtual Backpack Backpack {get; set; }
        public virtual int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
