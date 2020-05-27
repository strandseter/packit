// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-19-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="LoginViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Net.Http;
using System.Windows.Input;
using Packit.App.DataAccess.Http;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using Windows.Security.Credentials;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class LoginViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class LoginViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The user data access
        /// </summary>
        private UserDataAccess userDataAccess = new UserDataAccess();
        /// <summary>
        /// The email
        /// </summary>
        private string email;
        /// <summary>
        /// The password
        /// </summary>
        private string password;
        /// <summary>
        /// The login error message
        /// </summary>
        private string loginErrorMessage;
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the register command.
        /// </summary>
        /// <value>The register command.</value>
        public ICommand RegisterCommand { get; set; }
        /// <summary>
        /// Gets or sets the login command.
        /// </summary>
        /// <value>The login command.</value>
        public ICommand LoginCommand { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [email is valid].
        /// </summary>
        /// <value><c>true</c> if [email is valid]; otherwise, <c>false</c>.</value>
        public bool EmailIsValid { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get => email; set => Set(ref email, value); }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get => password; set => Set(ref password, value); }
        /// <summary>
        /// Gets or sets the login error message.
        /// </summary>
        /// <value>The login error message.</value>
        public string LoginErrorMessage { get => loginErrorMessage; set => Set(ref loginErrorMessage, value); }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public LoginViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            var vault = new PasswordVault();

            RegisterCommand = new RelayCommand(() => NavigationService.Navigate(typeof(RegisterUserPage)));

            LoginCommand = new RelayCommand(async () =>
            {
                LoginErrorMessage = "";

                if (!EmailIsValid)
                {
                    LoginErrorMessage = "Failed to Log in, please try again";
                    return;
                }

                try
                {
                    if (await userDataAccess.AuthenticateUser(new User { Email = Email, HashedPassword = Password }))
                        NavigationService.Navigate(typeof(MainPage));
                    else
                        LoginErrorMessage = "Failed to Log in, please try again";
                }
                catch (HttpRequestException)
                {
                    await PopUpService.ShowInternetConnectionErrorAsync();
                }
                catch (OperationCanceledException)
                {
                    await PopUpService.ShowConnectionTimedOutAsync();
                }
                catch (Exception ex)
                {
                    await PopUpService.ShowUnknownErrorAsync(ex.Message);
                }
            });
        }
        #endregion
    }
}
