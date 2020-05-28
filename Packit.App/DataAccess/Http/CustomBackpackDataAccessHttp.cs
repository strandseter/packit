using Newtonsoft.Json;
using Packit.App.DataAccess.RequestHandlers;
using Packit.App.Services;
using Packit.Exceptions;
using Packit.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.Http
{
    public class CustomBackpackDataAccessHttp : ICustomBackpackDataAccess
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly HttpRequestHandler requestHandler = new HttpRequestHandler();
        private static readonly Uri baseUri = new Uri($"http://localhost:52286/api/backpacks");

        public CustomBackpackDataAccessHttp()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);
        }

        public async Task<Backpack[]> GetAllSharedBackpacksAsync()
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/shared");

            string json = await requestHandler.HandleGetRequestAsync(httpClient.GetAsync, uri);
            var sharedBackpacks = JsonConvert.DeserializeObject<Backpack[]>(json);

            return sharedBackpacks;
        }

        public async Task<Backpack[]> GetAllSharedBackpacksByUserAsync()
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/shared/user");

            string json = await requestHandler.HandleGetRequestAsync(httpClient.GetAsync, uri);
            var sharedBackpacks = JsonConvert.DeserializeObject<Backpack[]>(json);

            return sharedBackpacks;
        }
    }
}
