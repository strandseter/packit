// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="BackpacksController.cs" company="Packit.Database.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    /// <summary>
    /// Class BackpacksController.
    /// Implements the <see cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    [Route("api/[controller]")]
    [ApiController]
    public class BackpacksController : PackitApiController
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IBackpackRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackpacksController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="repository">The repository.</param>
        public BackpacksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IBackpackRepository repository)
            : base(context, authenticationService, httpContextAccessor) => this.repository = repository;

        // GET: api/backpacks
        /// <summary>
        /// Gets the backpacks.
        /// </summary>
        /// <returns>IEnumerable&lt;Backpack&gt;.</returns>
        [HttpGet]
        public IEnumerable<Backpack> GetBackpacks() => repository.GetAll(CurrentUserId());

        // GET: api/backpacks/5
        /// <summary>
        /// Gets the backpack.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackpack([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        // GET: api/backpacks/shared
        /// <summary>
        /// Gets the shared backpacks.
        /// </summary>
        /// <returns>IEnumerable&lt;Backpack&gt;.</returns>
        [HttpGet]
        [Route("shared")]
        public async Task<IActionResult> GetSharedBackpacks() => await repository.GetAllSharedBackpacksAsync();

        // GET: api/backpacks/5/items
        /// <summary>
        /// Gets the items in backpack.
        /// </summary>
        /// <param name="backpackId">The backpack identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("{backpackId}/items")]
        public async Task<IActionResult> GetItemsInBackpack([FromRoute] int backpackId) => await repository.GetManyToManyAsync(backpackId);

        // GET: api/backpacks/all
        /// <summary>
        /// Gets the backpack with items.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetBackpackWithItems() => await repository.GetAllBackpacksWithItemsAsync(CurrentUserId());

        // GET: api/backpacks/shared/user
        /// <summary>Gets the shared backpacks by user.</summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("shared/user")]
        public async Task<IActionResult> GetSharedBackpackssByUser() => await repository.GetAllSharedBackpacksByUserAsync(CurrentUserId());

        // PUT: api/backpacks/5
        /// <summary>
        /// Puts the backpack.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="backpack">The backpack.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackpack([FromRoute] int id, [FromBody] Backpack backpack) => await repository.UpdateAsync(id, backpack, CurrentUserId());

        // PUT: api/backpacks/3/items/6/create
        /// <summary>
        /// Puts the item to backpack.
        /// </summary>
        /// <param name="backpackId">The backpack identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("{backpackId}/items/{itemId}/create")]
        public async Task<IActionResult> PutItemToBackpack([FromRoute] int backpackId, [FromRoute] int itemId) => await repository.CreateManyToManyAsync("GetItemBackpack", itemId, backpackId);

        // POST: api/backpacks
        /// <summary>
        /// Posts the backpack.
        /// </summary>
        /// <param name="backpack">The backpack.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> PostBackpack([FromBody] Backpack backpack) => await repository.CreateAsync(backpack, "GetBackpack", CurrentUserId());

        // DELETE: api/backpacks/5
        /// <summary>
        /// Deletes the backpack.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());

        // DELETE: api/backpacks/5/items/7/delete
        /// <summary>
        /// Deletes the item from backpack.
        /// </summary>
        /// <param name="backpackId">The backpack identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete]
        [Route("{backpackId}/items/{itemId}/delete")]
        public async Task<IActionResult> DeleteItemFromBackpack([FromRoute] int backpackId, [FromRoute] int itemId) => await repository.DeleteManyToManyAsync(itemId, backpackId);
    }
}