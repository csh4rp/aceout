using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.CourseReports.Models;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Lms.Models.CourseReports;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/lms/course-reports")]
    [ApiController]
    public class CourseReportsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CourseReportsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CourseReportFilter filter)
        {
            var query = _mapper.Map<CourseReportDataSourceQuery>(filter);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}