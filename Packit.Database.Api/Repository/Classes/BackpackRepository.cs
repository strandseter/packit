using Microsoft.AspNetCore.Mvc;
using Packit.DataAccess;
using Packit.Database.Api.GenericRepository;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System;
using System.Collections.Generic;
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

        public void Test()
        {

        }
    }
}
