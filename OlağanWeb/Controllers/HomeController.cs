using Microsoft.AspNetCore.Mvc;
using OlağanWeb.Models;
using System.Diagnostics;

namespace OlağanWeb.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    
    }
}