using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Packit.App.ViewModels
{
    public class TripsMainViewModel : Observable
    {
        private IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private IRelationDataAccess<Trip, Backpack> backpacsDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private IRelationDataAccess<Backpack, Item> itemsDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;

        public TripBackpackItemLink SelectedItem { get; set; }
        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadDataAsync));
        public ICommand TripDetailCommand { get; set; }

        public ObservableCollection<TripBackpackItemLink> TripBackpackItemLinks { get; } = new ObservableCollection<TripBackpackItemLink>();

        public TripsMainViewModel()
        {
            TripDetailCommand = new RelayCommand<TripBackpackItemLink>(param =>
            {
                NavigationService.Navigate(typeof(DetailTripPage), param);
            });
    }

        private async void LoadDataAsync()
        {
            await LoadAllAsync();
            await LoadTripImagesAsync();
        }

        private async Task LoadAllAsync()
        {
            await LoadTripsAsync();

            foreach (TripBackpackItemLink tbil in TripBackpackItemLinks)
            {
                await LoadBackpacksAsync(tbil);

                foreach (BackpackItemLink bil in tbil.BackpackItems)
                    await LoadItemsAsync(bil);
            }
        }

        private async Task LoadTripsAsync()
        {
            var trips = await tripsDataAccess.GetAllAsync();

            foreach (Trip t in trips)
                TripBackpackItemLinks.Add(new TripBackpackItemLink() { Trip = t });
        }

        private async Task LoadBackpacksAsync(TripBackpackItemLink tbil)
        {
            var backpacks = await backpacsDataAccess.GetEntitiesInEntityAsync(tbil.Trip.TripId, "backpacks");

            foreach (Backpack b in backpacks)
                tbil.BackpackItems.Add(new BackpackItemLink() { Backpack = b });
        }

        private async Task LoadItemsAsync(BackpackItemLink bil)
        {
            var items = await itemsDataAccess.GetEntitiesInEntityAsync(bil.Backpack.BackpackId, "items");

            foreach (Item i in items)
                bil.ItemImageLinks.Add(new ItemImageLink() { Item = i });
        }

        private async Task LoadItemImagesAsync()
        {

        }

        private async Task LoadTripImagesAsync()
        {
            foreach (TripBackpackItemLink tbil in TripBackpackItemLinks)
                tbil.Image = await imagesDataAccess.GetImageAsync(tbil.Trip.ImageStringName);
        }
    }
}
