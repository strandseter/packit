// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-20-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="UserDataAccess.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Packit.App.DataAccess.RequestHandlers;
using Packit.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.Http
{
    /// <summary>
    /// Class UserDataAccess.
    /// </summary>
    public class UserDataAccess
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient = new HttpClient();
        /// <summary>
        /// The time out milliseconds
        /// </summary>
        private const int timeOutMilliseconds = 10000;
        /// <summary>
        /// The base URI
        /// </summary>
        private readonly Uri baseUri = new Uri("http://localhost:52286/api/users");

        /// <summary>
        /// add user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<bool> AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            string json = JsonConvert.SerializeObject(user);

            HttpResponseMessage result;

            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (var cts = new CancellationTokenSource())
                {
                    cts.CancelAfter(timeOutMilliseconds);
                    result = await httpClient.PostAsync(baseUri, content, cts.Token);
                }
            }

            if (!result.IsSuccessStatusCode)
                return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<User>(json);
            user.UserId = returnedUser.UserId;

            if (!await AuthenticateUser(returnedUser))
                return false;

            return true;
        }

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if user is authenticated, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<bool> AuthenticateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.FirstName = ".";
            user.LastName = ".";

            var uri = new Uri($"{baseUri}/authenticate");

            string json = JsonConvert.SerializeObject(user);

            HttpResponseMessage result;

            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (var cts = new CancellationTokenSource())
                {
                    cts.CancelAfter(timeOutMilliseconds);
                    result = await httpClient.PostAsync(uri, content, cts.Token);
                }
            }

            if (!result.IsSuccessStatusCode)
                return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<User>(json);

            CurrentUserStorage.User = returnedUser;

            return true;
        }
    }
}
