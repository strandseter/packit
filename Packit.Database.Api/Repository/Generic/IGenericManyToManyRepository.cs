using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Generic
{
    public interface IGenericManyToManyRepository<T>
    {
        Task<IActionResult> CreateManyToMany(string message, int leftId, int rightId);
        Task<IActionResult> GetManyToMany(int id);
        Task<IActionResult> DeleteManyToMany(int leftId, int rightId);
    }
}
