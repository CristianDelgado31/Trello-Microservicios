using BoardService.Application.Board;
using BoardService.Application.Task;
using BoardService.Application.TaskList;
using Microsoft.Extensions.DependencyInjection;


namespace BoardService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBoardService, Board.BoardService>();
            services.AddScoped<ITaskListService, TaskListService>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
