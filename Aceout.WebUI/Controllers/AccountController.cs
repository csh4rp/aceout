using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Configuration;
using Aceout.Infrastructure.DataModel.Identity;
using Aceout.Infrastructure.Identity;
using Aceout.Tools.Helpers;
using Aceout.WebUI.Model;
using FluentEmail.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Aceout.Web.Mvc;

namespace Aceout.WebUI.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}")]
    public class AccountController : BaseController
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AppConfiguration _config;
        private readonly ILogger _logger;

        public AccountController(UserManager userManager,
            RoleManager roleManager,
            SignInManager<User> signInManager,
            IPasswordHasher<User> passwordHasher,
            AppConfiguration config,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Signs in user and returns JWT
        /// </summary>
        /// <param name="loginViewModel">Basic login data</param>
        /// <returns>JWT</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorsList = ModelState
                        .Select(x => x.Value.Errors)
                        .Where(x => x.Count > 0)
                        .SelectMany(x => x.Select(s => s.ErrorMessage))
                        .ToArray();

                }
               
                var id = User;
                var user = await _userManager.FindByEmailAsync(loginViewModel.Username) ?? await _userManager.FindByNameAsync(loginViewModel.Username);

                if (user == null)
                {

                }

                var passwordResult = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);
                //var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginViewModel.Password);

                if (passwordResult == Microsoft.AspNetCore.Identity.SignInResult.Failed)
                {

                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Authentication.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();//await _userManager.GetClaimsAsync(user);
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));

                var roles = await _userManager.GetRolesAsync(user);
                var permissions = await _roleManager.GetPermissionsAsync(roles);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(CustomClaimTypes.Roles, role));
                }

                foreach (var permission in permissions)
                {
                    claims.Add(new Claim(CustomClaimTypes.Permissions, permission.ToString()));
                }

                claims.Add(new Claim(CustomClaimTypes.Permissions, "AAA"));

                var token = new JwtSecurityToken(
                    issuer: _config.Authentication.Issuer,
                    audience: _config.Authentication.Issuer,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMonths(1),
                    claims: claims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            try
            { 
                var appUser = new User
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName
                };
                 
                var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);

                if (!result.Succeeded)
                {

                }

                appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == appUser.Email);
                var activateToken = RandomGenerator.GetRandomString(32);
                appUser.ActivationToken = activateToken;
                await _userManager.UpdateAsync(appUser);

                var res = await Email.From(_config.Email.EmailAddress)
                    .To(appUser.Email)
                    .Subject("Activation linke")
                    .Body("Activation token: " + activateToken)
                    .SendAsync();


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest();
            }
        }

        [HttpGet("activate")]
        public async Task<IActionResult> Activate(string token)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.ActivationToken == token);
                user.IsEmailConfirmed = true;
                user.IsLockoutEnabled = false;
                await _userManager.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest();
            }
        }



    }
}
