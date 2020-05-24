using Packit.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Packit.App.Views
{
    public sealed partial class ItemsPage : Page
    {
        public ItemsViewModel ViewModel { get; } = (App.Current as App).ServiceProvider.GetService<ItemsViewModel>();
        public ItemsPage() => InitializeComponent();
    }
}
