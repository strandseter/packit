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
        private readonly ITripRepository _repository;

        public TripsController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, ITripRepository repository)
            :base(context, authenticationService, httpContextAccessor)
        {
            _repository = repository;
        }

        // GET: api/Trips
        [HttpGet]
        public IEnumerable<Trip> GetTrips() => _repository.GetAll(CurrentUserId());

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip([FromRoute] int id) => await _repository.GetById(id, CurrentUserId()).ConfigureAwait(false);

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip([FromRoute] int id, [FromBody] Trip trip) => await _repository.Update(id, trip, CurrentUserId()).ConfigureAwait(false);

        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] Trip trip) => await _repository.Create(trip, "GetTrip").ConfigureAwait(false);

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip([FromRoute] int id) => await _repository.Delete(id, CurrentUserId()).ConfigureAwait(false);

        // PUT: api/trips/3/backpacks/4
        [HttpPut]
        [Route("{tripId}/backpacks/{backpackId}/create")]
        public async Task<IActionResult> PutBackpackToTrip([FromRoute] int tripId, [FromRoute] int backpackId) => await _repository.CreateManyToMany("GetBackpackTrip", backpackId, tripId).ConfigureAwait(false);

        // DELETE: api/trips/3/backpacks/7/delete
        [HttpDelete]
        [Route("{tripId}/backpacks/{backpackId}/delete")]
        public async Task<IActionResult> DeleteBackpackFromTrip([FromRoute] int tripId, [FromRoute] int backpackId) => await _repository.DeleteManyToMany(backpackId, tripId).ConfigureAwait(false);

        // GET: api/trips/4/backpacks
        [HttpGet]
        [Route("{tripId}/backpacks")]
        public async Task<IActionResult> GetBackpacksInTrip([FromRoute] int tripId) => await _repository.GetManyToMany(tripId).ConfigureAwait(false);
    }
}