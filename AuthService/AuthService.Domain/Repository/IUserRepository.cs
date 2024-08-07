using AuthService.Domain.Entities;


namespace AuthService.Domain.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> AddUser(User user);
        Task<User> SearchUser(User user);
        Task<User> UpdateUser(User user);
    }
}
