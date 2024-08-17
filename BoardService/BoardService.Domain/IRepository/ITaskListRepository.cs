using BoardService.Domain.Entities;

namespace BoardService.Domain.IRepository
{
    public interface ITaskListRepository
    {
        Task<List<TaskList>> GetTaskLists(int id, bool flag);
        Task<TaskList> CreateTaskList(TaskList taskList);
        Task<TaskList> UpdateTaskList(TaskList taskList);
        Task<TaskList?> SearchTaskList(int id);
    }
}
