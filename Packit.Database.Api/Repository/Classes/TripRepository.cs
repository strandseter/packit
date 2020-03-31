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
    public class TripRepository : GenericManyToManyRepository<Trip, Backpack, BackpackTrip>, ITripRepository
    {
        public TripRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement methods that are possible to make generic here.
    }
}
