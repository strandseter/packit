using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class SelectItemsPage : Page
    {
        public SelectItemsViewModel ViewModel { get; } = new SelectItemsViewModel();

        public SelectItemsPage()
        {
            InitializeComponent();
        }
    }
}
