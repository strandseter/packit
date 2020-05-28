// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-25-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpacksViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.Wrappers;
using Packit.Extensions;
using Packit.Model;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class BackpacksViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class BackpacksViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The backpacks data access
        /// </summary>
        private readonly IBasicDataAccess<Backpack> backpacksDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        /// <summary>
        /// The backpack item data access
        /// </summary>
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        /// <summary>
        /// The images data access
        /// </summary>
        private readonly ImagesDataAccessHttp imagesDataAccess = new ImagesDataAccessHttp();
        /// <summary>
        /// The is visible
        /// </summary>
        private bool isVisible;
        private bool titleIsValid;
        /// <summary>
        /// The backpack clone
        /// </summary>
        private Backpack backpackClone;
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;
        #endregion

        #region public properties
        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<BackpacksPage>(async () => await LoadDataAsync(), PopUpService));

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
        /// Gets or sets the edit command.
        /// </summary>
        /// <value>The edit command.</value>
        public ICommand EditCommand { get; set; }
        /// <summary>
        /// Gets or sets the delete command.
        /// </summary>
        /// <value>The delete command.</value>
        public ICommand DeleteCommand { get; set; }
        /// <summary>
        /// Creates new command.
        /// </summary>
        /// <value>The new command.</value>
        public ICommand NewCommand { get; set; }
        /// <summary>
        /// Gets or sets the remove item command.
        /// </summary>
        /// <value>The remove item command.</value>
        public ICommand RemoveItemCommand { get; set; }
        /// <summary>
        /// Gets or sets the delete item command.
        /// </summary>
        /// <value>The delete item command.</value>
        public ICommand DeleteItemCommand { get; set; }
        /// <summary>
        /// Gets or sets the add items command.
        /// </summary>
        /// <value>The add items command.</value>
        public ICommand AddItemsCommand { get; set; }
        /// <summary>
        /// Gets or sets the backpack to edit command.
        /// </summary>
        /// <value>The backpack to edit command.</value>
        public ICommand BackpackToEditCommand { get; set; }
        /// <summary>
        /// Gets or sets the backpack done editing command.
        /// </summary>
        /// <value>The backpack done editing command.</value>
        public ICommand BackpackDoneEditingCommand { get; set; }
        /// <summary>
        /// Gets or sets the share backpack command.
        /// </summary>
        /// <value>The share backpack command.</value>
        public ICommand ShareBackpackCommand { get; set; }
        public bool TitleIsValid { get => titleIsValid; set => Set(ref titleIsValid, value); }

        /// <summary>
        /// Gets the backpack with items and all its images.
        /// </summary>
        /// <value>The backpack with items with imagess.</value>
        public ObservableCollection<BackpackWithItemsWithImages> BackpackWithItemsWithImagess { get; } = new ObservableCollection<BackpackWithItemsWithImages>();
        /// <summary>
        /// A new Trip which backpacks is selected for.
        /// </summary>
        /// <value>The new trip.</value>
        public Trip NewTrip { get; set; }
        /// <summary>
        /// Gets or sets the selected trip image weather link.
        /// </summary>
        /// <value>The selected trip image weather link.</value>
        public TripImageWeatherLink SelectedTripImageWeatherLink { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BackpacksViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public BackpacksViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            NewCommand = new RelayCommand(() => NewBackpack());

            EditCommand = new RelayCommand(() => IsVisible = !IsVisible);

            DeleteCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
               await PopUpService.ShowDeleteDialogAsync(DeleteBackpackAsync, param, param.Backpack.Title);
            }, param => param != null);

            RemoveItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                await PopUpService.ShowRemoveDialogAsync(RemoveItemAsync, param, param.ItemImageLink.Item.Title, param.BackpackWithItemsWithImages.Backpack.Title);
            });

            AddItemsCommand = new RelayCommand<BackpackWithItemsWithImages>(param =>
            {
                NavigationService.Navigate(typeof(SelectItemsPage), param);
            });

            BackpackToEditCommand = new RelayCommand<Backpack>(param => backpackClone = param.DeepClone());

            BackpackDoneEditingCommand = new NetworkErrorHandlingRelayCommand<Backpack, BackpacksPage>(async param =>
            {
               await UpdateEditedBackpack(param);
            }, PopUpService);

            ShareBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(param =>
            {
                var test = param;
            });
        }
        #endregion

        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadBackpacksAsync();
            await LoadItemImagesAsync();
        }

        #region private update methods
        /// <summary>
        /// Updates the edited backpack.
        /// </summary>
        /// <param name="backpack">The backpack.</param>
        private async Task UpdateEditedBackpack(Backpack backpack)
        {
            if (StringIsEqual(backpack.Title, backpackClone.Title) && StringIsEqual(backpack.Description, backpackClone.Description))
                return;

            if (!TitleIsValid)
            {
                backpack.Title = backpackClone.Title;
                return;
            }

            backpack.Items.Clear();

            if (!await backpacksDataAccess.UpdateAsync(backpack))
            {
                backpack.Title = backpackClone.Title;
                backpack.Description = backpackClone.Description;
                await PopUpService.ShowCouldNotSaveChangesAsync(backpackClone.Title);
            }
        }
        #endregion

        #region private delete methods
        /// <summary>
        /// remove item as an asynchronous operation.
        /// </summary>
        /// <param name="itemImageBackpackWrapper">The item image backpack wrapper.</param>
        private async Task RemoveItemAsync(ItemImageBackpackWrapper itemImageBackpackWrapper)
        {
            if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(itemImageBackpackWrapper.BackpackWithItemsWithImages.Backpack.BackpackId, itemImageBackpackWrapper.ItemImageLink.Item.ItemId))
                itemImageBackpackWrapper.BackpackWithItemsWithImages.ItemImageLinks.Remove(itemImageBackpackWrapper.ItemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Title);
        }

        /// <summary>
        /// delete backpack as an asynchronous operation.
        /// </summary>
        /// <param name="backpackWithItemsWithImages">The backpack with items with images.</param>
        private async Task DeleteBackpackAsync(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (await backpacksDataAccess.DeleteAsync(backpackWithItemsWithImages.Backpack))
                BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
            else
                await PopUpService.ShowCouldNotDeleteAsync(backpackWithItemsWithImages.Backpack.Title);
        }
        #endregion

        #region protected load methods
        /// <summary>
        /// load backpacks as an asynchronous operation.
        /// </summary>
        protected virtual async Task LoadBackpacksAsync()
        {
            var backpacksWithItems = await backpacksDataAccess.GetAllWithChildEntitiesAsync();

            foreach (var backpack in backpacksWithItems)
            {
                var backpackWithItemsWithImages = new BackpackWithItemsWithImages(backpack);

                  foreach (var itemBackpack in backpack.Items)
                    backpackWithItemsWithImages.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });

                BackpackWithItemsWithImagess.Add(backpackWithItemsWithImages);
            }
        }

        /// <summary>
        /// load item images as an asynchronous operation.
        /// </summary>
        protected async Task LoadItemImagesAsync()
        {
            foreach (var bwiwi in BackpackWithItemsWithImagess)
            {
                foreach (var itemImageLink in bwiwi.ItemImageLinks)
                    itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
            }
        }
        #endregion

        #region nav
        /// <summary>
        /// Creates new backpack.
        /// </summary>
        private void NewBackpack()
        {
            if (NewTrip != null)
            {
                NavigationService.Navigate(typeof(NewBackpackPage), NewTrip);
            }
            if (SelectedTripImageWeatherLink != null)
            {
                NavigationService.Navigate(typeof(NewBackpackPage), SelectedTripImageWeatherLink);
            }
            if (NewTrip == null && SelectedTripImageWeatherLink == null)
            {
                NavigationService.Navigate(typeof(NewBackpackPage));
            }
        }
        #endregion
    }
}
