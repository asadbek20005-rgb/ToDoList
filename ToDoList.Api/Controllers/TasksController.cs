using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Common.Models;
using ToDoList.Service.Api;
using ToDoList.Service.Extensions;
using ToDoList.Service.Helper;
using Task = ToDoList.Data.Entites.Task;

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

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        Guid userId = _userHelperService.GetUserId();
        var tasks = await _taskService.GetAllTasks(userId);
        if (_taskService.IsValid)
        {
            return Ok(tasks);
        }
        _taskService.CopyToModelState(ModelState);
        return NotFound(ModelState);
    }


    [HttpGet("{taskId:int}")]
    public async Task<IActionResult> GetTaskById(int taskId)
    {
        Guid userId = _userHelperService.GetUserId();

        Task? task = await _taskService.GetTaskById(userId, taskId);
        if (_taskService.IsValid)
        {
            return Ok(task);
        }
        _taskService.CopyToModelState(ModelState);
        return NotFound(ModelState);
    }
}
