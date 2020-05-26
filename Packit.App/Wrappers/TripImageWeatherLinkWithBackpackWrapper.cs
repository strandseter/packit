// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-26-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="TripImageWeatherLinkWithBackpackWrapper.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.DataLinks;
using Packit.Model;

namespace Packit.App.Wrappers
{
    /// <summary>
    /// Class TripImageWeatherLinkWithBackpackWrapper.
    /// </summary>
    public class TripImageWeatherLinkWithBackpackWrapper
    {
        /// <summary>
        /// Gets or sets the trip image weather link.
        /// </summary>
        /// <value>The trip image weather link.</value>
        public TripImageWeatherLink TripImageWeatherLink { get; set; }
        /// <summary>
        /// Gets or sets the backpack.
        /// </summary>
        /// <value>The backpack.</value>
        public Backpack Backpack { get; set; }
    }
}
