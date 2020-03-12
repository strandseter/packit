using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Packit.Database.Api.Controllers.Abstractions
{
    public abstract class ApiController : ControllerBase
    {
        protected PackitContext Context { get; set; } //???

        public ApiController(PackitContext context)
        {
            Context = context;
        }

        protected async Task<IActionResult> AddManyToMany<T>(int left, int right, DbSet<T> dbset, string message) where T : class, IManyToManyAble
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ObjRelationExists(left, right, dbset))
                return NoContent();

            var obj = (T)Activator.CreateInstance(typeof(T));
            obj.SetLeftId(left);
            obj.SetRightId(right);

            dbset?.Add(obj);

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction(message, new { left, right }, obj);
        }



        //public async Task<IActionResult> QueryOneToMany<T>(int id, DbSet<T> dbset) where T : class
        //{

        //    return Ok();
        //}

        private bool ObjRelationExists<T>(int id1, int id2, DbSet<T> dbset) where T : class, IManyToManyAble
        {
            return dbset.Any(e => e.GetLeftId() == id1 && e.GetRightId() == id2);
        }
    }
}
