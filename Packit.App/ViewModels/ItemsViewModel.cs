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
using System.Collections.Generic;
using System.Linq;
using Packit.Extensions;
using Packit.App.Helpers;
using System.Net.Http;

namespace Packit.App.ViewModels
{
    public class ItemsViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private ICommand loadedCommand;
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private bool isVisible;
        private IList<ItemImageLink> itemImageLinksClone;

        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ObservableCollection<ItemImageLink> ItemImageLinks { get; } = new ObservableCollection<ItemImageLink>();

        public ItemsViewModel()
        {
            DeleteCommand = new RelayCommand<ItemImageLink>(async param => { await PopupService.ShowDeleteDialogAsync(DeleteItemAsync, param, param.Item.Title); }
                                                                            ,param => param != null);

            EditCommand = new RelayCommand(async () => await EditItem());

            AddCommand = new RelayCommand(() => NavigationService.Navigate(typeof(NewItemPage)));                                  
        }

        private async Task EditItem()
        {
            if (!IsVisible)
            {
                if (ItemImageLinks.Count == 0)
                    return;

                CloneItemImageLinksList();
            }

            if (IsVisible)
                await UpdateItemsAsync();

            IsVisible = !IsVisible;
        }

        private async Task DeleteItemAsync(ItemImageLink itemImageLink)
        {
            if (itemImageLink.Item.ImageStringName != null)
                await DeleteItemAndImageRequestAsync(itemImageLink);
            else
                await DeleteItemRequestAsync(itemImageLink);
        }

        private async Task DeleteItemAndImageRequestAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item) && await imagesDataAccess.DeleteImageAsync(itemImageLink.Item.ImageStringName))
                ItemImageLinks.Remove(itemImageLink);
        }

        private async Task DeleteItemRequestAsync(ItemImageLink itemImageLink)
        {
            if (!await itemsDataAccess.DeleteAsync(itemImageLink.Item))
                return;

            ItemImageLinks.Remove(itemImageLink);
        }

        private async Task UpdateItemsAsync()
        {
            for (int i = 0; i < itemImageLinksClone.Count; i++)
            {
                if (!StringIsEqual(ItemImageLinks[i].Item.Title, itemImageLinksClone[i].Item.Title) || (!StringIsEqual(ItemImageLinks[i].Item.Description, itemImageLinksClone[i].Item.Description)))
                    if (!await itemsDataAccess.UpdateAsync(ItemImageLinks[i].Item))
                        isVisible = true;
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                await LoadItemsAsync();
                await LoadImagesAsync();
            }
            catch (HttpRequestException ex)
            {
                await PopupService.ShowCouldNotLoadAsync(NavigationService.GoBack, "Items");
            }
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

        private void CloneItemImageLinksList() => itemImageLinksClone = ItemImageLinks.ToList().DeepClone();
    }
}
