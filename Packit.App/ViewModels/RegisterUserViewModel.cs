using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class RegisterUserViewModel : Observable
    {
        private string firstNameErrorMessage;
        private string lastNameErrorMessage;
        private string emailErrorMessage;
        private string dateOfBirthErrorMessage;
        private string passwordErrorMessage;

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public string FirstNameErrorMessage { get => firstNameErrorMessage; set => Set(ref firstNameErrorMessage, value); }
        public string LastNameErrorMessage { get => lastNameErrorMessage; set => Set(ref lastNameErrorMessage, value); }
        public string EmailErrorMessage { get => emailErrorMessage; set => Set(ref emailErrorMessage, value); }
        public string DateOfBirthErrorMessage { get => dateOfBirthErrorMessage; set => Set(ref dateOfBirthErrorMessage, value); }
        public string PasswordErrorMessage { get => passwordErrorMessage; set => Set(ref passwordErrorMessage, value); }

        public User NewUser { get; set; } = new User()
        {
            FirstName = "",
            LastName = "",
            Email = "",
            HashedPassword = ""
        };

        public RegisterUserViewModel()
        {
            NewUser.ErrorsChanged += User_ErrorsChanged;

            LoginCommand = new RelayCommand(() => NavigationService.Navigate(typeof(LoginPage)));

            RegisterCommand = new RelayCommand(() =>
            {
                if (NewUser.HasErrors)
                    return;


            });
        }

        private void User_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            FirstNameErrorMessage = InputErrorMessage(NewUser.GetErrors(nameof(NewUser.FirstName)));
            LastNameErrorMessage = InputErrorMessage(NewUser.GetErrors(nameof(NewUser.LastName)));
            EmailErrorMessage = InputErrorMessage(NewUser.GetErrors(nameof(NewUser.Email)));
            DateOfBirthErrorMessage = InputErrorMessage(NewUser.GetErrors(nameof(NewUser.DateOfBirth)));
            PasswordErrorMessage = InputErrorMessage(NewUser.GetErrors(nameof(NewUser.HashedPassword)));
        }

        private string InputErrorMessage(IEnumerable errors)
        {
            if (errors == null)
                return "";

            var builder = new StringBuilder();

            foreach (var message in errors)
                builder.Append($"{message.ToString()}\n");

            return builder.ToString();
        }
    }
}
