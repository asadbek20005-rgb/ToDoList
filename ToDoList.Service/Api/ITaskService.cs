using StatusGeneric;
using ToDoList.Common.Models;

namespace ToDoList.Service.Api;

public interface ITaskService : IStatusGeneric
{
    Task CreateTask(Guid userId, CreateTaskModel model);
    Task<List<Data.Entites.Task>?> GetAllTasks(Guid userId);
    Task<Data.Entites.Task?> GetTaskById(Guid userId,int taskId);
}
