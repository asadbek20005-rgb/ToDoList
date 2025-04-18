using System.Net;
using ToDoList.Common.Models;

namespace ToDoList.UI.Integrations;

public interface IUserIntegration
{
    public Task<HttpStatusCode> Register(RegisterModel model);
    public Task<Tuple<HttpStatusCode, string>> Login(LoginModel model);
}
