// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-27-2020
// ***********************************************************************
// <copyright file="Item.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Packit.Model
{
    /// <summary>
    /// Class Item.
    /// Implements the <see cref="Packit.Model.BaseInformation" />
    /// </summary>
    /// <seealso cref="Packit.Model.BaseInformation" />
    public class Item : BaseInformation
    {
        /// <summary>
        /// The check
        /// </summary>
        private Check check;

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        public int ItemId { get; set; }
        /// <summary>
        /// Gets the backpacks.
        /// </summary>
        /// <value>The backpacks.</value>
        public virtual ICollection<ItemBackpack> Backpacks { get; } = new List<ItemBackpack>();
        /// <summary>
        /// Gets the checks.
        /// </summary>
        /// <value>The checks.</value>
        public ICollection<Check> Checks { get; } = new List<Check>();
        /// <summary>
        /// Gets or sets the check.
        /// </summary>
        /// <value>The check.</value>
        [NotMapped]
        public Check Check 
        { 
            get => check;
            set => Set(ref check, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetId() => ItemId;
        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        public override void SetId(int value) => ItemId = value;
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"{Title} {ItemId}";
    }
}
