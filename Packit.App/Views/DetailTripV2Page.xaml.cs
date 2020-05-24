using Packit.App.DataLinks;
using Packit.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class DetailTripV2Page : Page
    {
        public DetailTripViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<DetailTripViewModel>();

        public DetailTripV2Page() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Initialize(e?.Parameter as TripImageWeatherLink);
        }
    }
}
