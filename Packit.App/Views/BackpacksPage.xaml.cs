using System;
using Packit.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class BackpacksPage : Page
    {
        public BackpacksViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<BackpacksViewModel>();

        public BackpacksPage()
        {
            InitializeComponent();
        }
    }
}
