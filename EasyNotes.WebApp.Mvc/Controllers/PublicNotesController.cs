using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class PublicNotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicNotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<PublicNote> objNotesList = _context.PublicNotes.ToList();
            return View(objNotesList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublicNote obj)
        {

            if (ModelState.IsValid)
            {
                obj.Id = Guid.NewGuid();
                obj.CreateDate = DateTime.Now;
                _context.PublicNotes.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Note successfully created!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
