using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.LessonReports.Model;
using Aceout.WebUI.Areas.Lms.Models.LessonReports;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/lms/lesson-reports")]
    [ApiController]
    public class LessonReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LessonReportsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LessonReportFilter filter)
        {
            var query = _mapper.Map<LessonReportDataSourceQuery>(filter);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}