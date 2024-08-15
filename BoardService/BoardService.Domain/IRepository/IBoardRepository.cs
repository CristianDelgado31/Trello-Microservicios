using BoardService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.IRepository
{
    public interface IBoardRepository
    {
        Task<List<Board>> GetBoards(int id);

        Task<Board> CreateBoard(Board board);
    }
}
