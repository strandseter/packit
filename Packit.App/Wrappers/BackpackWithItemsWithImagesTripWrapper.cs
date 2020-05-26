// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpackWithItemsWithImagesTripWrapper.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.DataLinks;
using System.Collections.ObjectModel;

namespace Packit.App.Wrappers
{
    /// <summary>
    /// Class BackpackWithItemsWithImagesTripWrapper.
    /// </summary>
    public class BackpackWithItemsWithImagesTripWrapper
    {
#pragma warning disable CA2227 // Collection properties should be read only
        /// <summary>
        /// Gets or sets the backpack with items with images.
        /// </summary>
        /// <value>The backpack with items with images.</value>
        public ObservableCollection<BackpackWithItemsWithImages>  BackpackWithItemsWithImages { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
        /// <summary>
        /// Gets or sets the trip image weather link.
        /// </summary>
        /// <value>The trip image weather link.</value>
        public TripImageWeatherLink TripImageWeatherLink { get; set; }
    }
}
