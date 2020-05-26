// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="Weather.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Packit.App.Helpers;
using Packit.Model.NotifyPropertyChanged;
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
    /// <summary>
    /// Class Weather.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public class Weather : Observable
    {
        /// <summary>
        /// The description
        /// </summary>
        private string description;
        /// <summary>
        /// The icon image
        /// </summary>
        private BitmapImage iconImage;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("main")]
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
        public string Description
        {
            get => description;
            set => description = UppercaseFirst(value);
        }
        /// <summary>
        /// Gets or sets the icon image.
        /// </summary>
        /// <value>The icon image.</value>
        public BitmapImage IconImage
        {
            get => iconImage;
            set => Set(ref iconImage, value);
        }

        /// <summary>
        /// Uppercases the first.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0], CultureInfo.CurrentCulture);
            return new string(a);
        }
    }
}
