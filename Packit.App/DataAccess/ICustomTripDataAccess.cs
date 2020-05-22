using Packit.Model;
using System;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface ICustomTripDataAccess
    {
        Task<Tuple<bool, Trip>> GetNextTrip();
    }
}
