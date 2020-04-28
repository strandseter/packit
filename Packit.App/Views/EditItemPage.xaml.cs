using Packit.App.ViewModels;
using Packit.App.DataLinks;
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
            //var parm = e.Parameter;
            //parm = null;

            base.OnNavigatedTo(e);
            ViewModel.Initialize(e?.Parameter as ItemImageLink);
        }


    }
}
