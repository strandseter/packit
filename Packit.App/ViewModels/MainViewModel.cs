// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="MainViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.Model;
using Windows.UI.Xaml.Media.Imaging;
using System.Linq;
using Packit.App.Wrappers;
using Microsoft.Toolkit.Uwp.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model.Models;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class MainViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class MainViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The custom trip data access
        /// </summary>
        private readonly ICustomTripDataAccess customTripDataAccess = new CustomTripDataAccessFactory().Create();
        /// <summary>
        /// The checks data access
        /// </summary>
        private readonly IBasicDataAccess<Check> checksDataAccess = new BasicDataAccessFactory<Check>().Create();
        /// <summary>
        /// The images data access
        /// </summary>
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;
        /// <summary>
        /// The next trip
        /// </summary>
        private Trip nextTrip;
        /// <summary>
        /// The trip image
        /// </summary>
        private BitmapImage tripImage;
        /// <summary>
        /// The has next trip
        /// </summary>
        private bool hasNextTrip;
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance has next trip.
        /// </summary>
        /// <value><c>true</c> if this instance has next trip; otherwise, <c>false</c>.</value>
        public bool HasNextTrip { get => hasNextTrip; set => Set(ref hasNextTrip, value); }
        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<MainPage>(async () => await LoadDataAsync(), PopUpService));
        /// <summary>
        /// Gets or sets the item checked command.
        /// </summary>
        /// <value>The item checked command.</value>
        public ICommand ItemCheckedCommand { get; set; }
        /// <summary>
        /// Gets or sets the trip details command.
        /// </summary>
        /// <value>The trip details command.</value>
        public ICommand TripDetailsCommand { get; set; }
        /// <summary>
        /// Creates new tripcommand.
        /// </summary>
        /// <value>The new trip command.</value>
        public ICommand NewTripCommand { get; set; }
        /// <summary>
        /// Gets or sets the next trip.
        /// </summary>
        /// <value>The next trip.</value>
        public Trip NextTrip { get => nextTrip; set => Set(ref nextTrip, value); }
        /// <summary>
        /// Gets or sets the trip image.
        /// </summary>
        /// <value>The trip image.</value>
        public BitmapImage TripImage { get => tripImage; set => Set(ref tripImage, value); }
        /// <summary>
        /// Gets the backspack with items with images.
        /// </summary>
        /// <value>The backspack with items with images.</value>
        public ObservableCollection<BackpackWithItemsWithImages> BackspackWithItemsWithImages { get; } = new ObservableCollection<BackpackWithItemsWithImages>();
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public MainViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            ItemCheckedCommand = new RelayCommand<ItemBackpackBoolWrapper>(async param => await CheckItemAsync(param));

            TripDetailsCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(DetailTripPage), new TripImageWeatherLink(NextTrip) { Image = TripImage });
            });

            NewTripCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(NewTripPage));
            });
        }
        #endregion

        #region private load methods
        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadTripAsync();

            if (!HasNextTrip)
                return;

            await LoadTripImage();

            await Task.Run(async () =>
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    LoadBackpacks();
                    LoadItems();
                });
            });

            await LoadItemImagesAsync();
            await LoadItemImagesAsync();
            await LoadTripImage();
        }

        /// <summary>
        /// Loads the backpacks.
        /// </summary>
        private void LoadBackpacks()
        {
            foreach (var backpackTrip in NextTrip.Backpacks)
                BackspackWithItemsWithImages.Add(new BackpackWithItemsWithImages(backpackTrip.Backpack));
        }

        /// <summary>
        /// load trip as an asynchronous operation.
        /// </summary>
        private async Task LoadTripAsync()
        {
            var response = await customTripDataAccess.GetNextTrip();

            if (response.Item1)
            {
                NextTrip = response.Item2;
                HasNextTrip = true;
            }
        }

        /// <summary>
        /// Loads the trip image.
        /// </summary>
        private async Task LoadTripImage()
        {
            TripImage = await imagesDataAccess.GetImageAsync(NextTrip.ImageStringName, "ms-appx:///Assets/generictrip.jpg");
        }

        /// <summary>
        /// Loads the items.
        /// </summary>
        private void LoadItems()
        {
            foreach (var bwiwi in BackspackWithItemsWithImages)
            {
                foreach (var itemBackpack in bwiwi.Backpack.Items)
                {
                    foreach (var check in from check in itemBackpack.Item.Checks
                                          where bwiwi.Backpack.BackpackId == check.BackpackId && itemBackpack.Item.ItemId == check.ItemId && NextTrip.TripId == check.TripId
                                          select check)
                    {
                        itemBackpack.Item.Check = check;
                        itemBackpack.Item.Check.IsChecked = true;
                    }

                    bwiwi.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });
                }
            }
        }

        /// <summary>
        /// load item images as an asynchronous operation.
        /// </summary>
        private async Task LoadItemImagesAsync()
        {
            foreach (var itemImageLink in from bwiwi in BackspackWithItemsWithImages
                                          from itemImageLink in bwiwi.ItemImageLinks
                                          select itemImageLink)
            {
                itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
            }
        }
        #endregion

        /// <summary>
        /// check item as an asynchronous operation.
        /// </summary>
        /// <param name="param">The parameter.</param>
        private async Task CheckItemAsync(ItemBackpackBoolWrapper param)
        {
            var itemcheker = new ItemChecker
                (
                    param.Item,
                    param.BackpackWithItemsWithImages.Backpack,
                    NextTrip,
                    param.IsChecked,
                    PopUpService,
                    checksDataAccess
                );

            await itemcheker.HandleItemCheck();
        }
    }
}
