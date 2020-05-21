using Newtonsoft.Json;
using Packit.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.Http
{
    public class CustomTripDataAccessHttp : ICustomTripDataAccess
    {
        private readonly HttpClient httpClient = new HttpClient();
        private static readonly Uri baseUri = new Uri($"http://localhost:52286/api/trips");

        public async Task<Trip> GetNextTrip()
        {
            var uri = new Uri($"{baseUri}/next");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            var nextTrip = JsonConvert.DeserializeObject<Trip>(json);

            return nextTrip;
        }

        //public async Task<T> GetByIdAsync(T entity)
        //{
        //    var uri = new Uri($"{baseUri}/{entity.GetId()}");
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

        //    HttpResponseMessage result = await httpClient.GetAsync(uri);
        //    string json = await result.Content.ReadAsStringAsync();
        //    T outEntity = JsonConvert.DeserializeObject<T>(json);

        //    return outEntity;
        //}
    }
}
