using ApiGameCatalog.Configurations;
using ApiGameCatalog.Entities;
using ApiGameCatalog.Repositories;
using ApiGameCatalog.Services;
using ApiGameCatalog.ViewModel;
using ApiGameCatalog.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        
        [HttpGet("{idUser:guid}")]
        public async Task<ActionResult<List<LoginViewModelInput>>> Retrieve([FromRoute] Guid idUser)
        {
            var user = await _userService.Retrieve(idUser);
            
            if (user == null)
                return BadRequest("An error has ocuured while trying to login");

            var userViewModelOutput = new UserViewModelOutput()
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Email = user.Email
            };

            var token = _authenticationService.GenerateToken(userViewModelOutput);

            return Ok(new
            {
                Token = token,
                User = userViewModelOutput
            });
        }

        [HttpPost]
        public async Task<ActionResult<RegisterViewModelInput>> RegisterUser([FromBody] RegisterViewModelInput registroViewModelInput)
        {
            try
            {
                var user = await _userService.RegisterUser(registroViewModelInput);
                return Ok(user);

                /*var user = new User();
                user.Login = registroViewModelInput.Login;
                user.Name = registroViewModelInput.Name;
                user.Email = registroViewModelInput.Email;
                user.Password = registroViewModelInput.Password;

                await _userRepository.Create(user);
                return Ok(user);*/
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("A user with this name is already existent");
            }
        }
    }
}
