using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class BackpackTrip : IManyToMany
    {
        public virtual int BackpackId { get; set; }
        public virtual Backpack Backpack {get; set; }
        public virtual int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        public void SetLeftId(int id) => BackpackId = id;
        public void SetRightId(int id) => TripId = id;
        public int GetLeftId() => BackpackId;
        public int GetRightId() => TripId;
        public override string ToString() => $"LeftId: {BackpackId}, RightId: {TripId}";
    }
}
