using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Model;
using Packit.Model.Interfaces;
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

        //Not working
        //protected IEnumerable<T1> GetOneToMany<T1, T2>(string id, DbSet<T1> dbset1, DbSet<T2> dbset2) where T1 : class, IManyTable  where T2 : class, IOneTable
        //{
        //    if (!ModelState.IsValid)
        //        BadRequest(ModelState);

        //    //var one = dbset2.Where(o => o.GetIdentityId() == id).FirstOrDefault();

        //    //var test = dbset2;

        //    var many = dbset1.Where(i => i.GetForeignObject().GetIdentityId() == id);

        //    return many;
        //}

        private bool ObjRelationExists<T>(int id1, int id2, DbSet<T> dbset) where T : class, IManyToManyAble
        {
            return dbset.Any(e => e.GetLeftId() == id1 && e.GetRightId() == id2);
        }
    }
}
