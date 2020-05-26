using Packit.App.DataLinks;
using Packit.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class DetailTripPage : Page
    {
        public DetailTripViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<DetailTripViewModel>();

        public DetailTripPage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e?.Parameter == null)
                ViewModel.Initialize();
            else
                ViewModel.Initialize(e?.Parameter as TripImageWeatherLink);
        }
    }
}
