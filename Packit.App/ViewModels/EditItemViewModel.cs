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
using Packit.Model.NotifyPropertyChanged;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ViewModels
{
    public class EditItemViewModel : Observable
    {
        private readonly IBasicDataAccess<Model.Item> itemsDataAccess = new BasicDataAccessFactory<Model.Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private StorageFile localImage;

        private DataLinks.ItemImageLink EditedItemImageLink { get; set; }
        public DataLinks.ItemImageLink ItemImageLink { get; set;}
        
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CameraCommand { get; set; }
        public ICommand DeviceCommand { get; set; }

        public EditItemViewModel()
        {
            CancelCommand = new RelayCommand(() => NavigationService.Navigate(typeof(ItemsPage)));

            SaveCommand = new RelayCommand<DataLinks.ItemImageLink>(async param =>
                                                        {
                                                            //if (localImage is null)
                                                            //    return;

                                                            try
                                                            {
                                                                if (await itemsDataAccess.UpdateAsync(param.Item) && await imagesDataAccess.AddImageAsync(localImage, "bla"))
                                                                {
                                                                    NavigationService.Navigate(typeof(ItemsPage));
                                                                }
                                                                    
                                       
                                                            }
                                                            catch(HttpRequestException ex)
                                                            {
                                                            }
                                                            catch(Exception ex)
                                                            {
                                                            }
                                                        });

            DeviceCommand = new RelayCommand(async () =>
                                            {
                                                localImage = await FileService.GetImageFromDeviceAsync();

                                                if (localImage is null)
                                                    return;

                                                ItemImageLink.Image = await FileService.StorageFileToBitmapImageAsync(localImage);
                                            });
        }

        public void Initialize(DataLinks.ItemImageLink itemImageLink) => ItemImageLink = itemImageLink;
    }
}
