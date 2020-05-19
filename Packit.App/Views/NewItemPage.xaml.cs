using System;

using Packit.App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class NewItemPage : Page
    {
        public NewItemViewModel ViewModel { get; } = new NewItemViewModel();

        public NewItemPage()
        {
            InitializeComponent();

            Loaded += NewItemPage_Loaded;
        }

        private void NewItemPage_Loaded(object sender, RoutedEventArgs e) => titleTextbox.Focus(FocusState.Programmatic);
    }
}
