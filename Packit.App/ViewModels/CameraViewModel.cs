using System;
using System.Windows.Input;

using Packit.App.EventHandlers;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model.NotifyPropertyChanged;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ViewModels
{
    public class CameraViewModel : Observable
    {
        private ICommand _photoTakenCommand;
        private BitmapImage _photo;

        public BitmapImage Photo
        {
            get { return _photo; }
            set { Set(ref _photo, value); }
        }

        public ICommand PhotoTakenCommand => _photoTakenCommand ?? (_photoTakenCommand = new RelayCommand<CameraControlEventArgs>(OnPhotoTaken));

        private void OnPhotoTaken(CameraControlEventArgs args)
        {
            if (string.IsNullOrEmpty(args.Photo))
                return;

            NavigationService.Navigate(typeof(ImageToItemsPage), new BitmapImage(new Uri(args.Photo)));
        }
    }
}
