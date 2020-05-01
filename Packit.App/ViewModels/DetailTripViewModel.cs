using System;
using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.App.DataAccess.Http;
using Packit.App.ThirdPartyApiModels;

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : Observable
    {
        private WeatherDataAccess weatherDataAccess = new WeatherDataAccess();

        public TripBackpackItemLink Trip { get; set; }
        public Weather Weather { get; set; }

        public DetailTripViewModel()
        {
        }

        public async System.Threading.Tasks.Task InitializeAsync(TripBackpackItemLink trip)
        {
            Trip = trip;
            await weatherDataAccess.GetCurrentWeatherAsync("oslo");
        }
    }
}
