// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-17-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="SelectBackpacksViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.Helpers;
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
    /// Class SelectBackpacksViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.BackpacksViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.BackpacksViewModel" />
    public class SelectBackpacksViewModel : BackpacksViewModel
    {
        #region private fields
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;
        /// <summary>
        /// The backpack relation data access
        /// </summary>
        private readonly IRelationDataAccess<Trip, Backpack> backpackRelationDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        /// <summary>
        /// The tripss data access
        /// </summary>
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        /// <summary>
        /// The is success
        /// </summary>
        private bool isSuccess = true;
        /// <summary>
        /// The backpacks is filtered
        /// </summary>
        private bool backpacksIsFiltered;
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the selected backpacks.
        /// </summary>
        /// <value>The selected backpacks.</value>
        public ObservableCollection<BackpackWithItemsWithImages> SelectedBackpacks { get; set; }
        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public override ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<ItemsPage>(async () => await LoadDataAsync(), PopUpService));
        /// <summary>
        /// Gets or sets the done selecting backpacks command.
        /// </summary>
        /// <value>The done selecting backpacks command.</value>
        public ICommand DoneSelectingBackpacksCommand { get; set; }
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [backpacks is filtered].
        /// </summary>
        /// <value><c>true</c> if [backpacks is filtered]; otherwise, <c>false</c>.</value>
        public bool BackpacksIsFiltered { get => backpacksIsFiltered; set => Set(ref backpacksIsFiltered, value); }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectBackpacksViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public SelectBackpacksViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            DoneSelectingBackpacksCommand = new NetworkErrorHandlingRelayCommand<IList<object>, BackpacksPage>(async param =>
            {
                //This is a workaround. It is not possible to bind readonly "SelectedItems" in multiselect grid/list-view.
                List<object> selectedItems = param.ToList();

                await Task.WhenAll(SaveAndNavigate(selectedItems), DisableCommand());
            }, PopUpService);

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());
        }
        #endregion

        #region private load methods
        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadBackpacksAsync();
            await LoadItemImagesAsync();
            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    FilterBackpacks();
                });
            });
        }
        #endregion

        #region private add methods
        /// <summary>
        /// Adds the backpack to newtrip.
        /// </summary>
        /// <param name="backpackWithItemsWithImages">The backpack with items with images.</param>
        private async Task AddBackpackToNewtrip(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (!await backpackRelationDataAccess.AddEntityToEntityAsync(NewTrip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
                isSuccess = false;
        }

        /// <summary>
        /// Adds the backpack to existing trip.
        /// </summary>
        /// <param name="backpackWithItemsWithImages">The backpack with items with images.</param>
        private async Task AddBackpackToExistingTrip(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (!await backpackRelationDataAccess.AddEntityToEntityAsync(SelectedTripImageWeatherLink.Trip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
                isSuccess = false;
        }
        #endregion

        /// <summary>
        /// Updates the selected trip.
        /// </summary>
        private async Task UpdateSelectedTrip()
        {
            var updatedTrip = await tripssDataAccess.GetByIdWithChildEntitiesAsync(SelectedTripImageWeatherLink.Trip);
            SelectedTripImageWeatherLink.Trip = updatedTrip;
        }

        /// <summary>
        /// Filters the backpacks. Removes backpacks already existing in a given trip.
        /// </summary>
        private void FilterBackpacks()
        {
            if (SelectedTripImageWeatherLink == null)
            {
                BackpacksIsFiltered = true;
                return;
            }

            foreach (var backpackWithItemsWithImages in BackpackWithItemsWithImagess.ToList())
            {
                foreach (var backbackTrip in SelectedTripImageWeatherLink.Trip.Backpacks)
                {
                    if (backpackWithItemsWithImages.Backpack.BackpackId == backbackTrip.BackpackId)
                    {
                        BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
                    }
                }
            }
            BackpacksIsFiltered = true;
        }

        /// <summary>Saves the and navigate.</summary>
        /// <param name="selectedItems">The selected items.</param>
        private async Task SaveAndNavigate(IList<object> selectedItems)
        {
            if (SelectedTripImageWeatherLink != null)
            {
                foreach (var obj in selectedItems)
                    await AddBackpackToExistingTrip((BackpackWithItemsWithImages)obj);

                if (isSuccess)
                {
                    await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripPage), SelectedTripImageWeatherLink);
                }
            }

            if (NewTrip != null)
            {
                foreach (var obj in selectedItems)
                    await AddBackpackToNewtrip((BackpackWithItemsWithImages)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(TripsMainPage));
            }
        }

        #region initialize viewmodel methods
        /// <summary>
        /// Initializes the backpack viewmodel with items with images and backpacks.
        /// </summary>
        /// <param name="backpackWithItemsWithImagesTripWrapper">The backpack with items with images trip wrapper.</param>
        internal void Initialize(BackpackWithItemsWithImagesTripWrapper  backpackWithItemsWithImagesTripWrapper)
        {
            SelectedTripImageWeatherLink = backpackWithItemsWithImagesTripWrapper.TripImageWeatherLink;
            SelectedBackpacks = backpackWithItemsWithImagesTripWrapper.BackpackWithItemsWithImages;
        }

        /// <summary>
        /// Initializes the specified trip image weather link.
        /// </summary>
        /// <param name="tripImageWeatherLink">The trip image weather link.</param>
        internal void Initialize(TripImageWeatherLink tripImageWeatherLink)
        {
            SelectedTripImageWeatherLink = tripImageWeatherLink;
        }

        /// <summary>
        /// Initializes with a new Trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        internal void Initialize(Trip trip) => NewTrip = trip;
        #endregion
    }
}
