using Newtonsoft.Json;
using Packit.App.Services;
using Packit.Exceptions;
using Packit.Model.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class BasicDataAccessHttp<T> : IBasicDataAccess<T> where T : IDatabase
    {
        private readonly HttpClient httpClient = new HttpClient();
        private static readonly Uri baseUri = new Uri($"http://localhost:52286/api/{typeof(T).Name}s");

        public async Task<bool> AddAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            string json = JsonConvert.SerializeObject(entity);

            HttpResponseMessage result;

            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                result = await httpClient.PostAsync(baseUri, content);
            }

            if (!result.IsSuccessStatusCode) return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedEntity = JsonConvert.DeserializeObject<T>(json);
            entity.SetId(returnedEntity.GetId());

            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            string json = JsonConvert.SerializeObject(entity);

            HttpResponseMessage result;

            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                result = await httpClient.PutAsync(uri, content);
            }

            return result.IsSuccessStatusCode;
        }

        public async Task<T[]> GetAllAsync()
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }

        public async Task<T[]> GetAllWithChildEntitiesAsync()
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/all");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }

        public async Task<T> GetByIdAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T outEntity = JsonConvert.DeserializeObject<T>(json);

            return outEntity;
        }

        public async Task<T> GetByIdWithChildEntitiesAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{entity.GetId()}/all");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T outEntity = JsonConvert.DeserializeObject<T>(json);

            return outEntity;
        }
    }
}
