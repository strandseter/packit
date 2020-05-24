using System;
using Packit.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<MainViewModel>();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
