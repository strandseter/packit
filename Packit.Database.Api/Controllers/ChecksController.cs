// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ChecksController.cs" company="Packit.Database.Api">
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
using Packit.Model.Models;

namespace Packit.Database.Api.Controllers
{
    /// <summary>
    /// Class ChecksController.
    /// Implements the <see cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    [Route("api/[controller]")]
    [ApiController]
    public class ChecksController : PackitApiController
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICheckRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecksController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="repository">The repository.</param>
        public ChecksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, ICheckRepository repository)
            : base(context, authenticationService, httpContextAccessor) => this.repository = repository;

        // GET: api/checks
        /// <summary>
        /// Gets the checks.
        /// </summary>
        /// <returns>IEnumerable&lt;Check&gt;.</returns>
        [HttpGet]
        public IEnumerable<Check> GetChecks() => repository.GetAll(CurrentUserId());

        // GET: api/checks/5
        /// <summary>
        /// Gets the check.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheck([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        // PUT: api/checks/5
        /// <summary>
        /// Puts the check.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="check">The check.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheck([FromRoute] int id, [FromBody] Check check) => await repository.UpdateAsync(id, check, CurrentUserId());

        // POST: api/checks
        /// <summary>
        /// Posts the check.
        /// </summary>
        /// <param name="check">The check.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> PostCheck([FromBody] Check check) => await repository.CreateAsync(check, "GetCheck", CurrentUserId());

        // DELETE: api/checks/5
        /// <summary>
        /// Deletes the check.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheck([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());
    }
}
