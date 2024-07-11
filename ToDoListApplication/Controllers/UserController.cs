using DoList.Common.Models.User;
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

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
          
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers= await _userService.GetAllUsers();
            return View(allUsers);
        }
      
        public async Task<IActionResult> Update(Guid userId)
        {
            var user = await _userService.GetUser(userId);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var loginUser = await _userService.Login(model);
            if(loginUser is not null)
            {
                return RedirectToAction("GetAllUsers", "User");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]       
        public async Task<IActionResult> Register(AddUserModel model)
        {
          var userAdding =  await _userService.AddUser(model);
            if(userAdding is not null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]

        public async Task<IActionResult> Update(Guid userId, UpdateUserModel model)
        {
            var user = await _userService.UpdateUser(userId, model);
            return RedirectToAction("GetAllUsers", "User");
        }
      
    }
}
