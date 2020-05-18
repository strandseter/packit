using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;

namespace Packit.App.ViewModels
{
    public class SelectBackpacksViewModel : BackpacksViewModel
    {
        private readonly IRelationDataAccess<Trip, Backpack> backpackRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Backpack> backpacksDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private bool isSuccess = true;

        public TripImageWeatherLink SelectedTrip { get; set; }

        public ICommand DoneSelectingBackpacksCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public SelectBackpacksViewModel()
        {
            DoneSelectingBackpacksCommand = new RelayCommand<IList<object>>(async param =>
            {
                //This is a workaround. It is not possible to bind readonly "SelectedItems" in multiselect grid/list-view.
                List<object> selectedItems = param.ToList();

                foreach (var obj in selectedItems)
                    await AddBackpackToTrip((BackpackWithItemsWithImages)obj);

                if (isSuccess)
                {
                    await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripV2Page), SelectedTrip);
                }
            });

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());
        }

        private async Task AddBackpackToTrip(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (!await backpackRelationDataAccess.AddEntityToEntityAsync(SelectedTrip.Trip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
                isSuccess = false;
        }

        private async Task UpdateSelectedTrip()
        {
            var updatedTrip = await tripssDataAccess.GetByIdWithChildEntitiesAsync(SelectedTrip.Trip);
            SelectedTrip.Trip = updatedTrip;
        }

        protected override async Task LoadBackpacksAsync()
        {
            var backpacksWithItems = await backpacksDataAccess.GetAllWithChildEntitiesAsync();

            foreach (var backpack in backpacksWithItems)
            {
                var backpackWithItemsWithImages = new BackpackWithItemsWithImages(backpack);

                foreach (var itemBackpack in backpack.Items)
                    backpackWithItemsWithImages.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });

                BackpackWithItemsWithImagess.Add(backpackWithItemsWithImages);

                foreach (var b in SelectedTrip.Trip.Backpacks)
                {
                    if (b.BackpackId == backpack.BackpackId)
                        BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
                }
            }
        }
    }
}
