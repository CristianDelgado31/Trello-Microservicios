using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.dto;
using UserService.Domain.Entities;

namespace UserService.Domain.Repository
{
    public interface IUserService
    {
        Task<bool> UpdateUser(UserUpdateDto user, int idAuth);
        Task<GetUserDto> GetUser(int idAuth);
    }
}
