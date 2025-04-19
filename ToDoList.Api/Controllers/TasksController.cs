using Microsoft.AspNetCore.Mvc;
using ToDoList.Common.Models;
using ToDoList.Service.Api;
using ToDoList.Service.Extensions;

namespace ToDoList.Api.Controllers;

[Route("api/Users/{userId:guid}/[controller]")]
[ApiController]
public class TasksController(ITaskService taskService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;


    [HttpPost]
    public async Task<IActionResult> CreateTask(Guid userId, CreateTaskModel model)
    {
        await _taskService.CreateTask(userId, model);
        if (_taskService.IsValid)
        {
            return Ok("Done");
        }
        _taskService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }
}
