using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StatusGeneric;
using ToDoList.Common.Models;
using ToDoList.Data.Entites;
using ToDoList.Data.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.Service.Api;

public class UserService(IBaseRepository<User> userRepository, IMemoryCache memoryCache) : StatusGenericHandler, IUserService
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private const string Key = "user";
    private readonly IBaseRepository<User> _userRepositroy = userRepository;

    public async Task Register(RegisterModel model)
    {
        await IsUserExist(model.Username);
        var newUser = new User
        {
            Username = model.Username,
        };
        newUser.PasswordHash = new PasswordHasher<User>().HashPassword(newUser, model.Password);

        await _userRepositroy.AddAsync(newUser);
        _memoryCache.Set(Key, newUser);
    }

    private async Task IsUserExist(string username)
    {
        var userIsHave = await _userRepositroy.GetAll().AnyAsync(u => u.Username == username);
        if (userIsHave)
        {
            AddError($"User:{username} already exists");
            return;
        }
    }
}
