using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.ThirdPartyApiModels.Openweathermap
{
    public class WeatherReport
    {
        [JsonProperty("name")]
        public string City { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weathers { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
    }
}
