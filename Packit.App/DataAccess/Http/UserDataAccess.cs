using Newtonsoft.Json;
using Packit.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.Http
{
    public class UserDataAccess
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly Uri baseUri = new Uri("http://localhost:52286/api/users");

        public async Task<bool> AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            string json = JsonConvert.SerializeObject(user);
            HttpResponseMessage result = await httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!result.IsSuccessStatusCode)
                return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<User>(json);
            user.UserId = returnedUser.UserId;

            if (!await AuthenticateUser(returnedUser))
                return false;

            return true;
        }

        public async Task<bool> AuthenticateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var uri = new Uri($"{baseUri}/authenticate");

            string json = JsonConvert.SerializeObject(user);
            HttpResponseMessage result = await httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!result.IsSuccessStatusCode)
                return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<User>(json);

            CurrentUserStorage.User = returnedUser;

            return true;
        }
    }
}
