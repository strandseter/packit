using System;

using Packit.App.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class ImageToItemsPage : Page
    {
        public ImageToItemsViewModel ViewModel { get; } = new ImageToItemsViewModel();

        public ImageToItemsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Initialize(e?.Parameter as BitmapImage);
        }
    }
}
