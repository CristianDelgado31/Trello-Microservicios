using AuthService.Application.Models;
using AuthService.Application.Models.SignIn;
using AuthService.Domain.Entities;



namespace AuthService.Application.Services.User
{
    public interface IUserService
    {
        Task<Domain.Entities.User> AddUser(CreateUserDto user);
        Task<IEnumerable<Domain.Entities.User>> GetUsers();
        Task<UserDto> VerifyUser(CheckUserDto user);
        //Metodo para RabbitMQ
        void SendRegistrationMessage(Domain.Entities.User user);
    }
}
