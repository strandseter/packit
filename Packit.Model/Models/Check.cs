// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-27-2020
// ***********************************************************************
// <copyright file="Check.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model.NotifyPropertyChanged;

namespace Packit.Model.Models
{
    /// <summary>
    /// Class Check.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// Implements the <see cref="Packit.Model.Models.IDatabase" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// <seealso cref="Packit.Model.Models.IDatabase" />
    public class Check : Observable, IDatabase
    {
        /// <summary>
        /// The is checked
        /// </summary>
        private bool isChecked;

        /// <summary>
        /// Gets or sets the check identifier.
        /// </summary>
        /// <value>The check identifier.</value>
        public int CheckId { get; set; }
        /// <summary>
        /// Gets or sets the trip identifier.
        /// </summary>
        /// <value>The trip identifier.</value>
        public int TripId { get; set; }
        /// <summary>
        /// Gets or sets the backpack identifier.
        /// </summary>
        /// <value>The backpack identifier.</value>
        public int BackpackId { get; set; }
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        public int ItemId { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Item Item { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "<Pending>")]
        public int UserId { get; set; } //I know the naming is an issue. See the document.
        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked { get => isChecked; set => Set(ref isChecked, value); }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetId() => CheckId;
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetUserId() => UserId;
        /// <summary>
        /// Sets the user identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetUserId(int value) => UserId = value;
        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetId(int value) => CheckId = value;
    }
}
