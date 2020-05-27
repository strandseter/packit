// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BasicDataAccessHttp.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Packit.App.DataAccess.RequestHandlers;
using Packit.App.Services;
using Packit.Exceptions;
using Packit.Model.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    /// <summary>
    /// Class BasicDataAccessHttp.
    /// Implements the <see cref="Packit.App.DataAccess.IBasicDataAccess{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Packit.App.DataAccess.IBasicDataAccess{T}" />
    public class BasicDataAccessHttp<T> : IBasicDataAccess<T> where T : IDatabase
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient = new HttpClient();
        /// <summary>
        /// The request handler
        /// </summary>
        private readonly HttpRequestHandler requestHandler = new HttpRequestHandler();
        /// <summary>
        /// The base URI
        /// </summary>
        private static readonly Uri baseUri = new Uri($"http://localhost:52286/api/{typeof(T).Name}s");

        public BasicDataAccessHttp()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);
        }

        /// <summary>
        /// add as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        /// <exception cref="ArgumentNullException">entity</exception>
        public async Task<bool> AddAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            string json = JsonConvert.SerializeObject(entity);

            HttpResponseMessage result;

            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                //result = await httpClient.PostAsync(baseUri, content);
                result = await requestHandler.HandlePostPutRequestAsync(httpClient.PostAsync, baseUri, content);
            }

            if (!result.IsSuccessStatusCode) return false;

            json = await result.Content.ReadAsStringAsync();
            var returnedEntity = JsonConvert.DeserializeObject<T>(json);
            entity.SetId(returnedEntity.GetId());

            return true;
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        /// <exception cref="ArgumentNullException">entity</exception>
        public async Task<bool> DeleteAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var uri = new Uri($"{baseUri}/{entity.GetId()}");

            return await requestHandler.HandleDeleteRequestAsync(httpClient.DeleteAsync, uri);
        }

        /// <summary>
        /// update as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        /// <exception cref="ArgumentNullException">entity</exception>
        public async Task<bool> UpdateAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var uri = new Uri($"{baseUri}/{entity.GetId()}");

            string json = JsonConvert.SerializeObject(entity);

            HttpResponseMessage result;

            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                result = await requestHandler.HandlePostPutRequestAsync(httpClient.PutAsync, uri, content);
            }

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// get all as an asynchronous operation.
        /// </summary>
        /// <returns>T[].</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<T[]> GetAllAsync()
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            string json = await HandleGetRequestAsync(httpClient.GetAsync, baseUri);
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }

        /// <summary>
        /// get all with child entities as an asynchronous operation.
        /// </summary>
        /// <returns>T[].</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<T[]> GetAllWithChildEntitiesAsync()
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/all");

            string json = await HandleGetRequestAsync(httpClient.GetAsync, uri);
            T[] entities = JsonConvert.DeserializeObject<T[]>(json);

            return entities;
        }

        /// <summary>
        /// get by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>T.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<T> GetByIdAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{entity.GetId()}");

            string json = await HandleGetRequestAsync(httpClient.GetAsync, uri);
            T outEntity = JsonConvert.DeserializeObject<T>(json);

            return outEntity;
        }

        /// <summary>
        /// get by identifier with child entities as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>T.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<T> GetByIdWithChildEntitiesAsync(T entity)
        {
            //This should not be here when the local database is connected
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{entity.GetId()}/all");

            var json = await HandleGetRequestAsync(httpClient.GetAsync, uri);

            T outEntity = JsonConvert.DeserializeObject<T>(json);

            return outEntity;
        }

        private static async Task<string> HandleGetRequestAsync(Func<Uri, CancellationToken, Task<HttpResponseMessage>> executeGet, Uri uri)
        {
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(8000);
                var result =  await executeGet(uri, cts.Token);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
