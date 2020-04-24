using Packit.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class TripsPage : Page
    {
        public TripsViewModel ViewModel { get; } = new TripsViewModel();
        public TripsPage() => InitializeComponent();
    }
}
