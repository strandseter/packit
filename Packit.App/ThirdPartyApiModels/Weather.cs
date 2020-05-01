using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.ThirdPartyApiModels
{
    public class Weather
    {
        [JsonProperty("name")]
        public string City { get; set; }
        [JsonProperty("weather.main")]
        public string Type { get; set; }
        [JsonProperty("weather.description")]
        public string TypeDescription { get; set; }
        [JsonProperty("main.temp")]
        public double KelvinTemperatureActual { get; set; }
        [JsonProperty("main.feels_like")]
        public double KelvinTemperatureFeelsLike { get; set; }
        [JsonProperty("wind.speed")]
        public double WindSpeed { get; set; }
    }
}
