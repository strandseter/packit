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
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private bool isSuccess = true;

        public override ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public TripImageWeatherLink SelectedTrip { get; set; }
        public Trip NewTrip { get; set; }
        public Backpack SelectedBackpack { get; set; }
        public BackpackWithItemsWithImages SelectedBackpackWithItemsWithImages { get; set; }
        public Backpack NewBackpack { get; set; }
        public ICommand DoneSelectingItemsCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public SelectItemsViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            DoneSelectingItemsCommand = new RelayCommand<IList<object>>(async param => await SaveChangesAndNavigate(param.ToList()));

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
                    FilterItemsCollection();
                });
            });
        }

        private async Task AddItemsToExistingBackpackWithItemsWithImages(ItemImageLink itemImageLink)
        {
            try
            {
                if (!await backpackDataAccess.AddEntityToEntityAsync(SelectedBackpackWithItemsWithImages.Backpack.BackpackId, itemImageLink.Item.ItemId))
                    isSuccess = false;
            }
            catch (NetworkConnectionException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<SelectItemsPage>(NavigationService.Navigate, ex);

            }
            catch (HttpRequestException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<SelectItemsPage>(NavigationService.Navigate, ex);
            }
        }

        private async Task AddItemsToNewBackpack(ItemImageLink itemImageLink)
        {
            try
            {
                if (!await backpackDataAccess.AddEntityToEntityAsync(NewBackpack.BackpackId, itemImageLink.Item.ItemId))
                    isSuccess = false;
            }
            catch (NetworkConnectionException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<SelectItemsPage>(NavigationService.Navigate, ex);
            }
            catch (HttpRequestException ex)
            {
                await PopUpService.ShowCouldNotLoadAsync<SelectItemsPage>(NavigationService.Navigate, ex);
            }
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

        private void FilterItemsCollection()
        {
            if (SelectedBackpackWithItemsWithImages == null)
                return;

            foreach (var itemImageLinkOld in ItemImageLinks.ToList())
            {
                foreach (var itemImageLinkNew in SelectedBackpackWithItemsWithImages.ItemImageLinks)
                {
                    if (itemImageLinkOld.Item.ItemId == itemImageLinkNew.Item.ItemId)
                        ItemImageLinks.Remove(itemImageLinkOld);
                }
            }
        }

        private async Task SaveChangesAndNavigate(IList<object> selectedItems)
        {
            if (NewTrip != null && NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                {
                    NavigationService.Navigate(typeof(SelectBackpacksPage), NewTrip);
                    return;
                }
            }

            if (NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                {
                    NavigationService.Navigate(typeof(BackpacksPage));
                }
            }

            if (SelectedBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpack((ItemImageLink)obj);

                if (isSuccess)
                {
                    NavigationService.Navigate(typeof(BackpacksPage));
                }
            }

            if (SelectedBackpackWithItemsWithImages != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpackWithItemsWithImages((ItemImageLink)obj);

                if (isSuccess)
                {
                    await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripV2Page), SelectedTrip);
                }
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
