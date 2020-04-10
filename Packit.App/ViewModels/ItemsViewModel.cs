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
        public ObservableCollection<Item> Items { get;} = new ObservableCollection<Item>();
        public ObservableCollection<BitmapImage> Images { get; } = new ObservableCollection<BitmapImage>();

        private readonly IGenericDataAccess<Item> itemsDataAccess = new GenericDataAccess<Item>();

        public ItemsViewModel()
        {
        }

        internal async Task LoadItemsAsync()
        {
            try
            {
                var items = await itemsDataAccess.GetAll("items");

                foreach (Item i in items)
                    Items.Add(i);

                await LoadImages();
            }
            catch(Exception ex)
            {
                //Display error to user
            }
        }

        private async Task LoadImages()
        {
            foreach(Item i in Items)
            {
                var image = await itemsDataAccess.GetImage(i.ImageStringName);
                Images.Add(image);
            }
        }

    }
}
