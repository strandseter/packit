using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.Local
{
    public class CustomTripDataAccessLocal : ICustomTripDataAccess
    {
        Task<Tuple<bool, Trip>> ICustomTripDataAccess.GetNextTrip()
        {
            throw new NotImplementedException();
        }
    }
}
