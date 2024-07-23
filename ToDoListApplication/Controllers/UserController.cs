using DoList.Common.Models.User;
using DoList.Service.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListApplication.Controllers
{
    public class UserController : Controller
    {

        public UserController(UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _contextAccessor = httpContextAccessor;

        }
        private readonly IHttpContextAccessor _contextAccessor;
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
            var allUsers = await _userService.GetAllUsers();
            return View(allUsers);
        }

        public async Task<IActionResult> UpdateView(Guid userId)
        {
            var user = await _userService.GetUser(userId);
            return View(user);
        }

        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = await _userService.GetUser(userId);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginUser = await _userService.Login(model);
                   _contextAccessor.HttpContext?.Session.SetString("Jwt", loginUser);
                    return RedirectToAction("GetAllUsers", "User");

                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction("Login", "User");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> GetToken()
        {
            var token = _contextAccessor.HttpContext.Session.GetString("Jwt");
            var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
            ViewBag.Token = token;  
            ViewBag.UserId = userId;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(AddUserModel model)
        {
            var userAdding = await _userService.AddUser(model);
            if (userAdding is not null)
            {
                return RedirectToAction("Login", "User");
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

        [HttpPost]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var user = await _userService.DeleteUser(userId);
            return RedirectToAction("GetAllUsers", "User");
        }
    }
}
