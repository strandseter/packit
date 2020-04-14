using System;
using Packit.App.Services;
using Packit.App.ViewModels;
using Windows.UI.Xaml;
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

        private async void ItemsPage_LoadedAsync(object sender, RoutedEventArgs e) => await ViewModel.LoadData();

        public void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<EditPage>(ViewModel.SelectedItem);
        }
    }
}
