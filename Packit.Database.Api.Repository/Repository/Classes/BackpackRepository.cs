using Packit.DataAccess;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;

namespace Packit.Database.Api.Repository.Classes
{
    public class BackpackRepository : GenericManyToManyRepository<Backpack, Item, ItemBackpack>, IBackpackRepository
    {
        public BackpackRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement methods that are not possible to make generic here.
    }
}
