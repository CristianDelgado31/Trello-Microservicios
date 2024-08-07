using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ApiGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _userServiceBaseUrl = "http://localhost:5164";
        private readonly ILogger<UserServiceController> logger;

        public UserServiceController(HttpClient httpClient, ILogger<UserServiceController> logger)
        {
            _httpClient = httpClient;
            this.logger = logger;
        }

        // METODO DE PRUEBA
        //[HttpGet]
        //[Authorize(Roles = "admin")]
        //public IActionResult Saludar()
        //{
        //    // Obtener los claims del usuario autenticado
        //    var claims = User.Claims.Select(c => new
        //    {
        //        c.Type,
        //        c.Value
        //    }).ToList();

        //    // Retornar los claims en el cuerpo de la respuesta
        //    return Ok(new
        //    {
        //        Message = "Hola este es un metodo para un rol especifico",
        //        Claims = claims
        //    });
        //}



        //[HttpGet]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> GetUser()
        //{
        //    // Obtener el token de la cabecera
        //    //var token = Request.Headers["Authorization"].ToString();

        //    // Configurar la solicitud al microservicio
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_userServiceBaseUrl}/api/User"); //http://localhost:5164/api/User
        //    //requestMessage.Headers.Add("Authorization", token);

        //    // Enviar la solicitud al microservicio
        //    var response = await _httpClient.SendAsync(requestMessage);
        //    var content = await response.Content.ReadAsStringAsync();

        //    // Devolver la respuesta al cliente
        //    return Content(content, response.Content.Headers.ContentType?.ToString());
        //}


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUser()
        {
            // Configurar la solicitud al microservicio
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_userServiceBaseUrl}/api/User"); //http://localhost:5164/api/User

            var response = await _httpClient.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync(); // Obtiene el contenido de la respuesta
            return Content(content, response.Content.Headers.ContentType?.ToString()); // Devuelve el contenido de la respuesta

        }

    }
}
