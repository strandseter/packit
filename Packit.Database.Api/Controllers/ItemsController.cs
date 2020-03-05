using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly PackitContext _context;

        public ItemsController(PackitContext context)
        {
            _context = context;
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

            var item = await _context.Items.FindAsync(id);

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

            if (id != item.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        // PUT: api/items/1/backpacks/2
        [HttpPut("{itemId}/backpacks/{backpackId}")]
        public Task<IActionResult> AddItemToBackpack([FromRoute] int itemId, [FromRoute] int backpackId)
        {
            return AddManyToMany<ItemBackpack>(itemId, backpackId, _context.ItemBackpack, "GetItemBackpack");
        }

        private async Task<IActionResult> AddManyToMany<T>(int id1, int id2, DbSet<T> list, string message) where T : class, IManyToManyJoinable
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = (T)Activator.CreateInstance(typeof(T));

            obj.Id1(id1);
            obj.Id2(id2);

            list.Add(obj);
            await _context.SaveChangesAsync();

            return CreatedAtAction(message, new { id1, id2 }, obj);
        }


        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

        private bool ItemBackpackExists(int itemId, int backpackId)
        {
            return _context.ItemBackpack.Any(ib => ib.ItemId == itemId && ib.BackpackId == backpackId);
        }

        private void Gsdjkfh()
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //if (ItemBackpackExists(itemId, backpackId))
            //    return NoContent();

            //var itemBackpack = new ItemBackpack() { ItemId = itemId, BackpackId = backpackId };
            //_context.ItemBackpack.Add(itemBackpack);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetItemBackpack", new { itemId, backpackId }, itemBackpack);
        }
    }
}