using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackpacksController : PackitApiController
    {
        private readonly IBackpackRepository repository;

        public BackpacksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IBackpackRepository repository)
            :base(context, authenticationService, httpContextAccessor)
        {
            this.repository = repository;
        }

        // GET: api/backpacks
        [HttpGet]
        public IEnumerable<Backpack> GetBackpacks() => repository.GetAll(CurrentUserId());

        // GET: api/backpacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackpack([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        // PUT: api/backpacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackpack([FromRoute] int id, [FromBody] Backpack backpack) => await repository.UpdateAsync(id, backpack, CurrentUserId());

        // POST: api/backpacks
        [HttpPost]
        public async Task<IActionResult> PostBackpack([FromBody] Backpack backpack) => await repository.CreateAsync(backpack, "GetBackpack", CurrentUserId());

        // DELETE: api/backpacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());

        // GET: api/backpacks/shared
        [HttpGet]
        [Route("shared")]
        public IEnumerable<Backpack> GetSharedBackpacks() => repository.GetSharedBackpacks();

        // PUT: api/backpacks/3/items/6/create
        [HttpPut]
        [Route("{backpackId}/items/{itemId}/create")]
        public async Task<IActionResult> PutItemToBackpack([FromRoute] int backpackId, [FromRoute] int itemId) => await repository.CreateManyToManyAsync("GetItemBackpack", itemId, backpackId);

        // DELETE: api/backpacks/5/items/7/delete
        [HttpDelete]
        [Route("{backpackId}/items/{itemId}/delete")]
        public async Task<IActionResult> DeleteItemFromBackpack([FromRoute] int backpackId, [FromRoute] int itemId) => await repository.DeleteManyToManyAsync(itemId, backpackId);

        // GET: api/backpacks/5/items
        [HttpGet]
        [Route("{backpackId}/items")]
        public async Task<IActionResult> GetItemsInBackpack([FromRoute] int backpackId) => await repository.GetManyToManyAsync(backpackId);
    }
}