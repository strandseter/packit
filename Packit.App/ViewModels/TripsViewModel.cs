using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.Model;

namespace Packit.App.ViewModels
{
    public class TripsViewModel : Observable
    {
        private IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();

        private IRelationDataAccess<Trip, Backpack> backpacsDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();

        private IRelationDataAccess<Backpack, Model.Item> itemsDataAccess = new RelationDataAccessFactory<Backpack, Model.Item>().Create();

        private readonly Images imagesDataAccess = new Images();

        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadDataAsync));
        public ICommand TestCommand { get; set; }

        public ObservableCollection<TripBackpackItemLink> TripBackpackItems { get; } = new ObservableCollection<TripBackpackItemLink>();
        public ObservableCollection<ObservableCollection<Backpack>> Backpacks { get; } = new ObservableCollection<ObservableCollection<Backpack>>();

        public TripsViewModel()
        {
        }

        private async void LoadDataAsync()
        {
            await LoadTripsAsync();
            await LoadTripImagesAsync();
            await LoadItemImagesAsync();
        }

        private async Task LoadTripsAsync()
        {
            var trips = await tripsDataAccess.GetAllAsync();

            foreach(Trip t in trips)
                TripBackpackItems.Add(new TripBackpackItemLink() { Trip = t});

            foreach(TripBackpackItemLink tbil in TripBackpackItems)
            {
                var backpacks = await backpacsDataAccess.GetEntitiesInEntityAsync(tbil.Trip.TripId, "backpacks");

                foreach (Backpack b in backpacks)
                    tbil.BackpackItems.Add(new BackpackItemLink() { Backpack = b });

                foreach(BackpackItemLink bil in tbil.BackpackItems)
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
            foreach (TripBackpackItemLink tbil in TripBackpackItems)
                tbil.Image = await imagesDataAccess.GetImageAsync(tbil.Trip.ImageStringName);
        }

        private async Task LoadItemImagesAsync()
        {
            foreach(TripBackpackItemLink tbil in TripBackpackItems)
                foreach(BackpackItemLink bil in tbil.BackpackItems)
                    foreach(ItemImageLink iml in bil.ItemImageLinks)
                        iml.Image = await imagesDataAccess.GetImageAsync(iml.Item.ImageStringName);
        }
    }
}
