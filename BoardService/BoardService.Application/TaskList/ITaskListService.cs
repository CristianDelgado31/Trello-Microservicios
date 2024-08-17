using BoardService.Application.TaskList.dto.create;
using BoardService.Application.TaskList.dto.update;

namespace BoardService.Application.TaskList
{
    public interface ITaskListService
    {
        Task<ResponseCreateTaskList> CreateTaskList(CreateTaskListDto newTaskList); // nose porque tiene que devolver un ResponseCreateTaskList

        Task<ResponseUpdateTaskList> UpdateTaskList(UpdateTaskList updateTaskList);
    }
}
