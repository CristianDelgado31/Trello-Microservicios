using BoardService.Application.Task;
using BoardService.Application.Task.Dto.Create;
using BoardService.Application.Task.Dto.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> CreateTask(CreateTaskDto newTask)
        {
            try
            {
                ResponseCreateTask result = await _taskService.CreateTask(newTask);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        [HttpPut]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> UpdateTask(UpdateTaskDto updateTask)
        {
            try
            {
                ResponseUpdateTask result = await _taskService.UpdateTask(updateTask);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseUpdateTask
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
