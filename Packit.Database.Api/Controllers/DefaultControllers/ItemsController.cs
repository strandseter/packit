using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Abstractions.ApiController
    {
        public ItemsController(PackitContext context)
            :base(context)
        {
        }

        // GET: api/Items
        [HttpGet]
        public IEnumerable<Item> GetItem()
        {
            return _context.Items;
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.FindAsync(id).ConfigureAwait(false);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem([FromRoute] int id, [FromBody] Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item?.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
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
            {
                return BadRequest(ModelState);
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetItem", new { id = item?.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.FindAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(item);
        }

        // PUT: api/items/1/backpacks/2
        [HttpPut("{itemId}/backpacks/{backpackId}")]
        public Task<IActionResult> AddItemToBackpack([FromRoute] int itemId, [FromRoute] int backpackId)
        {
            return AddManyToMany(itemId, backpackId, _context.ItemBackpack, "GetItemBackpack");
        }

        private bool ItemExists(int id) //Abstract this
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}