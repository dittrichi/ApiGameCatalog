using ApiGameCatalog.Configurations;
using ApiGameCatalog.Exceptions;
using ApiGameCatalog.Services;
using ApiGameCatalog.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ApiGameCatalog.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authenticationService;

        
        public UsersController(IUserService userService, IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginInputModel>> Login(LoginInputModel loginViewModelInput)
        {
            try
            {
                var user = await _userService.Login(loginViewModelInput);
                var token = _authenticationService.GenerateToken(user);
                return Ok(new
                {
                    Token = token,
                    User = user
                });
            }
            catch(LoginErrorException ex)
            {
                return NotFound("Login or Password incorrect");
            }            
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<RegisterInputModel>> RegisterUser([FromBody] RegisterInputModel registroViewModelInput)
        {
            try
            {
                var user = await _userService.RegisterUser(registroViewModelInput);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("A user with this name is already existent");
            }
        }
    }
}
