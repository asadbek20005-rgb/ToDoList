using DoList.Data.Entities;

namespace DoList.Data.Repositories
{
    public interface ITaskRepository
    {
        public Task<List<Tasks>> GetAllTasks();
        public Task<Tasks> GetTaskById(Guid userId);
        public Task<Tasks> GetUserTaskByName(string userName);
        public Task AddTask(Tasks user);
        public Task UpdateTask(Tasks user);
        public Task DeleteTask(Tasks userId);
    }
}
