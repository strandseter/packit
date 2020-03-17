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
        protected  string Token { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        protected IAuthenticationService AuthenticationService { get; set; } //???

        public PackitApiController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            AuthenticationService = authenticationService;
            HttpContextAccessor = httpContextAccessor;

             SetUserToken(httpContextAccessor);
        }

        protected async Task<IActionResult> AddManyToMany<T>(int left, int right, DbSet<T> dbset, string message) where T : class, IManyToManyAble
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ObjRelationExists(left, right, dbset))
                return NoContent();

            var obj = (T)Activator.CreateInstance(typeof(T));
            obj.SetLeftId(left);
            obj.SetRightId(right);

            dbset?.Add(obj);

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction(message, new { left, right }, obj);
        }

        private bool ObjRelationExists<T>(int id1, int id2, DbSet<T> dbset) where T : class, IManyToManyAble
        {
            return dbset.Any(e => e.GetLeftId() == id1 && e.GetRightId() == id2);
        }

        private void SetUserToken(IHttpContextAccessor accessor)
        {
            Token = accessor?.HttpContext.Request.Headers["Authorization"];

            if (Token != null)
                Token = Token.Replace("Bearer ", "", StringComparison.CurrentCulture);
        }

        protected bool UserIsAuthorized(User user)
        {
            return user?.JwtToken == Token;
        }
    }
}
