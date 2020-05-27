// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="NewBackpackViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>
    /// Class NewBackpackViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class NewBackpackViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The backpack data access
        /// </summary>
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        /// <summary>
        /// The backpack trip relation data access
        /// </summary>
        private readonly IRelationDataAccess<Trip, Backpack> backpackTripRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        /// <summary>
        /// The title is valid
        /// </summary>
        private bool titleIsValid;
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand { get; set; }
        /// <summary>
        /// Gets or sets the next command.
        /// </summary>
        /// <value>The next command.</value>
        public ICommand NextCommand { get; set; }
        /// <summary>
        /// Gets or sets the selected trip image weather link.
        /// </summary>
        /// <value>The selected trip image weather link.</value>
        public TripImageWeatherLink SelectedTripImageWeatherLink { get; set; }
        /// <summary>
        /// Creates new trip.
        /// </summary>
        /// <value>The new trip.</value>
        public Trip NewTrip { get; set; }
        /// <summary>
        /// Creates new backpack.
        /// </summary>
        /// <value>The new backpack.</value>
        public Backpack NewBackpack { get; set; } = new Backpack() { Title = "", Description = "" };

        /// <summary>
        /// Gets or sets a value indicating whether [title is valid].
        /// </summary>
        /// <value><c>true</c> if [title is valid]; otherwise, <c>false</c>.</value>
        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NewBackpackViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public NewBackpackViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            CancelCommand = new RelayCommand(() => NavigationService.GoBack());

            NextCommand = new NetworkErrorHandlingRelayCommand<bool, BackpacksPage>(async param =>
            {
               await SaveAndNavigate();
            }, PopUpService, param => param);
        }
        #endregion

        #region SaveAndnav
        /// <summary>
        /// Saves the and navigate.
        /// </summary>
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
            else
                await PopUpService.ShowCouldNotSaveAsync(NewBackpack.Title);
        }
        #endregion

        #region initialize viewmodel methods
        /// <summary>
        /// Initializes the specified trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        internal void Initialize(Trip trip) => NewTrip = trip;
        /// <summary>
        /// Initializes the specified trip image weather link.
        /// </summary>
        /// <param name="tripImageWeatherLink">The trip image weather link.</param>
        internal void Initialize(TripImageWeatherLink tripImageWeatherLink) => SelectedTripImageWeatherLink = tripImageWeatherLink;
        #endregion
    }
}
