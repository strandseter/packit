using System;
using Microsoft.Extensions.DependencyInjection;
using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class BrowseBackpacksPage : Page
    {
        public BrowseBackpacksViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<BrowseBackpacksViewModel>();

        public BrowseBackpacksPage()
        {
            InitializeComponent();
        }
    }
}
