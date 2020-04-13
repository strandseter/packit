using Packit.App.DataAccess;
using Packit.Model.Models;
using System.Collections.Generic;
using Windows.Networking.Connectivity;

namespace Packit.App.Factory
{
    public class BasicDataAccessFactory<T> where T : IDatabase
    {
        public IBasicDataAccessApi<T> Create() => IsConnected() ? new BasicDataAccessApi<T>() : (IBasicDataAccessApi<T>)new BasicDataAccessLocal<T>();

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
