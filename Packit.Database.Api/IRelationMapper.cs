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
        object CreateManyToMany<T>(int left, int right) where T : IManyToMany;
        bool ObjRelationExists<T>(int left, int right, DbSet<T> dbset) where T : class, IManyToMany;
    }
}
