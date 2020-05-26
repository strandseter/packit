// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-26-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="DateFormatConverter.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Packit.App.Convertes
{
    /// <summary>
    /// Class DateFormatConverter.
    /// Implements the <see cref="Windows.UI.Xaml.Data.IValueConverter" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class DateFormatConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="ArgumentNullException">value</exception>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var dateTime = DateTime.Parse(value.ToString(), CultureInfo.CurrentCulture);
            return dateTime.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
