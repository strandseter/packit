// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="TripRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Classes
{
    /// <summary>
    /// Class TripRepository.
    /// Implements the <see cref="Packit.Database.Api.Repository.Generic.ManyToManyRepository{Packit.Model.Trip, Packit.Model.Backpack, Packit.Model.BackpackTrip}" />
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.ITripRepository" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Repository.Generic.ManyToManyRepository{Packit.Model.Trip, Packit.Model.Backpack, Packit.Model.BackpackTrip}" />
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.ITripRepository" />
    public class TripRepository : ManyToManyRepository<Trip, Backpack, BackpackTrip>, ITripRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TripRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TripRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement non generic methods here.

        /// <summary>
        /// get all trips with backpacks items checks as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> GetAllTripsWithBackpacksItemsChecksAsync(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //TODO: Better errorhandling? 
            //Eager loading
            var trips = await Context.Trips
                .Where(t => t.UserId == userId)
                .Include(t => t.Backpacks)
                    .ThenInclude(b => b.Backpack)
                        .ThenInclude(b => b.Items)
                            .ThenInclude(b => b.Item)
                                .ThenInclude(i => i.Checks)
                .ToListAsync();

            if (trips == null)
                return NotFound();

            return Ok(trips);
        }

        /// <summary>
        /// get trip by identifier with backpacks items checks as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IActionResult.</returns>
        public async Task<IActionResult> GetTripByIdWithBackpacksItemsChecksAsync(int id, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trip = await Context.Trips
                .Where(t => t.TripId == id)
                    .Include(t => t.Backpacks)
                        .ThenInclude(b => b.Backpack)
                            .ThenInclude(b => b.Items)
                                .ThenInclude(b => b.Item)
                                    .ThenInclude(i => i.Checks)
                .FirstOrDefaultAsync();

            if (trip == null)
                return NotFound();

            return Ok(trip);
        }

        /// <summary>
        /// get next trip with backpacks items checks as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> GetNextTripWithBackpacksItemsChecksAsync(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trip = await Context.Trips
                .Where(t => t.UserId == userId)
                .Where(t => t.DepatureDate > DateTimeOffset.Now)
                .Include(t => t.Backpacks)
                        .ThenInclude(b => b.Backpack)
                            .ThenInclude(b => b.Items)
                                .ThenInclude(b => b.Item)
                                    .ThenInclude(i => i.Checks)
                .OrderBy(t => t.DepatureDate)
                .FirstOrDefaultAsync();

            if (trip == null)
                return NotFound();

            return Ok(trip);
        }
    }
}
