using System.Linq;
using System.Threading.Tasks;
using Aceout.Application.Queries;
using Aceout.Application.Queries.Infrastructure.Users.Models;
using Aceout.Application.Queries.Infrastructure.Users.Results;
using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Identity;
using Aceout.Web.Mvc;
using Aceout.WebUI.Areas.Administration.Models.Users;
using Aceout.WebUI.Model;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aceout.WebUI.Areas.Administration.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/dashboard/users")]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, RoleManager roleManager, UserManager userManager, IMapper mapper)
        {
            _mediator = mediator;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<DataSource<UserDto>> Get([FromQuery] UserFilter searchData)
        {
            var query = _mapper.Map<UserDataSourceQuery>(searchData);
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<UserViewModel> Get(int id)
        {
            var query = new UserDetailsQuery
            {
                Id = id
            };

            var data = await _mediator.Send(query);
            return _mapper.Map<UserViewModel>(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel model)
        {
            var user = _mapper.Map<User>(model);
            var userResult = await _userManager.CreateAsync(user, model.Password, model.UserRoles);

            if (!userResult.Succeeded)
            {
            }


            var viewModel = _mapper.Map<UserViewModel>(user);
            viewModel.UserRoles = model.UserRoles;

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put( [FromBody] UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            _mapper.Map(model, user);

            var userResult = await _userManager.UpdateAsync(user, model.Password, model.UserRoles);

            if (!userResult.Succeeded)
            {

            }

            var viewModel = _mapper.Map<UserViewModel>(user);
            viewModel.UserRoles = model.UserRoles;

            return Ok(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Object with specified id not found");
            }

            await _userManager.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("checkusername")]
        public async Task<IActionResult> CheckUsername([FromQuery] string userName, [FromQuery] int? id = null)
        {
            var result = await _userManager.CheckUserNameExistsAsync(userName, id);

            return Ok(new
            {
                uniqueUserName = !result
            });
        }

        [HttpGet("autocomplete")]
        public async Task<IActionResult> Autocomplete([FromQuery] string searchQuery)
        {
            var query = new UserAutocompleteQuery
            {
                Language = this.Language,
                SearchQuery = searchQuery
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

    }
}