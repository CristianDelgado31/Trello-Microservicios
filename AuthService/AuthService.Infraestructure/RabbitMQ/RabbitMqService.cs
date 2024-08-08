using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace AuthService.Infraestructure.RabbitMQ
{
    public class RabbitMqService
    {
        public static IModel CreateChannel()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "user_registration",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueDeclare(queue: "user_update",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                return channel;
            }
            catch (BrokerUnreachableException ex)
            {
                // Maneja la excepción, por ejemplo, registra el error
                Console.WriteLine($"Error al conectar con RabbitMQ: {ex.Message}");
                throw;
            }
        }
    }
}
