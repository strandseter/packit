using Newtonsoft.Json;
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

        //TODO: Remove
        private string dummyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJpZCI6IjQiLCJuYmYiOjE1ODczNzg4MDEsImV4cCI6MTYxMzI5ODgwMSwiaWF0IjoxNTg3Mzc4ODAxfQ.vjCQhH4TKQcFbmM42ZM2VCIYYRGO_49LEWm6zWuWK00";

        public async Task<bool> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            string json = JsonConvert.SerializeObject(entity);
            HttpResponseMessage result = await httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!result.IsSuccessStatusCode) return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedEntity = JsonConvert.DeserializeObject<T>(json);
            entity.SetId(returnedEntity.GetId());

            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            string json = JsonConvert.SerializeObject(entity);
            HttpResponseMessage result = await httpClient.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public async Task<T[]> GetAllAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }

        public async Task<T[]> GetAllWithChildEntitiesAsync()
        {
            var uri = new Uri($"{baseUri}/all");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }

        public async Task<T> GetByIdAsync(T entity)
        {
            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T outEntity = JsonConvert.DeserializeObject<T>(json);

            return outEntity;
        }

        public async Task<T> GetByIdWithChildEntitiesAsync(T entity)
        {
            var uri = new Uri($"{baseUri}/{entity.GetId()}/all");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T outEntity = JsonConvert.DeserializeObject<T>(json);

            return outEntity;
        }
    }
}
