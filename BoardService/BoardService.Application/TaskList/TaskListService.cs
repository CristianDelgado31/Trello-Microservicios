using BoardService.Application.TaskList.dto;
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
            var taskLists = await _taskListRepository.GetTaskLists(taskList.IdBoard);
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
    }
}
