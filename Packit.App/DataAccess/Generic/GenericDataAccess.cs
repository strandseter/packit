using Newtonsoft.Json;
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess
{
    public class GenericDataAccess<T> : IGenericDataAccess<T> where T : IDatabase
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:52286/api");

        private string dummyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMwMjIiLCJpZCI6IjMwMjIiLCJuYmYiOjE1ODYxNzkyODksImV4cCI6MTYxMjA5OTI4OSwiaWF0IjoxNTg2MTc5Mjg5fQ.oWdw9HHqqjbbBCeJ4GQTCBjS6zfpTbsYbdj6_npMvh4";

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

        public Task<bool> Edit(T entity, string uriExtension)
        {
            throw new NotImplementedException();
        }

        public async Task<T[]> GetAll(string uriExtension)
        {
            var uri = new Uri($"{baseUri}/{uriExtension}");

            //TODO: Remove dummy token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dummyToken);

            HttpResponseMessage result = await _httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }
    }
}
