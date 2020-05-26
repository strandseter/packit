using Packit.App.Services;
using Packit.Model.NotifyPropertyChanged;
using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace Packit.App.ViewModels
{
    public abstract class ViewModel : Observable
    {
        protected IPopUpService PopUpService { get; set; }

        public ViewModel(IPopUpService popUpService)
        {
            PopUpService = popUpService;
        }

        protected static string GenerateImageName()
        {
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var name = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{name}.jpg";
        }

        protected bool StringIsEqual(string firstString, string secondString)
        {
            if (firstString == null)
                throw new ArgumentNullException(nameof(firstString));

            return firstString.Equals(secondString, StringComparison.CurrentCulture);
        }
    }
}
