using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aceout.Infrastructure.Identity;
using Microsoft.Extensions.Localization;
using Aceout.Web.Mvc;
using AutoMapper;
using Aceout.WebUI.Areas.Lms.Models.UserCourses;
using MediatR;
using Aceout.Application.Services.Lms.UserCourses.Commands;
using Aceout.Application.Queries.Lms.UserCourses.Models;
using Aceout.Web.Security.Permissions;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [RequireLogOn]
    [Route("v{version:apiVersion}/lms/user-courses")]
    public class UserCoursesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserCoursesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("start-attempt")]
        public async Task<IActionResult> StartAttempt([FromBody] StartCourseViewModel model)
        {
            var command = new StartCourseCommand(model.CourseId, User.Id());
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { courseId = result.UserCourse.CourseId }, new
            {
                CourseID = result.UserCourse.CourseId,
                UserCourseId = result.UserCourse.Id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserCoursesFilter searchData)
        {
            var query = _mapper.Map<UserCourseDataSourceQuery>(searchData);
            query.UserId = User.Id();
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> Get([FromRoute] int courseId)
        {
            var query = new UserCourseDetailsQuery
            {
                CourseId = courseId,
                UserId = User.Id()
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

    }
}