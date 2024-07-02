using DoList.Data.Entities;

namespace DoList.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<List<Users>> GetAllUsers();
        public Task<Users> GetUserById(Guid userId);
        public Task<Users> GetUserByName(string userName);
        public Task AddUser(Users user);
        public Task UpdateUser(Users user);
        public Task DeleteUser(Users userId);
    }
}
