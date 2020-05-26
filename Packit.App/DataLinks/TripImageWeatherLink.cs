// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="TripImageWeatherLink.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.Helpers;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataLinks
{
    /// <summary>
    /// Class TripImageWeatherLink.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public class TripImageWeatherLink : Observable
    {
        /// <summary>
        /// The image
        /// </summary>
        private BitmapImage image;
        /// <summary>
        /// The weather report
        /// </summary>
        private WeatherReport weatherReport;
        /// <summary>
        /// The trip
        /// </summary>
        private Trip trip;

        /// <summary>
        /// Gets or sets the trip.
        /// </summary>
        /// <value>The trip.</value>
        public Trip Trip
        {
            get => trip;
            set => Set(ref trip, value);
        }
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public BitmapImage Image
        {
            get => image;
            set => Set(ref image, value);
        }

        /// <summary>
        /// Gets or sets the weather report.
        /// </summary>
        /// <value>The weather report.</value>
        public WeatherReport WeatherReport
        {
            get => weatherReport;
            set => Set(ref weatherReport, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TripImageWeatherLink"/> class.
        /// </summary>
        /// <param name="trip">The trip.</param>
        public TripImageWeatherLink(Trip trip) => Trip = trip;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => trip.Title;
    }
}
