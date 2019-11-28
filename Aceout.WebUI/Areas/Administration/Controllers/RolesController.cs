using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Application.Queries.Infrastructure.Roles.Models;
using Aceout.Application.Queries.Infrastructure.Roles.Results;
using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Identity;
using Aceout.Tools.Data;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Administration.Models.Roles;
using Aceout.WebUI.Model;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aceout.WebUI.Areas.Administration.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/dashboard/roles")]
    public class RolesController : BaseController
    {
        private readonly RoleManager _roleManager;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RolesController(RoleManager roleManager, IMediator mediator, IMapper mapper)
        {
            _roleManager = roleManager;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,  Type = typeof(DataSource<RoleDto>))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetList([FromQuery] RoleFilter filter)
        {
            var query = _mapper.Map<RoleDataSourceQuery>(filter);
            var roles = await _mediator.Send(query);

            return Ok(roles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RoleDetailsDto))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Get(int id)
        {
            var query = new RoleDetailsQuery
            {
                Id = id
            };

            var role = await _mediator.Send(query);

            return Ok(role);
        }

        [HttpGet("permissions")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PermissionViewModel>))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public IActionResult GetPermissions()
        {
            var permissions = typeof(Permission).GetEnumValues()
                .Cast<Permission>()
                .Select(x => new PermissionViewModel
                {
                    Id = x.ToString(),
                    Name = x.GetName(),
                    Description = x.GetDescription()
                });

            return Ok(permissions);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RoleViewModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Post([FromBody] RoleViewModel model)
        {
            var role = _mapper.Map<Role>(model);
            var permissions = model.Permissions.Select(x => Enum.Parse<Permission>(x));

            await _roleManager.CreateAsync(role, permissions);

            model.Id = role.Id;

            return Ok(model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RoleViewModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            _mapper.Map(model, role);

            var permissions = model.Permissions.Select(x => Enum.Parse<Permission>(x));

            await _roleManager.UpdateAsync(role, permissions);

            model.Id = role.Id;

            return Ok(model);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(void))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _roleManager.DeleteAsync(id);
            return Ok();
        }
    }
}