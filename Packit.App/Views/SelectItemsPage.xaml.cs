using System;
using Microsoft.Extensions.DependencyInjection;
using Packit.App.DataLinks;
using Packit.App.ViewModels;
using Packit.App.Wrappers;
using Packit.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class SelectItemsPage : Page
    {
        public SelectItemsViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<SelectItemsViewModel>();
        public SelectItemsPage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e?.Parameter.GetType() == typeof(Backpack))
            {
                ViewModel.Initialize(e.Parameter as Backpack);
            }

            if (e?.Parameter.GetType() == typeof(TripImageWeatherLinkWithBackpackWrapper))
            {
                ViewModel.Initialize(e.Parameter as TripImageWeatherLinkWithBackpackWrapper);
            }

            if (e?.Parameter.GetType() == typeof(BackpackWithItemsWithImages))
            {
                ViewModel.Initialize(e.Parameter as BackpackWithItemsWithImages);
            }

            if (e?.Parameter.GetType() == typeof(BackpackWithItemsTripImageWeatherWrapper))
            {
                ViewModel.Initialize(e?.Parameter as BackpackWithItemsTripImageWeatherWrapper);
            }

            if (e?.Parameter.GetType() == typeof(BackpackTripWrapper))
            {
                ViewModel.Initialize(e.Parameter as BackpackTripWrapper);
            }
        }
    }
}
