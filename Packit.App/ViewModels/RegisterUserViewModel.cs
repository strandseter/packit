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
    public class RegisterUserViewModel : ViewModel
    {
        private UserDataAccess userDataAccess = new UserDataAccess();
        private bool firstNameIsValid;
        private bool lastNameIsValid;
        private bool dateOfBrithIsValid;
        private bool emailIsValid;
        private bool passwordIsValid;
        private bool repeatedPasswordIsValid;
        private string registerErrorMessage = "";
        private string repeatedPassword = "";
        private string password = "";
        private string repeatedPasswordErrormessage = "";
        private string passwordErrormessage = "";

        public DateTimeOffset MaxDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset MinDate { get; set; } = new DateTimeOffset(1900, 5, 1, 8, 6, 32, 545, new TimeSpan(1, 0, 0));

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public bool FirstNameIsValid { get => firstNameIsValid; set => Set(ref firstNameIsValid, value); }
        public bool LastNameIsValid { get => lastNameIsValid; set => Set(ref lastNameIsValid, value); }
        public bool DateOfBrithIsValid { get => dateOfBrithIsValid; set => Set(ref dateOfBrithIsValid, value); }
        public bool EmailIsValid { get => emailIsValid; set => Set(ref emailIsValid, value); }
        public bool PasswordIsValid { get => passwordIsValid; set => Set(ref passwordIsValid, value); }
        public bool RepeatedPasswordIsValid { get => repeatedPasswordIsValid; set => Set(ref repeatedPasswordIsValid, value); }
        public string Password { get => password; set => Set(ref password, value); }
        public string RepeatedPassword { get => repeatedPassword; set => Set(ref repeatedPassword, value); }
        public string RepeatedPasswordErrormessage { get => repeatedPasswordErrormessage; set => Set(ref repeatedPasswordErrormessage, value); }
        public string PasswordErrormessage { get => passwordErrormessage; set => Set(ref passwordErrormessage, value); }
        public string RegsiterErrorMessage { get => registerErrorMessage; set => Set(ref registerErrorMessage, value); }

        public ICollection<bool> UserInputStringtFields { get; } = new Collection<bool>();
        public bool UserInputIsValid { get; set; } = true;

        public User NewUser { get; set; } = new User()
        {
            FirstName = "",
            LastName = "",
            Email = "",
            HashedPassword = ""
        };

        public RegisterUserViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            LoginCommand = new RelayCommand(() => NavigationService.Navigate(typeof(LoginPage)));

            RegisterCommand = new RelayCommand(async () =>
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

                await RegisterUser();
            });
        }

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

        private async Task RegisterUser()
        {
            try
            {
                if (await userDataAccess.AddUserAsync(NewUser))
                    NavigationService.Navigate(typeof(MainPage));
                else
                    registerErrorMessage = "Failed to register in, please try again";
            }
            catch (HttpRequestException ex)
            {
                await PopUpService.ShowInternetConnectionErrorAsync();
            }
            catch (Exception ex)
            {
                await PopUpService.ShowUnknownErrorAsync(ex.Message);
            }
            
        }
    }
}
