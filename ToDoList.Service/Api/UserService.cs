using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StatusGeneric;
using ToDoList.Common.Models;
using ToDoList.Data.Entites;
using ToDoList.Data.Repositories;
using ToDoList.Service.Jwt;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.Service.Api;

public class UserService(IBaseRepository<User> userRepository,
    IMemoryCache memoryCache,
    IJwtService jwtService)
    : StatusGenericHandler, IUserService
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private const string Key = "user";
    private readonly IBaseRepository<User> _userRepositroy = userRepository;
    private readonly IJwtService _jwtService = jwtService;
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

    public async Task<string> Login(LoginModel model)
    {
        User? user = await GetUserByUsername(model.Username);
        if (user is null)
        {
            AddError($"User:{model.Username} not found");
            return string.Empty;
        }

        bool isPasswordValid = VerifyPassword(user, model.Password);
        if (isPasswordValid)
        {
            string token = _jwtService.GenerateToken(user);
            return token;
        }
        return string.Empty;
    }

    private async Task<User?> GetUserByUsername(string username)
    {
        var user = await _userRepositroy.GetAll()
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }

        return user;
    }


    private bool VerifyPassword(User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
}