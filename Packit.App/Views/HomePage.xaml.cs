using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

namespace Packit.App.Views
{
    public sealed partial class HomePage : Page, INotifyPropertyChanged
    {
        public HomePage()
        {
            InitializeComponent();
            StartupMessage();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void StartupMessage()
        {
            var time = DateTime.Now.TimeOfDay;
            var morning = TimeSpan.Parse("12:00:00");
            var afternoon = TimeSpan.Parse("18:00:00");
            var evening = TimeSpan.Parse("00:00:00");
            var night = TimeSpan.Parse("06:00:00");

            if (time < morning)
                TitleText.Text = "Good morning";
            if (time < afternoon)
                TitleText.Text = "Good afternoon";
            if (time < evening)
                TitleText.Text = "Good evening";
            if (time < night)
                TitleText.Text = "Good night";
        }

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
