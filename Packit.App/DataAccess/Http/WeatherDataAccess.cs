using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Packit.App.ThirdPartyApiModels;
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

        private static readonly Uri WeatherCurrentUri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q=barcelona&appid=a9ebc7ad36dd10689256ab2cdfd0f770");
        private static readonly Uri forecastWeatherUri = new Uri("api.openweathermap.org/data/2.5/forecast/daily?id=CITY&cnt=COUNT&appid=KEY");
        private const string token = "a9ebc7ad36dd10689256ab2cdfd0f770";

        public async Task<Weather> GetCurrentWeatherAsync(string locaction)
        {
            var uri = new Uri($"{baseUri}/weather?q={locaction}&appid={token}");

            HttpResponseMessage result = await httpClient.GetAsync(uri);
            string json = await result.Content.ReadAsStringAsync();
            JObject weatherData = JObject.Parse(json);

            var test = weatherData["weather"]["main"].Value<string>();

            var weather = JsonConvert.DeserializeObject<Weather>(json);

            var weather = new Weather()
            {
                City = weatherData["name"].ToObject<string>(),
                Type = weatherData["weather"]["main"].Value<string>("main"),
                //TypeDescription = weatherData["weather"]["description"].ToObject<string>(),
                //KelvinTemperatureActual = weatherData["main"]["temp"].ToObject<double>(),
                //KelvinTemperatureFeelsLike = weatherData["main"]["feels_like"].ToObject<double>()
            };


            return weather;
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
