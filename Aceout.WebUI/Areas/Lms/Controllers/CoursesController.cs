using System.Threading;
using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.Courses.Models;
using Aceout.Application.Services.Lms.Courses.Commands;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Lms.Models.Courses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/lms/courses")]
    public class CoursesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CoursesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseModel model)
        {
            var command = _mapper.Map<CreateCourseCommand>(model);
            var result = await _mediator.Send(command);
         
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCourseModel model)
        {
            var command = _mapper.Map<UpdateCourseCommand>(model);
            var result = await _mediator.Send(command);

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CourseFilter searchData)
        {
            var query = _mapper.Map<CourseDataSourceQuery>(searchData);
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new CourseDetailsQuery
            {
                Id = id
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }
    }
}