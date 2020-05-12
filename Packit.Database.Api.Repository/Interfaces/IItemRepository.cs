using Microsoft.AspNetCore.Mvc;
using Packit.Database.Api.GenericRepository;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        //Declare non generic methods here.

        Task<IActionResult> GetAllItemsWithChecksAsync(int userId);
    }
}
