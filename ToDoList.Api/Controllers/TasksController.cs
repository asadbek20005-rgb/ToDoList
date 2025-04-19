using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Common.Models;
using ToDoList.Service.Api;
using ToDoList.Service.Extensions;
using ToDoList.Service.Helper;

namespace ToDoList.Api.Controllers;

[Route("api/Users/userId/[controller]")]
[ApiController]
[Authorize]
public class TasksController(ITaskService taskService, IUserHelperService userHelperService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;
    private readonly IUserHelperService _userHelperService = userHelperService;

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskModel model)
    {
        Guid userId = _userHelperService.GetUserId();
        await _taskService.CreateTask(userId, model);
        if (_taskService.IsValid)
        {
            return Ok("Done");
        }
        _taskService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }
}
