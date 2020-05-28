// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-28-2020
// ***********************************************************************
// <copyright file="BaseInformation.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model.Models;
using Packit.Model.NotifyPropertyChanged;
using System.ComponentModel.DataAnnotations;

namespace Packit.Model
{
    /// <summary>
    /// Class BaseInformation.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// Implements the <see cref="Packit.Model.Models.IDatabase" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// <seealso cref="Packit.Model.Models.IDatabase" />
    public abstract class BaseInformation : Observable, IDatabase
    {
        /// <summary>
        /// The title
        /// </summary>
        private string title;
        /// <summary>
        /// The description
        /// </summary>
        private string description;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        [StringLength(20)]
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [StringLength(30)]
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        /// <summary>
        /// Gets or sets the name of the image string.
        /// </summary>
        /// <value>The name of the image string.</value>
        public string ImageStringName { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "<Pending>")]
        public int UserId { get; set; } //I know the naming is a problem. But I did not have time to fix it. Se mye description document.

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseInformation"/> class.
        /// </summary>
        public BaseInformation() { }

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
        /// Gets the identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public abstract int GetId();
        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        public abstract void SetId(int value);
    }
}
