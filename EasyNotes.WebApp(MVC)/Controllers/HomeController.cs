using EasyNotes.WebApp_MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EasyNotes.WebApp_MVC_.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /* [Authorize(Roles ="User")]
         public IActionResult Notes()
         {
             return View();
         }

         [Authorize]
         public IActionResult Categories()
         {
             return View();
         }*/
        /*[HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}