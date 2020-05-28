using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class BrowseBackpacksPage : Page
    {
        public BrowseBackpacksViewModel ViewModel { get; } = new BrowseBackpacksViewModel();

        public BrowseBackpacksPage()
        {
            InitializeComponent();
        }
    }
}
