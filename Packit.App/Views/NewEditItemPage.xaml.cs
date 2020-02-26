using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class NewEditItemPage : Page, INotifyPropertyChanged
    {
        public AppWindow AppWindow { get; set; }

        public NewEditItemPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
