// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-07-2020
//
// Last Modified By : ander
// Last Modified On : 05-22-2020
// ***********************************************************************
// <copyright file="BooleanToTextConverter.cs" company="">
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
    /// Class BooleanToTextConverter.
    /// Implements the <see cref="Windows.UI.Xaml.Data.IValueConverter" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class BooleanToTextConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the true string.
        /// </summary>
        /// <value>The true string.</value>
        public string TrueString { get; set; }
        /// <summary>
        /// Gets or sets the false string.
        /// </summary>
        /// <value>The false string.</value>
        public string FalseString { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is reversed.
        /// </summary>
        /// <value><c>true</c> if this instance is reversed; otherwise, <c>false</c>.</value>
        public bool IsReversed { get; set; }
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>System.Object.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToBoolean(value, CultureInfo.CurrentCulture); //TODO: Fix warning

            if (IsReversed)
                val = !val;

            if (val)
                return TrueString;

            return FalseString;
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
