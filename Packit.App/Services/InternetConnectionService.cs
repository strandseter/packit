using System.Net.NetworkInformation;

namespace Packit.App.Services
{
    public static class InternetConnectionService
    {
        //This has no real purpose at this point. I did not get the time to include a local database in my project (Offline functionality).
        //This is used to check internet access in DataAccess factories.
        public static bool IsConnectedMock() => true;

        //Not optimal. But it is working with the schools VPN, which has caused a lot of trouble.
        public static bool IsConnected() => NetworkInterface.GetIsNetworkAvailable();
    }
}
