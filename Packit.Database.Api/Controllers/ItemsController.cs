using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Model;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Packit.Database.Api.Authentication;
using Microsoft.AspNetCore.Http;
using Packit.Database.Api.Repository.Interfaces;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : PackitApiController
    {
        private readonly IItemRepository repository;

        public ItemsController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IItemRepository repository )
            :base(context, authenticationService, httpContextAccessor)
        {
            this.repository = repository;
        }

        // GET: api/Items
        [HttpGet]
        public IEnumerable<Item> GetItem() => repository.GetAll(CurrentUserId());

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem([FromRoute] int id, [FromBody] Item item) => await repository.UpdateAsync(id, item, CurrentUserId());

        // POST: api/Items
        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] Item item) => await repository.CreateAsync(item, "GetItem", CurrentUserId());

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());
    }
}