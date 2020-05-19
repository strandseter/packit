using Packit.App.Services;
using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.ViewModels
{
    public class ViewModel : Observable
    {
        protected async Task CouldNotSave<T>(ICollection<T> failedUploads)
        {
            if (failedUploads == null) return;

            var builder = new StringBuilder();

            foreach (var failedUpdate in failedUploads)
                builder.Append($"{failedUpdate}, ");

            await PopupService.ShowCouldNotSaveChangesAsync(builder.ToString());
        }

        protected static string GenerateImageName()
        {
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var name = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{name}.jpg";
        }

        protected string InputErrorMessage(IEnumerable errors)
        {
            if (errors == null)
                return "";

            var builder = new StringBuilder();

            foreach (var message in errors)
                builder.Append($"{message.ToString()}\n");

            return builder.ToString();
        }

        protected bool StringIsEqual(string firstString, string secondString) => firstString.Equals(secondString, StringComparison.CurrentCulture);
    }
}
