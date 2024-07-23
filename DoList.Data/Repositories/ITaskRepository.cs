using DoList.Data.Entities;

namespace DoList.Data.Repositories
{
    public interface ITaskRepository
    {
        public Task<List<Tasks>> GetAllTasks();
        public Task<Tasks> GetTaskById(int taskId);
        public Task<Tasks> GetTaskByTaskname(string taskName);
        public Task AddTask(Tasks task);
        public Task UpdateTask(Tasks task);
        public Task DeleteTask(Tasks taskId);
    }
}
