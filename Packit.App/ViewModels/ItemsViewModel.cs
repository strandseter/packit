using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Packit.App.Helpers;
using Packit.Model;
using Packit.App.DataAccess;
using System.Windows.Input;
using Packit.App.Factories;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.DataLinks;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Packit.Extensions;

namespace Packit.App.ViewModels
{
    public class ItemsViewModel : Observable
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private ICommand loadedCommand;
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private bool isVisible;
        private IList<ItemImageLink> itemImageLinksClone;

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (value == isVisible) return;
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadData));
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ObservableCollection<ItemImageLink> ItemImageLinks { get; } = new ObservableCollection<ItemImageLink>();

        public ItemsViewModel()
        {
            DeleteCommand = new RelayCommand<ItemImageLink>(async param =>
            {
                if(param.Item.ImageStringName != null)
                    await DeleteItemAndImageAsync(param);
                else
                    await DeleteItemAsync(param);
                }, param => param != null);

            EditCommand = new RelayCommand(async () =>
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
            });

            AddCommand = new RelayCommand(() => NavigationService.Navigate(typeof(NewItemPage)));                                  
        }

        protected async void LoadData()
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
        }

        private async Task DeleteItemAndImageAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item) && await imagesDataAccess.DeleteImageAsync(itemImageLink.Item.ImageStringName))
                ItemImageLinks.Remove(itemImageLink);
        }

        private async Task DeleteItemAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item))
                ItemImageLinks.Remove(itemImageLink);
        }

        private async Task UpdateItemsAsync()
        {
            for (int i = 0; i < ItemImageLinks.Count; i++)
            {
                if (!StringIsEqual(ItemImageLinks[i].Item.Title, itemImageLinksClone[i].Item.Title) || (!StringIsEqual(ItemImageLinks[i].Item.Title, itemImageLinksClone[i].Item.Title)))
                    if (!await itemsDataAccess.UpdateAsync(ItemImageLinks[i].Item))
                        isVisible = true;
            }
        }

        private void CloneItemImageLinksList() => itemImageLinksClone = ItemImageLinks.ToList().DeepClone();

        private async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAllAsync();

            foreach (var i in items)
            {
                ItemImageLinks.Add(new ItemImageLink() { Item = i });
            }
        }

        private async Task LoadImagesAsync()
        {
            foreach (var iml in ItemImageLinks)
                iml.Image = await imagesDataAccess.GetImageAsync(iml.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
        }

        private bool StringIsEqual(string title1, string title2) => title1.Equals(title2, StringComparison.CurrentCulture);

    }
}
