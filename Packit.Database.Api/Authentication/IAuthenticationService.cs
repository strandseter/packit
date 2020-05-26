// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="IAuthenticationService.cs" company="Packit.Database.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Authentication
{
    /// <summary>
    /// Interface IAuthenticationService
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns>User.</returns>
        User Authenticate(string email, string hashedPassword);
    }
}
