using System;
using Microsoft.Extensions.DependencyInjection;
using Packit.App.ViewModels;
using Packit.App.Wrappers;
using Packit.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class SelectBackpacksPage : Page
    {
        public SelectBackpacksViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<SelectBackpacksViewModel>();
        public SelectBackpacksPage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            base.OnNavigatedTo(e);

            if (e.Parameter.GetType() == typeof(BackpackWithItemsWithImagesTripWrapper))
                ViewModel.Initialize(e?.Parameter as BackpackWithItemsWithImagesTripWrapper);

            if (e.Parameter.GetType() == typeof(Trip))
                ViewModel.Initialize(e.Parameter as Trip);
        }
    }
}
