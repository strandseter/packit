using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.GenericRepository
{
    public class GenericRepository<T> : ControllerBase, IGenericRepository<T> where T : class, IDatabase
    {
        private readonly PackitContext _context;

        public GenericRepository(PackitContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(T entity, string message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Set<T>().AddAsync(entity).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction(message, new { id = entity?.GetId() }, entity);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _context.Set<T>().FindAsync(id).ConfigureAwait(false);

            if (entity == null)
                return NotFound();

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await _context.Set<T>().FindAsync(id).ConfigureAwait(false);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        public async Task<IActionResult> Update(int id, T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != entity?.GetId())
                return BadRequest();

            _context.Set<T>().Update(entity).State = EntityState.Modified; //TODO: Entry()? Instead of Update()

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        private bool EntityExists(int id)
        {
            return _context.Set<T>().Any(e => e.GetId() == id);
        }
    }
}
