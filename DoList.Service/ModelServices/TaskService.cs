using DoList.Common.Dtos;
using DoList.Common.Models.Task;
using DoList.Data.Context;
using DoList.Data.Entities;
using DoList.Data.Repositories;
using DoList.Service.ConvertToExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DoList.Service.ModelServices
{
    public class TaskService
    {
        public TaskService(ITaskRepository taskRepository, HttpContextAccessor http, AppDbContext appDbContext)
        {
            _taskRepository = taskRepository;
            _httpContext = http;
            _appDbContext = appDbContext;
        }
        private readonly ITaskRepository _taskRepository;
        private readonly HttpContextAccessor _httpContext;
        private readonly AppDbContext _appDbContext;
        public async Task<TasksDto> AddTask(AddTaskModel model)
        {
			var userId = _httpContext.HttpContext?.Session.GetString("UserId");
			if (userId == null)
			{
				
				throw new InvalidOperationException("User is not logged in.");
			}
            var newTask = new Tasks
            {
                Taskname = model.Taskname,
                DueDate = model.DueDate,
                DueTime = model.DueTime,
                IsCompleted = false,
                UserId = Guid.Parse(userId),
            };

            if (!string.IsNullOrWhiteSpace(model.Description))
               newTask.Description = model.Description;
            if (!string.IsNullOrWhiteSpace(model.TaskType))
                newTask.TaskType = model.TaskType;

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
            return tasks!.ParseToModels();
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

        public async Task<TasksDto> UpdateTask(int taskId, UpdateTaskModel model)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task is not null)
            {
                if (!string.IsNullOrEmpty(model.Taskname))
                    task.Taskname = model.Taskname;
                if (!string.IsNullOrEmpty(model.Description))
                    task.Description = model.Description;
                if (model.DueDate is not null)
                    task.DueDate = model.DueDate.Value;
                if (model.DueTime is not null)
                    task.DueTime = model.DueTime.Value;

                task.IsCompleted = model.IsCompleted;
                task.TaskType = model.TaskType;

                await _taskRepository.UpdateTask(task);
                return task.ParseToModel();
            }

            throw new Exception("Task not found");
        }



    }
}   