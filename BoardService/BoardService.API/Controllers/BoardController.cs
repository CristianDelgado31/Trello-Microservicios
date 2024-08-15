using BoardService.Application.Board;
using BoardService.Application.Board.dto;
using BoardService.Domain.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetBoards()
        {
            //Id lo saco del jwt token, el resto por la variable del parametro
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }).ToList();

            var searchId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var id = int.Parse(searchId!);

            var boards = await _boardService.GetBoards(id);
            return Ok(boards);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> CreateBoard(CreateBoardDto newBoard)
        {
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }).ToList();

            var searchId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var id = int.Parse(searchId!);

            try
            {
                CreateBoardResponseDto result = await _boardService.CreateBoard(newBoard, id);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
