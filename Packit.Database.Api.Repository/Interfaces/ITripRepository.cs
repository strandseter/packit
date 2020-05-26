// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ITripRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Packit.Model;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    /// <summary>
    /// Interface ITripRepository
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Trip}" />
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.IManyToManyRepository{Packit.Model.Trip}" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Trip}" />
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.IManyToManyRepository{Packit.Model.Trip}" />
    public interface ITripRepository : IRepository<Trip>, IManyToManyRepository<Trip>
    {
        //Declare methods that are not possible to make generic here.

        /// <summary>
        /// Gets all trips with backpacks items checks asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> GetAllTripsWithBackpacksItemsChecksAsync(int userId);
        /// <summary>
        /// Gets the trip by identifier with backpacks items checks asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> GetTripByIdWithBackpacksItemsChecksAsync(int id, int user);
        /// <summary>
        /// Gets the next trip with backpacks items checks asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> GetNextTripWithBackpacksItemsChecksAsync(int userId);
    }
}
