// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="IItemRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    /// <summary>
    /// Interface IItemRepository
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Item}" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Item}" />
    public interface IItemRepository : IRepository<Item>
    {
        //Declare non generic methods here.

        /// <summary>
        /// Gets all items with checks asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> GetAllItemsWithChecksAsync(int userId);
    }
}
