using DoList.Service.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListApplication.Controllers
{
    public class UserController : Controller
    {
        public UserController(UserService userService) => _userService = userService;
        
        private readonly UserService _userService;
        public IActionResult Index()
        {
            return View();
        }
    }
}
