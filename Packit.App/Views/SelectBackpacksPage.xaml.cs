using System;

using Packit.App.ViewModels;
using Packit.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class SelectBackpacksPage : Page
    {
        public SelectBackpacksViewModel ViewModel { get; } = new SelectBackpacksViewModel();
        public SelectBackpacksPage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Initialize(e?.Parameter as Trip);
        }
    }
}
