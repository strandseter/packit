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
        private readonly IBackpackRepository _repository;

        public BackpacksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IBackpackRepository repository)
            :base(context, authenticationService, httpContextAccessor)
        {
            _repository = repository;
        }

        // GET: api/Backpacks
        [HttpGet]
        public IEnumerable<Backpack> GetBackpacks() => _repository.GetAll(CurrentUserId());

        // GET: api/Backpacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackpack([FromRoute] int id) => await _repository.GetByIdAsync(id, CurrentUserId());

        // PUT: api/Backpacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackpack([FromRoute] int id, [FromBody] Backpack backpack) => await _repository.UpdateAsync(id, backpack, CurrentUserId());

        // POST: api/Backpacks
        [HttpPost]
        public async Task<IActionResult> PostBackpack([FromBody] Backpack backpack) => await _repository.CreateAsync(backpack, "GetBackpack", CurrentUserId());

        // DELETE: api/Backpacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack([FromRoute] int id) => await _repository.DeleteAsync(id, CurrentUserId());

        // PUT: api/backpacks/3/items/6/create
        [HttpPut]
        [Route("{backpackId}/items/{itemId}/create")]
        public async Task<IActionResult> PutItemToBackpack([FromRoute] int backpackId, [FromRoute] int itemId) => await _repository.CreateManyToManyAsync("GetItemBackpack", itemId, backpackId);

        // DELETE: api/backpacks/5/items/7/delete
        [HttpDelete]
        [Route("{backpackId}/items/{itemId}/delete")]
        public async Task<IActionResult> DeleteItemFromBackpack([FromRoute] int backpackId, [FromRoute] int itemId) => await _repository.DeleteManyToManyAsync(itemId, backpackId);

        // GET: api/backpacks/5/items
        [HttpGet]
        [Route("{backpackId}/items")]
        public async Task<IActionResult> GetItemsInBackpack([FromRoute] int backpackId) => await _repository.GetManyToManyAsync(backpackId);
    }
}