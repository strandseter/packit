using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Generic
{
    public interface IGenericManyToManyRepository<T>
    {
        Task<IActionResult> CreateManyToManyAsync(string message, int leftId, int rightId);
        Task<IActionResult> GetManyToManyAsync(int id);
        Task<IActionResult> DeleteManyToManyAsync(int leftId, int rightId);
    }
}
