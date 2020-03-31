using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using Packit.Model;
using Packit.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Packit.Database.Api.Controllers.Abstractions
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class PackitApiController : ControllerBase
    {
        protected PackitContext Context { get; set; } //???
        protected string Token { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        protected IAuthenticationService AuthenticationService { get; set; } //???

        public PackitApiController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            AuthenticationService = authenticationService;
            HttpContextAccessor = httpContextAccessor;

            SetUserToken();
        }

        private void SetUserToken()
        {
            Token = HttpContextAccessor?.HttpContext.Request.Headers["Authorization"];

            if (Token != null)
                Token = Token.Replace("Bearer ", "", StringComparison.CurrentCulture);
        }

        protected bool UserIsAuthorized(User user) => user?.JwtToken == Token;
    }
}
