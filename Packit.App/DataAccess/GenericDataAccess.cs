using Newtonsoft.Json;
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class GenericDataAccess<T> : IGenericDataAccess<T> where T : IDatabase
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:52586/api");

        public async Task<bool> Add(T entity, string uriExtension)
        {
            var uri = new Uri($"{baseUri}/{uriExtension}");

            string json = JsonConvert.SerializeObject(entity);
            HttpResponseMessage result = await _httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!result.IsSuccessStatusCode) return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedEntity = JsonConvert.DeserializeObject<T>(json);
            entity.SetId(returnedEntity.GetId());

            return true;
        }

        public async Task<bool> Delete(T entity, string uriExtension)
        {
            var uri = new Uri($"{baseUri}/{uriExtension}/{entity.GetId()}");

            HttpResponseMessage result = await _httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }

        public Task<bool> Edit(T entity, int id, string uriExtension)
        {
            throw new NotImplementedException();
        }

        public async Task<T[]> GetAll(string uriExtension)
        {
            var uri = new Uri($"{baseUri}/{uriExtension}");

            HttpResponseMessage result = await _httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }
    }
}
