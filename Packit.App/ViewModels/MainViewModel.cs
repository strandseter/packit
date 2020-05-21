using System;
using Packit.App.DataAccess;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.Model.NotifyPropertyChanged;

namespace Packit.App.ViewModels
{
    public class MainViewModel : Observable
    {
        private readonly ICustomTripDataAccess customTripDataAccess = new CustomTripDataAccessFactory().Create();

        public MainViewModel()
        {

        }
    }
}
