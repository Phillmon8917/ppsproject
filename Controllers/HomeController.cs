using Microsoft.AspNetCore.Mvc;
using PPSProject.Models;
using System.Diagnostics;

namespace PPSProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Activities");
        }

    }
}
