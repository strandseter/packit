// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="DetailTripViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.App.DataAccess.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Packit.Model;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.DataAccess;
using Packit.App.Factories;
using Packit.App.Wrappers;
using Packit.Extensions;
using Packit.Model.Models;
using System.Net.Http;
using Packit.Exceptions;
using System.Globalization;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class DetailTripViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class DetailTripViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The images data access
        /// </summary>
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        /// <summary>
        /// The weather data access
        /// </summary>
        private readonly WeatherDataAccess weatherDataAccess = new WeatherDataAccess();
        /// <summary>
        /// The backpack data access
        /// </summary>
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        /// <summary>
        /// The item data access
        /// </summary>
        private readonly IBasicDataAccess<Item> itemDataAccess = new BasicDataAccessFactory<Item>().Create();
        /// <summary>
        /// The trip data access
        /// </summary>
        private readonly IBasicDataAccess<Trip> tripDataAccess = new BasicDataAccessFactory<Trip>().Create();
        /// <summary>
        /// The checks data access
        /// </summary>
        private readonly IBasicDataAccess<Check> checksDataAccess = new BasicDataAccessFactory<Check>().Create();
        /// <summary>
        /// The backpack item data access
        /// </summary>
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        /// <summary>
        /// The trip backpack data access
        /// </summary>
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;
        /// <summary>
        /// The trip clone
        /// </summary>
        private Trip tripClone;
        /// <summary>
        /// The is visible
        /// </summary>
        private bool isVisible;
        /// <summary>
        /// The weather report is loaded
        /// </summary>
        private bool weatherReportIsLoaded;
        /// <summary>
        /// The title is valid
        /// </summary>
        private bool titleIsValid;
        /// <summary>
        /// The destinationis valid
        /// </summary>
        private bool destinationisValid;
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [weather report is loaded].
        /// </summary>
        /// <value><c>true</c> if [weather report is loaded]; otherwise, <c>false</c>.</value>
        public bool WeatherReportIsLoaded
        {
            get => weatherReportIsLoaded;
            set => Set(ref weatherReportIsLoaded, value);
        }

        /// <summary>Gets or sets a value indicating whether [title is valid].</summary>
        /// <value>
        ///   <c>true</c> if [title is valid]; otherwise, <c>false</c>.</value>
        public bool TitleIsValid
        {
            get => titleIsValid;
            set => Set(ref titleIsValid, value);
        }

        /// <summary>Gets or sets a value indicating whether [destionation is valid].</summary>
        /// <value>
        ///   <c>true</c> if [destionation is valid]; otherwise, <c>false</c>.</value>
        public bool DestionationIsValid
        {
            get => destinationisValid;
            set => Set(ref destinationisValid, value);
        }

        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<TripsMainPage>(async () => await LoadDataAsync(), PopUpService));
        /// <summary>
        /// Gets or sets the edit trip command.
        /// </summary>
        /// <value>The edit trip command.</value>
        public ICommand EditTripCommand { get; set; }
        /// <summary>
        /// Gets or sets the delete trip command.
        /// </summary>
        /// <value>The delete trip command.</value>
        public ICommand DeleteTripCommand { get; set; }
        /// <summary>
        /// Gets or sets the cancel trip command.
        /// </summary>
        /// <value>The cancel trip command.</value>
        public ICommand CancelTripCommand { get; set; }
        /// <summary>
        /// Gets or sets the add backpacks command.
        /// </summary>
        /// <value>The add backpacks command.</value>
        public ICommand AddBackpacksCommand { get; set; }
        /// <summary>
        /// Gets or sets the remove backpack command.
        /// </summary>
        /// <value>The remove backpack command.</value>
        public ICommand RemoveBackpackCommand { get; set; }
        /// <summary>
        /// Gets or sets the delete backpack command.
        /// </summary>
        /// <value>The delete backpack command.</value>
        public ICommand DeleteBackpackCommand { get; set; }
        /// <summary>
        /// Gets or sets the share backpack command.
        /// </summary>
        /// <value>The share backpack command.</value>
        public ICommand ShareBackpackCommand { get; set; }
        /// <summary>
        /// Gets or sets the add item to backpack command.
        /// </summary>
        /// <value>The add item to backpack command.</value>
        public ICommand AddItemToBackpackCommand { get; set; }
        /// <summary>
        /// Gets or sets the remove item from backpack command.
        /// </summary>
        /// <value>The remove item from backpack command.</value>
        public ICommand RemoveItemFromBackpackCommand { get; set; }
        /// <summary>
        /// Gets or sets the delete item command.
        /// </summary>
        /// <value>The delete item command.</value>
        public ICommand DeleteItemCommand { get; set; }
        /// <summary>
        /// Gets or sets the item checked command.
        /// </summary>
        /// <value>The item checked command.</value>
        public ICommand ItemCheckedCommand { get; set; }
        /// <summary>
        /// Gets or sets the minimum date.
        /// </summary>
        /// <value>The minimum date.</value>
        public DateTimeOffset MinDate { get; set; } = DateTime.Now;
        /// <summary>
        /// Gets or sets the trip image weather link.
        /// </summary>
        /// <value>The trip image weather link.</value>
        public TripImageWeatherLink TripImageWeatherLink { get; set; }
        /// <summary>
        /// Gets the backpacks.
        /// </summary>
        /// <value>The backpacks.</value>
        public ObservableCollection<BackpackWithItemsWithImages> Backpacks { get; } = new ObservableCollection<BackpackWithItemsWithImages>();
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailTripViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public DetailTripViewModel(IPopUpService popUpService)
            : base(popUpService)
        {
            DeleteTripCommand = new RelayCommand(async () =>
            {
                await PopUpService.ShowDeleteDialogAsync(DeleteTripAsync, TripImageWeatherLink.Trip.Title);
            });

            RemoveItemFromBackpackCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                await PopUpService.ShowRemoveDialogAsync(RemoveItemFromBackpack, param, param.ItemImageLink.Item.Title, param.BackpackWithItemsWithImages.Backpack.Title);
            });

            DeleteItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                await PopUpService.ShowDeleteDialogAsync(DeleteItem, param, param.ItemImageLink.Item.Title);
            });

            RemoveBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                await PopUpService.ShowRemoveDialogAsync(RemoveBackpack, param, param.Backpack.Title, TripImageWeatherLink.Trip.Title);
            });

            DeleteBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                await PopUpService.ShowDeleteDialogAsync(DeleteBackpack, param, param.Backpack.Title);
            });

            AddBackpacksCommand = new RelayCommand<ItemBackpackWrapper>(param =>
            {
                NavigationService.Navigate(typeof(SelectBackpacksPage), new BackpackWithItemsWithImagesTripWrapper() { BackpackWithItemsWithImages = Backpacks, TripImageWeatherLink = TripImageWeatherLink });
            });

            AddItemToBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(param =>
            {
                NavigationService.Navigate(typeof(SelectItemsPage), new BackpackWithItemsTripImageWeatherWrapper() { Backpack = param, Trip = TripImageWeatherLink });
            });

            ItemCheckedCommand = new NetworkErrorHandlingRelayCommand<ItemBackpackBoolWrapper, TripsMainPage>(async param =>
            {
                await CheckItemAsync(param);
            }, PopUpService);

            ShareBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                param.Backpack.IsShared = true;

                if (!await backpackDataAccess.UpdateAsync(param.Backpack))
                    param.Backpack.IsShared = false;
            });

            EditTripCommand = new NetworkErrorHandlingRelayCommand<DetailTripPage>(async () =>
            {
                if (!IsVisible)
                    CloneTrip();

                if (IsVisible)
                    await Update();

                IsVisible = !IsVisible;
            }, PopUpService);
        }
        #endregion

        #region private load methods
        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            LoadBackpacks();
            LoadItemsInBackpacks();
            await LoadWeatherReportAsync();
            await LoadItemImagesAsync();
        }
        /// <summary>
        /// Loads the items in backpacks. And which item is checked,
        /// </summary>
        private void LoadItemsInBackpacks()
        {
            foreach (var bwi in Backpacks)
            {
                foreach (var itemBackpack in bwi.Backpack.Items)
                {
                    foreach (var check in itemBackpack.Item.Checks)
                    {
                        if (bwi.Backpack.BackpackId == check.BackpackId && itemBackpack.Item.ItemId == check.ItemId && TripImageWeatherLink.Trip.TripId == check.TripId)
                        {
                            itemBackpack.Item.Check = check;
                            itemBackpack.Item.Check.IsChecked = true;
                        }
                    }
                    bwi.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });
                }
            }
        }

        /// <summary>
        /// load item images as an asynchronous operation.
        /// </summary>
        private async Task LoadItemImagesAsync()
        {
            foreach (var bwi in Backpacks)
            {
                foreach (var itemImageLink in bwi.ItemImageLinks)
                {
                    itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
                }
            }
        }

        /// <summary>
        /// load weather report as an asynchronous operation.
        /// </summary>
        private async Task LoadWeatherReportAsync()
        {
            try
            {
                TripImageWeatherLink.WeatherReport = await weatherDataAccess.GetCurrentWeatherReportAsync(TripImageWeatherLink.Trip.Destination);
                TripImageWeatherLink.WeatherReport.Weathers[0].IconImage = await weatherDataAccess.GetCurrentWeatherIconAsync(TripImageWeatherLink.WeatherReport.Weathers[0].Icon);
                WeatherReportIsLoaded = true;
            }
            catch (HttpRequestException)
            {
                WeatherReportIsLoaded = false;
            }
        }

        /// <summary>
        /// Loads the backpacks.
        /// </summary>
        private void LoadBackpacks()
        {
            foreach (var bt in TripImageWeatherLink.Trip.Backpacks)
                Backpacks.Add(new BackpackWithItemsWithImages(bt.Backpack));
        }
        #endregion

        #region private update methods
        /// <summary>
        /// Updates this instance.
        /// </summary>
        private async Task Update()
        {
            await UpdateTripAsync();
            await UpdateWeatherAsync();
        }

        /// <summary>
        /// update trip as an asynchronous operation.
        /// </summary>
        private async Task UpdateTripAsync()
        {
            if (StringIsEqual(TripImageWeatherLink.Trip.Title, tripClone.Title)
                && StringIsEqual(TripImageWeatherLink.Trip.Destination, tripClone.Destination)
                && StringIsEqual(TripImageWeatherLink.Trip.DepatureDate.Date.ToString(CultureInfo.InvariantCulture), tripClone.DepatureDate.Date.ToString(CultureInfo.InvariantCulture)))
                return;

            TripImageWeatherLink.Trip.Backpacks.Clear();

            try
            {
                if (!TitleIsValid || !DestionationIsValid)
                {
                    TripImageWeatherLink.Trip = tripClone;
                    return;
                }

                if (!await tripDataAccess.UpdateAsync(TripImageWeatherLink.Trip))
                {
                    TripImageWeatherLink.Trip = tripClone;
                    await PopUpService.ShowCouldNotSaveAsync(tripClone.Title);
                }
            }
            catch (HttpRequestException)
            {
                TripImageWeatherLink.Trip = tripClone;
                throw;
            }
            catch (NetworkConnectionException)
            {
                TripImageWeatherLink.Trip = tripClone;
                throw;
            }
            catch (OperationCanceledException)
            {
                TripImageWeatherLink.Trip = tripClone;
                throw;
            }
        }

        /// <summary>
        /// update weather as an asynchronous operation.
        /// </summary>
        private async Task UpdateWeatherAsync()
        {
            if (StringIsEqual(TripImageWeatherLink.Trip.Destination, tripClone.Destination))
                return;

            await LoadWeatherReportAsync();
        }
        #endregion

        #region private delete methods
        /// <summary>
        /// Deletes the backpack.
        /// </summary>
        /// <param name="backpackWithItemsWithImages">The backpack with items with images.</param>
        private async Task DeleteBackpack(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (backpackWithItemsWithImages == null)
                throw new ArgumentNullException(nameof(backpackWithItemsWithImages));

            if (await backpackDataAccess.DeleteAsync(backpackWithItemsWithImages.Backpack))
                Backpacks.Remove(backpackWithItemsWithImages);
            else
                await PopUpService.ShowCouldNotDeleteAsync(backpackWithItemsWithImages.Backpack.Title);
        }

        /// <summary>
        /// Removes the backpack.
        /// </summary>
        /// <param name="backpackWithItemsWithImages">The backpack with items with images.</param>
        private async Task RemoveBackpack(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (backpackWithItemsWithImages == null)
                throw new ArgumentNullException(nameof(backpackWithItemsWithImages));

            if (await tripBackpackDataAccess.DeleteEntityFromEntityAsync(TripImageWeatherLink.Trip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
            {
                Backpacks.Remove(backpackWithItemsWithImages);
                TripImageWeatherLink.Trip.Backpacks.RemoveAll(x => x.BackpackId == backpackWithItemsWithImages.Backpack.BackpackId);
            }
            else
                await PopUpService.ShowCouldNotDeleteAsync(backpackWithItemsWithImages.Backpack.Title);
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="itemImageBackpackWrapper">The item image backpack wrapper.</param>
        private async Task DeleteItem(ItemImageBackpackWrapper itemImageBackpackWrapper)
        {
            if (itemImageBackpackWrapper == null)
                throw new ArgumentNullException(nameof(itemImageBackpackWrapper));

            if (await itemDataAccess.DeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item))
                itemImageBackpackWrapper.BackpackWithItemsWithImages.ItemImageLinks.Remove(itemImageBackpackWrapper.ItemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Title);
        }

        /// <summary>
        /// Removes the item from backpack.
        /// </summary>
        /// <param name="itemImageBackpackWrapper">The item image backpack wrapper.</param>
        private async Task RemoveItemFromBackpack(ItemImageBackpackWrapper itemImageBackpackWrapper)
        {
            if (itemImageBackpackWrapper == null)
                throw new ArgumentNullException(nameof(itemImageBackpackWrapper));

            if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(itemImageBackpackWrapper.BackpackWithItemsWithImages.Backpack.BackpackId, itemImageBackpackWrapper.ItemImageLink.Item.ItemId))
            {
                itemImageBackpackWrapper.BackpackWithItemsWithImages.ItemImageLinks.Remove(itemImageBackpackWrapper.ItemImageLink);

                if (itemImageBackpackWrapper.ItemImageLink.Item.Check != null)
                {
                    if (await checksDataAccess.DeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Check))
                        itemImageBackpackWrapper.ItemImageLink.Item.Check.IsChecked = false;
                }
            }
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Title);
        }

        /// <summary>
        /// delete trip as an asynchronous operation.
        /// </summary>
        private async Task DeleteTripAsync()
        {
            if (!string.IsNullOrEmpty(TripImageWeatherLink.Trip.ImageStringName))
                await DeleteTripAndImageRequestAsync(TripImageWeatherLink.Trip);
            else
                await DeleteTripRequestAsync(TripImageWeatherLink.Trip);
        }

        private async Task DeleteTripAndImageRequestAsync(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            if (await tripDataAccess.DeleteAsync(trip) && await imagesDataAccess.DeleteImageAsync(trip.ImageStringName))
                NavigationService.Navigate(typeof(TripsMainPage));
            else
                await PopUpService.ShowCouldNotDeleteAsync(trip.Title);
        }

        private async Task DeleteTripRequestAsync(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            if (await tripDataAccess.DeleteAsync(trip))
                NavigationService.Navigate(typeof(TripsMainPage));
            else
                await PopUpService.ShowCouldNotDeleteAsync(trip.Title);
        }
        #endregion

        /// <summary>
        /// check item as an asynchronous operation.
        /// </summary>
        /// <param name="param">The parameter.</param>
        private async Task CheckItemAsync(ItemBackpackBoolWrapper param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            var itemcheker = new ItemChecker
                (
                    param.Item,
                    param.BackpackWithItemsWithImages.Backpack,
                    TripImageWeatherLink.Trip,
                    param.IsChecked,
                    PopUpService,
                    checksDataAccess
                );

            await itemcheker.HandleItemCheck();
        }

        #region initialize viewmodel methods
        /// <summary>
        /// Initializes the specified trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        public void Initialize(TripImageWeatherLink trip) => TripImageWeatherLink = trip;
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize() => NavigationService.GoBack();
        #endregion
        /// <summary>
        /// Clones the trip.
        /// </summary>
        private void CloneTrip() => tripClone = (Trip)TripImageWeatherLink.Trip.Clone();
    }
}
