using Packit.App.DataLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Packit.App.Services
{
    public interface IPopUpService
    {
        Task ShowDeleteDialogAsync<T>(Func<T, Task> onYesExecute, T onYesParam, string itemName);
        Task ShowDeleteDialogAsync(Func<Task> onYesExecute, string itemName);
        Task ShowRemoveDialogAsync<T>(Func<T, Task> onYesExecute, T onYesParam, string subItemName, string mainItemName);
        Task ShowRemoveDialogAsync(Func<Task> onYesExecute, string subItemName, string mainItemName);
        Task ShowCouldNotLoadAsync<T>(Func<Type, object, NavigationTransitionInfo, bool> onRetryExecute, Exception exception) where T : Page;
        Task ShowCouldNotLoadAsync(Func<bool> onGoBackExecute, string notLoadingTitle);
        Task ShowCouldNotLoadAsync(Action onBackExecute, string notLoadingTitle);
        Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle, Exception exception);
        Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle);
        Task ShowCouldNotDeleteAsync(string itemName);
        Task ShowCouldNotAddAsync<T>(IList<T> selectedEntities, string mainItemName);
        Task ShowUnknownErrorAsync(string exeptionMessage);
        Task ShowUnknownErrorAsync(string exeptionMessage, Action onBackExecute);
        Task ShowInternetConnectionErrorAsync();
        Task ShowCouldNotSaveAsync(string notUpdatingTitle);
        Task ShowCouldNotLogIn();
    }
}
