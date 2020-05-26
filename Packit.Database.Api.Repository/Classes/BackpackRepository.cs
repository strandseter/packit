// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpackRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Classes
{
    /// <summary>
    /// Class BackpackRepository.
    /// Implements the <see cref="Packit.Database.Api.Repository.Generic.ManyToManyRepository{Packit.Model.Backpack, Packit.Model.Item, Packit.Model.ItemBackpack}" />
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.IBackpackRepository" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Repository.Generic.ManyToManyRepository{Packit.Model.Backpack, Packit.Model.Item, Packit.Model.ItemBackpack}" />
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.IBackpackRepository" />
    public class BackpackRepository : ManyToManyRepository<Backpack, Item, ItemBackpack>, IBackpackRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackpackRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BackpackRepository(PackitContext context)
            :base(context)
        {
        }

        //Implement type specific methods here.

        /// <summary>
        /// Gets the shared backpacks.
        /// </summary>
        /// <returns>IQueryable&lt;Backpack&gt;.</returns>
        public IQueryable<Backpack> GetSharedBackpacks()
        {
            return Context.Backpacks.Where(b => b.IsShared);
        }

        /// <summary>
        /// Gets all backpacks with items.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        public async Task<IActionResult> GetAllBackpacksWithItems(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var backpacks = await Context.Backpacks
                .Where(b => b.UserId == userId)
                .Include(b => b.Items)
                    .ThenInclude(i => i.Item)
                .ToListAsync();

            if (backpacks == null)
                return NotFound();

            return Ok(backpacks);
        }
    }
}
