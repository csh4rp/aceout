using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.Materials.Models;
using Aceout.Application.Services.Lms.Materials.Commands;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Lms.Models.Materials;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/lms/materials")]
    public class MaterialsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MaterialsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMaterialModel model)
        {
            try
            {
                var command = _mapper.Map<CreateMaterialCommand>(model);
                var result = await _mediator.Send(command);

                //     model.Id = result.MaterialId;

                return Ok(model);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateMaterialModel model)
        {
            var command = _mapper.Map<UpdateMaterialCommand>(model);

            var result = await _mediator.Send(command);
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MaterialFilter searchData)
        {
            var query = _mapper.Map<MaterialDataSourceQuery>(searchData);
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new MaterialDetailsQuery
            {
                Id = id
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteMaterialCommand(id);

            await _mediator.Send(command);

            return Ok();
        }
    }
}