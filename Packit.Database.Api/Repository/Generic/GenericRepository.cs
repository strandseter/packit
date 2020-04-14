using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Filters;

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

            await Context.Set<T>().AddAsync(entity);

            await SaveChanges();

            return CreatedAtAction(message, new { id = entity?.GetId() }, entity);
        }

        public async Task<IActionResult> Delete(int id, int? userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await Context.Set<T>().Where(e => e.GetId() == id && e.User.UserId == userId).FirstOrDefaultAsync();

            if (entity == null)
                return NotFound();

            Context.Set<T>().Remove(entity);

            await SaveChanges();

            return Ok(entity);
        }

        public IQueryable<T> GetAll(int? userId)
        {
            return Context.Set<T>().Where(e => e.User.UserId == userId);
        }

        public async Task<IActionResult> GetById(int id, int? userId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await Context.Set<T>().Where(e => e.GetId() == id && e.User.UserId == userId).FirstOrDefaultAsync();

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        public async Task<IActionResult> Update(int id, T entity, int? userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != entity?.GetId())
                return BadRequest();

            Context.Set<T>().Update(entity).State = EntityState.Modified;

            try
            {
                await SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        protected async Task<bool> EntityExists(int id) => await Context.Set<T>().AnyAsync(e => e.GetId() == id).ConfigureAwait(false);
        protected async Task SaveChanges() => await Context.SaveChangesAsync().ConfigureAwait(false);
    }
}
