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
    public class LoginViewModel : Observable
    {
        private UserDataAccess userDataAccess = new UserDataAccess();
        private string email;
        private string password;

        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public bool EmailIsValid { get; set; }

        public string Email { get => email; set => Set(ref email, value); }
        public string Password { get => password; set => Set(ref password, value); }

        public LoginViewModel()
        {
            var vault = new PasswordVault();

            RegisterCommand = new RelayCommand(() => NavigationService.Navigate(typeof(RegisterUserPage)));

            LoginCommand = new RelayCommand(async () =>
            {
                if (!EmailIsValid)
                {
                    await PopupService.ShowCouldNotLogIn();
                    return;
                }

                try
                {
                    if (await userDataAccess.AuthenticateUser(new User { Email = Email, HashedPassword = Password }))
                        NavigationService.Navigate(typeof(MainPage));
                    else
                        await PopupService.ShowCouldNotLogIn();
                }
                catch (HttpRequestException ex)
                {
                    await PopupService.ShowUnknownErrorAsync(ex.Message);
                }

                catch (Exception ex)
                {
                    await PopupService.ShowUnknownErrorAsync(ex.Message);
                }
            });
        }
    }
}
