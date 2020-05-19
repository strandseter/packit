using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; } = new LoginViewModel();

        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
