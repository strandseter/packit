// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="SelectItemsViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Toolkit.Uwp.Helpers;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.Wrappers;
using Packit.Exceptions;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class SelectItemsViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ItemsViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ItemsViewModel" />
    public class SelectItemsViewModel : ItemsViewModel
    {
        #region private fields
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;
        /// <summary>
        /// The tripss data access
        /// </summary>
        private readonly IBasicDataAccess<Trip> tripssDataAccess = new BasicDataAccessFactory<Trip>().Create();
        /// <summary>
        /// The backpack data access
        /// </summary>
        private readonly IRelationDataAccess<Backpack, Item> backpackDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        /// <summary>
        /// The is success
        /// </summary>
        private bool isSuccess = true;
        /// <summary>
        /// The items is filtered
        /// </summary>
        private bool itemsIsFiltered;
        #endregion

        #region public properties
        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public override ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        /// <summary>
        /// Gets or sets the selected trip.
        /// </summary>
        /// <value>The selected trip.</value>
        public TripImageWeatherLink SelectedTrip { get; set; }
        /// <summary>
        /// Creates new trip.
        /// </summary>
        /// <value>The new trip.</value>
        public Trip NewTrip { get; set; }
        /// <summary>
        /// Gets or sets the selected backpack.
        /// </summary>
        /// <value>The selected backpack.</value>
        public Backpack SelectedBackpack { get; set; }
        /// <summary>
        /// Gets or sets the selected backpack with items with images.
        /// </summary>
        /// <value>The selected backpack with items with images.</value>
        public BackpackWithItemsWithImages SelectedBackpackWithItemsWithImages { get; set; }
        /// <summary>
        /// Gets or sets the selected trip image weather link.
        /// </summary>
        /// <value>The selected trip image weather link.</value>
        public TripImageWeatherLink SelectedTripImageWeatherLink { get; set; }
        /// <summary>
        /// Creates new backpack.
        /// </summary>
        /// <value>The new backpack.</value>
        public Backpack NewBackpack { get; set; }
        /// <summary>
        /// Gets or sets the done selecting items command.
        /// </summary>
        /// <value>The done selecting items command.</value>
        public ICommand DoneSelectingItemsCommand { get; set; }
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [items is filtered].
        /// </summary>
        /// <value><c>true</c> if [items is filtered]; otherwise, <c>false</c>.</value>
        public bool ItemsIsFiltered { get => itemsIsFiltered; set => Set(ref itemsIsFiltered, value); }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectItemsViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public SelectItemsViewModel(IPopUpService popUpService)
            : base(popUpService)
        {
            DoneSelectingItemsCommand = new NetworkErrorHandlingRelayCommand<IList<object>, ItemsPage>(async param => await SaveChangesAndNavigate(param.ToList()), PopUpService);

            CancelCommand = new RelayCommand(() => NavigationService.GoBack());
        }
        #endregion

        #region load methods
        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    FilterItems();
                });
            });
        }
        #endregion

        #region private add methods
        /// <summary>
        /// Adds the items to existing backpack with items with images.
        /// </summary>
        /// <param name="itemImageLink">The item image link.</param>
        private async Task AddItemsToExistingBackpackWithItemsWithImages(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(SelectedBackpackWithItemsWithImages.Backpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }

        /// <summary>
        /// Adds the items to new backpack.
        /// </summary>
        /// <param name="itemImageLink">The item image link.</param>
        private async Task AddItemsToNewBackpack(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(NewBackpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }

        /// <summary>
        /// Adds the items to existing backpack.
        /// </summary>
        /// <param name="itemImageLink">The item image link.</param>
        private async Task AddItemsToExistingBackpack(ItemImageLink itemImageLink)
        {
            if (!await backpackDataAccess.AddEntityToEntityAsync(SelectedBackpack.BackpackId, itemImageLink.Item.ItemId))
                isSuccess = false;
        }
        #endregion

        /// <summary>
        /// Updates the selected trip.
        /// </summary>
        private async Task UpdateSelectedTrip()
        {
            var updatedTrip = await tripssDataAccess.GetByIdWithChildEntitiesAsync(SelectedTrip.Trip);
            SelectedTrip.Trip = updatedTrip;
        }

        /// <summary>
        /// Filters the items.
        /// </summary>
        private void FilterItems()
        {
            if (SelectedBackpackWithItemsWithImages == null)
            {
                ItemsIsFiltered = true;
                return;
            }

            foreach (var itemImageLinkOld in ItemImageLinks.ToList())
            {
                foreach (var itemImageLinkNew in SelectedBackpackWithItemsWithImages.ItemImageLinks)
                {
                    if (itemImageLinkOld.Item.ItemId == itemImageLinkNew.Item.ItemId)
                        ItemImageLinks.Remove(itemImageLinkOld);
                }
            }
            ItemsIsFiltered = true;
        }


        #region SaveAndNav
        /// <summary>
        /// Saves the changes and navigate.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        private async Task SaveChangesAndNavigate(IList<object> selectedItems)
        {
            if (NewTrip != null && NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(TripsMainPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, NewBackpack.Title);

                return;
            }

            if (SelectedTrip != null && NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                {
                    await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripPage), SelectedTrip);
                }
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpackWithItemsWithImages.Backpack.Title);

                return;
            }

            if (NewBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToNewBackpack((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(BackpacksPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, NewBackpack.Title);
            }

            if (SelectedBackpack != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpack((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(BackpacksPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpack.Title);
            }

            if (SelectedBackpackWithItemsWithImages != null && SelectedTrip != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpackWithItemsWithImages((ItemImageLink)obj);

                if (isSuccess)
                {
                    await UpdateSelectedTrip();
                    NavigationService.Navigate(typeof(DetailTripPage), SelectedTrip);
                }
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpackWithItemsWithImages.Backpack.Title);

                return;
            }

            if (SelectedBackpackWithItemsWithImages != null)
            {
                foreach (var obj in selectedItems)
                    await AddItemsToExistingBackpackWithItemsWithImages((ItemImageLink)obj);

                if (isSuccess)
                    NavigationService.Navigate(typeof(BackpacksPage));
                else
                    await PopUpService.ShowCouldNotAddAsync(selectedItems, SelectedBackpackWithItemsWithImages.Backpack.Title);
            }
        }
        #endregion

        #region initialize viewmodel methods
        /// <summary>
        /// Initializes the specified backpack trip.
        /// </summary>
        /// <param name="backpackTrip">The backpack trip.</param>
        internal void Initialize(BackpackWithItemsTripImageWeatherWrapper backpackTrip)
        {
            SelectedBackpackWithItemsWithImages = backpackTrip?.Backpack;
            SelectedTrip = backpackTrip?.Trip;
        }

        /// <summary>
        /// Initializes the specified backpack trip wrapper.
        /// </summary>
        /// <param name="backpackTripWrapper">The backpack trip wrapper.</param>
        internal void Initialize(BackpackTripWrapper backpackTripWrapper)
        {
            NewTrip = backpackTripWrapper.Trip;
            NewBackpack = backpackTripWrapper.Backpack;
        }

        /// <summary>
        /// Initializes the specified backpack with items with images.
        /// </summary>
        /// <param name="backpackWithItemsWithImages">The backpack with items with images.</param>
        internal void Initialize(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            SelectedBackpackWithItemsWithImages = backpackWithItemsWithImages;
        }

        /// <summary>
        /// Initializes the specified trip image weather link with backpack wrapper.
        /// </summary>
        /// <param name="tripImageWeatherLinkWithBackpackWrapper">The trip image weather link with backpack wrapper.</param>
        internal void Initialize(TripImageWeatherLinkWithBackpackWrapper tripImageWeatherLinkWithBackpackWrapper)
        {
            SelectedTrip = tripImageWeatherLinkWithBackpackWrapper.TripImageWeatherLink;
            NewBackpack = tripImageWeatherLinkWithBackpackWrapper.Backpack;
        }

        /// <summary>
        /// Initializes the specified backpack.
        /// </summary>
        /// <param name="backpack">The backpack.</param>
        internal void Initialize(Backpack backpack)
        {
            NewBackpack = backpack;
        }
        #endregion
    }
}
