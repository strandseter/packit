using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class ItemsPage : Page
    {
        public ItemsViewModel ViewModel { get; } = new ItemsViewModel();

        public ItemsPage()
        {
            InitializeComponent();
        }
    }
}
