// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="TripsController.cs" company="Packit.Database.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    /// <summary>
    /// Class TripsController.
    /// Implements the <see cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : PackitApiController
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ITripRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TripsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="repository">The repository.</param>
        public TripsController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, ITripRepository repository)
            : base(context, authenticationService, httpContextAccessor) => this.repository = repository;

        //GET: api/trips/all
        /// <summary>
        /// get all trips with backpacks items as an asynchronous operation.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllTripsWithBackpacksItemsAsync() => await repository.GetAllTripsWithBackpacksItemsChecksAsync(CurrentUserId());

        // GET: api/trips
        /// <summary>
        /// Gets the trips.
        /// </summary>
        /// <returns>IEnumerable&lt;Trip&gt;.</returns>
        [HttpGet]
        public IEnumerable<Trip> GetTrips() => repository.GetAll(CurrentUserId());

        // GET: api/trips/5
        /// <summary>
        /// Gets the trip.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        // GET: api/trips/4/all
        /// <summary>
        /// Gets the trip with backpacks items.
        /// </summary>
        /// <param name="tripId">The trip identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("{tripId}/all")]
        public async Task<IActionResult> GetTripWithBackpacksItems([FromRoute] int tripId) => await repository.GetTripByIdWithBackpacksItemsChecksAsync(tripId, CurrentUserId());

        // GET: api/trips/4/backpacks
        /// <summary>
        /// Gets the backpacks in trip.
        /// </summary>
        /// <param name="tripId">The trip identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("{tripId}/backpacks")]
        public async Task<IActionResult> GetBackpacksInTrip([FromRoute] int tripId) => await repository.GetManyToManyAsync(tripId);

        // GET: api/trips/next
        /// <summary>
        /// Gets the next trip.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("next")]
        public async Task<IActionResult> GetNextTrip() => await repository.GetNextTripWithBackpacksItemsChecksAsync(CurrentUserId());

        // PUT: api/trips/5
        /// <summary>
        /// Puts the trip.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="trip">The trip.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip([FromRoute] int id, [FromBody] Trip trip) => await repository.UpdateAsync(id, trip, CurrentUserId());

        // PUT: api/trips/3/backpacks/4
        /// <summary>
        /// Puts the backpack to trip.
        /// </summary>
        /// <param name="tripId">The trip identifier.</param>
        /// <param name="backpackId">The backpack identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("{tripId}/backpacks/{backpackId}/create")]
        public async Task<IActionResult> PutBackpackToTrip([FromRoute] int tripId, [FromRoute] int backpackId) => await repository.CreateManyToManyAsync("GetBackpackTrip", backpackId, tripId);

        // POST: api/trips
        /// <summary>
        /// Posts the trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] Trip trip) => await repository.CreateAsync(trip, "GetTrip", CurrentUserId());

        // DELETE: api/trips/5
        /// <summary>
        /// Deletes the trip.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());

        // DELETE: api/trips/3/backpacks/7/delete
        /// <summary>
        /// Deletes the backpack from trip.
        /// </summary>
        /// <param name="tripId">The trip identifier.</param>
        /// <param name="backpackId">The backpack identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete]
        [Route("{tripId}/backpacks/{backpackId}/delete")]
        public async Task<IActionResult> DeleteBackpackFromTrip([FromRoute] int tripId, [FromRoute] int backpackId) => await repository.DeleteManyToManyAsync(backpackId, tripId);
    }
}