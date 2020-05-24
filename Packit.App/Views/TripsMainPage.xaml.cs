using System;
using Microsoft.Extensions.DependencyInjection;
using Packit.App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class TripsMainPage : Page
    {
        public TripsMainViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<TripsMainViewModel>();
        public TripsMainPage()
        {
            InitializeComponent();
        }

        public void EntityListClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
