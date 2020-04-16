using Packit.App.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess
{
    public class Images
    {
        private readonly HttpClient httpClient = new HttpClient();
        private static readonly Uri baseUri = new Uri("http://localhost:61813/api/Images/");

        public async Task<BitmapImage> GetImageAsync(string imageStringName)
        {
            if (!InternetConnectionService.IsConnected())
                return new BitmapImage(new Uri("ms-appx:///Assets/grey.jpg"));

            var uri = new Uri($"{baseUri}{imageStringName}");

            BitmapImage bitmap = new BitmapImage();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                if (response == null || response.StatusCode != HttpStatusCode.OK)
                    return new BitmapImage(new Uri("ms-appx:///Assets/grey.jpg"));

                bitmap.UriSource = uri;

                //TODO: Use?
                    //using (var stream = await response.Content.ReadAsStreamAsync())
                    //{
                    //    using (var memStream = new MemoryStream())
                    //    {
                    //        await stream.CopyToAsync(memStream);
                    //        memStream.Position = 0;

                    //        await bitmap.SetSourceAsync(memStream.AsRandomAccessStream());
                    //    }
                    //}
                
            }
            catch (Exception)
            {
                throw;
            }

            return bitmap;
        }

        public async Task<bool> AddImageAsync(StorageFile file, string imageName)
        {
            if (file == null)
                return false;

            byte[] fileBytes;

            using (var stream = await file?.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (var reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }

            using (var form = new MultipartFormDataContent())
            {
                using (var stream = new StreamContent(new MemoryStream(fileBytes)))
                {
                    form.Add(stream, imageName, file.Name);

                    var response = await httpClient.PostAsync(baseUri, form);

                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task<bool> DeleteImageAsync(string imageName)
        {
            var uri = new Uri($"{baseUri}{imageName}");

            HttpResponseMessage result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }
    }
}
