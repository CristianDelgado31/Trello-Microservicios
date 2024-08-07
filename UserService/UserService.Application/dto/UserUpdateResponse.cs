using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.dto
{
    public class UserUpdateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public UserUpdateResponse(int id, string username, string email, string password, string role)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
