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
        public IRelationMapper RelationMapper { get; set; }
        private readonly IBackpackRepository _repository;

        public BackpacksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IRelationMapper relationMapper, IBackpackRepository repository)
            :base(context, authenticationService, httpContextAccessor)
        {
            RelationMapper = relationMapper;
            _repository = repository;
        }

        // GET: api/Backpacks
        [HttpGet]
        public IEnumerable<Backpack> GetBackpacks() => _repository.GetAll();

        // GET: api/Backpacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackpack([FromRoute] int id) => await _repository.GetById(id).ConfigureAwait(false);

        // PUT: api/Backpacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackpack([FromRoute] int id, [FromBody] Backpack backpack) => await _repository.Update(id, backpack).ConfigureAwait(false);

        // POST: api/Backpacks
        [HttpPost]
        public async Task<IActionResult> PostBackpack([FromBody] Backpack backpack) => await _repository.Create(backpack, "GetBackpack").ConfigureAwait(false);

        // DELETE: api/Backpacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack([FromRoute] int id) => await _repository.Delete(id).ConfigureAwait(false);

        // PUT: api/backpacks/3/items/6/delete
        [HttpPut]
        [Route("{backpackId}/items/{itemId}/create")]
        public async Task<IActionResult> PutItemToBackpack([FromRoute] int backpackId, [FromRoute] int itemId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!BackpackExists(backpackId) || !ItemExists(itemId))
                return NotFound();

            if (ItemBackpackExists(itemId, backpackId))
                return BadRequest();

            var itemBackpack = (ItemBackpack)RelationMapper.CreateManyToMany<ItemBackpack>(itemId, backpackId);

            await Context.ItemBackpack.AddAsync(itemBackpack).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetItemBackpack", new { itemId, backpackId }, itemBackpack);
        }

        // DELETE: api/backpacks/5/items/7/delete
        [HttpDelete]
        [Route("{backpackId}/items/{itemId}/delete")]
        public async Task<IActionResult> DeleteItemFromBackpack([FromRoute] int backpackId, [FromRoute] int itemid)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!BackpackExists(backpackId) || !ItemExists(itemid))
                return NotFound();

            if (!ItemBackpackExists(itemid, backpackId))
                return NotFound();

            var itemBackpack = await Context.ItemBackpack.FirstOrDefaultAsync(ib => ib.ItemId == itemid && ib.BackpackId == backpackId).ConfigureAwait(false);
            Context.ItemBackpack.Remove(itemBackpack);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(itemBackpack);
        }

        // GET: api/backpacks/5/items
        [HttpGet]
        [Route("{backpackId}/items")]
        public async Task<IActionResult> GetItemsInBackpack([FromRoute] int backpackId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var backpack = await Context.Backpacks.FindAsync(backpackId).ConfigureAwait(false);

            if (backpack == null) return NotFound();

            var items = Context.Items.Where(i => i.Backpacks.Any(b => b.BackpackId == backpackId));

            return Ok(items);
        }

        private bool ItemBackpackExists(int itemId, int backpackId) => Context.ItemBackpack.Any(ib => ib.ItemId == itemId && ib.BackpackId == backpackId);
        private bool BackpackExists(int id) => Context.Backpacks.Any(e => e.BackpackId == id);
        private bool ItemExists(int id) => Context.Items.Any(i => i.ItemId == id);
    }
}