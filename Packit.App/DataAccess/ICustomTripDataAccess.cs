// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-25-2020
// ***********************************************************************
// <copyright file="ICustomTripDataAccess.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model;
using System;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    /// <summary>
    /// Interface ICustomTripDataAccess
    /// </summary>
    public interface ICustomTripDataAccess
    {
        /// <summary>
        /// Gets the next trip.
        /// </summary>
        /// <returns>Task&lt;Tuple&lt;System.Boolean, Trip&gt;&gt;.</returns>
        Task<Tuple<bool, Trip>> GetNextTrip();
    }
}
