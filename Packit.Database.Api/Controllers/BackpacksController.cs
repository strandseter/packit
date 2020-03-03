using System;
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
    public class BackpacksController : ControllerBase
    {
        private readonly PackitContext _context;

        public BackpacksController(PackitContext context)
        {
            _context = context;
        }

        // GET: api/Backpacks
        [HttpGet]
        public IEnumerable<Backpack> GetBackpacks()
        {
            return _context.Backpacks;
        }

        // GET: api/Backpacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackpack([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backpack = await _context.Backpacks.FindAsync(id);

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

            if (id != backpack.BackpackId)
            {
                return BadRequest();
            }

            _context.Entry(backpack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            _context.Backpacks.Add(backpack);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBackpack", new { id = backpack.BackpackId }, backpack);
        }

        // DELETE: api/Backpacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backpack = await _context.Backpacks.FindAsync(id);
            if (backpack == null)
            {
                return NotFound();
            }

            _context.Backpacks.Remove(backpack);
            await _context.SaveChangesAsync();

            return Ok(backpack);
        }

        private bool BackpackExists(int id)
        {
            return _context.Backpacks.Any(e => e.BackpackId == id);
        }
    }
}