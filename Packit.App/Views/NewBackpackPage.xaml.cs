using System;

using Packit.App.ViewModels;
using Packit.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Packit.App.DataLinks;

namespace Packit.App.Views
{
    public sealed partial class NewBackpackPage : Page
    {
        public NewBackpackViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<NewBackpackViewModel>();

        public NewBackpackPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
                return;

            if (e.Parameter.GetType() == typeof(Trip))
            {
                ViewModel.Initialize(e?.Parameter as Trip);
            }

            if (e.Parameter.GetType() == typeof(TripImageWeatherLink))
            {
                ViewModel.Initialize(e?.Parameter as TripImageWeatherLink);
            }
        }
    }
}
