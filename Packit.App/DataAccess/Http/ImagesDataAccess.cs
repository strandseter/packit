// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 04-20-2020
//
// Last Modified By : ander
// Last Modified On : 05-25-2020
// ***********************************************************************
// <copyright file="ImagesDataAccess.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess
{
    /// <summary>
    /// Class ImagesDataAccess.
    /// </summary>
    public class ImagesDataAccess
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient = new HttpClient();
        /// <summary>
        /// The base URI
        /// </summary>
        private static readonly Uri baseUri = new Uri("http://localhost:61813/api/Images/");

        /// <summary>
        /// get image as an asynchronous operation.
        /// </summary>
        /// <param name="imageStringName">Name of the image string.</param>
        /// <param name="fallbackImageStringPath">The fallback image string path.</param>
        /// <returns>BitmapImage.</returns>
        public async Task<BitmapImage> GetImageAsync(string imageStringName, string fallbackImageStringPath)
        {
            var uriIsCreated = Uri.TryCreate(fallbackImageStringPath, UriKind.Absolute, out Uri fallbackImage);

            if (!uriIsCreated)
                return new BitmapImage(new Uri("ms-appx:///Assets/grey.jpg"));

            if (!InternetConnectionService.IsConnected())
                return new BitmapImage(fallbackImage);

            if (string.IsNullOrEmpty(imageStringName))
                return new BitmapImage(fallbackImage);

            var uri = new Uri($"{baseUri}{imageStringName}");

            var bitmap = new BitmapImage();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                if (response == null || !response.IsSuccessStatusCode)
                    return new BitmapImage(fallbackImage);
            }
            catch (HttpRequestException)
            {
                return new BitmapImage(fallbackImage);
            }
            
            bitmap.UriSource = uri;

            return bitmap;
        }

        /// <summary>
        /// add image as an asynchronous operation.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public async Task<bool> AddImageAsync(StorageFile file, string fileName)
        {
            if (file == null)
                return false;

            if (string.IsNullOrWhiteSpace(fileName))
               return false;

            byte[] fileBytes = await FileToBytesAsync(file);

            using (var form = new MultipartFormDataContent())
            {
                using (var stream = new StreamContent(new MemoryStream(fileBytes)))
                {
                    form.Add(stream, fileName, fileName);

                    var response = await httpClient.PostAsync(baseUri, form);

                    return response.IsSuccessStatusCode;
                }
            }
        }

        /// <summary>
        /// delete image as an asynchronous operation.
        /// </summary>
        /// <param name="imageName">Name of the image.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public async Task<bool> DeleteImageAsync(string imageName)
        {
            var uri = new Uri($"{baseUri}{imageName}");

            HttpResponseMessage result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// file to bytes as an asynchronous operation.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.Byte[].</returns>
        private static async Task<byte[]> FileToBytesAsync(StorageFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            return await Task.Run(async () =>
            {
                 byte[] fileBytes;

                 using (var stream = await file.OpenReadAsync())
                 {
                     fileBytes = new byte[stream.Size];
                     using (var reader = new DataReader(stream))
                     {
                         await reader.LoadAsync((uint)stream.Size);
                         reader.ReadBytes(fileBytes);
                     }
                 }
                 return fileBytes;
            });
        }
    }
}
