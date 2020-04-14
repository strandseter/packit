using System;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Services
{
    public static class DialogService
    {
        public static async void CouldNotLoadDataConnection(Exception ex) => await Dialog("Could not load data", $"Check connection and try again.\n{ex?.GetType()}", "Ok").ShowAsync();

        public static async void CouldNotLoadDataUknown(Exception ex) => await Dialog("Something unknown went wrong", $"Try again.\n{ex?.GetType()}", "Ok").ShowAsync();

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
