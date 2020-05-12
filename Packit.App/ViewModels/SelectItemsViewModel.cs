using System;
using System.Collections;
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
    public class SelectItemsViewModel : ItemsViewModel
    {
        private ICommand loadedCommand;
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();

        public override ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadDataAsync));
        public TripImageWeatherLink SelectedTrip { get; set; }
        public BackpackWithItems SelectedBackpackWithItems { get; set; }
        public ICommand DoneSelectingItemsCommand { get; set; }

        public SelectItemsViewModel()
        {
            DoneSelectingItemsCommand = new RelayCommand<IList<object>>(async param =>
            {
                //This is a workaround. It is not possible to bind readonly "SelectedItems" in multiselect grid/list-view.
                List<object> selectedItems = param.ToList();

                bool isSuccess = true;

                foreach (var obj in selectedItems)
                {
                    var itemImageLink = (ItemImageLink)obj;

                    if (!await backpackDataAccess.AddEntityToEntityAsync(SelectedBackpackWithItems.Backpack.BackpackId, itemImageLink.Item.ItemId))
                        isSuccess = false;
                }

                if (isSuccess)
                {
                    var updatedTrip = await tripssDataAccess.GetByIdWithChildEntitiesAsync(SelectedTrip.Trip);
                    SelectedTrip.Trip = updatedTrip;
                    NavigationService.Navigate(typeof(DetailTripV2Page), SelectedTrip);
                }
            });
        }


        private async void LoadDataAsync() //TODO: Refactor?
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
        }

        private async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAllAsync();

            foreach (var i in items)
            {
                var itemImageLink = new ItemImageLink() { Item = i };
                ItemImageLinks.Add(itemImageLink);

                foreach (var item in SelectedBackpackWithItems.Items)
                    if (i.ItemId == item.ItemId)
                        ItemImageLinks.Remove(itemImageLink);
            }
        }

        private async Task LoadImagesAsync()
        {
            foreach (var iml in ItemImageLinks)
                iml.Image = await imagesDataAccess.GetImageAsync(iml.Item.ImageStringName);
        }

        public void Initialize(BackpackTripWrapper backpackTrip)
        {
            SelectedBackpackWithItems = backpackTrip?.Backpack;
            SelectedTrip = backpackTrip?.Trip;
        }
    }
}
