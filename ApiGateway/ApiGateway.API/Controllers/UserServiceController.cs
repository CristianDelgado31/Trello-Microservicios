using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
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


        //[HttpGet]
        //[Authorize(Roles = "admin, user")]
        //public async Task<IActionResult> GetUser()
        //{
        //    // Configurar la solicitud al microservicio
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_userServiceBaseUrl}/api/User"); //http://localhost:5164/api/User

        //    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Request.Headers["Authorization"].ToString()); // esto es para enviar el token al microservicio

        //    var response = await _httpClient.SendAsync(requestMessage);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //    }

        //    var content = await response.Content.ReadAsStringAsync(); // Obtiene el contenido de la respuesta
        //    return Content(content, response.Content.Headers.ContentType?.ToString()); // Devuelve el contenido de la respuesta

        //}

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetUser()
        {
            // Extraer el token de autorización del encabezado
            if (!HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                return Unauthorized("No se encontró el token de autorización.");
            }

            var token = authorizationHeader.ToString().Replace("Bearer ", "");

            // Configurar la solicitud al microservicio
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_userServiceBaseUrl}/api/User");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Enviar la solicitud al microservicio
            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }

            // Obtener el contenido de la respuesta
            var content = await response.Content.ReadAsStringAsync();

            // Devolver el contenido de la respuesta con el tipo de contenido adecuado
            return Content(content, response.Content.Headers.ContentType?.ToString());
        }


    }
}
