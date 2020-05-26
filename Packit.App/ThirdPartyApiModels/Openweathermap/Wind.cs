// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-03-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="Wind.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace Packit.App.ThirdPartyApiModels.Openweathermap
{
    /// <summary>
    /// Class Wind.
    /// </summary>
    public class Wind
    {
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}
