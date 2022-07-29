using EasyNotes.WebApp.Mvc.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class NoteViewController : Controller
    {
        private readonly ApplicationDbContext _db;
        public IActionResult Index()
        {
            return View();
        }
    }
}
