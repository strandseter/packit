using System.Collections.Generic;
using System.Linq;
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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : PackitApiController
    {
        public UsersController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
            : base(context, authenticationService, httpContextAccessor) 
        {
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userInput)
        {
            var user = AuthenticationService.Authenticate(userInput?.Email, userInput?.HashedPassword);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user?.UserId)
            {
                return BadRequest();
            }

            Context.Entry(user).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [AllowAnonymous]
        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Context.Users.Add(user);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetUser", new { id = user?.UserId }, user);
        }

        private bool UserExists(int id)
        {
            return Context.Users.Any(e => e.UserId == id);
        }
    }
}