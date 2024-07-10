using DoList.Data.Context;
using DoList.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoList.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        private readonly AppDbContext _dbContext;
        public async Task AddUser(Users user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(Users user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Users> GetUserById(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user  == null)
            {
                throw new Exception("User not Found");
            }
            return user;
        }

        public async Task<Users> GetUserByUsername(string userName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
            
            return user;
        }

        public async Task UpdateUser(Users user)
        {
            _dbContext.Users.Update(user);

            await _dbContext.SaveChangesAsync();
        }



    }
}
