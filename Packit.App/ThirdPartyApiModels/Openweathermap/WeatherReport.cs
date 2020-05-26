// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="WeatherReport.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Packit.App.Helpers;
using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ThirdPartyApiModels.Openweathermap
{
    /// <summary>
    /// Class WeatherReport.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public class WeatherReport : Observable
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [JsonProperty("name")]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the weathers.
        /// </summary>
        /// <value>The weathers.</value>
        [JsonProperty("weather")]
        public List<Weather> Weathers { get; set; }
        /// <summary>
        /// Gets or sets the wind.
        /// </summary>
        /// <value>The wind.</value>
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        /// <summary>
        /// Gets or sets the main.
        /// </summary>
        /// <value>The main.</value>
        [JsonProperty("main")]
        public Main Main { get; set; }
    }
}
