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
    public class LoginViewModel : ViewModel
    {
        private UserDataAccess userDataAccess = new UserDataAccess();
        private string email;
        private string password;
        private string loginErrorMessage;

        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public bool EmailIsValid { get; set; }
        public string Email { get => email; set => Set(ref email, value); }
        public string Password { get => password; set => Set(ref password, value); }
        public string LoginErrorMessage { get => loginErrorMessage; set => Set(ref loginErrorMessage, value); }

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
                catch (HttpRequestException ex)
                {
                    await PopUpService.ShowInternetConnectionErrorAsync(ex.Message);
                }

                catch (Exception ex)
                {
                    await PopUpService.ShowUnknownErrorAsync(ex.Message);
                }
            });
        }
    }
}
