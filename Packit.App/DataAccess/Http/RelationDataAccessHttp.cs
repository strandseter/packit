using Newtonsoft.Json;
using Packit.App.Services;
using Packit.Exceptions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class RelationDataAccessHttp<T1, T2> : IRelationDataAccess<T1, T2>
    {
        readonly HttpClient httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri($"http://localhost:52286/api/{typeof(T1).Name}s");

        public async Task<bool> AddEntityToEntityAsync(int leftId, int rightId)
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{leftId}/{typeof(T2).Name}s/{rightId}/create");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.PutAsync(uri, null);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEntityFromEntityAsync(int leftId, int rightId)
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{leftId}/{typeof(T2).Name}s/{rightId}/delete");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }

        public async Task<T2[]> GetEntitiesInEntityAsync(int id, string param)
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{id}/{param}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T2[] entities = JsonConvert.DeserializeObject<T2[]>(json);

            return entities;
        }
    }
}
