using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Model;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Packit.Database.Api.Authentication;
using Microsoft.AspNetCore.Http;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : PackitApiController
    {
        public ItemsController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
            :base(context, authenticationService, httpContextAccessor)
        {
        }

        // GET: api/Items
        [HttpGet]
        public IEnumerable<Item> GetItem()
        {
            return Context.Items.Where(i => UserIsAuthorized(i.User));
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await Context.Items.FindAsync(id).ConfigureAwait(false);

            if (item == null)
                return NotFound();

            if (!UserIsAuthorized(item.User))
                return Unauthorized();

            return Ok(item);
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem([FromRoute] int id, [FromBody] Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != item?.ItemId)
                return BadRequest();

            if (!UserIsAuthorized(item.User))
                return Unauthorized();

            Context.Entry(item).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Context.Items.Add(item);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetItem", new { id = item?.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await Context.Items.FindAsync(id).ConfigureAwait(false);

            if (item == null)
                return NotFound();

            if (!UserIsAuthorized(item.User))
                return Unauthorized();

            Context.Items.Remove(item);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(item);
        }

        // PUT: api/items/1/backpacks/2
        [HttpPut("{itemId}/backpacks/{backpackId}")]
        public async Task<IActionResult> AddItemToBackpack([FromRoute] int itemId, [FromRoute] int backpackId)
        {
            return await AddManyToMany(itemId, backpackId, Context.ItemBackpack, "GetItemBackpack").ConfigureAwait(false);
        }

        private bool ItemExists(int id)
        {
            return Context.Items.Any(e => e.ItemId == id);
        }
    }
}