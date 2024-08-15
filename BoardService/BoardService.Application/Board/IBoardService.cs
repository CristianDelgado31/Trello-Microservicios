using BoardService.Application.Board.dto;

namespace BoardService.Application.Board
{
    public interface IBoardService
    {
        Task<List<Domain.Entities.Board>> GetBoards(int id);
        Task<CreateBoardResponseDto> CreateBoard(CreateBoardDto createBoardDto, int id);
    }
}
