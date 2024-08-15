using BoardService.Application.Task.Dto;
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
    }
}
