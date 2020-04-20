using Packit.DataAccess;
using Packit.Database.Api.GenericRepository;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement methods that are not possible to make generic here.
    }
}
