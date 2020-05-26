using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.Helpers;
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
    public class SelectBackpacksViewModel : BackpacksViewModel
    {
        private ICommand loadedCommand;
        private readonly IRelationDataAccess<Trip, Backpack> backpackRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private bool isSuccess = true;
        private bool backpacksIsFiltered;

        public ObservableCollection<BackpackWithItemsWithImages> SelectedBackpacks { get; set; }
        public override ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public ICommand DoneSelectingBackpacksCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public bool BackpacksIsFiltered { get => backpacksIsFiltered; set => Set(ref backpacksIsFiltered, value); }

        public SelectBackpacksViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            DoneSelectingBackpacksCommand = new RelayCommand<IList<object>>(async param =>
            {
                //This is a workaround. It is not possible to bind readonly "SelectedItems" in multiselect grid/list-view.
                List<object> selectedItems = param.ToList();

                if (SelectedTripImageWeatherLink != null)
                {
                    foreach (var obj in selectedItems)
                        await AddBackpackToExistingTrip((BackpackWithItemsWithImages)obj);

                    if (isSuccess)
                    {
                        await UpdateSelectedTrip();
                        NavigationService.Navigate(typeof(DetailTripV2Page), SelectedTripImageWeatherLink);
                    }
                }

                if (NewTrip != null)
                {
                    foreach (var obj in selectedItems)
                        await AddBackpackToNewtrip((BackpackWithItemsWithImages)obj);

                    if (isSuccess)
                        NavigationService.Navigate(typeof(TripsMainPage));
                }
            });

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());
        }

        private async Task LoadDataAsync()
        {
            await LoadBackpacksAsync();
            await LoadItemImagesAsync();
            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    FilterBackpacks();
                });
            });
        }

        private async Task AddBackpackToNewtrip(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (!await backpackRelationDataAccess.AddEntityToEntityAsync(NewTrip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
                isSuccess = false;
        }

        private async Task AddBackpackToExistingTrip(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (!await backpackRelationDataAccess.AddEntityToEntityAsync(SelectedTripImageWeatherLink.Trip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
                isSuccess = false;
        }

        private async Task UpdateSelectedTrip()
        {
            var updatedTrip = await tripssDataAccess.GetByIdWithChildEntitiesAsync(SelectedTripImageWeatherLink.Trip);
            SelectedTripImageWeatherLink.Trip = updatedTrip;
        }

        private void FilterBackpacks()
        {
            if (SelectedTripImageWeatherLink == null)
            {
                BackpacksIsFiltered = true;
                return;
            }

            foreach (var backpackWithItemsWithImages in BackpackWithItemsWithImagess.ToList())
            {
                foreach (var backbackTrip in SelectedTripImageWeatherLink.Trip.Backpacks)
                {
                    if (backpackWithItemsWithImages.Backpack.BackpackId == backbackTrip.BackpackId)
                    {
                        BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
                    }
                }
            }
            BackpacksIsFiltered = true;
        }

        internal void Initialize(BackpackWithItemsWithImagesTripWrapper  backpackWithItemsWithImagesTripWrapper)
        {
            SelectedTripImageWeatherLink = backpackWithItemsWithImagesTripWrapper.TripImageWeatherLink;
            SelectedBackpacks = backpackWithItemsWithImagesTripWrapper.BackpackWithItemsWithImages;
        }

        internal void Initialize(TripImageWeatherLink tripImageWeatherLink)
        {
            SelectedTripImageWeatherLink = tripImageWeatherLink;
        }

        internal void Initialize(Trip trip) => NewTrip = trip;
    }
}
