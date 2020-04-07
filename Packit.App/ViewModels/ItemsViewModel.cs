using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Packit.App.Helpers;
using Packit.Model;
using Packit.App.DataAccess;

namespace Packit.App.ViewModels
{
    public class ItemsViewModel : Observable
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        private Items itemsDataAccess = new Items();

        public ItemsViewModel()
        {
        }

        internal async Task LoadItemsAsync()
        {
            var items = await itemsDataAccess.GetItemsAsync();
            foreach (Item i in items)
                Items.Add(i);
        }
    }
}
