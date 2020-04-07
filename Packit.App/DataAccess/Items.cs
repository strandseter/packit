using Newtonsoft.Json;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class Items
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri itemsBaseUri = new Uri("http://localhost:52586/api/items"); //TODO: Replace URI

        public async Task<Item[]> GetItemsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(itemsBaseUri);
            string json = await result.Content.ReadAsStringAsync();
            Item[] items = JsonConvert.DeserializeObject<Item[]>(json);

            return items;
        }
    }
}
