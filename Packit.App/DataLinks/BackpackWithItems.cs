// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpackWithItems.cs" company="">
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
    /// Class BackpackWithItems.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public class BackpackWithItems : Observable
    {
        /// <summary>
        /// The items
        /// </summary>
        private ObservableCollection<Item> items;
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
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public ObservableCollection<Item> Items
        {
            get => items;
            set => Set(ref items, value);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BackpackWithItems"/> class.
        /// </summary>
        /// <param name="backpack">The backpack.</param>
        public BackpackWithItems(Backpack backpack)
        {
            Backpack = backpack;
            Items = new ObservableCollection<Item>();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => Backpack.Title;
    }
}
