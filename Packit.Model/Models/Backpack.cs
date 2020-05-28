// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-28-2020
// ***********************************************************************
// <copyright file="Backpack.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Packit.Model
{
    /// <summary>
    /// Class Backpack.
    /// Implements the <see cref="Packit.Model.BaseInformation" />
    /// </summary>
    /// <seealso cref="Packit.Model.BaseInformation" />
    public class Backpack : BaseInformation
    {
        /// <summary>
        /// Gets or sets the backpack identifier.
        /// </summary>
        /// <value>The backpack identifier.</value>
        public int BackpackId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is shared.
        /// </summary>
        /// <value><c>true</c> if this instance is shared; otherwise, <c>false</c>.</value>
        public bool IsShared { get; set; }
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public virtual ICollection<ItemBackpack> Items { get; } = new List<ItemBackpack>();
        /// <summary>
        /// Gets the trips.
        /// </summary>
        /// <value>The trips.</value>
        public virtual ICollection<BackpackTrip> Trips { get; } = new List<BackpackTrip>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Backpack"/> class.
        /// </summary>
        public Backpack() 
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetId() => BackpackId;

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        public override void SetId(int value) => BackpackId = value;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"{Title}, {BackpackId}";
    }
}
