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
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement non generic methods here.

        public async Task<IActionResult> GetAllItemsWithChecksAsync(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await Context.Items
                .Where(i => i.UserId == userId)
                .Include(i => i.Checks)
                .ToListAsync();

            if (res == null)
                return NotFound();

            return Ok(res);
        }
    }
}
