using System;
using System.Net.Http;
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

        private async void ItemsPage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel.LoadData();
            }
            catch (HttpRequestException ex)
            {
                DialogService.CouldNotLoadDataConnection(ex);
            }
            catch (Exception ex)
            {
                DialogService.CouldNotLoadDataUknown(ex);
            }
        }

        public void Edit_Clicked(object sender, RoutedEventArgs e) => NavigationService.Navigate<EditItemPage>(ViewModel.SelectedItem);
    }
}
