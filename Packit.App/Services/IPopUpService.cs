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
        Task ShowCouldNotLoadAsync<T>(Func<Type, object, NavigationTransitionInfo, bool> onRetryExecute, string notLoadingTitle, Exception exception) where T : Page;
        Task ShowCouldNotLoadAsync(Func<bool> onGoBackExecute, string notLoadingTitle);
        Task ShowCouldNotLoadAsync(Action onBackExecute, string notLoadingTitle);
        Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle, Exception exception);
        Task ShowCouldNotSaveChangesAsync(string notUpdatingTitle);
        Task ShowUnknownErrorAsync(string exeptionMessage);
        Task ShowUnknownErrorAsync(string exeptionMessage, Action onBackExecute);
        Task ShowInternetConnectionErrorAsync(string exeptionMessage);
        Task ShowCouldNotLogIn();
    }
}
