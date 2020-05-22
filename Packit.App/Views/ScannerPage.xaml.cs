using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class ScannerPage : Page
    {
        public ScannerViewModel ViewModel { get; } = new ScannerViewModel();

        public ScannerPage()
        {
            InitializeComponent();
        }
    }
}
