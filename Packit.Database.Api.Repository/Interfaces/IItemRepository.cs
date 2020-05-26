using Microsoft.AspNetCore.Mvc;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        //Declare non generic methods here.

        Task<IActionResult> GetAllItemsWithChecksAsync(int userId);
    }
}
