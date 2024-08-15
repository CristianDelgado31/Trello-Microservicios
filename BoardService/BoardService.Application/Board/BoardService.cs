using BoardService.Application.Board.dto;
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
            var boards = await _boardRepository.GetBoards(id);
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
            return await _boardRepository.GetBoards(id);
        }
    }
}
