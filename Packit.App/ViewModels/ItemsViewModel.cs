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
        public ObservableCollection<Item> Items { get;} = new ObservableCollection<Item>();

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
            }
            catch(Exception ex)
            {
                //Display error to user
            }
            
        }
    }
}
