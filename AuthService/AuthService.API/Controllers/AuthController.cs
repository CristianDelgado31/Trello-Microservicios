using AuthService.Application.Models;
using AuthService.Application.Models.SignIn;
using AuthService.Application.Services;
using AuthService.Domain.Entities;
using AuthService.Infraestructure.Security;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                var newUser = await _userService.AddUser(user);

                // si el usuario se creo correctamente
                _userService.SendRegistrationMessage(newUser); // Esto esta de prueba nada mas
                return Ok(newUser);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Esto esta de prueba nada mas
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> VerifyUser(CheckUserDto user)
        {
            //if(user.Email == null || user.Password == null)
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, "Email or password is missing");
            //}

            // verifica si el usuario existe
            var result = await _userService.VerifyUser(user);

            if (result == null)
            {
                //retornar que no existe el usuario
                return NotFound(new UserResponse { Success = false, Token = null, Error = "User not found" });
            }

            var generateToken = new JwtTokenGenerator(_configuration);
            var token = generateToken.GenerateJwt(result);

            return Ok(new UserResponse
            {
                Success = true,
                Token = token,
                Error = null
            });
        }

    }

}
