using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using Windows.UI.Xaml.Media.Imaging;
using System.Linq;
using Packit.App.Wrappers;
using Microsoft.Toolkit.Uwp.Helpers;

namespace Packit.App.ViewModels
{
    public class MainViewModel : Observable
    {
        private readonly ICustomTripDataAccess customTripDataAccess = new CustomTripDataAccessFactory().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;
        private Trip nextTrip;
        private BitmapImage tripImage;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public ICommand ItemCheckedCommand { get; set; }
        public Trip NextTrip { get => nextTrip; set => Set(ref nextTrip, value); }
        public BitmapImage TripImage { get => tripImage; set => Set(ref tripImage, value); }
        public ObservableCollection<BackpackWithItemsWithImages> BackspackWithItemsWithImages { get; } = new ObservableCollection<BackpackWithItemsWithImages>();

        public MainViewModel()
        {
            ItemCheckedCommand = new RelayCommand<ItemBackpackBoolWrapper>(param =>
            {
                var fdgdfg = param;
            });
        }

        private async Task LoadDataAsync()
        {
            await LoadTripAsync();
            await LoadTripImage();
            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    LoadBackpacks();
                    LoadItems();
                });

            }); ;
            await LoadItemImagesAsync();
        }

        private void LoadBackpacks()
        {
            foreach (var backpackTrip in NextTrip.Backpacks)
                BackspackWithItemsWithImages.Add(new BackpackWithItemsWithImages(backpackTrip.Backpack));
        }

        private async Task LoadTripAsync()
        {
            NextTrip = await customTripDataAccess.GetNextTrip();
        }

        private async Task LoadTripImage()
        {
            TripImage = await imagesDataAccess.GetImageAsync(NextTrip.ImageStringName, "ms-appx:///Assets/generictrip.jpg");
        }

        

        private void LoadItems()
        {
            foreach (var bwiwi in BackspackWithItemsWithImages)
            {
                foreach (var itemBackpack in bwiwi.Backpack.Items)
                {
                    foreach (var check in from check in itemBackpack.Item.Checks
                                          where bwiwi.Backpack.BackpackId == check.BackpackId && itemBackpack.Item.ItemId == check.ItemId && NextTrip.TripId == check.TripId
                                          select check)
                    {
                        itemBackpack.Item.Check = check;
                        itemBackpack.Item.Check.IsChecked = true;
                    }

                    bwiwi.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });
                }
            }
        }

        private async Task LoadItemImagesAsync()
        {
            foreach (var bwiwi in BackspackWithItemsWithImages)
            {
                foreach (var itemImageLink in bwiwi.ItemImageLinks)
                {
                    itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
                }
            }
        }
    }
}
