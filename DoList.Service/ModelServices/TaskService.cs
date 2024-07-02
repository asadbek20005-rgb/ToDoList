using DoList.Data.Context;

namespace DoList.Service.ModelServices
{
    public class TaskService
    {
        public UserService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        private readonly AppDbContext _dbContext;
    }
}
