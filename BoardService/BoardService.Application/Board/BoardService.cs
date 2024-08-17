using BoardService.Application.Board.dto;
using BoardService.Application.Board.dto.update;
using BoardService.Domain.IRepository;

namespace BoardService.Application.Board
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<CreateBoardResponseDto> CreateBoard(CreateBoardDto createBoardDto, int id)
        {
            var newBoard = new Domain.Entities.Board
            {
                Name = createBoardDto.Name,
                IdAuth = id
            };

            // Verificar si no hay un tablero con el mismo nombre
            var boards = await _boardRepository.GetBoards(id, false); // Obtain all boards from the user
            if (boards.Any(x => x.Name == newBoard.Name))
            {
                throw new Exception("There is already a board with the same name");
            }

            var result = await _boardRepository.CreateBoard(newBoard);

            return new CreateBoardResponseDto
            {
                Id = result.Id
            };
        }

        public async Task<List<Domain.Entities.Board>> GetBoards(int id)
        {
            return await _boardRepository.GetBoards(id, true);
        }

        public async Task<ResponseUpdateBoard> UpdateBoard(UpdateBoardDto updateBoardDto, int id)
        {
            var findBoard = await _boardRepository.SearchBoard(updateBoardDto.Id);

            if (findBoard == null)
                throw new Exception("Board not found");

            var boards = await _boardRepository.GetBoards(id, false);

            var nameExists = boards.FirstOrDefault(x => x.Name == updateBoardDto.Name);

            if (nameExists != null)
            {
                if (nameExists.Id != updateBoardDto.Id) { 
                    throw new Exception("There is already a board with the same name");
                }
                else {
                    throw new Exception("The board already has the same name"); 
                }
            }


            var board = new Domain.Entities.Board
            {
                Id = updateBoardDto.Id,
                Name = updateBoardDto.Name,
                IdAuth = id
            };

            var result = await _boardRepository.UpdateBoard(board);

            return new ResponseUpdateBoard
            {
                Success = true,
                Message = "The board has been updated successfully"
            };

        }
    }
}
