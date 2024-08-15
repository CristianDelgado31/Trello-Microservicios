using BoardService.Domain.Entities;

namespace BoardService.Domain.IRepository
{
    public interface ITaskListRepository
    {
        Task<List<TaskList>> GetTaskLists(int id);

        Task<TaskList> CreateTaskList(TaskList taskList);
    }
}
