// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ItemBackpackBoolWrapper.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.DataLinks;
using Packit.Model;
using Windows.UI.Xaml;

namespace Packit.App.Wrappers
{
    /// <summary>
    /// Class ItemBackpackBoolWrapper.
    /// Implements the <see cref="Windows.UI.Xaml.DependencyObject" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.DependencyObject" />
    public class ItemBackpackBoolWrapper : DependencyObject
    {
        /// <summary>
        /// The item property
        /// </summary>
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(
                "Item", typeof(Item),
                typeof(ItemBackpackBoolWrapper), null);

        /// <summary>
        /// The backpack with items with images property
        /// </summary>
        public static readonly DependencyProperty BackpackWithItemsWithImagesProperty =
           DependencyProperty.Register(
               "BackpackWithItemsWithImages", typeof(BackpackWithItemsWithImages),
               typeof(ItemBackpackBoolWrapper), null);

        /// <summary>
        /// The is checked property
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                "IsChecked", typeof(bool),
                typeof(ItemBackpackBoolWrapper), null);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
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
