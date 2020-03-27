using Packit.DataAccess;
using Packit.Database.Api.GenericRepository;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Classes
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        public TripRepository(PackitContext context)
            :base(context)
        {
        }
    }
}
