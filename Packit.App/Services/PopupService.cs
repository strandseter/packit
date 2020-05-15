using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Services
{
    public static class PopupService
    {
        public static async Task ShowDeleteDialogAsync<T>(Func<T, Task> execute, T entity, string itemName)
        {
            var popUp = new PopupMenu();
            var message = new MessageDialog($"Are you sure you want to delete {itemName}?", "Delete Item");
            message.Commands.Add(new UICommand("Yes", (command) => { execute(entity); }));
            message.Commands.Add(new UICommand("No", (command) => { return; }));
            await message.ShowAsync();
        }

        public static async Task ShowCouldNotLoadAsync<T>(Func<T, Task> execute, T refreshEntity ,string notLoadingName) where T : Page
        {
            var popup = new PopupMenu();
            var message = new MessageDialog($"Could not load {notLoadingName}, please try again", "Could not load");
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Refresh", (command) => execute(refreshEntity) ));
            await message.ShowAsync();
        }

        public static async Task ShowUnknownErrorAsync()
        {
            var popup = new PopupMenu();
            var message = new MessageDialog("An unknown error occured, please try again.", "Could not load");
        }

        public static async Task ShowCouldNotLoadAsync(Action execute, string notLoadingName)
        {
            var popup = new PopupMenu();
            var message = new MessageDialog($"Could not load {notLoadingName}, please try again", "Could not load");
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Reload", (command) => execute()));
            await message.ShowAsync();
        }
    }
}
