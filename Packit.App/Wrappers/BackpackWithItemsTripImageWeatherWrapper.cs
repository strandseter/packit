// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-10-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpackWithItemsTripImageWeatherWrapper.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.DataLinks;

namespace Packit.App.Wrappers
{
    /// <summary>
    /// Class BackpackWithItemsTripImageWeatherWrapper.
    /// </summary>
    public class BackpackWithItemsTripImageWeatherWrapper
    {
        /// <summary>
        /// Gets or sets the backpack.
        /// </summary>
        /// <value>The backpack.</value>
        public BackpackWithItemsWithImages Backpack { get; set; }
        /// <summary>
        /// Gets or sets the trip.
        /// </summary>
        /// <value>The trip.</value>
        public TripImageWeatherLink Trip { get; set; }
    }
}
