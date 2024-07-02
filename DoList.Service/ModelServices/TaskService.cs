using DoList.Data.Context;

namespace DoList.Service.ModelServices
{
    public class TaskService
    {
        public TaskService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        private readonly AppDbContext _dbContext;
    }
}
