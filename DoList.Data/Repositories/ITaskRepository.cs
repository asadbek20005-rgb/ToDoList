using DoList.Data.Entities;

namespace DoList.Data.Repositories
{
    public interface ITaskRepository
    {
        public Task<List<Tasks>> GetAllTasks();
        public Task<Tasks> GetTaskById(int userId);
        public Task<Tasks> GetTaskByTaskname(string userName);
        public Task AddTask(Tasks user);
        public Task UpdateTask(Tasks user);
        public Task DeleteTask(Tasks userId);
    }
}
