using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Application.AuthService.login
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
