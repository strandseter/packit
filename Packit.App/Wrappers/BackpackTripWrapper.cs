// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-23-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpackTripWrapper.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model;

namespace Packit.App.Wrappers
{
    /// <summary>
    /// Class BackpackTripWrapper.
    /// </summary>
    public class BackpackTripWrapper
    {
        /// <summary>
        /// Gets or sets the backpack.
        /// </summary>
        /// <value>The backpack.</value>
        public Backpack Backpack { get; set; }
        /// <summary>
        /// Gets or sets the trip.
        /// </summary>
        /// <value>The trip.</value>
        public Trip Trip { get; set; }
    }
}
