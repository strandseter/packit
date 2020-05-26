using System;
using System.Collections.ObjectModel;
using System.Net.Http;
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
    public class BackpacksViewModel : ViewModel
    {
        #region private fields
        private readonly IBasicDataAccess<Backpack> backpacksDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private bool isVisible;
        private Backpack backpackClone;
        private ICommand loadedCommand;
        #endregion

        #region public properties
        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<BackpacksPage>(async () => await LoadDataAsync(), PopUpService));

        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand AddItemsCommand { get; set; }
        public ICommand BackpackToEditCommand { get; set; }
        public ICommand BackpackDoneEditingCommand { get; set; }
        public ICommand ShareBackpackCommand { get; set; }
        public ObservableCollection<BackpackWithItemsWithImages> BackpackWithItemsWithImagess { get; } = new ObservableCollection<BackpackWithItemsWithImages>();
        public Trip NewTrip { get; set; }
        public TripImageWeatherLink SelectedTripImageWeatherLink { get; set; }
        #endregion

        #region constructor
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

        private async Task LoadDataAsync()
        {
            await LoadBackpacksAsync();
            await LoadItemImagesAsync();
        }

        #region private update methods
        private async Task UpdateEditedBackpack(Backpack backpack)
        {
            if (StringIsEqual(backpack.Title, backpackClone.Title) && StringIsEqual(backpack.Description, backpackClone.Description))
                return;

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
        private async Task RemoveItemAsync(ItemImageBackpackWrapper itemImageBackpackWrapper)
        {
            if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(itemImageBackpackWrapper.BackpackWithItemsWithImages.Backpack.BackpackId, itemImageBackpackWrapper.ItemImageLink.Item.ItemId))
                itemImageBackpackWrapper.BackpackWithItemsWithImages.ItemImageLinks.Remove(itemImageBackpackWrapper.ItemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Title);
        }

        private async Task DeleteBackpackAsync(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (await backpacksDataAccess.DeleteAsync(backpackWithItemsWithImages.Backpack))
                BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
            else
                await PopUpService.ShowCouldNotDeleteAsync(backpackWithItemsWithImages.Backpack.Title);
        }
        #endregion

        #region protected load methods
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
