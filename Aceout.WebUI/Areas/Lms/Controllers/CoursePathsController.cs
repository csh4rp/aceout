using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.CoursePaths.Models;
using Aceout.Application.Services.Lms.CoursePaths.Commands;
using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Identity;
using Aceout.Web.Mvc;
using Aceout.Web.Security.Permissions;
using Aceout.WebUI.Areas.Lms.Models.CoursePaths;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [RequireLogOn]
    [Route("v{version:apiVersion}/lms/course-paths")]
    public class CoursePathsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CoursePathsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCoursePathModel model)
        {
            var command = _mapper.Map<CreateCoursePathCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<CoursePathViewModel>(result.CoursePath);
            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCoursePathModel model)
        {
            var command = _mapper.Map<UpdateCorusePathCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<CoursePathViewModel>(result.CoursePath);
            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteCoursePathCommand(id);
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [RequireLogOn]
        public async Task<IActionResult> Get([FromQuery] CoursePathFilter searchData)
        {
            var query = _mapper.Map<CoursePathDataSourceQuery>(searchData);
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            var query = new CoursePathListQuery
            {
                Language = this.Language
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("user-list")]
        public async Task<IActionResult> UserList()
        {
            var query = new CoursePathUserListQuery
            {
                UserId = User.Id()
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new CoursePathDetailsQuery
            {
                Id = id
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }
    }
}