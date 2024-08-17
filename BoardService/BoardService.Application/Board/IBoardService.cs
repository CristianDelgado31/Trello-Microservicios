using BoardService.Application.Board.dto;
using BoardService.Application.Board.dto.update;

namespace BoardService.Application.Board
{
    public interface IBoardService
    {
        Task<List<Domain.Entities.Board>> GetBoards(int id);
        Task<CreateBoardResponseDto> CreateBoard(CreateBoardDto createBoardDto, int id);
        Task<ResponseUpdateBoard> UpdateBoard(UpdateBoardDto updateBoardDto, int id);
    }
}
