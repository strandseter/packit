using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Packit.App.Helpers;
using Packit.Model;
using Packit.App.DataAccess;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System.Windows.Input;
using Packit.App.Factory;

namespace Packit.App.ViewModels
{
    public class ItemsViewModel : Observable
    {
        private readonly IDataAccess<Item> itemsDataAccess = new DataAccessFactory<Item>().Create();

        private readonly Images imagesDataAccess = new Images();

        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ObservableCollection<ItemImageLink> ItemImageLinks { get; } = new ObservableCollection<ItemImageLink>();
        public ItemImageLink SelectedItem { get; set; }

        public ItemsViewModel()
        {
            DeleteCommand = new RelayCommand<ItemImageLink>(async itemImageLink =>
                                                            {
                                                                if (await itemsDataAccess.Delete((Item)itemImageLink.Item) && await imagesDataAccess.DeleteImage(itemImageLink.Item.ImageStringName))
                                                                    ItemImageLinks.Remove(itemImageLink);
                                                            }, itemImageLink => itemImageLink != null);
        }

        internal async Task LoadData()
        {
            await LoadItemsAsync();
            await LoadImages();
        }

        private async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAll();

            foreach (Item i in items)
                ItemImageLinks.Add(new ItemImageLink() { Item = i});             
        }

        private async Task LoadImages()
        {
            foreach (ItemImageLink iml in ItemImageLinks)
                iml.Image = await imagesDataAccess.GetImage(iml.Item.ImageStringName);
        }
    }
}
