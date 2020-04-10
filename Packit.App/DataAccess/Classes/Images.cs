using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess
{
    public class Images
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private static readonly Uri _baseUri = new Uri("http://localhost:61813/api/Images/");

        public async Task<BitmapImage> GetImage(string imageStringName)
        {
            var uri = new Uri($"{_baseUri}{imageStringName}");

            BitmapImage bitmap = new BitmapImage();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var memStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memStream);
                            memStream.Position = 0;

                            bitmap.SetSource(memStream.AsRandomAccessStream());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bitmap;
        }
    }
}
