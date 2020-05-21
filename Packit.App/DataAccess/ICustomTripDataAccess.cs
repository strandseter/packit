using Packit.Model;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface ICustomTripDataAccess
    {
        Task<Trip> GetNextTrip();
    }
}
