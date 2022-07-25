using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {

            IEnumerable<Note> objNotesList = _context.Notes
                
                .ToList(); 
            
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
        public IActionResult Create(Note obj)
        {
            
            if (ModelState.IsValid)
            {
                obj.UserName = User.Identity.Name;
                _context.Notes.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(obj);
        }
    }
}
