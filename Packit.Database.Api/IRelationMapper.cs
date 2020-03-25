using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api
{
    public interface IRelationMapper
    {
        object CreateManyToMany<T>(int left, int right) where T : IManyToManyAble;
        bool ObjRelationExists<T>(int left, int right, DbSet<T> dbset) where T : class, IManyToManyAble;
    }

    //protected async Task<IActionResult> AddManyToMany<T>(int left, int right, DbSet<T> dbset, string message) where T : class, IManyToManyAble
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(ModelState);

    //    if (ObjRelationExists(left, right, dbset))
    //        return NoContent();

    //    var obj = (T)Activator.CreateInstance(typeof(T));
    //    obj.SetLeftId(left);
    //    obj.SetRightId(right);

    //    dbset?.Add(obj);

    //    await Context.SaveChangesAsync().ConfigureAwait(false);

    //    return CreatedAtAction(message, new { left, right }, obj);
    //}
}
