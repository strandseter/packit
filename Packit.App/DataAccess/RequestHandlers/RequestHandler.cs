// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-27-2020
//
// Last Modified By : ander
// Last Modified On : 05-27-2020
// ***********************************************************************
// <copyright file="RequestHandler.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.RequestHandlers
{
    /// <summary>
    /// Class RequestHandler. This class cannot be inherited.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    //I found out right before handing in the project that there is a better way to do this..
    public sealed class HttpRequestHandler : IDisposable
    {
        /// <summary>
        /// The time out milliseconds
        /// </summary>
        private const int timeOutMilliseconds = 10000;
        /// <summary>
        /// The CTS
        /// </summary>
        private CancellationTokenSource cts;

        /// <summary>
        /// handle get request as an asynchronous operation.
        /// </summary>
        /// <param name="executeGet">The execute get.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">executeGet</exception>
        /// <exception cref="ArgumentNullException">uri</exception>
        public async Task<string> HandleGetRequestAsync(Func<Uri, CancellationToken, Task<HttpResponseMessage>> executeGet, Uri uri)
        {
            ResetCancellationToken();

            if (executeGet == null)
                throw new ArgumentNullException(nameof(executeGet));

            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            var result = await executeGet(uri, cts.Token);

            return await result.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// handle delete request as an asynchronous operation.
        /// </summary>
        /// <param name="executeDelete">The execute delete.</param>
        /// <param name="uri">The URI.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">executeDelete</exception>
        /// <exception cref="ArgumentNullException">uri</exception>
        public async Task<bool> HandleDeleteRequestAsync(Func<Uri, CancellationToken, Task<HttpResponseMessage>> executeDelete, Uri uri)
        {
            ResetCancellationToken();

            if (executeDelete == null)
                throw new ArgumentNullException(nameof(executeDelete));

            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            var result = await executeDelete(uri, cts.Token);

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// handle post put request as an asynchronous operation.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="content">The content.</param>
        /// <returns>HttpResponseMessage.</returns>
        /// <exception cref="ArgumentNullException">execute</exception>
        /// <exception cref="ArgumentNullException">uri</exception>
        public async Task<HttpResponseMessage> HandlePostPutRequestAsync(Func<Uri, StringContent, CancellationToken,Task<HttpResponseMessage>> execute, Uri uri, StringContent content)
        {
            ResetCancellationToken();

            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return await execute(uri, content, cts.Token);
        }

        /// <summary>
        /// Resets the cancellation token.
        /// </summary>
        private void ResetCancellationToken()
        {
            if (cts != null)
                cts.Dispose();

            cts = new CancellationTokenSource();
            cts.CancelAfter(timeOutMilliseconds);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => cts?.Dispose();
    }
}
