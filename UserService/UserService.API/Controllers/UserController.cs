using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.dto;
using UserService.Domain.Entities;
using UserService.Domain.Repository;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Update(UserUpdateDto user)
        {
            //Id lo saco del jwt token, el resto por la variable del parametro
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }).ToList();

            var searchId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var id = int.Parse(searchId);


            var result = await _userService.UpdateUser(user, id);

            if (result)
            {
                return Ok(new { Message = "Usuario actualizado" });
            }
            else
            {
                return BadRequest(new { Message = "Usuario no encontrado" });
            }

        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IActionResult Saludar()
        {
            // Obtener los claims del usuario autenticado
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }).ToList();

            var id = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            // Retornar los claims en el cuerpo de la respuesta
            return Ok(new
            {
                Message = "Hola este es un metodo para un rol especifico",
                Id = id,
                //Claims = claims
            });
        }
    }
}

//http://localhost:5164