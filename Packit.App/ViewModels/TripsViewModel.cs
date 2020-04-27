using System;
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

        private IRelationDataAccess<Backpack, Item> itemsDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();

        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadDataAsync));
        public ICommand TestCommand { get; set; }

        public ObservableCollection<TripBackpackItemLinkOld> TripBackpackItems { get; } = new ObservableCollection<TripBackpackItemLinkOld>();
        public ObservableCollection<ObservableCollection<Backpack>> Backpacks { get; } = new ObservableCollection<ObservableCollection<Backpack>>();


        public ObservableCollection<TripBackpackLink> TripBackpacks { get; } = new ObservableCollection<TripBackpackLink>();
        public ObservableCollection<BackpackItemLink> BackpackItems { get; } = new ObservableCollection<BackpackItemLink>();


        public ObservableCollection<Trip> Trips { get; } = new ObservableCollection<Trip>();


        public TripsViewModel()
        {
        }

        private async void LoadDataAsync()
        {
            await LoadDataAsyncBlah();
        }

        //TODO: Refactor
        private async Task LoadData2()
        {
            var trips = await tripsDataAccess.GetAllAsync();

            foreach (Trip t in trips)
                TripBackpacks.Add(new TripBackpackLink() { Trip = t });

            foreach(TripBackpackLink tbl in TripBackpacks)
            {
                var backpacks = await backpacsDataAccess.GetEntitiesInEntityAsync(tbl.Trip.TripId, "backpacks");

                foreach (Backpack b in backpacks)
                {
                    BackpackItems.Add(new BackpackItemLink() { Backpack = b,});
                    tbl.Backpacks.Add(b);
                }
            }

            foreach (BackpackItemLink bil in BackpackItems)
            {
                var items = await itemsDataAccess.GetEntitiesInEntityAsync(bil.Backpack.BackpackId, "items");

                foreach (Item i in items)
                    bil.Items.Add(i);
            }
        }

        private async Task LoadDataAsyncBlah()
        {
            var trips = await tripsDataAccess.GetAllAsync();

            foreach(Trip t in trips)
                TripBackpackItems.Add(new TripBackpackItemLinkOld() { Trip = t});

            foreach(TripBackpackItemLinkOld tbil in TripBackpackItems)
            {
                var backpacks = await backpacsDataAccess.GetEntitiesInEntityAsync(tbil.Trip.TripId, "backpacks");

                foreach (Backpack b in backpacks)
                    tbil.BackpackItems.Add(new BackpackItemLinkOld() { Backpack = b });

                foreach(BackpackItemLinkOld bil in tbil.BackpackItems)
                {
                    var items = await itemsDataAccess.GetEntitiesInEntityAsync(bil.Backpack.BackpackId, "items");

                    foreach (Item i in items)
                        bil.Items.Add(i);
                }
            }
        }
    }
}
