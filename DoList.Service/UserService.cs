using DoList.Data.Context;

namespace DoList.Service
{
    public class UserService
    {
        public UserService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        private readonly AppDbContext _dbContext;
    }
}
