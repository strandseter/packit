using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;

namespace Packit.App.Services
{
    public static class InternetConnectionService
    {
        //This has no real purpose at this point. I did not get the time to include a local database in my project (Offline functionality).
        public static bool IsConnected()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }
    }
}
