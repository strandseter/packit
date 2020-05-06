using Packit.App.Helpers;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using Packit.Model;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataLinks
{
    public class TripImageWeatherLink : Observable
    {
        private BitmapImage image;
        private WeatherReport weatherReport;

        public Trip Trip { get; set; }
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
