using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess.Http
{
    public class WeatherDataAccess
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly Uri baseUri = new Uri("http://api.openweathermap.org/data/2.5");
        private readonly Uri baseUriIcon = new Uri("http://openweathermap.org/img/wn");
        private const string currentWeatherToken = "a9ebc7ad36dd10689256ab2cdfd0f770";

        public async Task<WeatherReport> GetCurrentWeatherReportAsync(string locaction)
        {
            var uri = new Uri($"{baseUri}/weather?q={locaction}&appid={currentWeatherToken}");

            HttpResponseMessage result = await httpClient.GetAsync(uri);

            if (result == null || !result.IsSuccessStatusCode)
                throw new HttpRequestException();

            string json = await result.Content.ReadAsStringAsync();

            var weatherReport = JsonConvert.DeserializeObject<WeatherReport>(json);

            return weatherReport;
        }

        public async Task<BitmapImage> GetCurrentWeatherIconAsync(string iconId)
        {
            var uri = new Uri($"{baseUriIcon}/{iconId}@2x.png");

            BitmapImage bitmap = new BitmapImage();

            HttpResponseMessage result = await httpClient.GetAsync(uri);

            if (result == null || !result.IsSuccessStatusCode)
                return new BitmapImage(new Uri("ms-appx:///Assets/generictrip.jpg"));

            bitmap.UriSource = uri;

            return bitmap;
        }
    }
}
