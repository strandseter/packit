using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class NewBackpackPage : Page
    {
        public NewBackpackViewModel ViewModel { get; } = new NewBackpackViewModel();

        public NewBackpackPage()
        {
            InitializeComponent();
        }
    }
}
