using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Services;
using UserService.Domain.Repository;

namespace UserService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registra el Worker para que se ejecute en segundo plano
            services.AddHostedService<Worker>();
            services.AddScoped<IUserService, Services.UserService>();

            return services;
        }   
    }
}
