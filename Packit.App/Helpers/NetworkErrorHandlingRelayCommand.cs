// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-24-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="NetworkErrorHandlingRelayCommand.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Packit.App.Services;
using Packit.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Helpers
{
    /// <summary>
    /// Class NetworkErrorHandlingRelayCommand.
    /// Implements the <see cref="Packit.App.Helpers.RelayCommand" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Packit.App.Helpers.RelayCommand" />
    public class NetworkErrorHandlingRelayCommand<T> : RelayCommand where T : Page
    {
        /// <summary>
        /// The pop up service
        /// </summary>
        private readonly IPopUpService popUpService;
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Func<Task> executeAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkErrorHandlingRelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="popUpService">The pop up service.</param>
        public NetworkErrorHandlingRelayCommand(Action execute, IPopUpService popUpService)
            : base(execute, null)
        {
            this.popUpService = popUpService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkErrorHandlingRelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="popUpService">The pop up service.</param>
        public NetworkErrorHandlingRelayCommand(Action execute, Func<bool> canExecute, IPopUpService popUpService)
            : base(execute, canExecute)
        {
            this.popUpService = popUpService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkErrorHandlingRelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="popUpService">The pop up service.</param>
        public NetworkErrorHandlingRelayCommand(Func<Task> execute, IPopUpService popUpService)
        {
            this.executeAsync = execute;
            this.popUpService = popUpService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkErrorHandlingRelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="popUpService">The pop up service.</param>
        /// <param name="canExecute">The can execute.</param>
        public NetworkErrorHandlingRelayCommand(Func<Task> execute, IPopUpService popUpService, Func<bool> canExecute)
            : base(canExecute)
        {
            this.executeAsync = execute;
            this.popUpService = popUpService;
        }

        //Async void should be avoided(Task instead), but microsoft them self stated this:
        //"Async void methods should be avoided unless they’re event handlers (or the logical equiv­alent of event handlers).
        //Implementations of ICommand.Execute are logically event handlers and, thus, may be async void."
        //Source: https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override async void Execute(object parameter)
        {
            //This structure is made to be expandable and easy to edit in the future. NetworkConnectionException would not have been an exception to catch if I had the time to implement offline/local database.
            try
            {
                await executeAsync();
            }
            catch (NetworkConnectionException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T>(NavigationService.Navigate, ex);
            }
            catch (HttpRequestException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T>(NavigationService.Navigate, ex);
            }
            catch (OperationCanceledException)
            {
                await popUpService.ShowConnectionTimedOutAsync();
            }
            catch (JsonReaderException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T>(NavigationService.Navigate, ex);
            }
            //TODO: Maybe remove before handing in the exam?
            catch (Exception ex)
            {
                await popUpService.ShowUnknownErrorAsync(ex.Message);
            }
        }
    }

    /// <summary>
    /// Class NetworkErrorHandlingRelayCommand.
    /// Implements the <see cref="Packit.App.Helpers.RelayCommand{T1}" />
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// <typeparam name="T2">The type of the t2.</typeparam>
    /// <seealso cref="Packit.App.Helpers.RelayCommand{T1}" />
    public class NetworkErrorHandlingRelayCommand<T1, T2> : RelayCommand<T1> where T2 : Page
    {
        /// <summary>
        /// The pop up service
        /// </summary>
        private readonly IPopUpService popUpService;
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Func<T1, Task> executeAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkErrorHandlingRelayCommand{T1, T2}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="popUpService">The pop up service.</param>
        public NetworkErrorHandlingRelayCommand(Action<T1> execute, IPopUpService popUpService)
            : base(execute, null)
        {
            this.popUpService = popUpService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkErrorHandlingRelayCommand{T1, T2}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="popUpService">The pop up service.</param>
        public NetworkErrorHandlingRelayCommand(Func<T1, Task> execute, IPopUpService popUpService)
        {
            this.executeAsync = execute;
            this.popUpService = popUpService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkErrorHandlingRelayCommand{T1, T2}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="popUpService">The pop up service.</param>
        /// <param name="canExecute">The can execute.</param>
        public NetworkErrorHandlingRelayCommand(Func<T1, Task> execute, IPopUpService popUpService, Func<T1, bool> canExecute )
            : base(canExecute)
        {
            this.executeAsync = execute;
            this.popUpService = popUpService;
        }

        //Async void should be avoided(Task instead), but microsoft them self stated this:
        //"Async void methods should be avoided unless they’re event handlers (or the logical equiv­alent of event handlers).
        //Implementations of ICommand.Execute are logically event handlers and, thus, may be async void."
        //Source: https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override async void Execute(object parameter)
        {
            try
            {
                await executeAsync((T1)parameter);
            }
            catch (NetworkConnectionException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T2>(NavigationService.Navigate, ex);
            }
            catch (HttpRequestException ex)
            {
                await popUpService.ShowCouldNotLoadAsync<T2>(NavigationService.Navigate, ex);
            }
            catch (JsonReaderException)
            {
                await popUpService.ShowInternetConnectionErrorAsync();
            }
            catch (OperationCanceledException)
            {
                await popUpService.ShowConnectionTimedOutAsync();
            }
            //TODO: Maybe remove before handing in the exam?
            catch (Exception ex)
            {
                await popUpService.ShowUnknownErrorAsync(ex.Message);
            }
        }
    }
}
