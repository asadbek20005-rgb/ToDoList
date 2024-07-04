using DoList.Common.Dtos;
using DoList.Common.Models.Task;
using DoList.Data.Entities;
using DoList.Data.Repositories;
using DoList.Service.ConvertToExtension;

namespace DoList.Service.ModelServices
{
    public class TaskService
    {
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        private readonly ITaskRepository _taskRepository;

        public async Task<TasksDto> AddTask(AddTaskModel model)
        {
            var newTask = new Tasks
            {
                Taskname = model.Taskname,
                Description = model.Description,
                TaskType = model.TaskType,
                DueDate = model.DueDate,
                DueTime = model.DueTime,
            };

            await _taskRepository.AddTask(newTask);

            return newTask.ParseToModel();
        }


        public async Task<TasksDto> GetTask(int taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId) ??
                 throw new Exception($"Task with id {taskId} not found.");
            return task.ParseToModel();
        }


        public async Task<List<TasksDto>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasks()?? null;
            return tasks.ParseToModels();
        }
        public async Task<string> DeleteTask(int taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task is not null)
            {
                await _taskRepository.DeleteTask(task);
                return $"{taskId} is deleted successfully";
            }
            throw new Exception($"User with id {taskId} not found.");
        }

    }
}
