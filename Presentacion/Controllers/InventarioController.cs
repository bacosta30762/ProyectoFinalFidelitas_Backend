using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
