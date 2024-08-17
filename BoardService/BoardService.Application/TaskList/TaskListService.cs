using BoardService.Application.TaskList.dto.create;
using BoardService.Application.TaskList.dto.update;
using BoardService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.TaskList
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _taskListRepository;

        public TaskListService(ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;
        }

        public async Task<ResponseCreateTaskList> CreateTaskList(CreateTaskListDto newTaskList)
        {
            var taskList = new Domain.Entities.TaskList
            {
                Name = newTaskList.Name,
                IdBoard = newTaskList.IdBoard
            };

            //Verificar si no hay una lista de tarea con el mismo nombre
            var taskLists = await _taskListRepository.GetTaskLists(taskList.IdBoard, false);
            if (taskLists.Any(x => x.Name == taskList.Name))
            {
                throw new Exception("There is already a task list with the same name");
            }

            var result = await _taskListRepository.CreateTaskList(taskList);

            return new ResponseCreateTaskList
            {
                Id = result.Id
            };
        }

        public async Task<ResponseUpdateTaskList> UpdateTaskList(UpdateTaskList updateTaskList)
        {
            var findTaskList = await _taskListRepository.SearchTaskList(updateTaskList.Id);

            if (findTaskList == null)
                throw new Exception("Task list not found");

            var taskLists = await _taskListRepository.GetTaskLists(findTaskList.IdBoard, false);

            var nameExists = taskLists.FirstOrDefault(x => x.Name == updateTaskList.Name);

            if (nameExists != null)
            {
                if (nameExists.Id != updateTaskList.Id)
                {
                    throw new Exception("There is already a task list with the same name");
                }
                else
                {
                    throw new Exception("The name is the same as the current one");
                }
            }
            else
            {
                findTaskList.Name = updateTaskList.Name;
                await _taskListRepository.UpdateTaskList(findTaskList);
            }

            return new ResponseUpdateTaskList
            {
                Success = true,
                Message = "Task list updated successfully"

            };
        }
    }
}
