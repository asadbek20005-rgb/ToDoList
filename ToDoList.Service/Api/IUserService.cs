using StatusGeneric;
using ToDoList.Common.Models;

namespace ToDoList.Service.Api;

public interface IUserService : IStatusGeneric
{
    Task Register(RegisterModel model);
    Task<string> Login(LoginModel model);
}
