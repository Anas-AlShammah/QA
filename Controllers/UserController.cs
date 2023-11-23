using Microsoft.AspNetCore.Mvc;
using QA.Dtos;
using QA.Interfaces;

namespace QA.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {

            _userService = userService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginData data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var res = await _userService.Authenticate(data.Username, data.Password, this.ModelState);
            if (!ModelState.IsValid)
            {
                return View(res);
            }
            if (res == null)
            {
                return RedirectToAction("Login", "User");

            }
            if (res.Roles[0] == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }
   

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterUser data)
        {
            

            var res = await _userService.Register(data, this.ModelState);

           
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return Redirect("/");
        }
    }
}
