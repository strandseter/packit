// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-03-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="Main.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace Packit.App.ThirdPartyApiModels.Openweathermap
{
    /// <summary>
    /// Class Main.
    /// </summary>
    public class Main
    {
        /// <summary>
        /// The temperature
        /// </summary>
        private double temperature;
        /// <summary>
        /// The temperature feels like
        /// </summary>
        private double temperatureFeelsLike;

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>The temperature.</value>
        [JsonProperty("temp")]
        public double Temperature
        {
            get => temperature;
            set => temperature = Math.Round(value - 273.15, 1);
        }
        /// <summary>
        /// Gets or sets the temperature feels like.
        /// </summary>
        /// <value>The temperature feels like.</value>
        [JsonProperty("feels_like")]
        public double TemperatureFeelsLike
        {
            get => temperatureFeelsLike;
            set => temperatureFeelsLike = Math.Round(value - 273.15, 1);
        }
        /// <summary>
        /// Gets or sets the humidity.
        /// </summary>
        /// <value>The humidity.</value>
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}
