using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Packit.App.Services
{
    public interface IPopUpService
    {
        Task ShowDeleteDialogAsync<T>(Func<T, Task> onYesExecuteAsync, T onYesParam, string itemName);
        Task ShowDeleteDialogAsync(Func<Task> onYesExecuteAsync, string itemName);
        Task ShowRemoveDialogAsync<T>(Func<T, Task> onYesExecuteAsync, T onYesParam, string subItemName, string mainItemName);
        Task ShowRemoveDialogAsync(Func<Task> onYesExecuteAsync, string subItemName, string mainItemName);
        Task ShowCouldNotLoadAsync<T>(Func<Type, object, NavigationTransitionInfo, bool> onRetryExecute, Exception exception) where T : Page;
        Task ShowCouldNotLoadAsync(Func<bool> onGoBackExecute, string notLoadingTitle);
        Task ShowCouldNotLoadAsync(Action onBackExecute, string notLoadingTitle);
        Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle, Exception exception);
        Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle);
        Task ShowCouldNotDeleteAsync(string itemName);
        Task ShowCouldNotAddAsync<T>(IList<T> selectedEntities, string mainItemName);
        Task ShowUnknownErrorAsync(string exeptionMessage);
        Task ShowInternetConnectionErrorAsync();
        Task ShowCouldNotSaveAsync(string notUpdatingTitle);
        Task ShowCouldNotLogIn();
    }
}
