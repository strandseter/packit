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
        protected PackitContext Context { get;}

        public GenericRepository(PackitContext context)
        {
            Context = context;
        }

        public async Task<IActionResult> Create(T entity, string message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Context.Set<T>().AddAsync(entity).ConfigureAwait(false);

            await SaveChanges(); //TODO: Suppress??

            return CreatedAtAction(message, new { id = entity?.GetId() }, entity);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await Context.Set<T>().FindAsync(id).ConfigureAwait(false);

            if (entity == null)
                return NotFound();

            Context.Set<T>().Remove(entity);

            await SaveChanges(); //TODO: Suppress??

            return Ok(entity);
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await Context.Set<T>().FindAsync(id).ConfigureAwait(false);

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

            Context.Set<T>().Update(entity).State = EntityState.Modified; //TODO: Entry()? Instead of Update()

            try
            {
                await SaveChanges(); //TODO: Suppress??
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityExists<T>(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        protected async Task<bool> EntityExists<Tentity>(int id) where Tentity : class, IDatabase => await Context.Set<Tentity>().AnyAsync(e => e.GetId() == id).ConfigureAwait(false);

        protected async Task SaveChanges() => await Context.SaveChangesAsync().ConfigureAwait(false);
    }
}
