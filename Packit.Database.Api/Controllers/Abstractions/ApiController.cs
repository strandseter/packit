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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class PackitApiController : ControllerBase
    {
        protected PackitContext Context { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; } //TODO: Remove?
        protected IAuthenticationService AuthenticationService { get; set; }

        public PackitApiController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            AuthenticationService = authenticationService;
            HttpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var idClaim = int.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase)).Value);

            return idClaim;
        }
    }
}
