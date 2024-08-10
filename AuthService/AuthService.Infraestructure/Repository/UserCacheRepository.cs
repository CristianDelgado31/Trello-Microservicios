using AuthService.Domain.Entities;
using AuthService.Domain.Repository;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infraestructure.Repository
{
    public class UserCacheRepository : IUserCacheRepository
    {
        private readonly IDistributedCache _cache;

        public UserCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<User?> GetUserFromCacheAsync(User user)
        {
            var cachedData = await _cache.GetStringAsync("users");

            if(cachedData == null)
            {
                throw new InvalidOperationException("Cache is empty"); // Si es nulo deberia hacer un set en redis con los datos de la base de datos
            }

            var resultListCache = System.Text.Json.JsonSerializer.Deserialize<List<User>>(cachedData);

            return resultListCache!.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password); // si no encuentra nada devuelve null
        }

        public async Task SetUserFromCacheAsync(List<User> users)
        {
            try {
                var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                await _cache.SetStringAsync("users", System.Text.Json.JsonSerializer.Serialize(users), options);

            } catch (Exception) {
                throw new InvalidOperationException("Error setting cache");
            }
            
        }
    }
}
