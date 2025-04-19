using ToDoList.Data.Entites;

namespace ToDoList.Service.Jwt;

public interface IJwtService
{
    string GenerateToken(User user);
}
