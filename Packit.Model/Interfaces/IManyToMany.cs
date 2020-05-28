// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-15-2020
// ***********************************************************************
// <copyright file="IManyToMany.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Packit.Model
{
    /// <summary>
    /// Interface IManyToMany
    /// </summary>
    public interface IManyToMany
    {
        /// <summary>
        /// Sets the left identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void SetLeftId(int id);
        /// <summary>
        /// Sets the right identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void SetRightId(int id);
        /// <summary>
        /// Gets the left identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int GetLeftId();
        /// <summary>
        /// Gets the right identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int GetRightId();
    }
}
