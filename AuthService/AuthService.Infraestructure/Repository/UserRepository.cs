using AuthService.Domain.Entities;
using AuthService.Domain.Repository;
using AuthService.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;


namespace AuthService.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get;}

        public async Task<User> AddUser(User user)
        {
            var verification = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (verification != null)
            {
                throw new InvalidOperationException("User already exists");
            }

            await _dbContext.Users.AddAsync(user);
            await UnitOfWork.SaveChangesAsync();

            return user;
        }
        public async Task<List<User>> GetUsers()
        {
            var result = await _dbContext.Users.ToListAsync();

            if (result == null)
            {
                throw new InvalidOperationException("No users found");
            }

            return result;
        }

        public async Task<User> SearchUser(User user)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            return result;
        }

        public async Task<User> UpdateUser(User user)
        {
            var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (userToUpdate == null)
            {
                throw new InvalidOperationException("User not found");
            }

            userToUpdate.Username = user.Username;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.Role = user.Role;

            await UnitOfWork.SaveChangesAsync();

            return userToUpdate;
        }
    }
}
