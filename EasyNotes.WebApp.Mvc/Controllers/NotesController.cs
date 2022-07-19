using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Note> objNotesList = _context.Notes.ToList();
            return View(objNotesList);
        }
    }
}
