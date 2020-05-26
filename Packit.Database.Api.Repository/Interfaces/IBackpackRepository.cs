// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="IBackpackRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Packit.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    /// <summary>
    /// Interface IBackpackRepository
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Backpack}" />
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.IManyToManyRepository{Packit.Model.Backpack}" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Backpack}" />
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.IManyToManyRepository{Packit.Model.Backpack}" />
    public interface IBackpackRepository : IRepository<Backpack>, IManyToManyRepository<Backpack>
    {
        //Declare type specific methods here.

        /// <summary>
        /// Gets the shared backpacks.
        /// </summary>
        /// <returns>IQueryable&lt;Backpack&gt;.</returns>
        IQueryable<Backpack> GetSharedBackpacks();
        /// <summary>
        /// Gets all backpacks with items.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> GetAllBackpacksWithItems(int userId);
    }
}
