using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ThirdPartyApiModels.Openweathermap
{
    public class Weather
    {
        private string description;
        private BitmapImage iconImage;

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public string Type { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("description")]
        public string Description
        {
            get => description;
            set => description = UppercaseFirst(value);
        }
        public BitmapImage IconImage { get => iconImage; set => iconImage = value; }

        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0], CultureInfo.CurrentCulture);
            return new string(a);
        }
    }
}
