using Packit.DataAccess;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Database.Api.Repository.Classes
{
    public class CheckRepository : GenericRepository<Check>, ICheckRepository
    {
        public CheckRepository(PackitContext context)
            : base(context)
        {
        }

        //Implement non generic menthds here.
    }
}
