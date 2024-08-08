using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.dto
{
    public record GetUserDto(int IdUser, string Username, string Email, string Password, string Role, int IdAuth);
}
