using Microsoft.AspNetCore.Mvc;
using Packit.Database.Api.GenericRepository;
using Packit.Database.Api.Repository.Generic;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    public interface ITripRepository : IGenericRepository<Trip>, IGenericManyToManyRepository<Trip>
    {
        //Declare methods that are not possible to make generic here.

        Task<IActionResult> GetAllTripsWithBackpacksItemsAsync(int userId);
        Task<IActionResult> GetTripByIdWithBackpacksItemsAsync(int id, int user);
    }
}
