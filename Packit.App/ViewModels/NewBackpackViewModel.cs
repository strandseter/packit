using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.Wrappers;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class NewBackpackViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> backpackTripRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private bool titleIsValid;

        public ICommand CancelCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public Trip SelectedTrip { get; set; }
        public Backpack NewBackpack { get; set; } = new Backpack() { Title = "", Description = "" };

        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }

        public NewBackpackViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            NextCommand = new NetworkErrorHandlingRelayCommand<bool, BackpacksPage>(async param =>
            {
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
            }, PopUpService, param => param);
        }

        internal void Initialize(Trip trip) => SelectedTrip = trip;
    }
}
