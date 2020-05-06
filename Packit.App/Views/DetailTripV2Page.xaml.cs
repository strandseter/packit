using System;
using Packit.App.DataLinks;
using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class DetailTripV2Page : Page
    {
        public DetailTripViewModel ViewModel { get; } = new DetailTripViewModel();

        public DetailTripV2Page()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Initialize(e?.Parameter as TripImageWeatherLink);
        }
    }
}
