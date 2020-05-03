using System;
using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.App.DataAccess.Http;
using Packit.App.ThirdPartyApiModels;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : Observable
    {
        private readonly WeatherDataAccess weatherDataAccess = new WeatherDataAccess();
        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadData));

        public TripBackpackItemLink Trip { get; set; }

        public DetailTripViewModel()
        {
        }

        private async void LoadData()
        {
            Trip.WeatherReport = await weatherDataAccess.GetCurrentWeatherReportAsync(Trip.Trip.Destination);
        }

        public void InitializeAsync(TripBackpackItemLink trip) => Trip = trip;
    }
}
