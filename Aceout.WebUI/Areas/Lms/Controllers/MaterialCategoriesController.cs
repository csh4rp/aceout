using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.Categories.Models;
using Aceout.Application.Services.Lms.MaterialCategories.Commands;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Lms.Models.MaterialCategory;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Areas.Lms.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/lms/material-categories")]
    public class MaterialCategoriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MaterialCategoriesController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMaterialCategoryModel model)
        {
            var command = _mapper.Map<CreateCategoryCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<MaterialCategoryViewModel>(result.Category);

            return Ok(vm);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateMaterialCategoryModel model)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(model);
            var result = await _mediator.Send(command);

            var vm = _mapper.Map<MaterialCategoryViewModel>(result.Category);
            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteCategoryCommand(id);
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MaterialCategoryFilter searchData)
        {
            var query = _mapper.Map<MaterialCategoryDataSourceQuery>(searchData);
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new MaterialCategoryDetailsQuery
            {
                Id = id
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new MaterialCategoryListQuery
            {
                Language = this.Language
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }
    }
}