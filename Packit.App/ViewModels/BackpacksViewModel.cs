using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.Wrappers;
using Packit.Model;

namespace Packit.App.ViewModels
{
    public class BackpacksViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Backpack> backpacksDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private bool isVisible;
        private ICommand loadedCommand;

        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));

        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand AddItemsCommand { get; set; }
        public ICommand ShareBackpackCommand { get; set; }
        public ObservableCollection<BackpackWithItemsWithImages> BackpackWithItemsWithImagess { get; } = new ObservableCollection<BackpackWithItemsWithImages>();
        public Trip NewTrip { get; set; }
        public TripImageWeatherLink SelectedTripImageWeatherLink { get; set; }

        public BackpacksViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            EditCommand = new RelayCommand(() => IsVisible = !IsVisible);

            DeleteCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
               await PopUpService.ShowDeleteDialogAsync(DeleteBackpackAsync, param, param.Backpack.Title);
            }, param => param != null);

            NewCommand = new RelayCommand(() =>
            {
                if(NewTrip != null)
                {
                    NavigationService.Navigate(typeof(NewBackpackPage), NewTrip);
                }
                if (SelectedTripImageWeatherLink != null)
                {
                    NavigationService.Navigate(typeof(NewBackpackPage), SelectedTripImageWeatherLink.Trip);
                }
                else
                    NavigationService.Navigate(typeof(NewBackpackPage));
            });

            RemoveItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(param.BackpackWithItemsWithImages.Backpack.BackpackId, param.ItemImageLink.Item.ItemId))
                    param.BackpackWithItemsWithImages.ItemImageLinks.Remove(param.ItemImageLink);
            });

            DeleteItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                if (await itemsDataAccess.DeleteAsync(param.ItemImageLink.Item))
                    param.BackpackWithItemsWithImages.ItemImageLinks.Remove(param.ItemImageLink);
            });

            AddItemsCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                NavigationService.Navigate(typeof(SelectItemsPage), param.Backpack);
            });

            ShareBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                var test = param;
            });

        }

        private async Task LoadDataAsync()
        {
            try
            {
                await LoadBackpacksAsync();
                await LoadItemImagesAsync();
            }
            catch (HttpRequestException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<BackpacksPage>(NavigationService.Navigate, ex);
            }
        }

        private async Task DeleteBackpackAsync(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (await backpacksDataAccess.DeleteAsync(backpackWithItemsWithImages.Backpack))
                BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
        }

        private async Task DeleteItemAndImageRequestAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item) && await imagesDataAccess.DeleteImageAsync(itemImageLink.Item.ImageStringName))
            {

            }
                //ItemImageLinks.Remove(itemImageLink);
        }

        protected virtual async Task LoadBackpacksAsync()
        {
            var backpacksWithItems = await backpacksDataAccess.GetAllWithChildEntitiesAsync();

            foreach (var backpack in backpacksWithItems)
            {
                var backpackWithItemsWithImages = new BackpackWithItemsWithImages(backpack);

                  foreach (var itemBackpack in backpack.Items)
                    backpackWithItemsWithImages.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });

                BackpackWithItemsWithImagess.Add(backpackWithItemsWithImages);
            }
        }

        private async Task LoadItemImagesAsync()
        {
            foreach (var bwiwi in BackpackWithItemsWithImagess)
            {
                foreach (var itemImageLink in bwiwi.ItemImageLinks)
                    itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
            }
        }
    }
}
