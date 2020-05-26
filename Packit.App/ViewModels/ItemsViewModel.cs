using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Packit.Model;
using Packit.App.DataAccess;
using System.Windows.Input;
using Packit.App.Factories;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.DataLinks;
using Packit.Extensions;
using Packit.App.Helpers;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class ItemsViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class ItemsViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The items data access
        /// </summary>
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        /// <summary>
        /// The loaded command
        /// </summary>
        private ICommand loadedCommand;
        /// <summary>
        /// The images data access
        /// </summary>
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        /// <summary>
        /// The is visible
        /// </summary>
        private bool isVisible;
        /// <summary>
        /// The item clone
        /// </summary>
        private Item itemClone;
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
        /// Gets the loaded command.
        /// </summary>
        /// <value>The loaded command.</value>
        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<ItemsPage>(async () => await LoadDataAsync(), PopUpService));
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
        /// Gets or sets the add command.
        /// </summary>
        /// <value>The add command.</value>
        public ICommand AddCommand { get; set; }
        /// <summary>
        /// Gets or sets the item to edit command.
        /// </summary>
        /// <value>The item to edit command.</value>
        public ICommand ItemToEditCommand { get; set; }
        /// <summary>
        /// Gets or sets the item done editing command.
        /// </summary>
        /// <value>The item done editing command.</value>
        public ICommand ItemDoneEditingCommand { get; set; }
        /// <summary>
        /// Gets the item image links.
        /// </summary>
        /// <value>The item image links.</value>
        public ObservableCollection<ItemImageLink> ItemImageLinks { get; } = new ObservableCollection<ItemImageLink>();
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public ItemsViewModel(IPopUpService popUpService)
            : base(popUpService)
        {
            ItemToEditCommand = new RelayCommand<Item>(param => itemClone = param.DeepClone());

            EditCommand = new RelayCommand(() => IsVisible = !IsVisible);

            AddCommand = new RelayCommand(() => NavigationService.Navigate(typeof(NewItemPage)));

            DeleteCommand = new RelayCommand<ItemImageLink>(async param =>
            {
                await PopUpService.ShowDeleteDialogAsync(DeleteItemAsync, param, param.Item.Title);
            }, param => param != null);

            ItemDoneEditingCommand = new NetworkErrorHandlingRelayCommand<Item, ItemsPage>(async param =>
            {
               await UpdateEditeditem(param);
            }, popUpService);
        }
        #endregion

        #region private loading methods
        /// <summary>
        /// load data as an asynchronous operation.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
        }


        /// <summary>
        /// load items as an asynchronous operation.
        /// </summary>
        protected async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAllAsync();

            foreach (var i in items)
                ItemImageLinks.Add(new ItemImageLink() { Item = i });
        }

        /// <summary>
        /// load images as an asynchronous operation.
        /// </summary>
        protected async Task LoadImagesAsync()
        {
            foreach (var iml in ItemImageLinks)
                iml.Image = await imagesDataAccess.GetImageAsync(iml.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
        }
        #endregion

        #region private update methods
        /// <summary>
        /// Updates the editeditem.
        /// </summary>
        /// <param name="item">The item.</param>
        private async Task UpdateEditeditem(Item item)
        {
            if (StringIsEqual(item.Description, itemClone.Description) && StringIsEqual(item.Title, itemClone.Title))
                return;

            if (!await itemsDataAccess.UpdateAsync(item))
            {
                item.Title = itemClone.Title;
                item.Description = itemClone.Description;
                await PopUpService.ShowCouldNotSaveChangesAsync(itemClone.Title);
            }
        }
        #endregion

        #region private update methods
        /// <summary>
        /// delete item as an asynchronous operation.
        /// </summary>
        /// <param name="itemImageLink">The item image link.</param>
        private async Task DeleteItemAsync(ItemImageLink itemImageLink)
        {
            if (!string.IsNullOrEmpty(itemImageLink.Item.ImageStringName))
                await DeleteItemAndImageRequestAsync(itemImageLink);
            else
                await DeleteItemRequestAsync(itemImageLink);
        }

        /// <summary>
        /// delete item and image request as an asynchronous operation.
        /// </summary>
        /// <param name="itemImageLink">The item image link.</param>
        private async Task DeleteItemAndImageRequestAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item) && await imagesDataAccess.DeleteImageAsync(itemImageLink.Item.ImageStringName))
                ItemImageLinks.Remove(itemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageLink.Item.Title);
        }

        /// <summary>
        /// delete item request as an asynchronous operation.
        /// </summary>
        /// <param name="itemImageLink">The item image link.</param>
        private async Task DeleteItemRequestAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item))
                ItemImageLinks.Remove(itemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageLink.Item.Title);
        }
        #endregion
    }
}
