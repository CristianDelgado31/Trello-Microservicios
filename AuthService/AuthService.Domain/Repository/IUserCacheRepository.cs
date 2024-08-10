using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Repository
{
    public interface IUserCacheRepository
    {
        Task<User?> GetUserFromCacheAsync(User user);
        Task SetUserFromCacheAsync(List<User> user);
    }
}
