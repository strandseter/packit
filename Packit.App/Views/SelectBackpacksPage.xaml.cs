using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class SelectBackpacksPage : Page
    {
        public SelectBackpacksViewModel ViewModel { get; } = new SelectBackpacksViewModel();

        public SelectBackpacksPage()
        {
            InitializeComponent();
        }
    }
}
