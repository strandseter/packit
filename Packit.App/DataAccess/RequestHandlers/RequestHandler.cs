using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Packit.App.DataAccess.RequestHandlers
{
    public sealed class RequestHandler : IDisposable
    {
        private const int timeOutMilliseconds = 10000;
        private CancellationTokenSource cts;

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

        public async Task<HttpResponseMessage> HandlePostPutRequestAsync(Func<Uri, StringContent, CancellationToken,Task<HttpResponseMessage>> execute, Uri uri, StringContent content)
        {
            ResetCancellationToken();

            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return await execute(uri, content, cts.Token);
        }

        private void ResetCancellationToken()
        {
            if (cts != null)
                cts.Dispose();

            cts = new CancellationTokenSource();
            cts.CancelAfter(timeOutMilliseconds);
        }

        public void Dispose() => cts?.Dispose();
    }
}
