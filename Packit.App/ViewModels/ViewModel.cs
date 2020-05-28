// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-16-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.Services;
using Packit.Model.NotifyPropertyChanged;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.App.ViewModels
{
    /// <summary>
    /// Class ViewModel.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public abstract class ViewModel : Observable
    {
        private bool commandIsEnabled = true;

        /// <summary>Gets or sets a value indicating whether [save is enabled].</summary>
        /// <value>
        ///   <c>true</c> if [save is enabled]; otherwise, <c>false</c>.</value>
        public bool CommandIsEnabled
        {
            get => commandIsEnabled;
            set => Set(ref commandIsEnabled, value);
        }
        /// <summary>
        /// Gets or sets the pop up service.
        /// </summary>
        /// <value>The pop up service.</value>
        protected IPopUpService PopUpService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="popUpService">The pop up service.</param>
        public ViewModel(IPopUpService popUpService)
        {
            PopUpService = popUpService;
        }

        /// <summary>
        /// Disables the command.
        /// </summary>
        protected async Task DisableCommand()
        {
            CommandIsEnabled = false;
            await Task.Delay(8000);
            CommandIsEnabled = true;
        }

        /// <summary>
        /// Generates the name of the image.
        /// </summary>
        /// <returns>System.String.</returns>
        protected static string GenerateImageName()
        {
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var name = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{name}.jpg";
        }

        /// <summary>
        /// Strings the is equal.
        /// </summary>
        /// <param name="firstString">The first string.</param>
        /// <param name="secondString">The second string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">firstString</exception>
        protected static bool StringIsEqual(string firstString, string secondString)
        {
            if (firstString == null)
                throw new ArgumentNullException(nameof(firstString));

            return firstString.Equals(secondString, StringComparison.CurrentCulture);
        }
    }
}
