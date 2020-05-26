using System;
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
    public class NewBackpackViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> backpackTripRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private bool titleIsValid;

        public ICommand CancelCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public TripImageWeatherLink SelectedTripImageWeatherLink { get; set; }
        public Trip NewTrip { get; set; }
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
               await SaveAndNavigate();
            }, PopUpService, param => param);
        }

        private async Task SaveAndNavigate()
        {
            if (await backpackDataAccess.AddAsync(NewBackpack))
            {
                if (NewTrip != null)
                {
                    if (await backpackTripRelationDataAccess.AddEntityToEntityAsync(NewTrip.TripId, NewBackpack.BackpackId))
                        NavigationService.Navigate(typeof(SelectItemsPage), new BackpackTripWrapper() { Backpack = NewBackpack, Trip = NewTrip });
                }
                if (SelectedTripImageWeatherLink != null)
                {
                    if (await backpackTripRelationDataAccess.AddEntityToEntityAsync(SelectedTripImageWeatherLink.Trip.TripId, NewBackpack.BackpackId))
                        NavigationService.Navigate(typeof(SelectItemsPage), new TripImageWeatherLinkWithBackpackWrapper { TripImageWeatherLink = SelectedTripImageWeatherLink, Backpack = NewBackpack });
                }
                if (NewTrip == null && SelectedTripImageWeatherLink == null)
                    NavigationService.Navigate(typeof(SelectItemsPage), NewBackpack);
            }
        }

        internal void Initialize(Trip trip) => NewTrip = trip;
        internal void Initialize(TripImageWeatherLink tripImageWeatherLink) => SelectedTripImageWeatherLink = tripImageWeatherLink;
    }
}
