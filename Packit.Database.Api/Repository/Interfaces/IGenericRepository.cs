using Microsoft.AspNetCore.Mvc;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(int userId);
        Task<IActionResult> GetByIdAsync(int id, int userId);
        Task<IActionResult> CreateAsync(T entity, string message, int userId);
        Task<IActionResult> UpdateAsync(int id, T entity, int userId);
        Task<IActionResult> DeleteAsync(int id, int userId);
    }
}
