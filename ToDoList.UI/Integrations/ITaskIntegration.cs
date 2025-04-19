using System.Net;
using ToDoList.Common.Models;

namespace ToDoList.UI.Integrations;

public interface ITaskIntegration
{
    Task<HttpStatusCode> CreateTask(CreateTaskModel model);
}
