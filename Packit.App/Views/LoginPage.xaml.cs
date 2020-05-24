using System;

using Packit.App.ViewModels;
using Windows.System;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Packit.App.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<LoginViewModel>();

        public LoginPage()
        {
            InitializeComponent();
        }

        public void OnEnterPressed(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                ViewModel.LoginCommand.Execute(null);
        }
    }
}
