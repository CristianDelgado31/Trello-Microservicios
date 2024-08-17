using BoardService.Application.TaskList;
using BoardService.Application.TaskList.dto.create;
using BoardService.Application.TaskList.dto.update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _taskListService;

        public TaskListController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> CreateTaskList(CreateTaskListDto newTaskList)
        {
            try
            {
                ResponseCreateTaskList result = await _taskListService.CreateTaskList(newTaskList);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> UpdateTaskList(UpdateTaskList updateTaskList)
        {
            try
            {
                ResponseUpdateTaskList result = await _taskListService.UpdateTaskList(updateTaskList);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseUpdateTaskList
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
