using System;
using System.IO;
using System.Net.Http;
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
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ViewModels
{
    public class EditItemViewModel : Observable
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private StorageFile localImage;

        private ItemImageLink EditedItemImageLink { get; set; }
        public ItemImageLink ItemImageLink { get; set;}
        
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CameraCommand { get; set; }
        public ICommand DeviceCommand { get; set; }

        public EditItemViewModel()
        {
            CancelCommand = new RelayCommand(() => NavigationService.Navigate(typeof(ItemsPage)));

            SaveCommand = new RelayCommand<ItemImageLink>(async param =>
                                                        {
                                                            //if (localImage is null)
                                                            //    return;

                                                            try
                                                            {
                                                                if (await itemsDataAccess.UpdateAsync(param.Item) && await imagesDataAccess.AddImageAsync(localImage, "bla"))
                                                                {
                                                                    NavigationService.Navigate(typeof(ItemsPage));
                                                                }
                                                                    
                                                                else
                                                                    DialogService.CouldNotSaveChanges();
                                                            }
                                                            catch(HttpRequestException ex)
                                                            {
                                                                DialogService.CouldNotSaveChanges(ex);
                                                            }
                                                            catch(Exception ex)
                                                            {
                                                                DialogService.UnknownErrorOccurred(ex);
                                                            }
                                                        });

            DeviceCommand = new RelayCommand(async () =>
                                            {
                                                localImage = await FileService.GetImageFromDevice();

                                                if (localImage is null)
                                                    return;

                                                ItemImageLink.Image = await FileService.StorageFileToBitmapImageAsync(localImage);
                                            });
        }

        public void Initialize(ItemImageLink itemImageLink) => ItemImageLink = itemImageLink;
    }
}
