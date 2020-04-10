using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Packit.App.Helpers;
using Packit.Model;
using Packit.App.DataAccess;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ViewModels
{
    public class ItemsViewModel : Observable
    {
        public ObservableCollection<ItemImageLink> ItemImageLinks { get; } = new ObservableCollection<ItemImageLink>();

        private readonly IItems itemsDataAccess = new Items();

        private readonly Images imagesDataAccess = new Images();

        public ItemsViewModel()
        {
        }

        internal async Task LoadData()
        {
            await LoadItemsAsync();
            await LoadImages();
        }

        private async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetAll("items");

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
