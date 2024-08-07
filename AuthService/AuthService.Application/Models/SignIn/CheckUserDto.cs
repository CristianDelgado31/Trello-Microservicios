using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models.SignIn
{
    public class CheckUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
