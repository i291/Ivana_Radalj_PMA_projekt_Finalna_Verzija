using Microsoft.AspNetCore.Mvc;

namespace MvcDemo.Controllers
{
    public class PutovanjaController : Controller
    {
        // GET
        public IActionResult Putovanja()
        {
            return View();
        }
        public IActionResult EditPage()
        {
            return View();
        }
    }
}