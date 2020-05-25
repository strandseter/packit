using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class NewItemViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private StorageFile localImage;
        private bool titleIsValid;

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ImageDeviceCommand { get; set; }

        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }

        public ItemImageLink ItemImageLink { get; set; } = new ItemImageLink() { Item = new Item() { Title = "", Description = "" } };

        public NewItemViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            SaveCommand = new NetworkErrorHandlingRelayCommand<bool, ItemsPage>(async param =>
            {
               await AddItemAsync();
            }, PopUpService, param => param);

            ImageDeviceCommand = new RelayCommand(async () =>
            {
                await PickLocalImageAsync();
            });
        }

        private async Task AddItemAsync()
        {
            if (localImage == null)
                await AddItemRequestAsync();
            else
                await AddItemAndImagerequestAsync();
        }

        private async Task AddItemRequestAsync()
        {
            if (await itemsDataAccess.AddAsync(ItemImageLink.Item))
                NavigationService.GoBack();
            else
                await PopUpService.ShowCouldNotSaveAsync(ItemImageLink.Item.Title);
        }

        private async Task AddItemAndImagerequestAsync()
        {
            var randomImageName = GenerateImageName();
            ItemImageLink.Item.ImageStringName = randomImageName;

            if (await itemsDataAccess.AddAsync(ItemImageLink.Item) && await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                NavigationService.GoBack();
            else
                await PopUpService.ShowCouldNotSaveAsync(ItemImageLink.Item.Title);
        }

        private async Task PickLocalImageAsync()
        {
            localImage = await FileService.GetImageFromDeviceAsync();

            if (localImage == null)
                return;

            ItemImageLink.Image = await FileService.StorageFileToBitmapImageAsync(localImage);
        }
    }
}
