using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Packit.Database.Api
{
    public class RelationMapper : IRelationMapper
    {
        public object CreateManyToMany<T> (int left, int right) where T : IManyToManyAble
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            obj.SetLeftId(left);
            obj.SetRightId(right);

            return obj;
        }

        public bool ObjRelationExists<T>(int left, int right, DbSet<T> dbset) where T : class, IManyToManyAble
        {
            return dbset.Any(e => e.GetLeftId() == left && e.GetRightId() == right);
        }
    }
}
