using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repository;
using UserService.Infraestructure.Data;

namespace UserService.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

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

        public async Task<User> GetUserById(int idAuth) // idAuth
        { 
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.IdAuth == idAuth);
        }

        public async Task<User> UpdateUser(User user)
        {
            var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.IdAuth == user.IdAuth);

            if (userToUpdate == null)
            {
                throw new InvalidOperationException("User not found");
            }

            userToUpdate.Username = user.Username ?? userToUpdate.Username; // ?? es un operador de coalescencia nula, si el valor de la izquierda es nulo, se asigna el valor de la derecha
            userToUpdate.Password = user.Password ?? userToUpdate.Password;
            userToUpdate.Email = user.Email ?? userToUpdate.Email;
            userToUpdate.Role = user.Role ?? userToUpdate.Role;

            await UnitOfWork.SaveChangesAsync();

            return userToUpdate;    
        }
    }
}
