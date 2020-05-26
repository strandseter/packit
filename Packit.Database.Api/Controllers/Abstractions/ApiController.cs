// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ApiController.cs" company="Packit.Database.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using System;
using System.Linq;

namespace Packit.Database.Api.Controllers.Abstractions
{
    /// <summary>
    /// Class PackitApiController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class PackitApiController : ControllerBase
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected PackitContext Context { get; set; }
        /// <summary>
        /// Gets or sets the HTTP context accessor.
        /// </summary>
        /// <value>The HTTP context accessor.</value>
        protected IHttpContextAccessor HttpContextAccessor { get; set; } //TODO: Remove?
        /// <summary>
        /// Gets or sets the authentication service.
        /// </summary>
        /// <value>The authentication service.</value>
        protected IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackitApiController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public PackitApiController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            AuthenticationService = authenticationService;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Currents the user identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected int CurrentUserId()
        {
            var idClaim = int.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase)).Value);

            return idClaim;
        }
    }
}
