// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="NewTripViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
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
using Packit.Model.NotifyPropertyChanged;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class NewTripViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class NewTripViewModel : ViewModel
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
        /// The local image
        /// </summary>
        private StorageFile localImage;
        /// <summary>
        /// The trip image
        /// </summary>
        private BitmapImage tripImage;
        /// <summary>
        /// The title is valid
        /// </summary>
        private bool titleIsValid;
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the trip.
        /// </summary>
        /// <value>The trip.</value>
        public Trip Trip { get; set; } = new Trip { Title="", Description="", Destination=""};
        /// <summary>
        /// Gets or sets the trip image.
        /// </summary>
        /// <value>The trip image.</value>
        public BitmapImage TripImage
        {
            get => tripImage;
            set => Set(ref tripImage, value);
        }
        /// <summary>
        /// Gets or sets the candcel command.
        /// </summary>
        /// <value>The candcel command.</value>
        public ICommand CandcelCommand { get; set; }
        /// <summary>
        /// Gets or sets the next command.
        /// </summary>
        /// <value>The next command.</value>
        public ICommand NextCommand { get; set; }
        /// <summary>
        /// Gets or sets the image device command.
        /// </summary>
        /// <value>The image device command.</value>
        public ICommand ImageDeviceCommand { get; set; }
        /// <summary>
        /// Gets or sets the add backpack command.
        /// </summary>
        /// <value>The add backpack command.</value>
        public ICommand AddBackpackCommand { get; set; }
        /// <summary>
        /// Gets or sets the remove backpack command.
        /// </summary>
        /// <value>The remove backpack command.</value>
        public ICommand RemoveBackpackCommand { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [title is valid].
        /// </summary>
        /// <value><c>true</c> if [title is valid]; otherwise, <c>false</c>.</value>
        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NewTripViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public NewTripViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            CandcelCommand = new RelayCommand(() => NavigationService.GoBack());

            ImageDeviceCommand = new RelayCommand(async () =>
            {
                await PickLocalImageAsync();
            });

            NextCommand = new NetworkErrorHandlingRelayCommand<bool, TripsMainPage>(async param =>
            {
                await AddTripAsync();
            }, PopUpService, param => param);
        }
        #endregion

        #region private add methods
        /// <summary>
        /// add trip as an asynchronous operation.
        /// </summary>
        private async Task AddTripAsync()
        {
            if (localImage == null)
                await AddTripRequestAsync();
            else
                await AddTripAndImageRequestAsync();
        }

        /// <summary>
        /// add trip request as an asynchronous operation.
        /// </summary>
        private async Task AddTripRequestAsync()
        {
            if (await tripsDataAccess.AddAsync(Trip))
                NavigationService.Navigate(typeof(SelectBackpacksPage), Trip);
            else
                await PopUpService.ShowCouldNotSaveAsync(Trip.Title);
        }

        /// <summary>
        /// add trip and image request as an asynchronous operation.
        /// </summary>
        private async Task AddTripAndImageRequestAsync()
        {
            var randomImageName = GenerateImageName();
            Trip.ImageStringName = randomImageName;

            if (await tripsDataAccess.AddAsync(Trip) && await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                NavigationService.Navigate(typeof(SelectBackpacksPage), Trip);
            else
                await PopUpService.ShowCouldNotSaveAsync(Trip.Title);
        }
        #endregion

        /// <summary>
        /// pick local image as an asynchronous operation.
        /// </summary>
        private async Task PickLocalImageAsync()
        {
            localImage = await FileService.GetImageFromDeviceAsync();

            if (localImage == null)
                return;

            TripImage = await FileService.StorageFileToBitmapImageAsync(localImage);
        }
    }
}
