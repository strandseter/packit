using Packit.App.ViewModels;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataLinks
{
    public class TripBackpackItemLink : INotifyPropertyChanged
    {
        public Trip Trip { get; set; }
        private BitmapImage image;
        
        public TripsMainViewModel ViewModel { get; set; }
        public ObservableCollection<BackpackItemLink> BackpackItems { get; } = new ObservableCollection<BackpackItemLink>();

        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapImage Image
        {
            get => image;
            set
            {
                if (value == image) return;
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
