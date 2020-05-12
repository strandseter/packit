using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.GenericRepository;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Classes
{
    public class TripRepository : GenericManyToManyRepository<Trip, Backpack, BackpackTrip>, ITripRepository
    {
        public TripRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement non generic methods here.

        public async Task<IActionResult> GetAllTripsWithBackpacksItemsAsync(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //TODO: Better errorhandling? And make generic?
            //Eager loading
            var res = await Context.Trips
                .Include(t => t.Backpacks)
                    .ThenInclude(b => b.Backpack)
                        .ThenInclude(b => b.Items)
                            .ThenInclude(b => b.Item)
                .ToListAsync();

            if (res == null)
                return NotFound();

            return Ok(res);
        }

        public async Task<IActionResult> GetTripByIdWithBackpacksItemsAsync(int id, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await Context.Trips
                .Where(t => t.TripId == id)
                    .Include(t => t.Backpacks)
                        .ThenInclude(b => b.Backpack)
                            .ThenInclude(b => b.Items)
                                .ThenInclude(b => b.Item)
                .FirstOrDefaultAsync();

            if (res == null)
                return NotFound();

            return Ok(res);
        }
    }
}
