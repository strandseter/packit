using System;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Windows.Storage;

namespace Packit.App.ViewModels
{
    public class NewItemViewModel : Observable
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private StorageFile localImage;

        public ItemImageLink ItemImageLink { get; set; } = new ItemImageLink() { Item = new Item() };

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ImageDeviceCommand { get; set; }

        public NewItemViewModel()
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            SaveCommand = new RelayCommand(async () =>
            {
                //TODO: Error handling

                if (await itemsDataAccess.AddAsync(ItemImageLink.Item) && await imagesDataAccess.AddImageAsync(localImage))
                {
                    NavigationService.GoBack();
                }
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
