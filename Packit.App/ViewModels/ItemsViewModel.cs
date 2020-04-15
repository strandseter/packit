using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Packit.App.Helpers;
using Packit.Model;
using Packit.App.DataAccess;
using System.Windows.Input;
using Packit.App.Factory;
using Packit.App.Services;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;
using System.Collections;
using Packit.App.Views;

namespace Packit.App.ViewModels
{
    public class ItemsViewModel : Observable
    {
        private readonly IBasicDataAccess<Item> itemsDataAccess = new BasicDataAccessFactory<Item>().CreateBasicDataAccess();
        private ICommand loadedCommand;
        private readonly Images imagesDataAccess = new Images();

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(LoadData));
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ObservableCollection<ItemImageLink> ItemImageLinks { get; } = new ObservableCollection<ItemImageLink>();

        public ItemsViewModel()
        {
            DeleteCommand = new RelayCommand<ItemImageLink>(async itemImageLink =>
                                                            {
                                                                if (await itemsDataAccess.DeleteAsync(itemImageLink.Item) && await imagesDataAccess.DeleteImageAsync(itemImageLink.Item.ImageStringName))
                                                                    ItemImageLinks.Remove(itemImageLink);
                                                            }, itemImageLink => itemImageLink != null);

            EditCommand = new RelayCommand<ItemImageLink>(itemImageLink =>
                                                            {
                                                                NavigationService.Navigate(typeof(EditItemPage), itemImageLink);
                                                            }, itemImageLink => itemImageLink != null);

            AddCommand = new RelayCommand(() => NavigationService.Navigate(typeof(NewItemPage)));                                  
        }

        private async void LoadData()
        {
            await LoadItemsAsync();
            await LoadImagesAsync();
        }

        private async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAllAsync();

            foreach (Item i in items)
                ItemImageLinks.Add(new ItemImageLink() { Item = i });
        }

        private async Task LoadImagesAsync()
        {
            foreach (ItemImageLink iml in ItemImageLinks)
                iml.Image = await imagesDataAccess.GetImageAsync(iml.Item.ImageStringName);
        }
    }
}
