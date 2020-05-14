using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class NewTripPage : Page
    {
        public NewTripViewModel ViewModel { get; } = new NewTripViewModel();

        public NewTripPage()
        {
            InitializeComponent();
        }
    }
}
