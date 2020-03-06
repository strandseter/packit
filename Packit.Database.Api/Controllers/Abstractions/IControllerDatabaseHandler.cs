using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Controllers.Abstractions
{
    interface IControllerDatabaseHandler
    {
        Task<IActionResult> AddManyToMany<T>(int id1, int id2, DbSet<T> dbset, string message) where T : class, IManyToManyAble;
        //bool ObjRelationExists<T>(int id1, int id2, DbSet<T> dbset) where T : class, IManyToManyAble;
    }
}
