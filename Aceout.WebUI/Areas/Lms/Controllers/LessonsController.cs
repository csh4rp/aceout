using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.Lessons;
using Aceout.Application.Queries.Lms.Lessons.Models;
using Aceout.Application.Services.Lms.Lessons.Commands;
using Aceout.Domain.Model.Lms;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Lms.Models.Lessons;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/lms/lessons")]
    public class LessonsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public LessonsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLessonModel model)
        {
            var command = _mapper.Map<CreateLessonCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<LessonViewModel>(result.Lesson);
            vm.Elements = model.Elements;
            
            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateLessonModel model)
        {
            var command = _mapper.Map<UpdateLessonCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<LessonViewModel>(result.Lesson);
            vm.Elements = model.Elements;

            return Ok(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LessonFilter searchData)
        {
            var query = _mapper.Map<LessonDataSourceQuery>(searchData);
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new LessonDetailsQuery
            {
                Id = id
            };

            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteLessonCommand(id);
            await _mediator.Send(command);

            return Ok();
        }
    }
}