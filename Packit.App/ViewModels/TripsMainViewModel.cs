using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Packit.App.ViewModels
{
    public class TripsMainViewModel : Observable
    {
        private readonly IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadDataAsync));
        public ICommand TripDetailCommand { get; set; }

        public ObservableCollection<TripImageWeatherLink> Trips { get; } = new ObservableCollection<TripImageWeatherLink>();

        public TripsMainViewModel()
        {
            TripDetailCommand = new RelayCommand<TripImageWeatherLink>(param =>
            {
                NavigationService.Navigate(typeof(DetailTripV2Page), param);
            });
        }

        private async void LoadDataAsync()
        {
            await LoadTrips();
            await LoadTripImagesAsync();
        }

        private async Task LoadTrips()
        {
            var trips = await tripsDataAccess.GetAllTestAsync();

            foreach (Trip trip in trips)
                Trips.Add(new TripImageWeatherLink(trip));
        }

        private async Task LoadTripImagesAsync()
        {
            foreach (TripImageWeatherLink t in Trips)
                t.Image = await imagesDataAccess.GetImageAsync(t.Trip.ImageStringName);
        }
    }
}
