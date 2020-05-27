// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="UsersController.cs" company="Packit.Database.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    /// <summary>
    /// Class UsersController.
    /// Implements the <see cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Controllers.Abstractions.PackitApiController" />
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : PackitApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public UsersController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
            : base(context, authenticationService, httpContextAccessor) 
        {
        }

        /// <summary>
        /// Authenticates the specified user information.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns>IActionResult.</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userInput)
        {
            if (userInput == null)
                return BadRequest();

            User user;

            try
            {
                user = AuthenticationService.Authenticate(userInput?.Email, userInput?.HashedPassword);
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(ex);
            }

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Posts the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>IActionResult.</returns>
        [AllowAnonymous]
        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await Context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest("Email already used");
            
            Context.Users.Add(user);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }
    }
}