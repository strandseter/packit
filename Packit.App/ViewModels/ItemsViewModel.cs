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
    public class ItemsViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private ICommand loadedCommand;
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private bool isVisible;
        private Item itemClone;

        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<ItemsPage>(async () => await LoadDataAsync(), PopUpService));
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ItemToEditCommand { get; set; }
        public ICommand ItemDoneEditingCommand { get; set; }
        public ObservableCollection<ItemImageLink> ItemImageLinks { get; } = new ObservableCollection<ItemImageLink>();

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

        private async Task LoadDataAsync()
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
        }

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

        private async Task DeleteItemAsync(ItemImageLink itemImageLink)
        {
            if (!string.IsNullOrEmpty(itemImageLink.Item.ImageStringName))
                await DeleteItemAndImageRequestAsync(itemImageLink);
            else
                await DeleteItemRequestAsync(itemImageLink);
        }

        private async Task DeleteItemAndImageRequestAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item) && await imagesDataAccess.DeleteImageAsync(itemImageLink.Item.ImageStringName))
                ItemImageLinks.Remove(itemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageLink.Item.Title);
        }

        private async Task DeleteItemRequestAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item))
                ItemImageLinks.Remove(itemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageLink.Item.Title);
        }

        protected virtual async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAllAsync();

            foreach (var i in items)
                ItemImageLinks.Add(new ItemImageLink() { Item = i });
        }

        protected async Task LoadImagesAsync()
        {
            foreach (var iml in ItemImageLinks)
                iml.Image = await imagesDataAccess.GetImageAsync(iml.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
        }
    }
}
