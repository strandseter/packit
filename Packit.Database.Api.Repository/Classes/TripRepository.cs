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

        //Implement methods that are not possible to make generic here.

        public async Task<IActionResult> GetAllTripBackpacksItemsAsync(int userId)
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
    }
}
