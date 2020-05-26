using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Generic
{
    public class GenericRepository<T> : ControllerBase, IRepository<T> where T : class, IDatabase
    {
        protected PackitContext Context { get; }

        public GenericRepository(PackitContext context)
        {
            Context = context;
        }

        public async Task<IActionResult> CreateAsync(T entity, string message, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entity == null)
                return BadRequest();

            entity.SetUserId(userId);

            await Context.Set<T>().AddAsync(entity);

            await SaveChangesAsync();

            return CreatedAtAction(message, new { id = entity?.GetId() }, entity);
        }

        public async Task<IActionResult> DeleteAsync(int id, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await Context.Set<T>().Where(e => e.GetId() == id && e.GetUserId() == userId).FirstOrDefaultAsync();

            if (entity == null)
                return NotFound();

            Context.Set<T>().Remove(entity);

            await SaveChangesAsync();

            return Ok(entity);
        }

        public IQueryable<T> GetAll(int userId)
        {
            return Context.Set<T>().Where(e => e.GetUserId() == userId);
        }

        public async Task<IActionResult> GetByIdAsync(int id, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await Context.Set<T>().Where(e => e.GetId() == id && e.GetUserId() == userId).FirstOrDefaultAsync();

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        public async Task<IActionResult> UpdateAsync(int id, T entity, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entity == null)
                return BadRequest();

            if (id != entity.GetId())
                return BadRequest();

            Context.Set<T>().Update(entity).State = EntityState.Modified;

            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityExistsAsync(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        protected async Task<bool> EntityExistsAsync(int id) => await Context.Set<T>().AnyAsync(e => e.GetId() == id);
        protected async Task SaveChangesAsync() => await Context.SaveChangesAsync();
    }
}
