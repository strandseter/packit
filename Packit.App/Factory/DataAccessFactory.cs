using Packit.App.DataAccess;
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Packit.App.Factory
{
    public class DataAccessFactory<T> where T : IDatabase
    {
        public IDataAccess<T> Create() => IsConnected() ? new GenericApiDataAccess<T>() : (IDataAccess<T>)new GenericLocalDataAccess<T>();

        private bool IsConnected()
        {
            IReadOnlyList<ConnectionProfile> connections = NetworkInformation.GetConnectionProfiles();

            foreach(var con in connections)
            {
                if (con == null) continue;

                if (con.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess) return true;
            }
            return false;
        }
    }
}
