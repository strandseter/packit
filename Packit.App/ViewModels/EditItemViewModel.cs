using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.Factory;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Windows.Storage.Streams;

namespace Packit.App.ViewModels
{
    public class EditItemViewModel : Observable
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().CreateBasicDataAccess();
        private readonly Images imagesDataAccess = new Images();
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
                                                            if (await itemsDataAccess.EditAsync(param.Item))
                                                                NavigationService.Navigate(typeof(ItemsPage));
                                                        });
            DeviceCommand = new RelayCommand(async () =>
                                                        {
                                                            try
                                                            {
                                                                if(await imagesDataAccess.AddImageAsync(await FilePickerService.GetFileFromDeviceAsync()))
                                                                    DialogService.ImageUploaded();
                                                            }
                                                            catch (FormatException ex)
                                                            {
                                                                DialogService.UnsoppurtedFileFormat(ex);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                DialogService.CouldNotLoadDataUknown(ex);
                                                            }
                                                        });
        }

        public void Initialize(ItemImageLink itemImageLink) => this.ItemImageLink = itemImageLink;
    }
}
