// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-18-2020
// ***********************************************************************
// <copyright file="ItemBackpack.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    /// <summary>
    /// Class ItemBackpack.
    /// Implements the <see cref="Packit.Model.IManyToMany" />
    /// </summary>
    /// <seealso cref="Packit.Model.IManyToMany" />
    public class ItemBackpack : IManyToMany
    {
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        public virtual int ItemId { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public virtual Item Item { get; set; }
        /// <summary>
        /// Gets or sets the backpack identifier.
        /// </summary>
        /// <value>The backpack identifier.</value>
        public virtual int BackpackId { get; set; }
        /// <summary>
        /// Gets or sets the backpack.
        /// </summary>
        /// <value>The backpack.</value>
        public virtual Backpack Backpack { get; set; }

        /// <summary>
        /// Sets the left identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SetLeftId(int id) => ItemId = id;
        /// <summary>
        /// Gets the left identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetLeftId() => ItemId;
        /// <summary>
        /// Sets the right identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SetRightId(int id) => BackpackId = id;
        /// <summary>
        /// Gets the right identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetRightId() => BackpackId;
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"LeftId: {ItemId}, RightId: {BackpackId}";
    }
}
