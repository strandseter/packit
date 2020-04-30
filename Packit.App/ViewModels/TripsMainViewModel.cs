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
        private readonly Images imagesDataAccess = new Images();
        private ICommand loadedCommand;
        private IRelationDataAccess<Trip, Backpack> backpacsDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private IRelationDataAccess<Backpack, Item> itemsDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();

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
            await LoadTripsAsync();
            await LoadTripImagesAsync();
        }

        private async Task LoadTripsAsync()
        {
            var trips = await tripsDataAccess.GetAllAsync();

            foreach (Trip t in trips)
                TripBackpackItemLinks.Add(new TripBackpackItemLink() { Trip = t });

            foreach (TripBackpackItemLink tbil in TripBackpackItemLinks)
            {
                var backpacks = await backpacsDataAccess.GetEntitiesInEntityAsync(tbil.Trip.TripId, "backpacks");

                foreach (Backpack b in backpacks)
                    tbil.BackpackItems.Add(new BackpackItemLink() { Backpack = b });

                foreach (BackpackItemLink bil in tbil.BackpackItems)
                {
                    var items = await itemsDataAccess.GetEntitiesInEntityAsync(bil.Backpack.BackpackId, "items");

                    foreach (Item i in items)
                    {
                        bil.ItemImageLinks.Add(new ItemImageLink() { Item = i });
                    }
                }
            }
        }

        private async Task LoadTripImagesAsync()
        {
            foreach (TripBackpackItemLink tbil in TripBackpackItemLinks)
                tbil.Image = await imagesDataAccess.GetImageAsync(tbil.Trip.ImageStringName);
        }

        internal void GridViewClicked(object sender, ItemClickEventArgs e)
        {
            NavigationService.Navigate(typeof(DetailTripPage), SelectedItem);
        }
    }
}
