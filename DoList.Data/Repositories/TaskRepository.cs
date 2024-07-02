using DoList.Data.Context;
using DoList.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoList.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public TaskRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        private readonly AppDbContext _dbContext;
        public async Task AddTask(Tasks task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTask(Tasks task)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<Tasks> GetTaskById(int taskId)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new Exception("Task not Found");
            }
            return task;
        }

        public async Task<Tasks> GetTaskByTaskname(string taskname)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Taskname == taskname);
            if (task == null)
            {
                throw new Exception("Task not Found");
            }
            return task;
        }

        public async Task UpdateTask(Tasks task)
        {
            _dbContext.Tasks.Update(task);

            await _dbContext.SaveChangesAsync();
        }

    }
}
