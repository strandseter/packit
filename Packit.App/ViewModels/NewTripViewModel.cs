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

        public Trip Trip { get; set; } = new Trip();
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

        public NewTripViewModel()
        {
            CandcelCommand = new RelayCommand(() => NavigationService.GoBack());

            ImageDeviceCommand = new RelayCommand(async () =>
            {
                localImage = await FileService.GetImageFromDeviceAsync();

                if (localImage == null)
                    return;

                TripImage = await FileService.StorageFileToBitmapImageAsync(localImage);
            });

            NextCommand = new RelayCommand<bool>(async param =>
            {
                if (localImage != null)
                {
                    var randomImageName = GenerateImageName();
                    Trip.ImageStringName = randomImageName;

                    if (!await tripsDataAccess.AddAsync(Trip) || !await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                    {
                        await PopUpService.ShowCouldNotSaveChangesAsync($"{Trip.Title} or {nameof(localImage)}");
                        return;
                    }

                    NavigationService.Navigate(typeof(SelectBackpacksPage), Trip);
                    return;
                }

                if (!await tripsDataAccess.AddAsync(Trip))
                {
                    await PopUpService.ShowCouldNotSaveChangesAsync(Trip.Title);
                    return;
                }

                NavigationService.Navigate(typeof(SelectBackpacksPage), Trip);
            }, param => param);
        }
    }
}
