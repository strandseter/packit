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
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("feels_like")]
        public double TemperatureFeelsLike { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}
