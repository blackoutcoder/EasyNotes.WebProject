using Microsoft.AspNetCore.Mvc;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class Category : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
