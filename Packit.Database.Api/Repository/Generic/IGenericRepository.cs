using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Create(T entity, string message);
        Task<IActionResult> Update(int id, T entity);
        Task<IActionResult> Delete(int id);
    }
}
