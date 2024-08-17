using BoardService.Application.Task.Dto.Create;
using BoardService.Application.Task.Dto.Update;
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

        public async Task<ResponseUpdateTask> UpdateTask(UpdateTaskDto updateTaskDto)
        {
            var findTask = await _taskRepository.SearchTask(updateTaskDto.Id, updateTaskDto.IdTaskList);

            if (findTask == null)
                throw new Exception("Task not found in the task list");

            var task = new Domain.Entities.Task
            {
                Id = updateTaskDto.Id,
                Name = updateTaskDto.Name,
                Description = updateTaskDto.Description,
                IdTaskList = updateTaskDto.IdTaskList
            };

            //Verificar si no hay una tarea con el mismo nombre
            var tasks = await _taskRepository.GetTasks(task.IdTaskList);

            var nameExists = tasks.FirstOrDefault(x => x.Name == task.Name);

            if (nameExists != null) {
                if (nameExists.Id != task.Id)
                {
                    throw new Exception("There is already a task with the same name");
                }
                else
                {
                    throw new Exception("The task already has the same name");
                }
            }

            var response = await _taskRepository.UpdateTask(task); // no uso nunca lo que retorna el metodo, podria ser void

            return new ResponseUpdateTask
            {
                Success = true,
                Message = "Task updated successfully"
            };
        }
    }
}
