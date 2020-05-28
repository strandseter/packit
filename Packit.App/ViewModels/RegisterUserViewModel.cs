// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-19-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="RegisterUserViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess.Http;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using Windows.UI.Xaml;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class RegisterUserViewModel.
    /// Implements the <see cref="Packit.App.ViewModels.ViewModel" />
    /// </summary>
    /// <seealso cref="Packit.App.ViewModels.ViewModel" />
    public class RegisterUserViewModel : ViewModel
    {
        #region private fields
        /// <summary>
        /// The user data access
        /// </summary>
        private UserDataAccess userDataAccess = new UserDataAccess();
        /// <summary>
        /// The first name is valid
        /// </summary>
        private bool firstNameIsValid;
        /// <summary>
        /// The last name is valid
        /// </summary>
        private bool lastNameIsValid;
        /// <summary>
        /// The date of brith is valid
        /// </summary>
        private bool dateOfBrithIsValid;
        /// <summary>
        /// The email is valid
        /// </summary>
        private bool emailIsValid;
        /// <summary>
        /// The password is valid
        /// </summary>
        private bool passwordIsValid;
        /// <summary>
        /// The repeated password is valid
        /// </summary>
        private bool repeatedPasswordIsValid;
        /// <summary>
        /// The register error message
        /// </summary>
        private string registerErrorMessage = "";
        /// <summary>
        /// The repeated password
        /// </summary>
        private string repeatedPassword = "";
        /// <summary>
        /// The password
        /// </summary>
        private string password = "";
        /// <summary>
        /// The repeated password errormessage
        /// </summary>
        private string repeatedPasswordErrormessage = "";
        /// <summary>
        /// The password errormessage
        /// </summary>
        private string passwordErrormessage = "";
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the maximum date.
        /// </summary>
        /// <value>The maximum date.</value>
        public DateTimeOffset MaxDate { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// Gets or sets the minimum date.
        /// </summary>
        /// <value>The minimum date.</value>
        public DateTimeOffset MinDate { get; set; } = new DateTimeOffset(1900, 5, 1, 8, 6, 32, 545, new TimeSpan(1, 0, 0));
        /// <summary>
        /// Gets or sets the login command.
        /// </summary>
        /// <value>The login command.</value>
        public ICommand LoginCommand { get; set; }
        /// <summary>
        /// Gets or sets the register command.
        /// </summary>
        /// <value>The register command.</value>
        public ICommand RegisterCommand { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [first name is valid].
        /// </summary>
        /// <value><c>true</c> if [first name is valid]; otherwise, <c>false</c>.</value>
        public bool FirstNameIsValid { get => firstNameIsValid; set => Set(ref firstNameIsValid, value); }
        /// <summary>
        /// Gets or sets a value indicating whether [last name is valid].
        /// </summary>
        /// <value><c>true</c> if [last name is valid]; otherwise, <c>false</c>.</value>
        public bool LastNameIsValid { get => lastNameIsValid; set => Set(ref lastNameIsValid, value); }
        /// <summary>
        /// Gets or sets a value indicating whether [date of brith is valid].
        /// </summary>
        /// <value><c>true</c> if [date of brith is valid]; otherwise, <c>false</c>.</value>
        public bool DateOfBrithIsValid { get => dateOfBrithIsValid; set => Set(ref dateOfBrithIsValid, value); }
        /// <summary>
        /// Gets or sets a value indicating whether [email is valid].
        /// </summary>
        /// <value><c>true</c> if [email is valid]; otherwise, <c>false</c>.</value>
        public bool EmailIsValid { get => emailIsValid; set => Set(ref emailIsValid, value); }
        /// <summary>
        /// Gets or sets a value indicating whether [password is valid].
        /// </summary>
        /// <value><c>true</c> if [password is valid]; otherwise, <c>false</c>.</value>
        public bool PasswordIsValid { get => passwordIsValid; set => Set(ref passwordIsValid, value); }
        /// <summary>
        /// Gets or sets a value indicating whether [repeated password is valid].
        /// </summary>
        /// <value><c>true</c> if [repeated password is valid]; otherwise, <c>false</c>.</value>
        public bool RepeatedPasswordIsValid { get => repeatedPasswordIsValid; set => Set(ref repeatedPasswordIsValid, value); }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get => password; set => Set(ref password, value); }
        /// <summary>
        /// Gets or sets the repeated password.
        /// </summary>
        /// <value>The repeated password.</value>
        public string RepeatedPassword { get => repeatedPassword; set => Set(ref repeatedPassword, value); }
        /// <summary>
        /// Gets or sets the repeated password errormessage.
        /// </summary>
        /// <value>The repeated password errormessage.</value>
        public string RepeatedPasswordErrormessage { get => repeatedPasswordErrormessage; set => Set(ref repeatedPasswordErrormessage, value); }
        /// <summary>
        /// Gets or sets the password errormessage.
        /// </summary>
        /// <value>The password errormessage.</value>
        public string PasswordErrormessage { get => passwordErrormessage; set => Set(ref passwordErrormessage, value); }
        /// <summary>
        /// Gets or sets the regsiter error message.
        /// </summary>
        /// <value>The regsiter error message.</value>
        public string RegsiterErrorMessage { get => registerErrorMessage; set => Set(ref registerErrorMessage, value); }
        /// <summary>
        /// Gets the user input stringt fields.
        /// </summary>
        /// <value>The user input stringt fields.</value>
        public ICollection<bool> UserInputStringtFields { get; } = new Collection<bool>();
        /// <summary>
        /// Gets or sets a value indicating whether [user input is valid].
        /// </summary>
        /// <value><c>true</c> if [user input is valid]; otherwise, <c>false</c>.</value>
        public bool UserInputIsValid { get; set; } = true;

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <value>The new user.</value>
        public User NewUser { get; set; } = new User()
        {
            FirstName = "",
            LastName = "",
            Email = "",
            HashedPassword = ""
        };
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public RegisterUserViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            LoginCommand = new RelayCommand(() => NavigationService.Navigate(typeof(LoginPage)));

            RegisterCommand = new RelayCommand(async () => await ValidateUserInputAsync());
        }
        #endregion

        private async Task ValidateUserInputAsync()
        {
            RegsiterErrorMessage = "";

            UserInputStringtFields.Clear();
            UserInputStringtFields.Add(FirstNameIsValid);
            UserInputStringtFields.Add(LastNameIsValid);
            UserInputStringtFields.Add(EmailIsValid);

            if (!CheckUserInput())
            {
                RegsiterErrorMessage = "Failed to register, please try again";
                return;
            }

            await Task.WhenAll(RegisterUser(), DisableCommand());
        }

        /// <summary>
        /// Checks the user input.
        /// </summary>
        /// <returns><c>true</c> if user input is valid, <c>false</c> otherwise.</returns>
        private bool CheckUserInput()
        {
            foreach (var userInputisValid in UserInputStringtFields)
                if (!userInputisValid)
                    return false;

            var passwordIsValid = true;

            if (NewUser.HashedPassword.Length < 8)
            {
                PasswordErrormessage = "Password is too short";
                passwordIsValid = false;
            }
            if (!string.Equals(NewUser.HashedPassword, RepeatedPassword, StringComparison.CurrentCulture))
            {
                RepeatedPasswordErrormessage = "Not matching";
                passwordIsValid = false;
            }

            if (!passwordIsValid)
                return false;

            return true;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        private async Task RegisterUser()
        {
            try
            {
                if (await userDataAccess.AddUserAsync(NewUser))
                    NavigationService.Navigate(typeof(MainPage));
                else
                    RegsiterErrorMessage = "Failed to register, please try again";
            }
            catch (HttpRequestException)
            {
                RegsiterErrorMessage = "Failed to register, please try again";
                await PopUpService.ShowInternetConnectionErrorAsync();
            }
            catch (OperationCanceledException)
            {
                RegsiterErrorMessage = "Failed to register, please try again";
                await PopUpService.ShowConnectionTimedOutAsync();
            }
            catch (Exception ex)
            {
                RegsiterErrorMessage = "Failed to register, please try again";
                await PopUpService.ShowUnknownErrorAsync(ex.Message);
            }
        }
    }
}
