// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-27-2020
// ***********************************************************************
// <copyright file="Trip.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model.Models;
using System;
using System.Collections.Generic;

namespace Packit.Model
{
    /// <summary>
    /// Class Trip.
    /// Implements the <see cref="Packit.Model.BaseInformation" />
    /// Implements the <see cref="System.ICloneable" />
    /// </summary>
    /// <seealso cref="Packit.Model.BaseInformation" />
    /// <seealso cref="System.ICloneable" />
    public class Trip : BaseInformation, ICloneable
    {
        /// <summary>
        /// The destination
        /// </summary>
        private string destination;
        /// <summary>
        /// The depature date
        /// </summary>
        private DateTimeOffset depatureDate = DateTimeOffset.Now;

        /// <summary>
        /// Gets or sets the trip identifier.
        /// </summary>
        /// <value>The trip identifier.</value>
        public int TripId { get; set; }
        /// <summary>
        /// Gets the backpacks.
        /// </summary>
        /// <value>The backpacks.</value>
        public virtual ICollection<BackpackTrip> Backpacks { get; } = new List<BackpackTrip>();

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        public string Destination 
        { 
            get => destination;
            set => Set(ref destination, value);
        }

        /// <summary>
        /// Gets or sets the depature date.
        /// </summary>
        /// <value>The depature date.</value>
        public DateTimeOffset DepatureDate 
        { 
            get => depatureDate;
            set => Set(ref depatureDate, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trip"/> class.
        /// </summary>
        public Trip()
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetId() => TripId;

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        public override void SetId(int value) => TripId = value;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"{Title}, ";

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone() => MemberwiseClone();
    }
}
