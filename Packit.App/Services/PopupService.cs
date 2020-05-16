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
        public static async Task ShowDeleteDialogAsync<T>(Func<T, Task> onYesExecute, T onYesParam, string itemName)
        {
            var message = new MessageDialog($"Are you sure you want to delete {itemName}?", "Delete Item");
            message.Commands.Add(new UICommand("Yes", (command) => { onYesExecute(onYesParam); }));
            message.Commands.Add(new UICommand("No", (command) => { return; }));
            await message.ShowAsync();
        }

        public static async Task ShowCouldNotLoadAsync(Func<bool> onGoBackExecute, string notLoadingTitle)
        {
            var message = new MessageDialog($"Could not load {notLoadingTitle}, please try again.", "Could not load");
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Go back", (command) => onGoBackExecute() ));
            await message.ShowAsync();
        }

        public static async Task ShowCouldNotLoadAsync(Action onBackExecute, string notLoadingTitle)
        {
            var message = new MessageDialog($"Could not load {notLoadingTitle}, please try again", "Could not load");
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Go back", (command) => onBackExecute()));
            await message.ShowAsync();
        }

        public static async Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle)
        {
            var message = new MessageDialog($"Could not save changes in {notUpdatingTitle}", "Could not save changes");
            message.Commands.Add(new UICommand($"Ok", (command) => { return; }));
            await message.ShowAsync();
        }

        public static async Task ShowUnknownErrorAsync(Func<bool> onGoBackExecute, string notLoadingTitl)
        {
            var popup = new PopupMenu();
            var message = new MessageDialog("An unknown error occured, please try again.", "Could not load");
            message.Commands.Add(new UICommand("Go back", (command) => onGoBackExecute()));
            await message.ShowAsync();
        }
    }
}
