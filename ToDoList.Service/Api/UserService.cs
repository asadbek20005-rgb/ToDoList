using Microsoft.AspNetCore.Identity;
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

    async Task IUserService.Register(RegisterModel model)
    {
        IsUserExist(model.Username);
        var newUser = new User
        {
            Username = model.Username,
        };
        newUser.PasswordHash = new PasswordHasher<User>().HashPassword(newUser, model.Password);

        await _userRepositroy.AddAsync(newUser);
        _memoryCache.Set(Key, newUser);
    }

    private void IsUserExist(string username)
    {
        if (_memoryCache.TryGetValue(Key, out User? user))
        {
            AddError($"User with username: {username} is already exist");
            return;
        }
    }
}
