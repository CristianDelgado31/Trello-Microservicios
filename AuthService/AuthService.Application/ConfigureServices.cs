using AuthService.Application.Services;
using AuthService.Application.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IUserService, UserService>(); // como estaba antes
            services.AddScoped<IUserService, UserService>();
            services.AddHostedService<Worker>();

            return services;
        }
    }
}
