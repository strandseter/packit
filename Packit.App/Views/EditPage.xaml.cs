using System;

using Packit.App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Packit.App.Views
{
    public sealed partial class EditPage : Page
    {
        public EditViewModel ViewModel { get; } = new EditViewModel();

        public EditPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var obj = e.Parameter;

            TestText.Text = obj.ToString();
        }
    }
}
