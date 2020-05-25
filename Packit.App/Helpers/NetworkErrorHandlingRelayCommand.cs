using Newtonsoft.Json;
using Packit.App.Services;
using Packit.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Helpers
{
    public class NetworkErrorHandlingRelayCommand<T> : RelayCommand where T : Page
    {
        private readonly IPopUpService popUpService;
        private readonly Func<Task> execute;

        public NetworkErrorHandlingRelayCommand(Action execute, IPopUpService popUpService)
            : base(execute, null)
        {
            this.popUpService = popUpService;
        }

        public NetworkErrorHandlingRelayCommand(Action execute, Func<bool> canExecute, IPopUpService popUpService)
            : base(execute, canExecute)
        {
            this.popUpService = popUpService;
        }

        public NetworkErrorHandlingRelayCommand(Func<Task> execute, IPopUpService popUpService)
        {
            this.execute = execute;
            this.popUpService = popUpService;
        }

        public NetworkErrorHandlingRelayCommand(Func<Task> execute, IPopUpService popUpService, Func<bool> canExecute)
            : base(canExecute)
        {
            this.execute = execute;
            this.popUpService = popUpService;
        }

        //Async void should be avoided(Task instead), but microsoft them self stated this:
        //"Async void methods should be avoided unless they’re event handlers (or the logical equiv­alent of event handlers).
        //Implementations of ICommand.Execute are logically event handlers and, thus, may be async void."
        //Source: https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
        public override async void Execute(object parameter)
        {
            //This structure is made to be expandable and easy to edit in the future. NetworkConnectionException would not have been an exception to catch if I had the time to implement offline/local database.
            try
            {
                await execute();
            }
            catch (NetworkConnectionException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T>(NavigationService.Navigate, ex);
            }
            catch (HttpRequestException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T>(NavigationService.Navigate, ex);
            }
            catch (JsonReaderException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T>(NavigationService.Navigate, ex);
            }
        }
    }

    public class NetworkErrorHandlingRelayCommand<T1, T2> : RelayCommand<T1> where T2 : Page
    {
        private readonly IPopUpService popUpService;
        private readonly Func<T1, Task> execute;

        public NetworkErrorHandlingRelayCommand(Action<T1> execute, IPopUpService popUpService)
            : base(execute, null)
        {
            this.popUpService = popUpService;
        }

        public NetworkErrorHandlingRelayCommand(Func<T1, Task> execute, IPopUpService popUpService)
        {
            this.execute = execute;
            this.popUpService = popUpService;
        }

        public NetworkErrorHandlingRelayCommand(Func<T1, Task> execute, IPopUpService popUpService, Func<T1, bool> canExecute )
            : base(canExecute)
        {
            this.execute = execute;
            this.popUpService = popUpService;
        }

        //Async void should be avoided(Task instead), but microsoft them self stated this:
        //"Async void methods should be avoided unless they’re event handlers (or the logical equiv­alent of event handlers).
        //Implementations of ICommand.Execute are logically event handlers and, thus, may be async void."
        //Source: https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
        public override async void Execute(object parameter)
        {
            try
            {
                await execute((T1)parameter);
            }
            catch (NetworkConnectionException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T2>(NavigationService.Navigate, ex);
            }
            catch (HttpRequestException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T2>(NavigationService.Navigate, ex);
            }
            catch (JsonReaderException ex)
            {
                await popUpService.ShowInternetConnectionErrorAsync();
            }
        }
    }
}
