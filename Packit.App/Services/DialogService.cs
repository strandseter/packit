using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Services
{
    public static class DialogService
    {
        internal static async void CouldNotLoadDataConnection(Exception ex)
        {
            await Dialog("Could not load data", $"Check connection and try again.\n{ex?.GetType()}", "Ok").ShowAsync();
        }

        internal static async void CouldNotLoadDataUknown(Exception ex)
        {
            await Dialog("Something unknown went wrong", $"Try again.\n{ex?.GetType()}", "Ok").ShowAsync();
        }

        internal static async void UnsoppurtedFileFormat(Exception ex)
        {
            await Dialog("Unsupported file format", $"{ex?.GetType()}", "Ok").ShowAsync();
        }

        internal static async void CouldNotSaveChanges(Exception ex)
        {
            await Dialog("Could not save changes", $"{ex.GetType()}", "Ok").ShowAsync();
        }

        internal static async void CouldNotSaveChanges()
        {
            await Dialog("Could not save changes", "Try again", "Ok").ShowAsync();
        }

        internal static async void UnknownErrorOccurred(Exception ex)
        {
            await Dialog("Unknown error occurred", $"{ex.GetType()}", "Ok").ShowAsync();
        }

        internal static async void ImageUploaded()
        {
            //MessageDialog md = new MessageDialog("Lorem ipsum dolor sit amet", "Message Dialog Title");
            //var t = md.ShowAsync();
            await Dialog("Test", "er", "df").ShowAsync();

            await Task.Delay(TimeSpan.FromSeconds(2));
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
