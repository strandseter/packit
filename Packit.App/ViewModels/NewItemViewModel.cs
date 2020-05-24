using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.Model;
using Windows.Storage;

namespace Packit.App.ViewModels
{
    public class NewItemViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private StorageFile localImage;
        private bool titleIsValid;

        public ItemImageLink ItemImageLink { get; set; } = new ItemImageLink() { Item = new Item() { Title = "", Description = ""} };

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ImageDeviceCommand { get; set; }
        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }

        public NewItemViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            SaveCommand = new RelayCommand<bool>(async param =>
            {
                if (localImage != null)
                {
                    var randomImageName = GenerateImageName();

                    ItemImageLink.Item.ImageStringName = randomImageName;

                    if (await itemsDataAccess.AddAsync(ItemImageLink.Item) && await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                        NavigationService.GoBack();
                }

                if (await itemsDataAccess.AddAsync(ItemImageLink.Item))
                {
                    NavigationService.GoBack();
                }
            }, param => param);

            ImageDeviceCommand = new RelayCommand(async () =>
            {
                localImage = await FileService.GetImageFromDeviceAsync();

                if (localImage is null)
                    return;

                ItemImageLink.Image = await FileService.StorageFileToBitmapImageAsync(localImage);
            });
        }
    }
}
