using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.Elements.Models;
using Aceout.Application.Queries.Lms.Elements.Results;
using Aceout.Application.Queries.Lms.UserElements.Models;
using Aceout.Application.Queries.Lms.UserElements.Results;
using Aceout.Application.Services.Lms.UserElements.Commands;
using Aceout.Domain.Model.Lms;
using Aceout.Infrastructure.Identity;
using Aceout.Web.Security.Permissions;
using Aceout.WebUI.Areas.Lms.Models.UserElements;
using Aceout.WebUI.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/lms/user-elements")]
    [ApiController]
    [RequireLogOn]
    [Produces("application/json")]
    public class UserElementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserElementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{elementId:int}")]
        public async Task<IActionResult> Get([FromRoute] int elementId)
        {
            var query = new UserElementDetailsQuery
            {
                UserId = User.Id(),
                ElementId = elementId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("list/{lessonId:int}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserElementDetailsDto>))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetList([FromRoute] int lessonId)
        {
            var query = new UserElementListQuery(User.Id(), lessonId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("answers/{lessonId:int}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IEnumerable<ElementAnswerData>>))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetAnswers([FromRoute] int lessonId)
        {
            var query = new ElementsAnswersQuery(lessonId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("{elementId:int}")]
        public async Task<IActionResult> Put([FromRoute] int elementId, [FromBody] UserElementModel model)
        {
            var command = new SaveElementCommand(User.Id(), elementId, model.Answers);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new {elementId}, model);
        }
    }
}