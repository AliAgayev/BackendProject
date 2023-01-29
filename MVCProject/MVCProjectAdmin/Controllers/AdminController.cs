using Microsoft.AspNetCore.Mvc;

namespace MVCProjectAdmin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
