// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-01-2020
//
// Last Modified By : ander
// Last Modified On : 05-15-2020
// ***********************************************************************
// <copyright file="WeatherDataAccess.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess.Http
{
    /// <summary>
    /// Class WeatherDataAccess.
    /// </summary>
    public class WeatherDataAccess
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient = new HttpClient();
        /// <summary>
        /// The base URI
        /// </summary>
        private readonly Uri baseUri = new Uri("http://api.openweathermap.org/data/2.5");
        /// <summary>
        /// The base URI icon
        /// </summary>
        private readonly Uri baseUriIcon = new Uri("http://openweathermap.org/img/wn");
        /// <summary>
        /// The current weather token
        /// </summary>
        private const string currentWeatherToken = "a9ebc7ad36dd10689256ab2cdfd0f770";

        /// <summary>
        /// get current weather report as an asynchronous operation.
        /// </summary>
        /// <param name="locaction">The locaction.</param>
        /// <returns>WeatherReport.</returns>
        /// <exception cref="HttpRequestException"></exception>
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

        /// <summary>
        /// get current weather icon as an asynchronous operation.
        /// </summary>
        /// <param name="iconId">The icon identifier.</param>
        /// <returns>BitmapImage.</returns>
        public async Task<BitmapImage> GetCurrentWeatherIconAsync(string iconId)
        {
            var uri = new Uri($"{baseUriIcon}/{iconId}@2x.png");

            BitmapImage bitmap = new BitmapImage();

            HttpResponseMessage result = await httpClient.GetAsync(uri);

            if (result == null || !result.IsSuccessStatusCode)
                return new BitmapImage(new Uri("ms-appx:///Assets/grey.jpg"));

            bitmap.UriSource = uri;

            return bitmap;
        }
    }
}
