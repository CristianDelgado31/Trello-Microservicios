using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infraestructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
