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
        public Task<Trip> GetNextTrip()
        {
            throw new NotImplementedException();
        }
    }
}
