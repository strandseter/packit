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

        public Trip SelectedTrip { get; set; }

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
                    //await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripV2Page), );
                }
            });

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());
        }

        private async Task AddBackpackToTrip(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (!await backpackRelationDataAccess.AddEntityToEntityAsync(SelectedTrip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
                isSuccess = false;
        }

        protected override async Task LoadBackpacksAsync()
        {
            var backpacksWithItems = await backpacksDataAccess.GetAllWithChildEntitiesAsync();

            foreach (var backpack in backpacksWithItems)
            {
                var backpackWithItemsWithImages = new BackpackWithItemsWithImages(backpack);

                foreach (var itemBackpack in backpack.Items)
                {
                    itemBackpack.Item.Checks.Clear();
                    backpackWithItemsWithImages.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });
                }

                BackpackWithItemsWithImagess.Add(backpackWithItemsWithImages);
            }
        }

        internal void Initialize(Trip trip) => SelectedTrip = trip;
    }
}
