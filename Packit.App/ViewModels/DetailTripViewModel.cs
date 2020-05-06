using System;
using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.App.DataAccess.Http;
using Packit.App.ThirdPartyApiModels;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Packit.Model;

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : Observable
    {
        private readonly WeatherDataAccess weatherDataAccess = new WeatherDataAccess();
        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadData));

        public TripImageWeatherLink TripWithImageWeather { get; set; }

        public WeatherReport WeatherReport { get; set; }

        public ObservableCollection<BackpackWithItems> Backpacks { get; } = new ObservableCollection<BackpackWithItems>();


        public DetailTripViewModel()
        {
        }

        private async void LoadData()
        {
            LoadBackpacks();
            LoadItemsInBackpacks();
            await LoadWeatherReportAsync();

        }

        private void LoadBackpacks()
        {
            foreach(var bt in TripWithImageWeather.Trip.Backpacks)
                Backpacks.Add(new BackpackWithItems(bt.Backpack));
        }

        private void LoadItemsInBackpacks()
        {
            foreach(var bwi in Backpacks)
            {
                foreach(var item in bwi.Backpack.Items)
                    bwi.Items.Add(item.Item);
            }
        }

        private async Task LoadWeatherReportAsync()
        {
            TripWithImageWeather.WeatherReport = await weatherDataAccess.GetCurrentWeatherReportAsync(TripWithImageWeather.Trip.Destination);
            //WeatherReport.Weathers[0].IconImage = await weatherDataAccess.GetCurrentWeatherIconAsync(WeatherReport.Weathers[0].Icon);
        }

        public void Initialize(TripImageWeatherLink trip) => TripWithImageWeather = trip;
    }
}
