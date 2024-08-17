using BoardService.Application.Task.Dto.Create;
using BoardService.Application.Task.Dto.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.Task
{
    public interface ITaskService
    {
        Task<ResponseCreateTask> CreateTask(CreateTaskDto createTaskDto);

        Task<ResponseUpdateTask> UpdateTask(UpdateTaskDto updateTaskDto);
    }
}
