using AuthService.Domain.Repository;
using AuthService.Infraestructure.Data;
using AuthService.Infraestructure.RabbitMQ;
using AuthService.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;


namespace AuthService.Infraestructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProjectUserAuth") ??
                                throw new InvalidOperationException("Connection string 'ProjectUserAuth' not found"))
                );
                
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            // Configura RabbitMQ
            services.AddSingleton<IModel>(RabbitMqService.CreateChannel());

            //Redis
            services.AddTransient<IUserCacheRepository, UserCacheRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
            });

            return services;
        }
    }
}
