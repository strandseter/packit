using Newtonsoft.Json;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess.Http
{
    public class ImageToTextDataAccess
    {
        private readonly HttpClient httpClient = new HttpClient();
        private Uri uri = new Uri("https://andersimagescannerpleasework.cognitiveservices.azure.com/vision/v3.0/read/analyze?language=en");

        public async Task<Item[]> GetItemsFromImageAsync(byte[] imageBytes)
        {
            HttpResponseMessage result;

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "3bc10b80a8ce462fb1c4ce580c76c62d");

            using (var content = new ByteArrayContent(imageBytes))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                result = await httpClient.PostAsync(uri, content);
            }

            string json = await result.Content.ReadAsStringAsync();
            Item[] items = JsonConvert.DeserializeObject<Item[]>(json);

            return items;
        }
    }
}
