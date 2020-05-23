using System;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.Wrappers;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class NewBackpackViewModel : Observable
    {
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> backpackTripRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();


        public ICommand CancelCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public Trip SelectedTrip { get; set; }
        public Backpack NewBackpack { get; set; } = new Backpack() { Title = "", Description = "" }; 

        public NewBackpackViewModel()
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            NextCommand = new RelayCommand(async () => {

                if (await backpackDataAccess.AddAsync(NewBackpack))
                {
                    if (SelectedTrip != null)
                    {
                        if (await backpackTripRelationDataAccess.AddEntityToEntityAsync(SelectedTrip.TripId, NewBackpack.BackpackId))
                            NavigationService.Navigate(typeof(SelectItemsPage), new BackpackTripWrapper() { Backpack = NewBackpack, Trip = SelectedTrip });
                    }
                    else
                        NavigationService.Navigate(typeof(SelectItemsPage), NewBackpack);
                }
            });
        }

        internal void Initialize(Trip trip)
        {
            SelectedTrip = trip;
        }
    }
}
