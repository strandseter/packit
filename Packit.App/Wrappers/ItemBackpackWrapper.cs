// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ItemBackpackWrapper.cs" company="">
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
    /// Class ItemBackpackWrapper.
    /// Implements the <see cref="Windows.UI.Xaml.DependencyObject" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.DependencyObject" />
    public class ItemBackpackWrapper : DependencyObject
    {
        /// <summary>
        /// The item property
        /// </summary>
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(
                "Item", typeof(Item),
                typeof(ItemBackpackWrapper), null);

        /// <summary>
        /// The backpack with items with images property
        /// </summary>
        public static readonly DependencyProperty BackpackWithItemsWithImagesProperty =
           DependencyProperty.Register(
               "BackpackWithItemsWithImages", typeof(BackpackWithItems),
               typeof(ItemBackpackWrapper), null);

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
        /// Gets or sets the backpack with items.
        /// </summary>
        /// <value>The backpack with items.</value>
        public BackpackWithItemsWithImages BackpackWithItems
        {
            get { return (BackpackWithItemsWithImages)GetValue(BackpackWithItemsWithImagesProperty); }
            set { SetValue(BackpackWithItemsWithImagesProperty, value); }
        }
    }
}
