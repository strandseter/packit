using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.ThirdPartyApiModels.Openweathermap
{
    public class Main
    {
        private double temperature;
        private double temperatureFeelsLike;

        [JsonProperty("temp")]
        public double Temperature
        {
            get => temperature;
            set => temperature = Math.Round(value - 273.15, 1);
        }
        [JsonProperty("feels_like")]
        public double TemperatureFeelsLike
        {
            get => temperatureFeelsLike;
            set => temperatureFeelsLike = Math.Round(value - 273.15, 1);
        }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}
