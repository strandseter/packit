using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Services
{
    public static class DialogService
    {
        public static async void CouldNotLoadDataConnection(Exception ex)
        {
            await Dialog("Could not load data", $"Check connection and try again.\n{ex?.GetType()}", "Ok").ShowAsync();
        }

        public static async void CouldNotLoadDataUknown(Exception ex)
        {
            await Dialog("Something unknown went wrong", $"Try again.\n{ex?.GetType()}", "Ok").ShowAsync();
        }

        public static async void UnsoppurtedFileFormat(Exception ex)
        {
            await Dialog("Unsupported file format", $"{ex?.GetType()}", "Ok").ShowAsync();
        }

        public static async void ImageUploaded()
        {
            MessageDialog md = new MessageDialog("Lorem ipsum dolor sit amet", "Message Dialog Title");
            var t = md.ShowAsync();

            await Task.Delay(TimeSpan.FromSeconds(2));

            t.Cancel();
        }

        private static ContentDialog Dialog(string title, string content, string closeBtnText)
        {
            return new ContentDialog()
            {
                Title = title,
                Content = content,
                CloseButtonText = closeBtnText
            };
        }
    }
}
