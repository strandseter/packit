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
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : PackitApiController
    {
        public IRelationMapper RelationMapper { get; set; }

        public TripsController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IRelationMapper relationMapper)
            :base(context, authenticationService, httpContextAccessor)
        {
            RelationMapper = relationMapper;
        }

        // GET: api/Trips
        [HttpGet]
        public IEnumerable<Trip> GetTrips()
        {
            return Context.Trips;
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trip = await Context.Trips.FindAsync(id).ConfigureAwait(false);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip([FromRoute] int id, [FromBody] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trip?.TripId)
            {
                return BadRequest();
            }

            Context.Entry(trip).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Context.Trips.Add(trip);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetTrip", new { id = trip?.TripId }, trip);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trip = await Context.Trips.FindAsync(id).ConfigureAwait(false);
            if (trip == null)
            {
                return NotFound();
            }

            Context.Trips.Remove(trip);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(trip);
        }

        private bool TripExists(int id)
        {
            return Context.Trips.Any(e => e.TripId == id);
        }
    }
}