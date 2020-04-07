using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class ItemsPage : Page
    {
        public ItemsViewModel ViewModel { get; } = new ItemsViewModel();

        public ItemsPage()
        {
            InitializeComponent();

            Loaded += ItemsPage_LoadedAsync;
        }

        private async void ItemsPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadItemsAsync();
        }
    }
}
