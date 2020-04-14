using Packit.App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class EditItemPage : Page
    {
        public EditItemViewModel ViewModel { get; } = new EditItemViewModel();

        public EditItemPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var obj = e.Parameter;

            
        }
    }
}
