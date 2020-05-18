using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;

namespace Packit.App.Services
{
    public static class InternetConnectionService
    {
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
                return false;
            }
        }
    }
}
