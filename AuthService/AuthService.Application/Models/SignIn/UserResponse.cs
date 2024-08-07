using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models.SignIn
{
    public class UserResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
