
using BoardService.Application.TaskList.dto;

namespace BoardService.Application.TaskList
{
    public interface ITaskListService
    {
        Task<ResponseCreateTaskList> CreateTaskList(CreateTaskListDto newTaskList); // nose porque tiene que devolver un ResponseCreateTaskList
    }
}
