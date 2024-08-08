using AuthService.Application.Models;
using AuthService.Application.Models.SignIn;
using AuthService.Domain.Entities;
using AuthService.Domain.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthService.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IModel _channel;

        public UserService(IUserRepository userRepository, IModel channel)
        {
            _userRepository = userRepository;
            _channel = channel;
        }

        public async Task<Domain.Entities.User> AddUser(CreateUserDto user)
        {
            var newUser = new Domain.Entities.User
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };

            return await _userRepository.AddUser(newUser);

        }

        public async Task<IEnumerable<Domain.Entities.User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserDto> VerifyUser(CheckUserDto user)
        {
            var userToVerify = new Domain.Entities.User
            {
                Email = user.Email,
                Password = user.Password
            };

            var result = await _userRepository.SearchUser(userToVerify);

            if(result == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = result.Id,
                Username = result.Username,
                Email = result.Email,
                Role = result.Role
            };
        }

        public void SendRegistrationMessage(Domain.Entities.User user)
        {
            var message = JsonConvert.SerializeObject(user);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                routingKey: "user_registration",
                                basicProperties: null,
                                body: body);

        }

    }
}
