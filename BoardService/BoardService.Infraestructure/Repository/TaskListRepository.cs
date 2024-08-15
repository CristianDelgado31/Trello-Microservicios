using BoardService.Domain.Entities;
using BoardService.Domain.IRepository;
using BoardService.Infraestructure.Databases;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace BoardService.Infraestructure.Repository
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly ConnectionStrings _connections;
        private readonly ITaskRepository _taskRepository;

        public TaskListRepository(IOptions<ConnectionStrings> options, ITaskRepository taskRepository)
        {
            _connections = options.Value;
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskList>> GetTaskLists(int id) // id board
        {
            List<TaskList> tasksLists = new List<TaskList>();

            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM To_Do_Lists WHERE IdBoard = @IdBoard", connection))
                {
                    command.Parameters.AddWithValue("@IdBoard", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            TaskList taskList = new TaskList
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                IdBoard = reader.GetInt32(2),
                                Tasks = []
                            };

                            // Testear
                            taskList.Tasks = await _taskRepository.GetTasks(taskList.Id); //

                            tasksLists.Add(taskList);
                        }
                    }
                }
            }
            return tasksLists;
        }   

        public async Task<TaskList> CreateTaskList(TaskList taskList)
        {
            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO To_Do_Lists (Names, IdBoard) VALUES (@Name, @IdBoard); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Name", taskList.Name);
                    command.Parameters.AddWithValue("@IdBoard", taskList.IdBoard);

                    taskList.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
            return taskList;
        }
    }
}
