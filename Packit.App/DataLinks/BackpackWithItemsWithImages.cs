// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-18-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpackWithItemsWithImages.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using System.Collections.ObjectModel;

namespace Packit.App.DataLinks
{
    /// <summary>
    /// Class BackpackWithItemsWithImages.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public class BackpackWithItemsWithImages : Observable
    {
        /// <summary>
        /// The items
        /// </summary>
        private ObservableCollection<ItemImageLink> items;
        /// <summary>
        /// The backpack
        /// </summary>
        private Backpack backpack;

        /// <summary>
        /// Gets or sets the backpack.
        /// </summary>
        /// <value>The backpack.</value>
        public Backpack Backpack
        {
            get => backpack;
            set => Set(ref backpack, value);
        }
        /// <summary>
        /// Gets or sets the item image links.
        /// </summary>
        /// <value>The item image links.</value>
        public ObservableCollection<ItemImageLink> ItemImageLinks
        {
            get => items;
            set => Set(ref items, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackpackWithItemsWithImages"/> class.
        /// </summary>
        /// <param name="backpack">The backpack.</param>
        public BackpackWithItemsWithImages(Backpack backpack)
        {
            Backpack = backpack;
            ItemImageLinks = new ObservableCollection<ItemImageLink>();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => Backpack.Title;
    }
}
