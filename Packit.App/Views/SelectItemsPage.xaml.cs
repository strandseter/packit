using System;
using Microsoft.Extensions.DependencyInjection;
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
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            base.OnNavigatedTo(e);

            if (e.Parameter.GetType() == typeof(Backpack))
            {
                ViewModel.Initialize(e.Parameter as Backpack);
            }

            if (e.Parameter.GetType() == typeof(BackpackWithItemsTripImageWeatherWrapper))
            {
                ViewModel.Initialize(e?.Parameter as BackpackWithItemsTripImageWeatherWrapper);
            }

            if (e.Parameter.GetType() == typeof(BackpackTripWrapper))
            {
                ViewModel.Initialize(e.Parameter as BackpackTripWrapper);
            }
        }
    }
}
