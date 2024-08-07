using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.dto
{
    public class UserUpdateDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        //public string? Role { get; set; }

        //public UserUpdateDto(string username, string email, string password, string role)
        //{
        //    Username = username;
        //    Email = email;
        //    Password = password;
        //    Role = role;
        //}
    }
}
