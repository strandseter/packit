using Packit.App.Helpers;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataLinks
{
    public class TripImageWeatherLink : Observable
    {
        private BitmapImage image;
        private WeatherReport weatherReport;
        private Trip trip;

        public Trip Trip
        {
            get => trip;
            set
            {
                if (value == trip) return;
                trip = value;
                OnPropertyChanged(nameof(Trip));
            }
        }
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

        public TripImageWeatherLink(Trip trip) => Trip = trip;
    }
}
