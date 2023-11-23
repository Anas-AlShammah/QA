using Microsoft.AspNetCore.Mvc;

namespace QA.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
