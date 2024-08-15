using BoardService.Domain.IRepository;
using BoardService.Infraestructure.Databases;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace BoardService.Infraestructure.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ConnectionStrings _connections;

        public TaskRepository(IOptions<ConnectionStrings> options)
        {
            _connections = options.Value;
        }
        public async Task<Domain.Entities.Task> CreateTasK(Domain.Entities.Task task)
        {
            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO Tasks (Names, Descriptions, Id_ToDo_List) VALUES (@Name, @Description, @IdTaskList); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("Name", task.Name);
                    command.Parameters.AddWithValue("Description", task.Description);
                    command.Parameters.AddWithValue("IdTaskList", task.IdTaskList);

                    task.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
            return task;
        }

        public async Task<List<Domain.Entities.Task>> GetTasks(int id) // id TaskList
        {
            var tasks = new List<Domain.Entities.Task>();

            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Tasks WHERE Id_ToDo_List = @IdTaskList", connection))
                {
                    command.Parameters.AddWithValue("IdTaskList", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var task = new Domain.Entities.Task()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                IdTaskList = reader.GetInt32(3)
                            };

                            tasks.Add(task);
                        }
                    }
                }
            }
            return tasks;
        }
    }
}
