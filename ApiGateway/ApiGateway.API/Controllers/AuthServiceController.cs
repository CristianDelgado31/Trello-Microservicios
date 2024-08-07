using ApiGateway.Application.AuthService.create;
using ApiGateway.Application.AuthService.login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthServiceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _authServiceBaseUrl = "http://localhost:5076";

        public AuthServiceController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_authServiceBaseUrl}/api/Auth/login");
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(content)!;

            if (!response.IsSuccessStatusCode)
            {
                return NotFound(loginResponse);
            }


            return Ok(loginResponse); // 
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(CreateUserDto register)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_authServiceBaseUrl}/api/Auth/register");
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            CreateUserDto registerResponse = JsonConvert.DeserializeObject<CreateUserDto>(content)!;

            if (!response.IsSuccessStatusCode)
            {
                return NotFound(registerResponse);
            }

            return Ok(registerResponse);
        }
    }

}
