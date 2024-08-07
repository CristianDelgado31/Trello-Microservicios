using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
//using UserService.Domain.Interfaces;
using UserService.Domain.Repository;
using UserService.Infraestructure.Consumer;

namespace UserService.Application.Services
{ 
    // Worker service que consume mensajes de la cola de RabbitMQ
    public class Worker : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public Worker(IModel channel, IServiceProvider serviceProvider)
        {
            _channel = channel;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            // Crea el consumidor y suscribe al evento Received
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await ProcessMessageAsync(message);
            };

            _channel.BasicConsume(queue: "userQueue",
                                  autoAck: true,
                                  consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(string message)
        {
            var user = JsonConvert.DeserializeObject<User>(message); // Deserializa el mensaje

            using (var scope = _serviceProvider.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                await userService.AddUser(new User
                {
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Role = user.Role,
                    IdAuth = user.Id // Referencia al ID del registro de la db Auth
                });
            }
        }
    }
}
