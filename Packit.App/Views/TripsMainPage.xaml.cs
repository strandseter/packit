using System;

using Packit.App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class TripsMainPage : Page
    {
        public TripsMainViewModel ViewModel { get; } = new TripsMainViewModel();

        public TripsMainPage()
        {
            InitializeComponent();
        }

        public void EntityListClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
