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
    public class NewTripViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private StorageFile localImage;
        private BitmapImage tripImage;
        private bool titleIsValid;

        public Trip Trip { get; set; } = new Trip { Title="", Description="", Destination=""};
        public BitmapImage TripImage
        {
            get => tripImage;
            set => Set(ref tripImage, value);
        }

        public ICommand CandcelCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand ImageDeviceCommand { get; set; }
        public ICommand AddBackpackCommand { get; set; }
        public ICommand RemoveBackpackCommand { get; set; }

        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }

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

        private async Task AddTripAsync()
        {
            if (localImage == null)
                await AddTripRequestAsync();
            else
                await AddTripAndImageRequestAsync();
        }

        private async Task AddTripRequestAsync()
        {
            if (await tripsDataAccess.AddAsync(Trip))
                NavigationService.Navigate(typeof(SelectBackpacksPage), Trip);
            else
                await PopUpService.ShowCouldNotSaveAsync(Trip.Title);
        }

        private async Task AddTripAndImageRequestAsync()
        {
            var randomImageName = GenerateImageName();
            Trip.ImageStringName = randomImageName;

            if (await tripsDataAccess.AddAsync(Trip) && await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                NavigationService.Navigate(typeof(SelectBackpacksPage), Trip);
            else
                await PopUpService.ShowCouldNotSaveAsync(Trip.Title);
        }

        private async Task PickLocalImageAsync()
        {
            localImage = await FileService.GetImageFromDeviceAsync();

            if (localImage == null)
                return;

            TripImage = await FileService.StorageFileToBitmapImageAsync(localImage);
        }
    }
}
