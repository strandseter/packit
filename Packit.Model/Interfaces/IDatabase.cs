// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-27-2020
// ***********************************************************************
// <copyright file="IDatabase.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Packit.Model.Models
{
    /// <summary>
    /// Interface IDatabase
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int GetId();
        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetId(int value);
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int GetUserId();
        /// <summary>
        /// Sets the user identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetUserId(int value);
    }
}
