// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-26-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="IManyToManyRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Interfaces
{
    /// <summary>
    /// Interface IManyToManyRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IManyToManyRepository<T>
    {
        /// <summary>
        /// Creates the many to many asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="leftId">The left identifier.</param>
        /// <param name="rightId">The right identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> CreateManyToManyAsync(string message, int leftId, int rightId);
        /// <summary>
        /// Gets the many to many asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> GetManyToManyAsync(int id);
        /// <summary>
        /// Deletes the many to many asynchronous.
        /// </summary>
        /// <param name="leftId">The left identifier.</param>
        /// <param name="rightId">The right identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        Task<IActionResult> DeleteManyToManyAsync(int leftId, int rightId);
    }
}
