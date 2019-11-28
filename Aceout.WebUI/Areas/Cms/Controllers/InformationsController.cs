using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Application.Services.Cms.Informations.Commands;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Cms.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Aceout.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Aceout.Application.Queries.Cms.Informations.Models;
using Aceout.Web.Security.Permissions;

namespace Aceout.WebUI.Areas.Cms.Controllers
{
    [ApiVersion("1.0")]
    [RequireLogOn]
    [Route("v{version:apiVersion}/cms/informations")]
    public class InformationsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InformationsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("list/{pageNumber}/{count}")]
        public async Task<IActionResult> Get([FromRoute] int pageNumber, int count)
        {
            var query = new InformationListQuery
            {
                Count = count,
                PageNumber = pageNumber,
                UserId = User.Id()
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new InformationDetailsQuery
            {
                Id = id
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] InformationsFilter searchData)
        {
            var query = _mapper.Map<InformationDataSourceQuery>(searchData);
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateInforationModel model)
        {
            var command = _mapper.Map<CreateInformationCommand>(model);
            command.UserId = User.Id();

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = result.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateInformationModel model)
        {
            var command = _mapper.Map<UpdateInformationCommand>(model);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = result.Id });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteInformationCommand(id);

            var result = await _mediator.Send(command);

            return NoContent();
        }
    }
}