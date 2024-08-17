using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using BoardService.Domain.Entities;
using BoardService.Infraestructure.Databases;
using BoardService.Domain.IRepository;

namespace BoardService.Infraestructure.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ConnectionStrings _connections;
        private readonly ITaskListRepository _taskListRepository;

        public BoardRepository(IOptions<ConnectionStrings> options, ITaskListRepository taskListRepository)
        {
            _connections = options.Value;
            _taskListRepository = taskListRepository;
        }

        public async Task<Board> CreateBoard(Board board)
        {
            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO Boards (Names, IdAuth) VALUES (@Name, @IdAuth); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Name", board.Name);
                    command.Parameters.AddWithValue("@IdAuth", board.IdAuth);

                    board.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
            return board;
        }

        public async Task<List<Board>> GetBoards(int id, bool flag) // id auth - si flag es true, se obtienen los tableros con sus listas de tareas
        {
            List<Board> boards = new List<Board>();
            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Boards WHERE IdAuth = @IdAuth", connection))
                {
                    command.Parameters.AddWithValue("@IdAuth", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Board board = new Board
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                IdAuth = reader.GetInt32(2), // Asegúrate de que esta columna exista en la tabla "Boards"
                                TaskLists = []
                            };

                            if (flag)
                            {
                                board.TaskLists = await _taskListRepository.GetTaskLists(board.Id, true);
                            }

                            boards.Add(board);
                        }
                    }
                }
            }

            return boards;
        }

        public async Task<Board?> SearchBoard(int id)
        {
            Board? board = null;
            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Boards WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            board = new Board
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                IdAuth = reader.GetInt32(2) // Asegúrate de que esta columna exista en la tabla "Boards"
                            };
                        }
                    }
                }
            }

            return board;
        }

        public async Task<Board> UpdateBoard(Board board)
        {
            using (SqlConnection connection = new SqlConnection(_connections.SQLChain))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UPDATE Boards SET Names = @Name WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Name", board.Name);
                    command.Parameters.AddWithValue("@Id", board.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
            return board;
        }

    }
}
