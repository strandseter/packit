using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.Http
{
    public class WeatherDataAccess
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly Uri baseUri = new Uri("http://api.openweathermap.org/data/2.5");
        private const string currentWeatherToken = "a9ebc7ad36dd10689256ab2cdfd0f770";

        public async Task<WeatherReport> GetCurrentWeatherReportAsync(string locaction)
        {
            var uri = new Uri($"{baseUri}/weather?q={locaction}&appid={currentWeatherToken}");

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();

            var weatherReport = JsonConvert.DeserializeObject<WeatherReport>(json);

            return weatherReport;
        }

        //public async Task<T[]> GetAllAsync()
        //{
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dummyToken);

        //    HttpResponseMessage result = await httpClient.GetAsync(baseUri);
        //    string json = await result.Content.ReadAsStringAsync();
        //    T[] entities = JsonConvert.DeserializeObject<T[]>(json);

        //    return entities;
        //}
    }
}
