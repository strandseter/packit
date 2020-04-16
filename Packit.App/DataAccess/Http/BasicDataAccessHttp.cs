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

        private string dummyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjgiLCJpZCI6IjgiLCJuYmYiOjE1ODcwNjU1NTQsImV4cCI6MTYxMjk4NTU1NCwiaWF0IjoxNTg3MDY1NTU0fQ.SxlDElBO_S0lQaV8DRRpvyEny5yjU6C9wWXaIQUJ4B8";

        public async Task<bool> AddAsync(T entity)
        {
            var uri = new Uri($"{baseUri}/create");

            string json = JsonConvert.SerializeObject(entity);
            HttpResponseMessage result = await httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!result.IsSuccessStatusCode) return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedEntity = JsonConvert.DeserializeObject<T>(json);
            entity.SetId(returnedEntity.GetId());

            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dummyToken);

            HttpResponseMessage result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var uri = new Uri($"{baseUri}/{entity.GetId()}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dummyToken);

            string json = JsonConvert.SerializeObject(entity);
            HttpResponseMessage result = await httpClient.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public async Task<T[]> GetAllAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dummyToken);

            HttpResponseMessage result = await httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }
    }
}
