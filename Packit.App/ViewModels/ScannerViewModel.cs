using System;
using System.Windows.Input;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class ScannerViewModel : Observable
    {
        public ICommand CameraCommand { get; set; }
        public ICommand DeviceCommand { get; set; }

        public ScannerViewModel()
        {
            CameraCommand = new RelayCommand(() => NavigationService.Navigate(typeof(CameraPage)));
        }
    }
}
