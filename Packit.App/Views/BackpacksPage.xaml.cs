using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class BackpacksPage : Page
    {
        public BackpacksViewModel ViewModel { get; } = new BackpacksViewModel();

        public BackpacksPage()
        {
            InitializeComponent();
        }
    }
}
