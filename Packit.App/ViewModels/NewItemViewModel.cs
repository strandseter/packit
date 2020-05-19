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
using Packit.App.InputValidation;
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
        private bool itemHasErrors;
        private string titleErrorMessage;
        private string descriptionErrorMessage;

        public ItemImageLink ItemImageLink { get; set; } = new ItemImageLink() { Item = new Item() };

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ImageDeviceCommand { get; set; }

        public bool ItemHasErrors
        {
            get => itemHasErrors;
            set => Set(ref itemHasErrors, value);
        }

        public string TitleErrorMessage
        {
            get => titleErrorMessage;
            set => Set(ref titleErrorMessage, value);
        }

        public string DescriptionErrorMessage
        {
            get => descriptionErrorMessage;
            set => Set(ref descriptionErrorMessage, value);
        }

        public NewItemViewModel()
        {
            ItemImageLink.Item.ErrorsChanged += Item_ErrorsChanged;

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            SaveCommand = new RelayCommand(async () =>
            {
                if (ItemHasErrors)
                    return;

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
                localImage = await FileService.GetImageFromDeviceAsync();

                if (localImage is null)
                    return;

                ItemImageLink.Image = await FileService.StorageFileToBitmapImageAsync(localImage);
            });
        }

        private void Item_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ItemHasErrors = ItemImageLink.Item.HasErrors;
            TitleErrorMessage = InputErrorMessage(ItemImageLink.Item.GetErrors(nameof(ItemImageLink.Item.Title)));
            DescriptionErrorMessage = InputErrorMessage(ItemImageLink.Item.GetErrors(nameof(ItemImageLink.Item.Description)));
        }
    }
}
