using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Classes
{
    public class BackpackRepository : GenericManyToManyRepository<Backpack, Item, ItemBackpack>, IBackpackRepository
    {
        public BackpackRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement type specific methods here.

        public IQueryable<Backpack> GetSharedBackpacks()
        {
            return Context.Backpacks.Where(b => b.IsShared);
        }

        public async Task<IActionResult> GetAllBackpacksWithItems(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var backpacks = await Context.Backpacks
                .Include(b => b.Items)
                    .ThenInclude(i => i.Item)
                .ToListAsync();

            if (backpacks == null)
                return NotFound();

            return Ok(backpacks);
        }
    }
}
