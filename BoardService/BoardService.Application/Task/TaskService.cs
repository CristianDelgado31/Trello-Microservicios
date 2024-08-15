using BoardService.Application.Task.Dto;
using BoardService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.Task
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResponseCreateTask> CreateTask(CreateTaskDto createTaskDto)
        {
            var newTask = new Domain.Entities.Task
            {
                Name = createTaskDto.Name,
                Description = createTaskDto.Description,
                IdTaskList = createTaskDto.IdTaskListId
            };

            //Verificar si no hay una tarea con el mismo nombre
            var task = await _taskRepository.GetTasks(newTask.IdTaskList);
            if (task.Any(x => x.Name == newTask.Name))
            {
                throw new Exception("There is already a task with the same name");
            }

            var response = await _taskRepository.CreateTasK(newTask);

            return new ResponseCreateTask
            {
                Id = response.Id,
            };
        }
    }
}
