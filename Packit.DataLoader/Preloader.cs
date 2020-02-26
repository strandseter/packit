using System;


using System.Collections.ObjectModel;
using System.Net.Http;

namespace Packit.DataLoader
{
    public class Preloader<T>
    {
        public ObservableCollection<T> Load<T>(Uri uri)
        {
            var

            return new ObservableCollection<T>();
        }

        //private async void InitializeItems()
        //{
        //    var result = await httpClient.GetAsync(itemsUri);
        //    var json = await result.Content.ReadAsStringAsync();
        //    var items = DeserializeObject<Item[]>(json);
        //    foreach (Item item in items)
        //        ItemsList.Add(item);
        //}
    }
}
