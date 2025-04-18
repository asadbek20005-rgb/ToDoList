using Microsoft.AspNetCore.Mvc;
using ToDoList.Common.Models;
using ToDoList.Service.Api;
using ToDoList.Service.Extensions;

namespace ToDoList.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("account/register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        await _userService.Register(model);

        if (_userService.IsValid)
        {
            return Ok("done");
        }

        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }
}
