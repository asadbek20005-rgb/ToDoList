using System.Net;
using ToDoList.Common.Models;

namespace ToDoList.UI.Integrations;

public interface IUserIntegration
{
    public Task<HttpStatusCode> Register(RegisterModel model);
}
