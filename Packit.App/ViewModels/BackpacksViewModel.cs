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
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class BackpacksViewModel : Observable
    {
        private readonly IBasicDataAccess<Backpack> backpacksDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();

        private bool isVisible;

        private ICommand loadedCommand;

        public virtual ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));

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
        public ICommand ShareBackpackCommand { get; set; }
        public ObservableCollection<BackpackWithItemsWithImages> BackpackWithItemsWithImagess { get; } = new ObservableCollection<BackpackWithItemsWithImages>();

        public BackpacksViewModel()
        {
            EditCommand = new RelayCommand(async () =>
            {
                var test = 0;
            });

            DeleteCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
               await PopupService.ShowDeleteDialogAsync(DeleteBackpackAsync, param, param.Backpack.Title);
            }, param => param != null);

            NewCommand = new RelayCommand(async () =>
            {
                NavigationService.Navigate(typeof(NewBackpackPage));
            });

            RemoveItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(param.BackpackWithItemsWithImages.Backpack.BackpackId, param.ItemImageLink.Item.ItemId))
                    param.BackpackWithItemsWithImages.ItemImageLinks.Remove(param.ItemImageLink);
            });

            DeleteItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                if (await itemsDataAccess.DeleteAsync(param.ItemImageLink.Item))
                    param.BackpackWithItemsWithImages.ItemImageLinks.Remove(param.ItemImageLink);
            });

            AddItemsCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                var test = param;
            });

            ShareBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                var test = param;
            });

        }

        private async Task DeleteBackpackAsync(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (await backpacksDataAccess.DeleteAsync(backpackWithItemsWithImages.Backpack))
                BackpackWithItemsWithImagess.Remove(backpackWithItemsWithImages);
        }

        private async Task DeleteItemAndImageRequestAsync(ItemImageLink itemImageLink)
        {
            if (await itemsDataAccess.DeleteAsync(itemImageLink.Item) && await imagesDataAccess.DeleteImageAsync(itemImageLink.Item.ImageStringName)) ;
                //ItemImageLinks.Remove(itemImageLink);
        }

        private async Task LoadDataAsync()
        {
            await LoadBackpacksAsync();
            await LoadItemImagesAsync();
        }

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

        private async Task LoadItemImagesAsync()
        {
            foreach (var bwiwi in BackpackWithItemsWithImagess)
            {
                foreach (var itemImageLink in bwiwi.ItemImageLinks)
                    itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
            }
        }
    }
}
