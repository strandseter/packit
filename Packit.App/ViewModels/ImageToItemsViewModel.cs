
using Packit.App.DataAccess.Http;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model.NotifyPropertyChanged;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ViewModels
{
    public class ImageToItemsViewModel : Observable
    {
        private readonly ImageToTextDataAccess imageToTextDataAccess = new ImageToTextDataAccess();

        public BitmapImage CameraImage { get; set; } 
        public ICommand SelectNewImageCommand { get; set; }
        public ICommand ScanCommand { get; set; }

        public ImageToItemsViewModel()
        {
            SelectNewImageCommand = new RelayCommand(() => NavigationService.Navigate(typeof(ScannerPage)));

            ScanCommand = new RelayCommand(async () =>
            {
                var imageBytes = FileService.BitmapImageToByteArray(CameraImage);

                var items = await imageToTextDataAccess.GetItemsFromImageAsync(imageBytes.Result);

                var fggg = 10;
            });
        }

        internal void Initialize(BitmapImage bitmapImage) => CameraImage = bitmapImage;
    }
}
