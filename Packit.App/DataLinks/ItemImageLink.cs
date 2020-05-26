// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ItemImageLink.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataLinks
{
    /// <summary>
    /// Class ItemImageLink.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public class ItemImageLink : Observable
    {
        /// <summary>
        /// The image
        /// </summary>
        private BitmapImage image;
        /// <summary>
        /// The item
        /// </summary>
        private Item item;

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public BitmapImage Image
        {
            get => image;
            set => Set(ref image, value);
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Item Item
        {
            get => item;
            set => Set(ref item, value);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => Item.Title;
    }
}
