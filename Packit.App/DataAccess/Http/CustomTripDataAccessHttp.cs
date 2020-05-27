// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="CustomTripDataAccessHttp.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>
    /// Class CustomTripDataAccessHttp.
    /// Implements the <see cref="Packit.App.DataAccess.ICustomTripDataAccess" />
    /// </summary>
    /// <seealso cref="Packit.App.DataAccess.ICustomTripDataAccess" />
    public class CustomTripDataAccessHttp : ICustomTripDataAccess
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient = new HttpClient();
        /// <summary>
        /// The request handler
        /// </summary>
        private readonly RequestHandler requestHandler = new RequestHandler();
        /// <summary>
        /// The base URI
        /// </summary>
        private static readonly Uri baseUri = new Uri($"http://localhost:52286/api/trips");

        /// <summary>
        /// Gets the next trip.
        /// </summary>
        /// <returns>Tuple&lt;System.Boolean, Trip&gt;.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<Tuple<bool, Trip>>GetNextTrip()
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/next");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            string json = await requestHandler.HandleGetRequestAsync(httpClient.GetAsync, uri);
            var nextTrip = JsonConvert.DeserializeObject<Trip>(json);

            if (nextTrip == null)
                return new Tuple<bool, Trip>(false, nextTrip);

            return new Tuple<bool, Trip>(true, nextTrip);
        }
    }
}
