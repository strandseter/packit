// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-10-2020
//
// Last Modified By : ander
// Last Modified On : 05-18-2020
// ***********************************************************************
// <copyright file="ListViewBaseToSelectedItemsConverter.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Packit.App.Convertes
{
    /// <summary>
    /// Class ListViewBaseToSelectedItemsConverter.
    /// Implements the <see cref="Windows.UI.Xaml.Data.IValueConverter" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class ListViewBaseToSelectedItemsConverter : IValueConverter
    {
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
            var listViewBase = value as ListViewBase;
            return listViewBase?.SelectedItems;
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
