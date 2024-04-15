using Microsoft.AspNetCore.Mvc;

namespace ShelterDB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
