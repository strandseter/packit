using System;
using Packit.App.DataLinks;
using Packit.App.Helpers;

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : Observable
    {
        public TripBackpackItemLink Trip { get; set; }

        public DetailTripViewModel()
        {
        }

        public void Initialize(TripBackpackItemLink trip) => Trip = trip;
    }
}
