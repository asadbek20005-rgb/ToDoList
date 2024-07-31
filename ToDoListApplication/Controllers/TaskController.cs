using DoList.Common.Models.Task;
using DoList.Service.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListApplication.Controllers
{
    public class TaskController : Controller
    {
        public TaskController(TaskService taskService) => _taskService = taskService;
        private readonly TaskService _taskService;
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllTasks()
        {
            var allTasks = await _taskService.GetAllTasks();
            return View(allTasks);
        }

        public async Task<IActionResult> AddTask()
        {
            return View();
        }

        public async Task<IActionResult> Update(int taskId)
        {
            var task = await _taskService.GetTask(taskId);
            return View(task);
        }
             
        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var newTask = await _taskService.AddTask(model);
                    return RedirectToAction("GetAllTasks", "Task");
                }catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int taskId, UpdateTaskModel model)
        {
           await _taskService.UpdateTask(taskId, model);
            return RedirectToAction("GetAllTasks");
        }

    }
}
