using System;

using Packit.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class RegisterUserPage : Page
    {
        public RegisterUserViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<RegisterUserViewModel>();

        public RegisterUserPage()
        {
            InitializeComponent();
        }
    }
}
