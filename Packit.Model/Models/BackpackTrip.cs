// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-28-2020
// ***********************************************************************
// <copyright file="BackpackTrip.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Packit.Model
{
    /// <summary>
    /// Class BackpackTrip.
    /// Implements the <see cref="Packit.Model.IManyToMany" />
    /// </summary>
    /// <seealso cref="Packit.Model.IManyToMany" />
    public class BackpackTrip : IManyToMany
    {
        /// <summary>
        /// Gets or sets the backpack identifier.
        /// </summary>
        /// <value>The backpack identifier.</value>
        public virtual int BackpackId { get; set; }
        /// <summary>
        /// Gets or sets the backpack.
        /// </summary>
        /// <value>The backpack.</value>
        public virtual Backpack Backpack {get; set; }
        /// <summary>
        /// Gets or sets the trip identifier.
        /// </summary>
        /// <value>The trip identifier.</value>
        public virtual int TripId { get; set; }
        /// <summary>
        /// Gets or sets the trip.
        /// </summary>
        /// <value>The trip.</value>
        public virtual Trip Trip { get; set; }

        //This model could have been simplifed. But I noticed it right before hand in.
        /// <summary>
        /// Sets the left identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SetLeftId(int id) => BackpackId = id;
        /// <summary>
        /// Sets the right identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SetRightId(int id) => TripId = id;
        /// <summary>
        /// Gets the left identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetLeftId() => BackpackId;
        /// <summary>
        /// Gets the right identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetRightId() => TripId;
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"LeftId: {BackpackId}, RightId: {TripId}";
    }
}
