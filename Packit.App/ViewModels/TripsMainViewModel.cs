// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="TripsMainViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
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

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class TripsMainViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class TripsMainViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The trips data access
        /// </summary>
        private readonly IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        /// <summary>
        /// The images data access
        /// </summary>
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;
        #endregion

        #region public properties
        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<TripsMainPage>(async () => await LoadDataAsync(), PopUpService));
        /// <summary>
        /// Gets or sets the trip detail command.
        /// </summary>
        /// <value>The trip detail command.</value>
        public ICommand TripDetailCommand { get; set; }
        /// <summary>
        /// Gets or sets the add trip command.
        /// </summary>
        /// <value>The add trip command.</value>
        public ICommand AddTripCommand { get; set; }
        /// <summary>
        /// Gets the trips.
        /// </summary>
        /// <value>The trips.</value>
        public ObservableCollection<TripImageWeatherLink> Trips { get; } = new ObservableCollection<TripImageWeatherLink>();
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TripsMainViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public TripsMainViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            TripDetailCommand = new RelayCommand<TripImageWeatherLink>(param =>
            {
                NavigationService.Navigate(typeof(DetailTripPage), param);
            });

            AddTripCommand = new RelayCommand(() => NavigationService.Navigate(typeof(NewTripPage)));
        }
        #endregion

        #region private load methods
        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadTrips();
            await LoadTripImagesAsync();
        }

        /// <summary>
        /// Loads the trips.
        /// </summary>
        private async Task LoadTrips()
        {
            var trips = await tripsDataAccess.GetAllWithChildEntitiesAsync();

            foreach (Trip trip in trips)
                Trips.Add(new TripImageWeatherLink(trip));
        }

        /// <summary>
        /// load trip images as an asynchronous operation.
        /// </summary>
        private async Task LoadTripImagesAsync()
        {
            foreach (TripImageWeatherLink t in Trips)
                t.Image = await imagesDataAccess.GetImageAsync(t.Trip.ImageStringName, "ms-appx:///Assets/generictrip.jpg");
        }
        #endregion
    }
}
