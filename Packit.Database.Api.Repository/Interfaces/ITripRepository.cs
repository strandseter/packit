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

        Task<IActionResult> GetAllTripsWithBackpacksItemsChecksAsync(int userId);
        Task<IActionResult> GetTripByIdWithBackpacksItemsChecksAsync(int id, int user);
        Task<IActionResult> GetNextTripWithBackpacksItemsChecksAsync(int userId);
    }
}
