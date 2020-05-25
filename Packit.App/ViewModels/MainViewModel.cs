using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.Model;
using Windows.UI.Xaml.Media.Imaging;
using System.Linq;
using Packit.App.Wrappers;
using Microsoft.Toolkit.Uwp.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model.Models;

namespace Packit.App.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ICustomTripDataAccess customTripDataAccess = new CustomTripDataAccessFactory().Create();
        private readonly IBasicDataAccess<Check> checksDataAccess = new BasicDataAccessFactory<Check>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;
        private Trip nextTrip;
        private BitmapImage tripImage;
        private bool hasNextTrip;

        public bool HasNextTrip { get => hasNextTrip; set => Set(ref hasNextTrip, value); }

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<MainPage>(async () => await LoadDataAsync(), PopUpService));
        public ICommand ItemCheckedCommand { get; set; }
        public ICommand TripDetailsCommand { get; set; }
        public ICommand NewTripCommand { get; set; }

        public Trip NextTrip { get => nextTrip; set => Set(ref nextTrip, value); }
        public BitmapImage TripImage { get => tripImage; set => Set(ref tripImage, value); }
        public ObservableCollection<BackpackWithItemsWithImages> BackspackWithItemsWithImages { get; } = new ObservableCollection<BackpackWithItemsWithImages>();

        public MainViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            ItemCheckedCommand = new RelayCommand<ItemBackpackBoolWrapper>(async param => await CheckItemAsync(param));

            TripDetailsCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(DetailTripV2Page), new TripImageWeatherLink(NextTrip) { Image = TripImage });
            });

            NewTripCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(NewTripPage));
            });
        }

        private async Task LoadDataAsync()
        {
            await LoadTripAsync();

            if (!HasNextTrip)
                return;

            await LoadTripImage();

            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    LoadBackpacks();
                    LoadItems();
                });
            });

            await LoadItemImagesAsync();
            await LoadItemImagesAsync();
            await LoadTripImage();
        }

        private async Task CheckItemAsync(ItemBackpackBoolWrapper param)
        {
            var itemcheker = new ItemChecker
                (
                    param.Item,
                    param.BackpackWithItemsWithImages.Backpack,
                    NextTrip,
                    param.IsChecked,
                    PopUpService,
                    checksDataAccess
                );

            await itemcheker.HandleItemCheck();
        }

        private void LoadBackpacks()
        {
            foreach (var backpackTrip in NextTrip.Backpacks)
                BackspackWithItemsWithImages.Add(new BackpackWithItemsWithImages(backpackTrip.Backpack));
        }

        private async Task LoadTripAsync()
        {
            var response = await customTripDataAccess.GetNextTrip();

            if (response.Item1)
            {
                NextTrip = response.Item2;
                HasNextTrip = true;
            }
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
            foreach (var itemImageLink in from bwiwi in BackspackWithItemsWithImages
                                          from itemImageLink in bwiwi.ItemImageLinks
                                          select itemImageLink)
            {
                itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
            }
        }
    }
}
