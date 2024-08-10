using AuthService.Application.Models;
using AuthService.Application.Models.SignIn;
using AuthService.Domain.Entities;
using AuthService.Domain.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthService.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCacheRepository _userCacheRepository;
        private readonly IModel _channel;

        public UserService(IUserRepository userRepository, IModel channel, IUserCacheRepository userCacheRepository)
        {
            _userRepository = userRepository;
            _channel = channel;
            _userCacheRepository = userCacheRepository;
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

            var result = new Domain.Entities.User();
            bool flagCacheEmpty = false;

            //Find user in redis
            try {
                result = await _userCacheRepository.GetUserFromCacheAsync(userToVerify);

                // Find user in database
                if (result == null)
                    result = await _userRepository.SearchUser(userToVerify);
                
            } catch (Exception) { // Si salta error es porque esta vacio el cache
                flagCacheEmpty = true;
            }

            if(flagCacheEmpty == true)
            {
                try {
                    var listUsers = await _userRepository.GetUsers();
                    await _userCacheRepository.SetUserFromCacheAsync(listUsers);

                    result = await _userCacheRepository.GetUserFromCacheAsync(userToVerify);

                } catch (Exception e) {
                    Debug.WriteLine(e.Message); // testeando esto
                }
            }


            ////Find user in database
            //var result = await _userRepository.SearchUser(userToVerify);

            if (result == null)
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
