// ***********************************************************************
// Assembly         : Packit.Database.Api
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="AuthenticationService.cs" company="Packit.Database.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Packit.DataAccess;
using Packit.Model;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Packit.Database.Api.Authentication
{
    /// <summary>
    /// Class AuthenticationService.
    /// Implements the <see cref="Packit.Database.Api.Authentication.IAuthenticationService" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Authentication.IAuthenticationService" />
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// The application settings
        /// </summary>
        private readonly AppSettings AppSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings?.Value;
        }

        /// <summary>
        /// Authenticates the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns>User.</returns>
        public User Authenticate(string email, string hashedPassword)
        {
            using (var db = new PackitContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Email == email && u.HashedPassword == hashedPassword);

                if (user == null)
                    return null;

                //This part is somewhat inspired by a source on the internet. But i cannot find it!
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString(CultureInfo.InvariantCulture)),
                    new Claim("id", user.UserId.ToString(CultureInfo.InvariantCulture))
                }),
                    Expires = DateTime.UtcNow.AddDays(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                user.JwtToken = handler.WriteToken(handler.CreateToken(descriptor));

                user.HashedPassword = null;

                return user;
            }
        }
    }
}
