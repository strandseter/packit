using System;
using Packit.App.DataLinks;
using Packit.App.ViewModels;
using Packit.App.Wrappers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class SelectItemsPage : Page
    {
        public SelectItemsViewModel ViewModel { get; } = new SelectItemsViewModel();
        public SelectItemsPage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Initialize(e?.Parameter as BackpackTripWrapper);
        }
    }
}
