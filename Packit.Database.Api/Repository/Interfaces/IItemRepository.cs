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
        //Declare methods that are not possible to make generic here.

    }
}
