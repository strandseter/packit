using Microsoft.AspNetCore.Mvc;
using Packit.Database.Api.GenericRepository;
using Packit.Database.Api.Repository.Generic;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    public interface IBackpackRepository : IGenericRepository<Backpack>, IGenericManyToManyRepository<Backpack>
    {
        //Declare type specific methods here.

        IQueryable<Backpack> GetSharedBackpacks();
        Task<IActionResult> GetAllBackpacksWithItems(int userId);
    }
}
