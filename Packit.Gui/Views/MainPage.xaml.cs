using System;

using Packit.Gui.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.Gui.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
