using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.UserLessons.Models;
using Aceout.Application.Queries.Lms.UserLessons.Results;
using Aceout.Application.Services.Lms.UserLessons.Commands;
using Aceout.Application.Services.Lms.UserLessons.Results;
using Aceout.Infrastructure.Identity;
using Aceout.Web.Mvc;
using Aceout.Web.Security.Permissions;
using Aceout.WebUI.Areas.Lms.Models.UserLessons;
using Aceout.WebUI.Model;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    /// <summary>
    /// User lesson management
    /// </summary>
    [ApiVersion("1.0")]
    [RequireLogOn]
    [Route("v{version:apiVersion}/lms/user-lessons")]
    [Produces("application/json")]
    [ApiController]
    public class UserLessonsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserLessonsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets latest user lesson details
        /// </summary>
        /// <param name="lessonId">lesson ID</param>
        /// <returns>User lesson details</returns>
        [HttpGet("{lessonId}")]
        [ProducesResponseType(200, Type = typeof(UserLessonDetailsDto))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Get([FromRoute] int lessonId)
        {
            var query = new UserLessonDetailsQuery
            {
                LessonId = lessonId,
                UserId = User.Id()
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("start-attempt")]
        [ProducesResponseType(201, Type = typeof(StartLessonViewModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> StartAttempt([FromBody] StartLessonModel model)
        {
            var command = new StartLessonCommand(model.LessonId, User.Id());
            var result = await _mediator.Send(command);
            var vm = _mapper.Map<StartLessonViewModel>(result);

            return CreatedAtAction(nameof(Get), new { lessonId = result.LessonId }, vm);
        }

        [HttpPost("finish-attempt")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> FinishAttempt([FromBody] FinishLessonModel model)
        {
            var command = new FinishLessonCommand(User.Id(), model.LessonId);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<StartLessonViewModel>(result);

            return CreatedAtAction(nameof(Get), new { lessonId = result.LessonId }, vm);
        }

    }
}