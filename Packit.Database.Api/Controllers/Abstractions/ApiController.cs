using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Controllers.Abstractions
{
    public abstract class ApiController : ControllerBase
    {
        protected readonly PackitContext _context; //???

        public ApiController(PackitContext context)
        {
            _context = context;
        }

        protected async Task<IActionResult> AddManyToMany<T>(int id1, int id2, DbSet<T> dbset, string message) where T : class, IManyToManyAble
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ObjRelationExists(id1, id2, dbset))
                return NoContent();

            var obj = (T)Activator.CreateInstance(typeof(T));

            obj.Id1(id1);
            obj.Id2(id2);

            dbset?.Add(obj);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction(message, new { id1, id2 }, obj);
        }

        private bool ObjRelationExists<T>(int id1, int id2, DbSet<T> dbset) where T : class, IManyToManyAble
        {
            return dbset.Any(e => e.Id1() == id1 && e.Id2() == id2);
        }
    }
}
