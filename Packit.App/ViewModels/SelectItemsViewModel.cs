using Microsoft.Toolkit.Uwp.Helpers;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.Wrappers;
using Packit.Exceptions;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App.ViewModels
{
    public class SelectItemsViewModel : ItemsViewModel
    {
        private ICommand loadedCommand;
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private bool isSuccess = true;
        private bool itemsIsFiltered;

        public override ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public TripImageWeatherLink SelectedTrip { get; set; }
        public Trip NewTrip { get; set; }
        public Backpack SelectedBackpack { get; set; }
        public BackpackWithItemsWithImages SelectedBackpackWithItemsWithImages { get; set; }
        public TripImageWeatherLink SelectedTripImageWeatherLink { get; set; }
        public Backpack NewBackpack { get; set; }
        public ICommand DoneSelectingItemsCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public bool ItemsIsFiltered { get => itemsIsFiltered; set => Set(ref itemsIsFiltered, value); }

        public SelectItemsViewModel(IPopUpService popUpService)
            : base(popUpService)
        {
            DoneSelectingItemsCommand = new NetworkErrorHandlingRelayCommand<IList<object>, ItemsPage>(async param => await SaveChangesAndNavigate(param.ToList()), PopUpService);

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());
        }

        private async Task LoadDataAsync()
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    FilterItems();
                });
            });
        }

        private async Task AddItemsToExistingBackpackWithItemsWithImages(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(SelectedBackpackWithItemsWithImages.Backpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }

        private async Task AddItemsToNewBackpack(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(NewBackpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }

        private async Task AddItemsToExistingBackpack(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(SelectedBackpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }

        private async Task UpdateSelectedTrip()
        {
            var updatedTrip = await tripssDataAccess.GetByIdWithChildEntitiesAsync(SelectedTrip.Trip);
            SelectedTrip.Trip = updatedTrip;
        }

        private void FilterItems()
        {
            if (SelectedBackpackWithItemsWithImages == null)
            {
                ItemsIsFiltered = true;
                return;
            }

            foreach (var itemImageLinkOld in ItemImageLinks.ToList())
            {
                foreach (var itemImageLinkNew in SelectedBackpackWithItemsWithImages.ItemImageLinks)
                {
                    if (itemImageLinkOld.Item.ItemId == itemImageLinkNew.Item.ItemId)
                        ItemImageLinks.Remove(itemImageLinkOld);
                }
            }
            ItemsIsFiltered = true;
        }

        private async Task SaveChangesAndNavigate(IList<object> selectedItems)
        {
            if (NewTrip != null && NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(TripsMainPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, NewBackpack.Title);

                return;
            }

            if (SelectedTrip != null && NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                {
                    await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripV2Page), SelectedTrip);
                }
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpackWithItemsWithImages.Backpack.Title);

                return;
            }

            if (NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(BackpacksPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, NewBackpack.Title);
            }

            if (SelectedBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpack((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(BackpacksPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpack.Title);
            }

            if (SelectedBackpackWithItemsWithImages != null && SelectedTrip != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpackWithItemsWithImages((ItemImageLink)obj);

                if (isSuccess)
                {
                    await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripV2Page), SelectedTrip);
                }
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpackWithItemsWithImages.Backpack.Title);

                return;
            }

            if (SelectedBackpackWithItemsWithImages != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpackWithItemsWithImages((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(BackpacksPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpackWithItemsWithImages.Backpack.Title);
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

        internal void Initialize(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            SelectedBackpackWithItemsWithImages = backpackWithItemsWithImages;
        }

        internal void Initialize(TripImageWeatherLinkWithBackpackWrapper tripImageWeatherLinkWithBackpackWrapper)
        {
            SelectedTrip = tripImageWeatherLinkWithBackpackWrapper.TripImageWeatherLink;
            NewBackpack = tripImageWeatherLinkWithBackpackWrapper.Backpack;
        }

        internal void Initialize(Backpack backpack)
        {
            NewBackpack = backpack;
        }
    }
}
