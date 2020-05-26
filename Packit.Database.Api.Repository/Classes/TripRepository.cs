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
    public class TripRepository : ManyToManyRepository<Trip, Backpack, BackpackTrip>, ITripRepository
    {
        public TripRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement non generic methods here.

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
