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
    }
}
