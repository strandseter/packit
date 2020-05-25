using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class SelectBackpacksViewModel : BackpacksViewModel
    {
        private readonly IRelationDataAccess<Trip, Backpack> backpackRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Backpack> backpacksDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private bool isSuccess = true;

        public ObservableCollection<BackpackWithItemsWithImages> SelectedBackpacks { get; set; } 

        public ICommand DoneSelectingBackpacksCommand { get; set; }
        public ICommand CancelCommand { get; set; }

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

        protected override async Task LoadBackpacksAsync()
        {
            var backpacksWithItems = await backpacksDataAccess.GetAllWithChildEntitiesAsync();

            foreach (var backpack in backpacksWithItems)
            {
                var backpackWithItemsWithImages = new BackpackWithItemsWithImages(backpack);

                BackpackWithItemsWithImagess.Add(backpackWithItemsWithImages);

                foreach (var itemBackpack in backpack.Items)
                {
                    itemBackpack.Item.Checks.Clear();
                    backpackWithItemsWithImages.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });
                }

                if (SelectedBackpacks == null)
                    continue;

                foreach (var b in SelectedBackpacks)
                    if (b.Backpack.BackpackId == backpack.BackpackId)
                        BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
            }
        }

        internal void Initialize(BackpackWithItemsWithImagesTripWrapper  backpackWithItemsWithImagesTripWrapper)
        {
            SelectedTripImageWeatherLink = backpackWithItemsWithImagesTripWrapper.TripImageWeatherLink;
            SelectedBackpacks = backpackWithItemsWithImagesTripWrapper.BackpackWithItemsWithImages;
        }

        internal void Initialize(Trip trip) => NewTrip = trip;
    }
}
