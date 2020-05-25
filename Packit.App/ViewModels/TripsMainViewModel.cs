using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Exceptions;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class TripsMainViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public ICommand TripDetailCommand { get; set; }
        public ICommand AddTripCommand { get; set; }

        public ObservableCollection<TripImageWeatherLink> Trips { get; } = new ObservableCollection<TripImageWeatherLink>();

        public TripsMainViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            TripDetailCommand = new RelayCommand<TripImageWeatherLink>(param =>
            {
                NavigationService.Navigate(typeof(DetailTripV2Page), param);
            });

            AddTripCommand = new RelayCommand(() => NavigationService.Navigate(typeof(NewTripPage)));
        }

        private async Task LoadDataAsync()
        {
            try
            {
                await LoadTrips();
                await LoadTripImagesAsync();
            }
            catch (NetworkConnectionException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<TripsMainPage>(NavigationService.Navigate, ex);

            }
            catch (HttpRequestException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<TripsMainPage>(NavigationService.Navigate, ex);
            }
        }

        private async Task LoadTrips()
        {
            try
            {
                var trips = await tripsDataAccess.GetAllWithChildEntitiesAsync();

                foreach (Trip trip in trips)
                    Trips.Add(new TripImageWeatherLink(trip));
            }
            catch (NetworkConnectionException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<TripsMainPage>(NavigationService.Navigate, ex);

            }
            catch (HttpRequestException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<TripsMainPage>(NavigationService.Navigate, ex);
            }
        }
    
        private async Task LoadTripImagesAsync()
        {
            try
            {
                foreach (TripImageWeatherLink t in Trips)
                    t.Image = await imagesDataAccess.GetImageAsync(t.Trip.ImageStringName, "ms-appx:///Assets/generictrip.jpg");
            }
            catch (NetworkConnectionException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<TripsMainPage>(NavigationService.Navigate, ex);

            }
            catch (HttpRequestException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<TripsMainPage>(NavigationService.Navigate, ex);
            }
        }
    }
}
