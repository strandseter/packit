using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.Wrappers;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App.ViewModels
{
    public class SelectItemsViewModel : ItemsViewModel
    {
        private ICommand loadedCommand;
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private bool isSuccess = true;

        public override ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public TripImageWeatherLink SelectedTrip { get; set; }
        public Trip NewTrip { get; set; }
        public BackpackWithItemsWithImages SelectedBackpackWithItemsWithImages { get; set; }
        public Backpack NewBackpack { get; set; }
        public ICommand DoneSelectingItemsCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public SelectItemsViewModel()
        {
            DoneSelectingItemsCommand = new RelayCommand<IList<object>>(async param =>
            {
                //This is a workaround. It is not possible to bind readonly "SelectedItems" in multiselect grid/list-view.
                List<object> selectedItems = param.ToList();

                if (NewBackpack != null)
                {
                    foreach (var obj in selectedItems)
                        await AddItemsToNewBackpack((ItemImageLink)obj);

                    if (isSuccess)
                    {
                        NavigationService.Navigate(typeof(BackpacksPage));
                    }
                }

                if (SelectedBackpackWithItemsWithImages != null)
                {
                    foreach (var obj in selectedItems)
                        await AddItemsToExistingBackpack((ItemImageLink)obj);

                    if (isSuccess)
                    {
                        await UpdateSelectedTrip();
                        NavigationService.Navigate(typeof(DetailTripV2Page), SelectedTrip);
                    }
                }

                if (NewTrip != null && NewBackpack != null)
                {
                    foreach (var obj in selectedItems)
                        await AddItemsToNewBackpack((ItemImageLink)obj);

                    if (isSuccess)
                    {
                        NavigationService.Navigate(typeof(SelectBackpacksPage), NewTrip);
                    }
                }
            });

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());
        }

        private async Task LoadDataAsync()
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
        }

        private async Task AddItemsToExistingBackpack(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(SelectedBackpackWithItemsWithImages.Backpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }

        private async Task AddItemsToNewBackpack(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(NewBackpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }

        private async Task UpdateSelectedTrip()
        {
            var updatedTrip = await tripssDataAccess.GetByIdWithChildEntitiesAsync(SelectedTrip.Trip);
            SelectedTrip.Trip = updatedTrip;
        }

        protected override async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAllAsync();

            foreach (var i in items)
            {
                var itemImageLink = new ItemImageLink() { Item = i };
                ItemImageLinks.Add(itemImageLink);

                if (SelectedBackpackWithItemsWithImages == null)
                    continue;

                foreach (var item in SelectedBackpackWithItemsWithImages.ItemImageLinks)
                    if (i.ItemId == item.Item.ItemId)
                        ItemImageLinks.Remove(itemImageLink);
            }
        }

        internal void Initialize(BackpackWithItemsTripImageWeatherWrapper backpackTrip)
        {
            SelectedBackpackWithItemsWithImages = backpackTrip?.Backpack;
            SelectedTrip = backpackTrip?.Trip;
        }

        internal void Initialize(BackpackTripWrapper backpackTripWrapper)
        {
            NewTrip = backpackTripWrapper.Trip;
            NewBackpack = backpackTripWrapper.Backpack;
        }

        internal void Initialize(Backpack backpack)
        {
            NewBackpack = backpack;
        }
    }
}
