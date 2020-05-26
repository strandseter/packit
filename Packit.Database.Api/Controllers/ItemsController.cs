// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ItemsController.cs" company="Packit.Database.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Packit.DataAccess;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Model;
using Packit.Database.Api.Authentication;
using Microsoft.AspNetCore.Http;
using Packit.Database.Api.Repository.Interfaces;

namespace Packit.Database.Api.Controllers
{
    /// <summary>
    /// Class ItemsController.
    /// Implements the <see cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : PackitApiController
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IItemRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="repository">The repository.</param>
        public ItemsController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IItemRepository repository)
            : base(context, authenticationService, httpContextAccessor) => this.repository = repository;

        // GET: api/Items
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <returns>IEnumerable&lt;Item&gt;.</returns>
        [HttpGet]
        public IEnumerable<Item> GetItem() => repository.GetAll(CurrentUserId());

        // GET: api/Items/5
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        //GET: api/items/all
        /// <summary>
        /// Gets all items with checks.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllItemsWithChecks() => await repository.GetAllItemsWithChecksAsync(CurrentUserId());

        // PUT: api/Items/5
        /// <summary>
        /// Puts the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem([FromRoute] int id, [FromBody] Item item) => await repository.UpdateAsync(id, item, CurrentUserId());

        // POST: api/Items
        /// <summary>
        /// Posts the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] Item item) => await repository.CreateAsync(item, "GetItem", CurrentUserId());

        // DELETE: api/Items/5
        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());
    }
}