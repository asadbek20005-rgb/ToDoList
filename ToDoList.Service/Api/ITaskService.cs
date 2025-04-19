using StatusGeneric;
using ToDoList.Common.Dtos;
using ToDoList.Common.Models;

namespace ToDoList.Service.Api;

public interface ITaskService : IStatusGeneric
{
    Task CreateTask(Guid userId, CreateTaskModel model);
}
