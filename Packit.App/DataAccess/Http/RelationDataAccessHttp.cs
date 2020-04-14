
using Newtonsoft.Json;
using Packit.Model.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class RelationDataAccessHttp<T1, T2> : IRelationDataAccess<T1, T2>
    {
        readonly HttpClient httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri($"http://localhost:52286/api/{typeof(T1).Name}s");

        public async Task<bool> AddEntityToEntityAsync(int leftId, int rightId, string param1, string param2)
        {
            var uri = new Uri($"{baseUri}/{leftId}/{param1}/{rightId}/{param2}");

            HttpResponseMessage result = await httpClient.PutAsync(uri, null);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEntityFromEntityAsync(int leftId, int rightId, string param1, string param2)
        {
            var uri = new Uri($"{baseUri}/{leftId}/{param1}/{rightId}/{param2}");

            HttpResponseMessage result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }

        public async Task<T2[]> GetEntitiesInEntityAsync(int id, string param)
        {
            var uri = new Uri($"{baseUri}/{id}/{param}");

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T2[] entities = JsonConvert.DeserializeObject<T2[]>(json);

            return entities;
        }
    }
}
