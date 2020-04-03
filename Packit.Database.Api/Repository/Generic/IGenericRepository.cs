using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(int? userId);
        Task<IActionResult> GetById(int id, int? userId);
        Task<IActionResult> Create(T entity, string message);
        Task<IActionResult> Update(int id, T entity, int? userId);
        Task<IActionResult> Delete(int id, int? userId);
    }
}
