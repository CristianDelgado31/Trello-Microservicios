using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.dto;
using UserService.Domain.Entities;
using UserService.Domain.Repository;

namespace UserService.Application.Services
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

        public async Task<GetUserDto> GetUser(int idAuth)
        {
            var user = await _userRepository.GetUserById(idAuth);
            return new GetUserDto(user.Id, user.Username!, user.Email!, user.Password!, user.Role!, user.IdAuth);
        }

        public async Task<bool> UpdateUser(UserUpdateDto updatedUser, int idAuth)
        {
            var user = new User();

            // Actualiza los datos del usuario
            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.IdAuth = idAuth;

            user = await _userRepository.UpdateUser(user); // Actualiza los datos del usuario en la base de datos

            // Envía un mensaje a RabbitMQ para notificar al servicio Auth
            SendUpdateMessage(user);

            return true;
        }
        private void SendUpdateMessage(User user)
        {
            var userAuth = new UserUpdateResponse(user.IdAuth, user.Username!, user.Email!, user.Password!, user.Role!);

            var message = JsonConvert.SerializeObject(userAuth);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: "user_update",
                                  basicProperties: null,
                                  body: body);
        }
    }
}
