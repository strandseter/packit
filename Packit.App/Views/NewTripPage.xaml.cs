using System;

using Packit.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class NewTripPage : Page
    {
        public NewTripViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<NewTripViewModel>();

        public NewTripPage()
        {
            InitializeComponent();
        }
    }
}
