using Aceout.Application.Queries;
using Aceout.Application.Queries.Organization.Groups.Models;
using Aceout.Application.Queries.Organization.Groups.Results;
using Aceout.Application.Services.Organization.Groups.Commands;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Organization.Models.Groups;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Aceout.WebUI.Model;

namespace Aceout.WebUI.Areas.Organization.Controllers
{
    [Route("v{version:apiVersion}/organization/groups")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class GroupsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GroupsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates group
        /// </summary>
        /// <param name="model">Group data</param>
        /// <returns>Created group</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(GroupViewModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]       
        public async Task<IActionResult> Post([FromBody] CreateGroupModel model)
        {
            var command = _mapper.Map<CreateGroupCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<GroupViewModel>(result.Group);
            vm.UserIds = model.UserIds;

            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateGroupModel model)
        {
            var command = _mapper.Map<UpdateGroupCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<GroupViewModel>(result.Group);
            vm.UserIds = model.UserIds;

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteGroupCommand(id);
            var result = await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<DataSource<GroupDto>> Get([FromQuery] GroupFilter searchData)
        {
            var query = _mapper.Map<GroupDataSourceQuery>(searchData);
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<GroupDetailsDto> Get([FromRoute] int id)
        {
            var query = new GroupDetailsQuery
            {
                Id = id
            };

            return await _mediator.Send(query);
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            var query = new GroupListQuery
            {
                Language = this.Language
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("autocomplete")]
        public async Task<IEnumerable<GroupDto>> Autocomplete([FromQuery] string searchQuery)
        {
            var query = new GroupAutocompleteQuery
            {
                Language = this.Language,
                SearchQuery = searchQuery
            };

            return await _mediator.Send(query);
        }
    }
}