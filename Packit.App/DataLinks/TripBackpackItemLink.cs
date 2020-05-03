using Packit.App.ThirdPartyApiModels.Openweathermap;
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
        private BitmapImage image;
        private WeatherReport weatherReport;

        public Trip Trip { get; set; }
        public ObservableCollection<BackpackItemLink> BackpackItems { get; } = new ObservableCollection<BackpackItemLink>();

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

        public WeatherReport WeatherReport
        {
            get => weatherReport;
            set
            {
                if (value == weatherReport) return;
                weatherReport = value;
                OnPropertyChanged(nameof(WeatherReport));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
