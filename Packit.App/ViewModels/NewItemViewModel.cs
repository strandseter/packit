using System;
using System.Linq;
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

namespace Packit.App.ViewModels
{
    public class NewItemViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Model.Item> itemsDataAccess = new BasicDataAccessFactory<Model.Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private StorageFile localImage;

        public DataLinks.ItemImageLink ItemImageLink { get; set; } = new DataLinks.ItemImageLink() { Item = new Model.Item() };

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ImageDeviceCommand { get; set; }

        public NewItemViewModel()
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            SaveCommand = new RelayCommand(async () =>
            {
                if (localImage != null)
                {
                    var randomImageName = GenerateImageName();

                    ItemImageLink.Item.ImageStringName = randomImageName;

                    if (await itemsDataAccess.AddAsync(ItemImageLink.Item) && await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                        NavigationService.GoBack();
                }
                    if (await itemsDataAccess.AddAsync(ItemImageLink.Item))
                    NavigationService.GoBack();
            });

            ImageDeviceCommand = new RelayCommand(async () =>
            {
                localImage = await FileService.GetImageFromDevice();

                if (localImage is null)
                    return;

                ItemImageLink.Image = await FileService.StorageFileToBitmapImageAsync(localImage);
            });

        }

        
    }
}
