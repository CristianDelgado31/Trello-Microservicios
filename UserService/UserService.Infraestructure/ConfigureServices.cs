using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using UserService.Domain.Repository;
using UserService.Infraestructure.Data;
using UserService.Infraestructure.RabbitMQConsumer;
using UserService.Infraestructure.Repository;

namespace UserService.Infraestructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("ProjectUserServices") ??
                                throw new ArgumentNullException("Connection string not found in appsettings.json"))
                );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IModel>(RabbitMqService.CreateChannel());

            return services;

        }
    }
}
