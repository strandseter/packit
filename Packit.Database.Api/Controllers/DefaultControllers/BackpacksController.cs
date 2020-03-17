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
    public class BackpacksController : PackitApiController
    {
        public BackpacksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
            :base(context, authenticationService, httpContextAccessor)
        {
        }

        // GET: api/Backpacks
        [HttpGet]
        public IEnumerable<Backpack> GetBackpacks()
        {
            return Context.Backpacks;
        }

        // GET: api/Backpacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackpack([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backpack = await Context.Backpacks.FindAsync(id).ConfigureAwait(false);

            if (backpack == null)
            {
                return NotFound();
            }

            return Ok(backpack);
        }

        // PUT: api/Backpacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackpack([FromRoute] int id, [FromBody] Backpack backpack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != backpack?.BackpackId)
            {
                return BadRequest();
            }

            Context.Entry(backpack).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackpackExists(id))
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

        // POST: api/Backpacks
        [HttpPost]
        public async Task<IActionResult> PostBackpack([FromBody] Backpack backpack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Context.Backpacks.Add(backpack);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetBackpack", new { id = backpack?.BackpackId }, backpack);
        }

        // DELETE: api/Backpacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backpack = await Context.Backpacks.FindAsync(id).ConfigureAwait(false);
            if (backpack == null)
            {
                return NotFound();
            }

            Context.Backpacks.Remove(backpack);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(backpack);
        }

        // GET: api/Backpacks/5/items
        [HttpGet("{id}/items")]
        public  IEnumerable<Item> QueryItemsInBackpack<T>([FromRoute] int id)
        {
            var items = Context.Items.Include(i => i).Where(i => i.ItemId == id);

            return items;
        }

        public async Task<IActionResult> AddBackpackToTrip([FromRoute] int backpackId, [FromRoute] int tripId)
        {
            return await AddManyToMany(backpackId, tripId, Context.BackpackTrip, "GetBackpackTrip").ConfigureAwait(false);
        }

        private bool BackpackExists(int id)
        {
            return Context.Backpacks.Any(e => e.BackpackId == id);
        }
    }
}