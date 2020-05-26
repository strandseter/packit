// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-18-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ItemImageBackpackWrapper.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.DataLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Packit.App.Wrappers
{
    /// <summary>
    /// Class ItemImageBackpackWrapper.
    /// Implements the <see cref="Windows.UI.Xaml.DependencyObject" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.DependencyObject" />
    public class ItemImageBackpackWrapper : DependencyObject
    {
        /// <summary>
        /// The item image link property
        /// </summary>
        public static readonly DependencyProperty ItemImageLinkProperty =
            DependencyProperty.Register(
                "ItemImageLink", typeof(ItemImageLink),
                typeof(ItemImageBackpackWrapper), null);

        /// <summary>
        /// The backpack with items with images property
        /// </summary>
        public static readonly DependencyProperty BackpackWithItemsWithImagesProperty =
           DependencyProperty.Register(
               "BackpackWithItemsWithImages", typeof(BackpackWithItemsWithImages),
               typeof(ItemImageBackpackWrapper), null);

        /// <summary>
        /// Gets or sets the item image link.
        /// </summary>
        /// <value>The item image link.</value>
        public ItemImageLink ItemImageLink
        {
            get { return (ItemImageLink)GetValue(ItemImageLinkProperty); }
            set { SetValue(ItemImageLinkProperty, value); }
        }

        /// <summary>
        /// Gets or sets the backpack with items with images.
        /// </summary>
        /// <value>The backpack with items with images.</value>
        public BackpackWithItemsWithImages BackpackWithItemsWithImages
        {
            get { return (BackpackWithItemsWithImages)GetValue(BackpackWithItemsWithImagesProperty); }
            set { SetValue(BackpackWithItemsWithImagesProperty, value); }
        }
    }
}
