using Microsoft.EntityFrameworkCore;
using StatusGeneric;
using ToDoList.Common.Models;
using ToDoList.Data.Entites;
using ToDoList.Data.Repositories;
using Task = ToDoList.Data.Entites.Task;
namespace ToDoList.Service.Api;

public class TaskService(IBaseRepository<Task> baseRepository,
    IBaseRepository<User> baseRepository1) : StatusGenericHandler, ITaskService
{
    private readonly IBaseRepository<Task> _taskRepostory = baseRepository;
    private readonly IBaseRepository<User> _userRepostory = baseRepository1;
    public async System.Threading.Tasks.Task CreateTask(Guid userId, CreateTaskModel model)
    {
        User? user = await GetUserById(userId);
        if (user is null)
        {
            AddError($"User:{userId} not found");
            return;
        }
        bool taskIsHas = await CheckTaskExist(model.Title);

        if (taskIsHas)
        {
            AddError($"Task:{model.Title} already exists");
            return;
        }

        var newTask = new Task
        {
            Title = model.Title,
            Description = model.Description,
            Status = model.Status,
            UserId = user.Id,
        };

        await _taskRepostory.AddAsync(newTask);
    }

    private async Task<User?> GetUserById(Guid userId)
    {
        var user = await _userRepostory.GetByIdAsync(userId);
        if (user is null)
        {
            return null;
        }
        return user;
    }

    private async System.Threading.Tasks.Task<bool> CheckTaskExist(string title)
    {
        var taskIsHas = await _taskRepostory.GetAll().AnyAsync(t => t.Title == title);
        if (taskIsHas)
        {
            return true;
        }
        return false;
    }
}
