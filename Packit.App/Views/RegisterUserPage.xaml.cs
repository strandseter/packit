using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class RegisterUserPage : Page
    {
        public RegisterUserViewModel ViewModel { get; } = new RegisterUserViewModel();

        public RegisterUserPage()
        {
            InitializeComponent();
        }
    }
}
