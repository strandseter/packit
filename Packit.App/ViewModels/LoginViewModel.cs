using System;
using System.Windows.Input;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class LoginViewModel : Observable
    {

        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            RegisterCommand = new RelayCommand(() => NavigationService.Navigate(typeof(RegisterUserPage)));

            LoginCommand = new RelayCommand(() =>
            {
                var param = 10;
            });
        }
    }
}
