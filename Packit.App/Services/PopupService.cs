using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Packit.App.Services
{
    public class PopUpService : IPopUpService
    {
        public async Task ShowDeleteDialogAsync<T>(Func<T, Task> onYesExecute, T onYesParam, string itemName)
        {
            var message = new MessageDialog($"Are you sure you want to delete {itemName}?", "Delete Item");
            message.Commands.Add(new UICommand("Yes", (command) => { onYesExecute(onYesParam); }));
            message.Commands.Add(new UICommand("No", (command) => { return; }));
            await message.ShowAsync();
        }

        public async Task ShowCouldNotLoadAsync<T>(Func<Type, object, NavigationTransitionInfo, bool> onRetryExecute, string notLoadingTitle, Exception exception) where T : Page
        {
            if (exception == null)
                throw new ArgumentException(nameof(exception));

            var message = new MessageDialog($"Could not load {notLoadingTitle}, check your connection and try again ({exception.Message}).", "Could not load");
            message.Commands.Add(new UICommand("Retry", (command) => onRetryExecute(typeof(T), DateTime.Now.Ticks, null)));
            message.Commands.Add(new UICommand("Close", (command) => { return; }));
            await message.ShowAsync();
        }

        public async Task ShowCouldNotLoadAsync(Func<bool> onGoBackExecute, string notLoadingTitle)
        {
            var message = new MessageDialog($"Could not load {notLoadingTitle}, check your connection and try again.", "Could not load");
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Go back", (command) => onGoBackExecute() ));
            await message.ShowAsync();
        }

        public async Task ShowCouldNotLoadAsync(Action onBackExecute, string notLoadingTitle)
        {
            var message = new MessageDialog($"Could not load {notLoadingTitle}, please try again", "Could not load");
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Go back", (command) => onBackExecute()));
            await message.ShowAsync();
        }

        public async Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle, Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            var message = new MessageDialog($"Could not upload: {notUpdatingTitle}", $"Could not upload changes({exception.Message}).");
            message.Commands.Add(new UICommand($"Close", (command) => { return; }));
            await message.ShowAsync();
        }

        public async Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle)
        {
            var message = new MessageDialog($"Could not upload: {notUpdatingTitle}", "Could not upload changes");
            message.Commands.Add(new UICommand($"Close", (command) => { return; }));
            await message.ShowAsync();
        }

        public async Task ShowUnknownErrorAsync(string exeptionMessage)
        {
            var popup = new PopupMenu();
            var message = new MessageDialog("An unknown error occured", exeptionMessage);
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Force close", (command) => CoreApplication.Exit()));
            await message.ShowAsync();
        }

        public async Task ShowUnknownErrorAsync(string exeptionMessage, Action onBackExecute)
        {
            var popup = new PopupMenu();
            var message = new MessageDialog("An unknown error occured", exeptionMessage);
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            message.Commands.Add(new UICommand("Go back", (command) => onBackExecute()));
            message.Commands.Add(new UICommand("Force close", (command) => CoreApplication.Exit()));
            await message.ShowAsync();
        }

        public async Task ShowInternetConnectionErrorAsync(string exeptionMessage)
        {
            var popup = new PopupMenu();
            var message = new MessageDialog($"Please check your connection and try again\nError: {exeptionMessage}", "A connection error occured");
            message.Commands.Add(new UICommand("Ok", (command) => { return; }));
            await message.ShowAsync();
        }

        public async Task ShowCouldNotLogIn()
        {
            var message = new MessageDialog("Could not log in, please try again", "Failed to log in");
            message.Commands.Add(new UICommand($"Ok", (command) => { return; }));
            await message.ShowAsync();
        }
    }
}
