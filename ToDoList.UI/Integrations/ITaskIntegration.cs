using System.Net;
using ToDoList.Common.Dtos;
using ToDoList.Common.Models;

namespace ToDoList.UI.Integrations;

public interface ITaskIntegration
{
    Task<HttpStatusCode> CreateTask(CreateTaskModel model);
    Task<Tuple<HttpStatusCode, List<TaskDto>?>> GetAllTasks();
    Task<Tuple<HttpStatusCode, TaskDto?>> GetTaskById(int taskId);
    Task<HttpStatusCode> UpdateTask(int taskId, UpdateTaskModel model);
}
