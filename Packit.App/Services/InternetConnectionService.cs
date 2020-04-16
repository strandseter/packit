using System.Collections.Generic;
using Windows.Networking.Connectivity;

namespace Packit.App.Services
{
    public static class InternetConnectionService
    {
        public static bool IsConnected()
        {
            IReadOnlyList<ConnectionProfile> connections = NetworkInformation.GetConnectionProfiles();

            foreach (var con in connections)
            {
                if (con == null) continue;
                if (con.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess) return true;
            }
            return true;
        }
    }
}
