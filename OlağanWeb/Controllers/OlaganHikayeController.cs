using Microsoft.AspNetCore.Mvc;

namespace OlağanWeb.Controllers
{
    public class OlaganHikayeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
