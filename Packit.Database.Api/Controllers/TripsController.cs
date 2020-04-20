using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : PackitApiController
    {
        private readonly ITripRepository repository;

        public TripsController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, ITripRepository repository)
            :base(context, authenticationService, httpContextAccessor)
        {
            this.repository = repository;
        }

        // GET: api/Trips
        [HttpGet]
        public IEnumerable<Trip> GetTrips() => repository.GetAll(CurrentUserId());

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip([FromRoute] int id, [FromBody] Trip trip) => await repository.UpdateAsync(id, trip, CurrentUserId());

        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] Trip trip) => await repository.CreateAsync(trip, "GetTrip", CurrentUserId());

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());

        // PUT: api/trips/3/backpacks/4
        [HttpPut]
        [Route("{tripId}/backpacks/{backpackId}/create")]
        public async Task<IActionResult> PutBackpackToTrip([FromRoute] int tripId, [FromRoute] int backpackId) => await repository.CreateManyToManyAsync("GetBackpackTrip", backpackId, tripId);

        // DELETE: api/trips/3/backpacks/7/delete
        [HttpDelete]
        [Route("{tripId}/backpacks/{backpackId}/delete")]
        public async Task<IActionResult> DeleteBackpackFromTrip([FromRoute] int tripId, [FromRoute] int backpackId) => await repository.DeleteManyToManyAsync(backpackId, tripId);

        // GET: api/trips/4/backpacks
        [HttpGet]
        [Route("{tripId}/backpacks")]
        public async Task<IActionResult> GetBackpacksInTrip([FromRoute] int tripId) => await repository.GetManyToManyAsync(tripId);
    }
}