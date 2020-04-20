using Packit.DataAccess;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System.Linq;

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
    }
}
