using BoardService.Domain.IRepository;
using BoardService.Infraestructure.Databases;
using BoardService.Infraestructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Infraestructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddSingleton<IBoardRepository, BoardRepository>();
            services.AddSingleton<ITaskListRepository, TaskListRepository>();
            services.AddSingleton<ITaskRepository, TaskRepository>();

            return services;
        }

    }
}
