using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model.Models;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecksController : PackitApiController
    {
        private readonly ICheckRepository repository;

        public ChecksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, ICheckRepository repository)
            : base(context, authenticationService, httpContextAccessor) => this.repository = repository;

        // GET: api/checks
        [HttpGet]
        public IEnumerable<Check> GetChecks() => repository.GetAll(CurrentUserId());

        // GET: api/checks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheck([FromRoute] int id) => await repository.GetByIdAsync(id, CurrentUserId());

        // PUT: api/checks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheck([FromRoute] int id, [FromBody] Check check) => await repository.UpdateAsync(id, check, CurrentUserId());

        // POST: api/checks
        [HttpPost]
        public async Task<IActionResult> PostCheck([FromBody] Check check) => await repository.CreateAsync(check, "GetCheck", CurrentUserId());

        // DELETE: api/checks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheck([FromRoute] int id) => await repository.DeleteAsync(id, CurrentUserId());
    }
}
