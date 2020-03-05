using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class BackpackTrip : IManyToManyJoinable
    {
        public virtual int BackpackId { get; set; }
        public virtual Backpack Backpack {get; set; }
        public virtual int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        public void Id1(int id)
        {
            BackpackId = id;
        }

        public void Id2(int id)
        {
            TripId = id;
        }
    }
}
