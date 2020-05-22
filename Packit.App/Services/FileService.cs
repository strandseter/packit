using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.Services
{
    public static class FileService
    {
        public static async Task<StorageFile> GetImageFromDeviceAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file == null)
                return null;

            return file;
        }

        public static async Task<BitmapImage> StorageFileToBitmapImageAsync(StorageFile storageFile)
        {
            if (storageFile == null)
                throw new ArgumentNullException(nameof(storageFile));

            using (IRandomAccessStream fileStream = await storageFile?.OpenAsync(FileAccessMode.Read))
            {
                BitmapImage bitmapImage = new BitmapImage();

                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }

        public static async Task<byte[]> BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] bytes;

            using (var randomAccessStream = await RandomAccessStreamReference.CreateFromUri(bitmapImage.UriSource).OpenReadAsync())
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(randomAccessStream);
                PixelDataProvider pixelData = await decoder.GetPixelDataAsync();

                bytes = pixelData.DetachPixelData();
            }
            return bytes;
        }
    }
}
